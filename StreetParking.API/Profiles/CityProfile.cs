using AutoMapper;
using StreetParking.API.Entities;
using StreetParking.API.Models;

namespace StreetParking.API.Profiles
{
    public class CityProfile:Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityWithOutPointOfInterestDto>();
            CreateMap<City, CityDto>();
            CreateMap<City, CityForCreationDto>().ReverseMap();
        }
    }
}
