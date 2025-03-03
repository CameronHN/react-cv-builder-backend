namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddRecordAsync(T entity);

        Task<T> GetRecordByIdAsync(int id);
        Task<IEnumerable<T>> GetAllRecordsAsync();
        Task UpdateRecordAsync(T entity);
        Task<bool> DeleteRecordAsync(int id);
    }
}
