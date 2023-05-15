using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.CompilerServices;

namespace BokkingOnline.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
	protected readonly ApplicationDbContext _context;
	protected DbSet<T> _dbSet;

	public GenericRepository(ApplicationDbContext context)
	{
		_context = context;
		_dbSet = _context.Set<T>();
	}

	public async Task CreateAsync(T entity)
	{
		await _dbSet.AddAsync(entity);
	}

	public async Task DeleteAsync(object id)
	{
		var entity = _dbSet.Find(id);
		if (entity != null)
		{
			_dbSet.Remove(entity);
		}
		await Task.CompletedTask;
	}

	public async Task DeleteAsync(T entity)
	{
		_dbSet.Remove(entity);
		await Task.CompletedTask;
	}

	public async Task<List<T>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task<T?> GetByIdAsync(object id)
	{
		return await _dbSet.FindAsync(id);
	}

	public void Save()
	{
		_context.SaveChanges();
	}

	public async Task UpdateAsync(T entity)
	{
		_dbSet.Update(entity);
		await Task.CompletedTask;
	}
}
