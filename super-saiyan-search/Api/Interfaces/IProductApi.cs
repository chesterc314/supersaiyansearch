namespace SuperSaiyanSearch.Api
{
    public interface IProductApi
    {
        ProductsReadDto Search(string keyword, string next, string previous, int? limit);
    }
}