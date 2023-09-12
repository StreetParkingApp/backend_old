using AutoMapper;
using StreetParking.API.Entities;
using StreetParking.API.Models;

namespace StreetParking.API.Profiles
{
    public class PointOfInterestProfile:Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<PointOfInterest, PointOfInterestDto>().ReverseMap();
            CreateMap<PointOfInterest, PointOfInterestForCreationDto>().ReverseMap();
            CreateMap<PointOfInterest, PointOfInterestForUpdateDto>();
            CreateMap<PointOfInterestForUpdateDto,PointOfInterest>();
        }
    }
}
