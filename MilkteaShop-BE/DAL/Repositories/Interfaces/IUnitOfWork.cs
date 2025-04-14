namespace DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
