using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    // контекст БД
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = null!; // Таблица работников по модели "Employee"
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
