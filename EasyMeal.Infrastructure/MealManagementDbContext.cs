using EasyMeal.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMeal.Infrastructure
{
    public class MealManagementDbContext : DbContext
    {
        public MealManagementDbContext(DbContextOptions<MealManagementDbContext> options) : base(options) { }
        public DbSet<Meal> Meals { get; set; }

        public DbSet<Chef> Chefs { get; set; }
    }
}
