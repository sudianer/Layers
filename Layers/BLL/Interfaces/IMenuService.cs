using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
	public interface IMenuService
	{
		DishDTO GetDish(int? id);
		void MakeDish(DishDTO dishDto);
		IEnumerable<DishDTO> GetDishes();
		void Dispose();
	}
}
