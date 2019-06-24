using EComerence.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.Repositories
{
    public class ApplicationDbContext :DbContext
    {
        const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EComerence;Trusted_Connection=True;";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderList> OrderLists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Category> Categories { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("EComerence.Api"));
        }
    }
}
