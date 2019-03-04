using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TaskCore.Data.Interfaces;
using TaskCore.Domain;

namespace TaskCore.Data
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity>
		where TEntity : class, IEntity
	{
		public TaskCoreContext _context = new TaskCoreContext();

		public async Task<TEntity> Add(TEntity entity)
		{
			_context.Add(entity);
			_context.Entry(entity).State = EntityState.Added;
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task Delete(int id)
		{
			var entity = await GetById(id);
			if (!_context.Set<TEntity>().Local.Any(c => c.Id == entity.Id))
			{
				_context.Set<TEntity>().Attach(entity);
			}
			else
			{
				TEntity t = _context.Set<TEntity>().Local.FirstOrDefault(c => c.Id == entity.Id);
				_context.Set<TEntity>().Local.Remove(t); // tracking fix
			}
			_context.Entry(entity).State = EntityState.Deleted;
			await _context.SaveChangesAsync();			
		}

		public IQueryable<TEntity> GetAll()
		{
			return _context.Set<TEntity>().AsNoTracking();
		}

		public async Task<TEntity> GetById(int id)
		{
			return await _context.Set<TEntity>().AsNoTracking()
				.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task<TEntity> Update(int id, TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			if (!_context.Set<TEntity>().Local.Any(c => c.Id == entity.Id))
			{
				_context.Set<TEntity>().Attach(entity);
			}
			else
			{
				TEntity t = _context.Set<TEntity>().Local.FirstOrDefault(c => c.Id == entity.Id);
				_context.Set<TEntity>().Local.Remove(t); // tracking fix
			}
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return entity;
		}
	}
}