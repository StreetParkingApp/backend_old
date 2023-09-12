using Microsoft.EntityFrameworkCore;
using StreetParking.API.DbContexts;
using StreetParking.API.Entities;

namespace StreetParking.API.Services
{
    public class StreetParkingRepository : IStreetParkingRepository
    {
        private readonly StreetParkingContext _context;

        public StreetParkingRepository(StreetParkingContext context)
        {
            _context = context?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name,string? searchQuery, int pageNumber, int pageSize)
        {
            var collection = _context.Cities as IQueryable<City>;   

            if (!string.IsNullOrEmpty(name)) { 
                
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                || (!string.IsNullOrEmpty(a.Description) && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetaData = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointOfInterest)
        {
            if (includePointOfInterest) 
            { 
                return await _context.Cities.Include(c => c.PointOfInterests)
                    .Where(c=> c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<bool> CityExistAsync(int cityId)
        {
             return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<bool> CityExistByNameAsync(string name)
        {
            return await _context.Cities.AnyAsync(c => c.Name == name);
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointOfInterests.Where(c => c.CityId == cityId && c.Id == pointOfInterestId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest?>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointOfInterests.Where(c => c.CityId == cityId).ToListAsync();
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointOfInterests.Add(pointOfInterest);
            }
        }

        public async Task AddCityAsync(City city)
        {
           await _context.Cities.AddAsync(city);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointOfInterests.Remove(pointOfInterest);
        }

        public Task<bool> CityNameMatchesCityId(string? cityName, int cityId)
        {
            return _context.Cities.AnyAsync(c => c.Id == cityId && c.Name == cityName);
        }
    }
}