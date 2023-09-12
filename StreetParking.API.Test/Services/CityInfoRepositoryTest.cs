using Microsoft.EntityFrameworkCore;
using StreetParking.API.DbContexts;
using StreetParking.API.Entities;
using StreetParking.API.Services;
using Xunit;

namespace StreetParking.API.Test.Services
{
    [TestClass]
    public class CityInfoRepositoryTest
    {
        private static async Task<StreetParkingContext> GetDbContext()
        {            
            var _options = new DbContextOptionsBuilder<StreetParkingContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            var databaseContext = new StreetParkingContext(_options);
            await databaseContext.Database.EnsureCreatedAsync();
            return databaseContext;
        }

        [Fact]
        [TestMethod]
        public async Task TestGetCities()
        {
            //Arrange
            var dbContext = GetDbContext();
            var cityInfoRepository = new StreetParkingRepository(dbContext.Result);

            //Act
            var result = await cityInfoRepository.GetCitiesAsync();

            //Assert
            Assert.IsNotNull(result);
        }

        [Fact]
        [TestMethod]
        public async Task TestGetCityById()
        {
            //Arrange
            var dbContext = GetDbContext();
            var cityInfoRepository = new StreetParkingRepository(dbContext.Result);
            var cityId = 1;

            //Act
            var result = await cityInfoRepository.GetCityAsync(cityId, false);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(cityId, result.Id);
        }

        [Fact]
        [TestMethod]
        public async Task TestGetCityByIdAddCityAsync_Test()
        {
            //Arrange
            var dbContext = GetDbContext();            
            var cityInfoRepository = new StreetParkingRepository(dbContext.Result);
            var cityNameAdded = "Test City 4";
            var cityToAdd = new City(cityNameAdded){ Description = "Test City 4" };

            //Act
            await cityInfoRepository.AddCityAsync(cityToAdd);
            await cityInfoRepository.SaveChangesAsync();

            //Assert
            var result = await cityInfoRepository.GetCitiesAsync();
            Assert.IsTrue(result.Any(x => x.Name == "Test City 4"));
        }        
    }

}
