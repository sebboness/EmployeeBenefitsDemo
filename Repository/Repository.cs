using Microsoft.EntityFrameworkCore;
using PaylocityDemo.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaylocityDemo.Data
{
	public class Repository<TObject> : IRepository<TObject>
		where TObject : class
	{
		public AppDbContext DbContext { get; private set; }

		public Repository(AppDbContext context)
		{
			DbContext = context;
		}

		protected DbSet<TObject> DbSet
		{
			get
			{
				return DbContext.Set<TObject>();
			}
		}

		public void Dispose()
		{
			if (DbContext != null)
				DbContext.Dispose();
		}

		public virtual IQueryable<TObject> All()
		{
			return DbSet.AsQueryable();
		}

		public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate)
		{
			return DbSet.Where(predicate).AsQueryable<TObject>();
		}

		public virtual IQueryable<TObject> Filter<Key>(Expression<Func<TObject, bool>> filter,
			Expression<Func<TObject, Key>> orderBy,
			out int total, int index = 0, int size = 50)
		{
			int skipCount = index * size;
			var _resetSet = filter != null ? DbSet.Where(filter).OrderBy(orderBy).AsQueryable() :
				DbSet.OrderBy(orderBy).AsQueryable();

			total = _resetSet.Count();

			_resetSet = skipCount == 0 ? _resetSet.Take(size) :
				_resetSet.Skip(skipCount).Take(size);


			return _resetSet.AsQueryable();
		}

		public bool Contains(Expression<Func<TObject, bool>> predicate)
		{
			return DbSet.Count(predicate) > 0;
		}

		public virtual TObject Find(params object[] keys)
		{
			return DbSet.Find(keys);
		}

		public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
		{
			var obj = DbSet.FirstOrDefault(predicate);
			return obj;
		}

		public virtual TObject Create(TObject TObject)
		{
			var newEntry = DbSet.Add(TObject);

			int count = TrySaveChanges();

			if (count > 0)
				return newEntry.Entity;

			return null;
		}

		public virtual int Count
		{
			get
			{
				return DbSet.Count();
			}
		}

		public virtual int Delete(TObject TObject)
		{
			DbSet.Remove(TObject);
			return DbContext.SaveChanges();
		}

		public virtual async Task<int> DeleteAsync(TObject TObject)
		{
			DbSet.Remove(TObject);
			return await DbContext.SaveChangesAsync();
		}

		public virtual int DeleteAllBy(Expression<Func<TObject, bool>> filter)
		{
			DbSet.RemoveRange(DbSet.Where(filter));
			return DbContext.SaveChanges();
		}

		public virtual async Task<int> DeleteAllByAsync(Expression<Func<TObject, bool>> filter)
		{
			DbSet.RemoveRange(DbSet.Where(filter));
			return await DbContext.SaveChangesAsync();
		}

		public virtual int Update(TObject TObject)
		{
			var entry = DbContext.Entry(TObject);
			DbSet.Attach(TObject);
			entry.State = EntityState.Modified;

			return TrySaveChanges();

		}

		public virtual int Delete(Expression<Func<TObject, bool>> predicate)
		{
			var objects = Filter(predicate);
			foreach (var obj in objects)
				DbSet.Remove(obj);

			return DbContext.SaveChanges();
		}

		public int TrySaveChanges()
		{
			try
			{
				return DbContext.SaveChanges();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				throw new Exception("DB Concurrency exception occurred", ex);
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("DB exception occurred", ex);
			}
		}
	}
}