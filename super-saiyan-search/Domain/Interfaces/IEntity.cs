namespace SuperSaiyanSearch.Domain.Interfaces
{
    public interface IEntity<Type>
    {
        Type Id { get; set; }
    }
}