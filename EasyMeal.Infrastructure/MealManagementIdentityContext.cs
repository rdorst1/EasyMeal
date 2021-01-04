using EasyMeal.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMeal.Infrastructure
{
    public class MealManagementIdentityContext : IdentityDbContext<AppUser>
    {
        public MealManagementIdentityContext(DbContextOptions<MealManagementIdentityContext> options) : base(options) { }
    }
}
