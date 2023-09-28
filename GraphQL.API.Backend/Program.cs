using GraphQL.API.Backend.Schema;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.UseRouting();
app.MapGraphQL("/graphql");
app.Run();
