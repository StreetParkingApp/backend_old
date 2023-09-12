using StreetParking.API.Entities;

namespace StreetParking.API.Services
{
    public interface IStreetParkingRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);

        Task<City?> GetCityAsync(int cityId, bool includePointOfInterest);

        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);

        Task<IEnumerable<PointOfInterest?>> GetPointsOfInterestForCityAsync(int cityId);

        Task<bool> CityExistAsync(int cityId);

        Task<bool> CityExistByNameAsync(string name);

        Task AddCityAsync(City city);

        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

        void DeletePointOfInterest(PointOfInterest pointOfInterest);

        Task<bool> CityNameMatchesCityId(string? cityName, int cityId);

        Task<bool> SaveChangesAsync();
    }
}
