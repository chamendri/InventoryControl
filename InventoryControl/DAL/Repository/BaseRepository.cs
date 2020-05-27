using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace InventoryControl.DAL.Repository
{
	/// <summary>
	/// implements the IRepository interface
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <seealso cref="InventoryControl.DAL.Repository.IRepository{TEntity}" />
	public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private DbContext context;
		private DbSet<TEntity> dbSet;

		public BaseRepository(DbContext context)
		{
			this.context = context;
			this.dbSet = context.Set<TEntity>();
		}

		/// <summary>
		/// Gets the with raw SQL.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		public virtual IEnumerable<TEntity> GetWithRawSql(string query,
			params object[] parameters)
		{
			return dbSet.SqlQuery(query, parameters).ToList();
		}

		/// <summary>
		/// Gets the specified filter.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <param name="orderBy">The order by.</param>
		/// <param name="includeProperties">The include properties.</param>
		/// <param name="skip">start from the given entry</param>
		/// <param name="take">fetch the number of given entries</param>
		/// <returns></returns>
		public virtual IEnumerable<TEntity> Get(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "", int skip = 0, int take = 0)
		{
			IQueryable<TEntity> query = dbSet;

			if(filter != null)
			{
				query = query.Where(filter);
			}

			if(includeProperties != null)
			{
				foreach(var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}

			if(orderBy != null && take != 0)
			{
				return orderBy(query).Skip(skip).Take(take).ToList();
			}
			if(orderBy != null)
			{
				return orderBy(query).ToList();
			}
			else
			{
				return query.ToList();
			}
		}

		/// <summary>
		/// Gets the by primary identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public virtual TEntity GetByID(object id)
		{
			return dbSet.Find(id);
		}

		/// <summary>
		/// Inserts the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Insert(TEntity entity)
		{
			dbSet.Add(entity);
		}

		/// <summary>
		/// Deletes the entry specified by the identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		public virtual void Delete(object id)
		{
			TEntity entityToDelete = dbSet.Find(id);
			Delete(entityToDelete);
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entityToDelete">The entity to delete.</param>
		public virtual void Delete(TEntity entityToDelete)
		{
			if(context.Entry(entityToDelete).State == EntityState.Detached)
			{
				dbSet.Attach(entityToDelete);
			}
			dbSet.Remove(entityToDelete);
		}

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entityToUpdate">The entity to update.</param>
		public virtual void Update(TEntity entityToUpdate)
		{
			dbSet.Attach(entityToUpdate);
			context.Entry(entityToUpdate).State = EntityState.Modified;
		}
	}
	}