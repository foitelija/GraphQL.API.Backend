using GraphQL.API.Backend.Schema;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer().AddQueryType<Query>();

var app = builder.Build();

app.UseRouting();
app.MapGraphQL("/graphql");
app.Run();
