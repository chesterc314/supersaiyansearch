namespace SuperSaiyanSearch.Api.Interfaces
{
    public interface IProductApi
    {
        ProductsReadDto Search(string keyword);
    }
}