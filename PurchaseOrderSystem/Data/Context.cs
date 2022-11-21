using Microsoft.EntityFrameworkCore;
using PurchaseOrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseOrderSystem.Data
{
    public class Context : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //this is fluent api requirement to have composite key mentioned in here
            //modelBuilder.Entity<Impact>().HasKey(c => new { c.Id, c.ImpactRef });
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<OrderMaster> OrderMaster { get; set; }
        public DbSet<ProductMaster> ProductMaster { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Login> Login { get; set; }
    }
    
}
