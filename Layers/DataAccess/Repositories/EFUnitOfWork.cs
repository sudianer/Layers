using System;
using DataAccess.EF;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
	public class EFUnitOfWork
	{
		private DishContext db;
		private DishRepository dishRepository;

		public DishRepository Dishes
		{
			get
			{
				if (dishRepository == null)
				{
					dishRepository = new DishRepository(db);
				}
				return dishRepository;
			}
		}
		public void Save()
		{
			db.SaveChanges();
		}

		private bool disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					db.Dispose();
				}
				this.disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
