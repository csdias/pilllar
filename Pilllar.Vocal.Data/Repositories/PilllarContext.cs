using System;
using System.ComponentModel.DataAnnotations.Schema;
using Pilllar.Vocal.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pilllar.Vocal.Domain;

namespace Pilllar.Vocal.Data.Repositories
{
    public partial class PilllarContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public PilllarContext()
        {
        }

        public PilllarContext(DbContextOptions<PilllarContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Seed();
        }

    }
}
