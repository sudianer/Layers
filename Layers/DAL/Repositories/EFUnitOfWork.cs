using System;
using DAL.Interfaces;
using DAL.EF;
using DAL.Entities;

namespace DAL.Repositories
{
	class EFUnitOfWork: IUnitOfWork
	{
		private DishContext db;
		private DishRepository dishRepository;

		public IRepository<Dish> Dishes
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
