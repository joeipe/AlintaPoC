using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        //public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Melbourne",
                    Description = "I live here",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Melbourne 1",
                            Description = "Melbourne 1"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Melbourne 2",
                            Description = "Melbourne 2"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Sydney",
                    Description = "I've been here",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Sydney 1",
                            Description = "Sydney 1"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Sydney 2",
                            Description = "Sydney 2"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Brisbane",
                    Description = "Love to live here",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Brisbane 1",
                            Description = "Brisbane 1"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "Brisbane 2",
                            Description = "Brisbane 2"
                        }
                    }
                }
            };
        }
    }
}
