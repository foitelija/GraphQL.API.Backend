namespace GraphQL.API.Backend.Models
{
    [InterfaceType("SearchResult")]
    // [UnionType("SearchResult")] - без GUID. Должно быть пустым полностью.
    public interface ISearchResultType
    {
        Guid Id { get; }
    }
}
