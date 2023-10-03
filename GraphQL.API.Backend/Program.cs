using FirebaseAdmin;
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using FirebaseAdminAuthentication.DependencyInjection.Models;
using Google.Apis.Auth.OAuth2;
using GraphQL.API.Backend.DataLoaders;
using GraphQL.API.Backend.Interfaces;
using GraphQL.API.Backend.Models;
using GraphQL.API.Backend.Repositories;
using GraphQL.API.Backend.Schema;
using GraphQL.API.Backend.Services;
using Microsoft.EntityFrameworkCore;
using Path = System.IO.Path;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<CourseType>()
    .AddType<InstructorType>()
    .AddFiltering()
    .AddTypeExtension<CourseQuery>()
    .AddTypeExtension<InstructorQuery>()
    .AddSorting()
    .AddProjections()
    .AddAuthorization()
.AddInMemorySubscriptions();
builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "firebase-config.json"))
}));
builder.Services.AddFirebaseAuthentication();
builder.Services.AddAuthorization(
    o=> o.AddPolicy("IsAdmin",
    p => p.RequireClaim(FirebaseUserClaimType.EMAIL, "foitelija@gmail.com")));

builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
builder.Services.AddScoped<IInstructorsRepository, InstructorsRepository>();
builder.Services.AddScoped<InstructorDataLoader>();

builder.Services.AddPooledDbContextFactory<SchoolDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolConnection"));
});

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();

app.UseWebSockets();

app.MapGraphQL("/graphql");
app.Run();
