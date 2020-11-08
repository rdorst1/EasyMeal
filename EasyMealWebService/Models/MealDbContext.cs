using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMealWebService.Models
{
    public class MealDbContext : DbContext
    {
        public MealDbContext(DbContextOptions<MealDbContext> options) : base(options)
        {

        }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Chef> Chef { get; set; }
    }
}
