using System;

namespace DAL.Entities
{
	public class Dish
	{
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string Title { get; set; }

        public string Ingredients { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Weight { get; set; }

        public decimal Calories { get; set; }

        public int TimeToMake { get; set; }
    }
}
