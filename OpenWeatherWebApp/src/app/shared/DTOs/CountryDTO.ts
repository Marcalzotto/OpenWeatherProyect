import {CityDTO} from './CityDTO';

export class CountryDTO
{
    constructor(
        public id:number,
        public code:string,
        public name:string,
        public city: CityDTO[]
    ) {}  
}