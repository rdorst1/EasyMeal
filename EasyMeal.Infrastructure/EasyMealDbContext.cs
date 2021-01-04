using EasyMeal.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Infrastructure
{
    public class EasyMealDbContext: DbContext
    {
        public EasyMealDbContext(DbContextOptions<EasyMealDbContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<WeekOrder> WeekOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
