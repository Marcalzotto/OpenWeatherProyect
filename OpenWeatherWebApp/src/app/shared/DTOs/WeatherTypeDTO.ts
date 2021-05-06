import { WeatherConditionDTO } from "./WeatherConditionDTO";

export class WeatherTypeDTO
{
    constructor(
        public  id:number,
        public  main:string,
        public  description:string,
        public  icon:string,
        public  conditionId:number,
        public  condition:WeatherConditionDTO 
    ){}
}