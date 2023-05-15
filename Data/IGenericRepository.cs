namespace BokkingOnline.Data;

public interface IGenericRepository<T> where T : class
{
	public Task<List<T>> GetAllAsync();
	public Task<T?> GetByIdAsync(object id);
	public Task CreateAsync(T entity);
	public Task UpdateAsync(T entity);
	public Task DeleteAsync(object id);
	public Task DeleteAsync(T entity);
	void Save();
}
