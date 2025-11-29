using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<T?> GetbyIdAsync(int id);
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);

        public Task<T> DeleteAsync( T entity);

    }
}
