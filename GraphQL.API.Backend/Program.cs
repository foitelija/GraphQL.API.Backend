using GraphQL.API.Backend.Schema;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.MapGraphQL("/graphql");
app.Run();
