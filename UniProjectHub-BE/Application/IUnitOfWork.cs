namespace Application
{
    public interface IUnitOfWork
    {
       

        public Task<int> SaveChangesAsync();
    }
}