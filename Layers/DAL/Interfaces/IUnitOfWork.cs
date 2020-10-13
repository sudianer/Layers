using System;
using DataAccess.Entities;

namespace DataAccess.Interfaces
{
	public interface IUnitOfWork: IDisposable
	{
		IRepository<Dish> Dishes { get; }		
		void Save();
	}
}
