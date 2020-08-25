namespace SuperSaiyanSearch.Domain.Interfaces
{
    public interface IProduct
    {
        string Name { get; set; }
        string Description { get; set; }
        string Brand { get; set; }
        decimal Price { get; set; }
        int Units { get; set; }
        string Source { get; set; }
        string SourceUrl { get; set; }
    }
}