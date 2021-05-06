import { CityDTO } from "./CityDTO";
import { UnitsDTO } from "./UnitsDTO";
import { WeatherTypeDTO } from "./WeatherTypeDTO";

export class WeatherConditionDTO
{
    constructor(
        public id:number,
        public base:string,
        public pressure:number,
        public humidity:number,
        public seaLevel:number,
        public groundLevel:number,
        public windSpeed:number,
        public windDegrees:number,
        public windGust:number,
        public clouds:number,
        public rainVolume1h:number,
        public rainVolume3h:number,
        public snowVolume1h:number,
        public snowVolume3h:number,
        public sunrise:number,
        public sunset:number,
        public timezone:number,
        public dt:number,
        public cityId:number,
        public statusCode:number,
        public regDate:Date,
        public units:UnitsDTO,
        public city:CityDTO,
        public weatherType:WeatherTypeDTO[]
    ){}
}