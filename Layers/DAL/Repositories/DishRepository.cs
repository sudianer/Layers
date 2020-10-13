using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;


namespace DAL.Repositories
{
	class DishRepository: IRepository<Dish>
	{
		private DishContext db;

		public DishRepository(DishContext context)
		{
			this.db = context;
		}

		public IEnumerable<Dish> GetAll()
		{
			return db.Dishes;
		}

		public Dish Get(int id)
		{
			return db.Dishes.Find(id);
		}

		public void Create(Dish dish)
		{
			db.Dishes.Add(dish);
		}

		public void Update(Dish dish)
		{
			db.Entry(dish).State = EntityState.Modified;
		}

		public IEnumerable<Dish> Find(Func<Dish, Boolean> predicate)
		{
			return db.Dishes.Where(predicate).ToList();
		}
		
		public void Delete(int id)
		{
			Dish dish = db.Dishes.Find(id);
			if (dish != null)
				db.Dishes.Remove(dish);
		}



	}
}
