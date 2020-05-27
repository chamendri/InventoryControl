using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.DAL.Repository
{
	/// <summary>
	/// repository interface which contains the basic definitions of the crud operations
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public interface IRepository<TEntity> where TEntity : class
	{
		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entityToDelete">The entity to delete.</param>
		void Delete(TEntity entityToDelete);

		/// <summary>
		/// Deletes the entry specified by the identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		void Delete(object id);

		/// <summary>
		/// Gets the specified filter.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <param name="orderBy">The order by.</param>
		/// <param name="includeProperties">The include properties.</param>
		/// <param name="skip">start from the given entry</param>
		/// <param name="take">fetch the number of given entries</param>
		/// <returns></returns>
		IEnumerable<TEntity> Get(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "", int skip = 0, int take = 0);

		/// <summary>
		/// Gets the entry specified by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		TEntity GetByID(object id);

		/// <summary>
		/// Gets the with raw SQL.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		IEnumerable<TEntity> GetWithRawSql(string query,
			params object[] parameters);

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Insert(TEntity entity);

		/// <summary>
		/// Updates the specified entity to update.
		/// </summary>
		/// <param name="entityToUpdate">The entity to update.</param>
		void Update(TEntity entityToUpdate);
	}
}
