using EasyMeal.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMeal.Infrastructure
{
    public class EasyMealIdentityContext : IdentityDbContext<AppUser>
    {
        public EasyMealIdentityContext(DbContextOptions<EasyMealIdentityContext> options) : base(options) { }
    }
}
