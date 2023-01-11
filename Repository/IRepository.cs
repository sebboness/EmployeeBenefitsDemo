using Microsoft.EntityFrameworkCore;
using PaylocityDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace PaylocityDemo.Data
{
	public interface IRepository<T> : IDisposable where T : class
	{
		/// <summary>
		/// DB context used by repository
		/// </summary>
		AppDbContext DbContext { get; }

		/// <summary>
		/// Gets all objects from database
		/// </summary>
		IQueryable<T> All();

		/// <summary>
		/// Gets objects from database by filter.
		/// </summary>
		/// <param name="predicate">Specified a filter</param>
		IQueryable<T> Filter(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Gets objects from database with filtering and paging.
		/// </summary>
		/// <typeparam name="Key"></typeparam>
		/// <param name="filter">Specified a filter</param>
		/// <param name="total">Returns the total records count of the filter.</param>
		/// <param name="index">Specified the page index.</param>
		/// <param name="size">Specified the page size</param>
		IQueryable<T> Filter<Key>(Expression<Func<T, bool>> filter,
			Expression<Func<T, Key>> orderBy,
			out int total, int index = 0, int size = 50);

		/// <summary>
		/// Gets the object(s) is exists in database by specified filter.
		/// </summary>
		/// <param name="predicate">Specified the filter expression</param>
		bool Contains(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Find object by keys.
		/// </summary>
		/// <param name="keys">Specified the search keys.</param>
		T Find(params object[] keys);

		/// <summary>
		/// Find object by specified expression.
		/// </summary>
		/// <param name="predicate"></param>
		T Find(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Create a new object to database.
		/// </summary>
		/// <param name="t">Specified a new object to create.</param>
		T Create(T t);

		/// <summary>
		/// Delete the object from database.
		/// </summary>
		/// <param name="t">Specified a existing object to delete.</param>        
		int Delete(T t);

		/// <summary>
		/// Delete the object from database async.
		/// </summary>
		/// <param name="t">Specified a existing object to delete.</param>        
		Task<int> DeleteAsync(T t);

		/// <summary>
		/// Delete objects from database by specified filter expression.
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		int DeleteAllBy(Expression<Func<T, bool>> filter);

		/// <summary>
		/// Delete objects from database by specified filter expression async.
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		Task<int> DeleteAllByAsync(Expression<Func<T, bool>> filter);

		/// <summary>
		/// Delete objects from database by specified filter expression.
		/// </summary>
		/// <param name="predicate"></param>
		int Delete(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Update object changes and save to database.
		/// </summary>
		/// <param name="t">Specified the object to save.</param>
		int Update(T t);

		/// <summary>
		/// Get the total objects count.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Tries to save changes to the repository
		/// </summary>
		/// <returns></returns>
		int TrySaveChanges();
	}
}