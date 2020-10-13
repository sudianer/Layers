using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Entities;
using BusinessLogic.Infrastructure;
using System.Collections.Generic;

namespace BusinessLogic.Services
{
	public class MenuService: IMenuService
	{
		IUnitOfWork Database { get; set; }

		public MenuService(IUnitOfWork uow)
		{
			Database = uow;
		}

		public IEnumerable<DishDTO> GetDishes()
		{
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Dish, DishDTO>()).CreateMapper();
			return mapper.Map<IEnumerable<Dish>, List<DishDTO>>(Database.Dishes.GetAll());
		}

		public void MakeDish(DishDTO dishDto)
		{
			Dish dish = Database.Dishes.Get(dishDto.Id);

			if (dish == null)
				throw new ValidationException("Блюдо не найдено", "");
			Database.Dishes.Create(dish);
			Database.Save();
		}

		public DishDTO GetDish(int? id)
		{
			if (id == null)
				throw new ValidationException("Не устновлен id блюда", "");

			var dish = Database.Dishes.Get(id.Value);

			if (dish == null)
				throw new ValidationException("Блюдо не найдено", "");

			return new DishDTO
			{
				Title = dish.Title,
				Id = dish.Id,
				TimeToMake = dish.TimeToMake,
				Description = dish.Description,
				Ingredients = dish.Ingredients,
				CreationDate = dish.CreationDate,
				Calories = dish.Calories,
				Price = dish.Price,
				Weight = dish.Weight
			};
		}

		public void Dispose()
		{
			Database.Dispose();
		}
	}
}
