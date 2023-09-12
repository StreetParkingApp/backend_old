using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StreetParking.API.Entities;
using StreetParking.API.Models;
using StreetParking.API.Services;
using System.Text.Json;

namespace StreetParking.API.Controllers
{
    [ApiController]
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cities")]
    public class CitiesController:ControllerBase
    {
        private readonly IStreetParkingRepository _StreetParkingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CitiesController> _logger;
        const int maxCitiesPageSize = 20;

        public CitiesController(ILogger<CitiesController> logger, IStreetParkingRepository StreetParkingRepository, IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _StreetParkingRepository = StreetParkingRepository ?? 
                throw new ArgumentNullException(nameof(StreetParkingRepository));
            _mapper = mapper?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all the cities that you want
        /// </summary>
        /// <param name="name"></param>
        /// <param name="searchQuery"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithOutPointOfInterestDto>>> GetCities([FromQuery] string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if(pageSize > maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }
            
            var (cityEntities, paginationMetaData) = await _StreetParkingRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            return Ok(_mapper.Map<IEnumerable<CityWithOutPointOfInterestDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = await _StreetParkingRepository.GetCityAsync(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithOutPointOfInterestDto>(city));
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreateCity(CityForCreationDto city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(city.Name != null && await _StreetParkingRepository.CityExistByNameAsync(city.Name))
            {
                return BadRequest($"City with {city.Name} name already exists");
            }

            var finalCity = _mapper.Map<City>(city);

            await _StreetParkingRepository.AddCityAsync(finalCity);

            await _StreetParkingRepository.SaveChangesAsync();

            return Ok(finalCity);
        }
    }
}
