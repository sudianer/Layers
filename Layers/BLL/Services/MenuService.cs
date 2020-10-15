﻿using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Infrastructure;
using DataAccess.Repositories;
using DataAccess.Entities;
using System.Linq;
using System;

namespace BusinessLogic.Services
{
	public class MenuService
	{
		EFUnitOfWork Database { get; set; }

		public MenuService(EFUnitOfWork uow)
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
			
			if (!IsTitleUnique(dishDto.Title, GetDishes()))
			{
				throw new ValidationException("Название уже существует", "Title");				
			}

			dishDto.CreationDate = DateTime.Now;
			Dish dish = DTOtoDish(dishDto);

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


		private Dish DTOtoDish(DishDTO dishDTO)
		{
			return new Dish
			{
				Title = dishDTO.Title,
				Id = dishDTO.Id,
				TimeToMake = dishDTO.TimeToMake,
				Description = dishDTO.Description,
				Ingredients = dishDTO.Ingredients,
				CreationDate = dishDTO.CreationDate,
				Calories = dishDTO.Calories,
				Price = dishDTO.Price,
				Weight = dishDTO.Weight
			};
		}

		/// <summary>
		/// Проверяет уникальность введенного названия
		/// </summary>
		/// <param name="title">Проверяемое название</param>
		private bool IsTitleUnique(string title, IEnumerable<DishDTO> dishes)
		{
			foreach (DishDTO dish in dishes)
			{
				if (dish.Title.Trim().ToLower() == title.Trim().ToLower())
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Проверяет, изменилялось ли название
		/// </summary>
		private bool IsTitleNotChanged(string title, int id, List<DishDTO> dishes)
		{
			foreach (DishDTO dish in dishes)
			{
				if (dish.Id == id && dish.Title.Trim().ToLower() == title.Trim().ToLower())
				{
					return true;
				}
			}

			return false;
		}
	
	
	}
}
