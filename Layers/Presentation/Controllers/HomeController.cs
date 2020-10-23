using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services;
using BusinessLogic.Infrastructure;
using WEB.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTO;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace WEB.Controllers
{
	/// <summary>
	/// Контроллер главной страницы
	/// </summary>
	public class HomeController : Controller
	{

		MenuService MenuService;
		public HomeController(MenuService menuService)
		{
			MenuService = menuService;
		}

		public async Task<IActionResult> Index(string sortOrder, string searchString, int pageNumber = 1, int pageSize = 5)
		{

			IEnumerable<DishViewModel> dishes = GetDishViewModels();

			ViewData["totalCount_dishes"] = dishes.Count();
			ViewData["CurrentFilter"] = searchString;

			if (!String.IsNullOrEmpty(searchString))
			{
				dishes = dishes.Where(s => s.Title.Contains(searchString)
										|| s.Ingredients.Contains(searchString)
										|| s.Description.Contains(searchString)
										|| s.Id.ToString().Contains(searchString)
										|| s.Price.ToString().Contains(searchString)
										|| s.CreationDate.ToString().Contains(searchString)
										|| s.TimeToMake.ToString().Contains(searchString));
			}

			dishes = sortOrder switch
			{
				"title_desc" => dishes.OrderByDescending(p => p.Title),
				"title" => dishes.OrderBy(p => p.Title),
				"date_desc" => dishes.OrderByDescending(p => p.CreationDate),
				"date" => dishes.OrderBy(p => p.CreationDate),
				"cost_desc" => dishes.OrderByDescending(p => p.CreationDate),
				"cost" => dishes.OrderBy(p => p.CreationDate),
				"id_desc" => dishes.OrderByDescending(p => p.Id),
				"id" => dishes.OrderBy(p => p.Id),
				"weight_desc" => dishes.OrderByDescending(p => p.Id),
				"weight" => dishes.OrderBy(p => p.Id),
				"calories_desc" => dishes.OrderByDescending(p => p.Id),
				"calories" => dishes.OrderBy(p => p.Id),
				"timeToMake_desc" => dishes.OrderByDescending(p => p.Id),
				"timeToMake" => dishes.OrderBy(p => p.Id),
				_ => dishes.OrderBy(p => p.Title),
			};

			var count = dishes.Count();

			IEnumerable<DishViewModel> dishesPerPage = dishes.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
			
			PageViewModel pageViewModel = new PageViewModel(count, pageNumber, pageSize);
			
			IndexViewModel viewModel = new IndexViewModel
			{
				PageViewModel = pageViewModel,
				dishes = dishesPerPage
			};

			return View(viewModel);
		}

		/// <summary>
		/// Возвращает вьюшку создания нового блюда
		/// </summary>
		public IActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// Обновляет бд, добавляя новое блюдо
		/// </summary>
		[HttpPost]
		public async Task<IActionResult> Create(DishViewModel dish)
		{			
			try 
			{ 
				MenuService.MakeDish(DishVMtoDTO(dish));
			}
			catch(BLValidationException ex)
			{
				ModelState.AddModelError(ex.Property, ex.Message);
			}
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Показывает форму с подробной информацией о блюде
		/// </summary>
		public async Task<IActionResult> Details(int? id)
		{
			DishViewModel dish = GetDishViewModel(id);
			return View(dish);
		}
		[HttpPost]
		public async Task<IActionResult> Details()
		{
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Показывает форму редактирования объекта
		/// </summary>
		public async Task<IActionResult> Edit(int? id)
		{
			DishViewModel dish = GetDishViewModel(id);

			return View(dish);
		}

		/// <summary>
		/// обновляет объект в базе данных
		/// </summary>
		/// <param name="dish"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Edit(DishViewModel dish)
		{
			try
			{
				MenuService.EditDish(DishVMtoDTO(dish));
			}
			catch(BLValidationException ex)
			{
				ModelState.AddModelError(ex.Property, ex.Message);
			}

			return RedirectToAction("Index");
		}

		/// <summary>
		/// Удаляет блюдо
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ActionName("Delete")]
		[HttpGet]
		public async Task<IActionResult> DeleteDish(int? id)
		{
			try
			{
				MenuService.DeleteDish(id);
			}
			catch(BLValidationException ex)
			{
				ModelState.AddModelError(ex.Property, ex.Message);
			}

			return RedirectToAction("Index");
		}

		private IEnumerable<DishViewModel> GetDishViewModels()
		{
			IEnumerable<DishDTO> dishDtos = MenuService.GetDishes();
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DishDTO, DishViewModel>()).CreateMapper();
			IEnumerable<DishViewModel> dishes = mapper.Map<IEnumerable<DishDTO>, List<DishViewModel>>(dishDtos);
			return dishes;
		}

		private DishViewModel GetDishViewModel(int? id)
		{
			DishViewModel dish = new DishViewModel();

			try
			{
				DishDTO dishDto = MenuService.GetDish(id);
				var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DishDTO, DishViewModel>()).CreateMapper();
				dish = mapper.Map<DishDTO, DishViewModel>(dishDto);
			}
			catch (BLValidationException ex)
			{
				ModelState.AddModelError(ex.Property, ex.Message);
			}

			return dish;
		}
			
		private DishDTO DishVMtoDTO(DishViewModel dish)
		{
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
	}
}
