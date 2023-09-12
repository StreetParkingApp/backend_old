using StreetParking.API.Models;

namespace StreetParking.API
{
    public class StreetParkingStore
    {
    public List<CityDto> Cities { get; set; }

    //public static CitiesDataStore Current { get; set; } = new CitiesDataStore();

    public StreetParkingStore()
    {

        Cities = new List<CityDto>()
            {

                new CityDto()
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test Descriptions XX",
                    PointOfInterests =  new List<PointOfInterestDto>()
                                    {
                                        new PointOfInterestDto
                                        {
                                            Id = 1,
                                            Name = "Park",
                                            Description = "A beautiful green space for relaxation."
                                        },
                                        new PointOfInterestDto
                                        {
                                            Id = 2,
                                            Name = "Museum",
                                            Description = "Explore history and art artifacts."
                                        },
                                        new PointOfInterestDto
                                        {
                                            Id = 3,
                                            Name = "Restaurant",
                                            Description = "Enjoy delicious cuisine from around the world."
                                        }
                                    }
                 },
                new CityDto()
                {
                    Id= 2,
                    Name = "Test 2",
                    Description = "Test Description 2",
                    PointOfInterests =  new List<PointOfInterestDto>()
                                    {
                                        new PointOfInterestDto
                                        {
                                            Id = 1,
                                            Name = "Park",
                                            Description = "A beautiful green space for relaxation."
                                        },
                                        new PointOfInterestDto
                                        {
                                            Id = 2,
                                            Name = "Museum",
                                            Description = "Explore history and art artifacts."
                                        },
                                        new PointOfInterestDto
                                        {
                                            Id = 3,
                                            Name = "Restaurant",
                                            Description = "Enjoy delicious cuisine from around the world."
                                        }
                                    }

                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Test 3",
                    Description = "Test description 2",
                    PointOfInterests =  new List<PointOfInterestDto>()
                                    {
                                        new PointOfInterestDto
                                        {
                                            Id = 1,
                                            Name = "Park",
                                            Description = "A beautiful green space for relaxation."
                                        },
                                        new PointOfInterestDto
                                        {
                                            Id = 2,
                                            Name = "Museum",
                                            Description = "Explore history and art artifacts."
                                        },
                                        new PointOfInterestDto
                                        {
                                            Id = 3,
                                            Name = "Restaurant",
                                            Description = "Enjoy delicious cuisine from around the world."
                                        }
                                    }
                 },
            };

    }
}
}
