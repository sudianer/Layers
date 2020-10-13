using System;
using DAL.Entities;

namespace DAL.Interfaces
{
	public interface IUnitOfWork: IDisposable
	{
		IRepository<Dish> Dishes { get; }		
		void Save();
	}
}
