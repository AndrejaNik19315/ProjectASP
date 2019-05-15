using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace EFDataAccess
{
    public class ProjectContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<GameClass> GameClasses { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-K6HQC4F;Initial Catalog=ProjectDb;Integrated Security=True");
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder) {

        }*/
    }
}
