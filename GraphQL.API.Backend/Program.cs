using GraphQL.API.Backend.DataLoaders;
using GraphQL.API.Backend.Interfaces;
using GraphQL.API.Backend.Repositories;
using GraphQL.API.Backend.Schema;
using GraphQL.API.Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddFiltering()
    .AddSorting()
.AddInMemorySubscriptions();

builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
builder.Services.AddScoped<IInstructorsRepository, InstructorsRepository>();
builder.Services.AddScoped<InstructorDataLoader>();

builder.Services.AddPooledDbContextFactory<SchoolDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolConnection"));
});

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.MapGraphQL("/graphql");
app.Run();
