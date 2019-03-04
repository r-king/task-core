using System.Linq;
using System.Threading.Tasks;
using TaskCore.Domain;

namespace TaskCore.Data.Interfaces
{
	public interface IBaseRepository<TEntity>
		where TEntity : class, IEntity
	{
		IQueryable<TEntity> GetAll();

		Task<TEntity> GetById(int id);

		Task<TEntity> Add(TEntity entity);

		Task<TEntity> Update(int id, TEntity entity);

		Task<TEntity> UpdateAsync(TEntity entity);

		Task Delete(int id);
	}
}