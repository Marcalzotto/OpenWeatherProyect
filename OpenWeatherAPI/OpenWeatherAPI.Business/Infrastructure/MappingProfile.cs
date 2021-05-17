using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OpenWeatherAPI.Business.Models;
using OpenWeatherAPI.Data.DataEntities;
using OpenWeatherAPI.Models.Models;

namespace OpenWeatherAPI.Business.Infrastructure
{
    /// <summary>
    /// Mapping profile, clase donde defines los mapeos entre entidades.
    /// </summary>
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<CountryDTO, Country>().ReverseMap();
            CreateMap<CityDTO, City>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.BranchOffice, opt => opt.MapFrom(src => src.BranchOffice))
                .ReverseMap();

            CreateMap<WeatherCondition, WeatherConditionDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Base, opt => opt.MapFrom(src => src.Base))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Clouds, opt => opt.MapFrom(src => src.Clouds))
                .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt))
                .ForPath(dest => dest.Units.FeelsLikeDefault, opt => opt.MapFrom(src => src.FeelsLike))
                .ForPath(dest => dest.Units.FeelsLikeMetrics, opt => opt.MapFrom(src => Math.Round((src.FeelsLike - 273.15), 2)))
                .ForPath(dest => dest.Units.FeelsLikeImperial, opt => opt.MapFrom(src => Math.Round(((src.FeelsLike - 273.15) * (9 / 5) + 32), 2)))
                .ForMember(dest => dest.GroundLevel, opt => opt.MapFrom(src => src.GroundLevel))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Humidity))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Pressure))
                .ForMember(dest => dest.RainVolume1h, opt => opt.MapFrom(src => src.RainVolume1h))
                .ForMember(dest => dest.RainVolume3h, opt => opt.MapFrom(src => src.RainVolume3h))
                .ForMember(dest => dest.RegDate, opt => opt.MapFrom(src => src.RegDate))
                .ForMember(dest => dest.SeaLevel, opt => opt.MapFrom(src => src.SeaLevel))
                .ForMember(dest => dest.SnowVolume1h, opt => opt.MapFrom(src => src.SnowVolume1h))
                .ForMember(dest => dest.SnowVolume3h, opt => opt.MapFrom(src => src.SnowVolume3h))
                .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(src => src.StatusCode))
                .ForMember(dest => dest.Sunset, opt => opt.MapFrom(src => src.Sunset))
                .ForMember(dest => dest.Sunrise, opt => opt.MapFrom(src => src.Sunrise))
                .ForPath(dest => dest.Units.TempDefault, opt => opt.MapFrom(src => src.Temperature))
                .ForPath(dest => dest.Units.TempMetrics, opt => opt.MapFrom(src => Math.Round((src.TempMin - 273.15), 2)))
                .ForPath(dest => dest.Units.TempImperial, opt => opt.MapFrom(src => Math.Round(((src.FeelsLike - 273.15) * (9 / 5) + 32), 2)))
                .ForPath(dest => dest.Units.TempMinDefault, opt => opt.MapFrom(src => src.TempMin))
                .ForPath(dest => dest.Units.TempMinMetrics, opt => opt.MapFrom(src => Math.Round((src.TempMin - 273.15), 2)))
                .ForPath(dest => dest.Units.TempMinImperial, opt => opt.MapFrom(src => Math.Round(((src.FeelsLike - 273.15) * (9 / 5) + 32), 2)))
                .ForPath(dest => dest.Units.TempMaxDefault, opt => opt.MapFrom(src => src.TempMax))
                .ForPath(dest => dest.Units.TempMaxMetrics, opt => opt.MapFrom(src => Math.Round((src.TempMin - 273.15), 2)))
                .ForPath(dest => dest.Units.TempMaxImperial, opt => opt.MapFrom(src => Math.Round(((src.FeelsLike - 273.15) * (9 / 5) + 32), 2)))
                .ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => src.Timezone))
                .ForMember(dest => dest.WindDegrees, opt => opt.MapFrom(src => src.WindDegrees))
                .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.WindSpeed))
                .ForMember(dest => dest.WindGust, opt => opt.MapFrom(src => src.WindGust))
                .ForMember(dest => dest.WeatherType, opt => opt.MapFrom(src => src.WeatherTypes));

            CreateMap<BranchOfficeDTO, BranchOffice>().ReverseMap();

            CreateMap<BranchOfficeForUpdateDTO, BranchOffice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CityId, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.City, opt => opt.Ignore());


            CreateMap<BranchOffice, BranchOfficeForUpdateDTO>();

            CreateMap<WeatherResponseDTO, WeatherCondition>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Base, opt => opt.MapFrom(src => src.Base))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Clouds, opt => opt.MapFrom(src => src.Clouds.All))
                .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt))
                .ForMember(dest => dest.FeelsLike, opt => opt.MapFrom(src => src.Main.Feels_like))
                .ForMember(dest => dest.GroundLevel, opt => opt.MapFrom(src => src.Main.Grnd_level))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Main.Humidity))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Main.Pressure))
                .ForMember(dest => dest.RainVolume1h, opt => opt.MapFrom(src => src.Rain.RainVolumeFor1h))
                .ForMember(dest => dest.RainVolume3h, opt => opt.MapFrom(src => src.Rain.RainVolumeFor3h))
                .ForMember(dest => dest.RegDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.SeaLevel, opt => opt.MapFrom(src => src.Main.Sea_level))
                .ForMember(dest => dest.SnowVolume1h, opt => opt.MapFrom(src => src.Snow.SnowVolumeFor1h))
                .ForMember(dest => dest.SnowVolume3h, opt => opt.MapFrom(src => src.Snow.SnowVolumeFor3h))
                .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(src => src.Cod))
                .ForMember(dest => dest.Sunset, opt => opt.MapFrom(src => src.Sys.Sunset))
                .ForMember(dest => dest.Sunrise, opt => opt.MapFrom(src => src.Sys.Sunrise))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Main.Temp))
                .ForMember(dest => dest.TempMin, opt => opt.MapFrom(src => src.Main.Temp_min))
                .ForMember(dest => dest.TempMax, opt => opt.MapFrom(src => src.Main.Temp_max))
                .ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => src.TimeZone))
                .ForMember(dest => dest.WindDegrees, opt => opt.MapFrom(src => src.Wind.Deg))
                .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.Wind.Speed))
                .ForMember(dest => dest.WindGust, opt => opt.MapFrom(src => src.Wind.Gust))
                .ForMember(dest => dest.WeatherTypes, opt => opt.MapFrom(src => src.Weather));

            CreateMap<WeatherDescriptionDTO, WeatherType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Main))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon));

            CreateMap<WeatherType, WeatherTypeDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<UserForCreateDTO, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Salt, opt => opt.Ignore());
        }
    }
}
