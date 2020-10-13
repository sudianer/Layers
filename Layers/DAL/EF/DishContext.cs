using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.EF
{
	class DishContext: DbContext
    {
        public DbSet<Dish> Dishes { get; set; }


        public DishContext(DbContextOptions<DishContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>()
                        .HasIndex(t => t.Title)
                        .IsUnique();                  
        }
    }
}
