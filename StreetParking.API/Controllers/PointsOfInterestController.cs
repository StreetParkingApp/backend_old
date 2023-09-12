using AutoMapper;
using StreetParking.API.Entities;
using StreetParking.API.Models;
using StreetParking.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace StreetParking.API.Controllers
{
    [Route("api/v{version:apiVersion}/cities/{cityId}/pointsofinterest")]
    [ApiController]
    //[Authorize(Policy = "MustBeFromCity")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _localMailService;

        private readonly IStreetParkingRepository _StreetParkingRepository;
        private readonly IMapper _mapper;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService localMailService, IStreetParkingRepository StreetParkingRepository, IMapper mapper)
        {
            _logger = logger ?? 
                throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? 
                throw new ArgumentNullException(nameof(localMailService));
            _StreetParkingRepository = StreetParkingRepository ?? 
                throw new ArgumentNullException(nameof(StreetParkingRepository));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]   
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            //var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

            //if (!(await _StreetParkingRepository.CityNameMatchesCityId(cityName, cityId)))
            //{
            //    return Forbid();
            //}

            if(!await _StreetParkingRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"City Id {cityId} was not found");
                return NotFound();
            }
            
            var pointOfInterests =  await _StreetParkingRepository.GetPointsOfInterestForCityAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterests));
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest") ]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _StreetParkingRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"City Id {cityId} was not found");
                return NotFound();
            }

            var pointOfInterest = await _StreetParkingRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _StreetParkingRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"City Id {cityId} was not found");
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterest);

            await _StreetParkingRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);

            await _StreetParkingRepository.SaveChangesAsync();

            var cratedPoint = _mapper.Map<PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new { cityId, pointOfInterestId = cratedPoint.Id }, cratedPoint);
        }

        [HttpPut("{pointOfInterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _StreetParkingRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"City Id {cityId} was not found");
                return NotFound();
            }

            var pointOfInterestEntity = await _StreetParkingRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound("The point of interest doesn't exist.");
            }

            _mapper.Map(pointOfInterest, pointOfInterestEntity);

            await _StreetParkingRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{pointOfInterestId}")]
        public async Task<ActionResult> PartiallyUpdate(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _StreetParkingRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"City Id {cityId} was not found");
                return NotFound();
            }

            var pointOfInterestEntity = await _StreetParkingRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound("The point of interest doesn't exist.");
            }

            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

             _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            await _StreetParkingRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _StreetParkingRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"City Id {cityId} was not found");
                return NotFound();
            }

            var pointOfInterestEntity = await _StreetParkingRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound("The point of interest doesn't exist.");
            }

            _StreetParkingRepository.DeletePointOfInterest(pointOfInterestEntity);
            await _StreetParkingRepository.SaveChangesAsync();

            _localMailService.Send("Testing", "message");

            return NoContent();
        }
    }
}
