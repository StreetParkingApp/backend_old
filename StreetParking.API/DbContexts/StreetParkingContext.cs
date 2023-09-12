using StreetParking.API.Entities;
using StreetParking.API.Models;
using Microsoft.EntityFrameworkCore;

namespace StreetParking.API.DbContexts
{
    public class StreetParkingContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterests { get; set; } = null!;

        public StreetParkingContext(DbContextOptions<StreetParkingContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("Test")
                {
                    Id = 1,
                    Description = "Test Descriptions",                    
                },
                new City("Test 2")
                {
                    Id = 2,
                    Description = "Test Description 2",                    
                },
                new City("Test 3")
                {
                    Id = 3,
                    Description = "Test description 2",                    
                }
                );

            modelBuilder.Entity<PointOfInterest>().HasData(
                  new PointOfInterest("Park")
                  {
                      Id = 1,
                      CityId = 1,
                      Description = "A beautiful green space for relaxation."
                  },
                  new PointOfInterest("Park")
                  {
                      Id = 2,
                      CityId = 2,
                      Description = "A beautiful green space for relaxation."
                  },
                  new PointOfInterest("Park")
                  {
                      Id = 3,
                      CityId = 3,
                      Description = "A beautiful green space for relaxation."
                  },
                  new PointOfInterest("Park")
                  {
                      Id = 4,
                      CityId = 1,
                      Description = "A beautiful green space for relaxation."
                  },
                  new PointOfInterest("Park")
                  {
                      Id = 5,
                      CityId = 1,
                      Description = "A beautiful green space for relaxation."
                  }
                );
           base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
