using System.Collections.Generic;
using BusinessLogic.DTO;

namespace BusinessLogic.Interfaces
{
	public interface IMenuService
	{
		DishDTO GetDish(int? id);
		void MakeDish(DishDTO dishDto);
		IEnumerable<DishDTO> GetDishes();
		void Dispose();
	}
}
