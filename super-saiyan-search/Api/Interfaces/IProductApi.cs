namespace SuperSaiyanSearch.Api
{
    public interface IProductApi
    {
        ProductsReadDto Search(string keyword);
    }
}