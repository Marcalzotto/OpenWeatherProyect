import { CityDTO } from "./CityDTO";

export class BranchOfficeDTO
{
    constructor(
        public id:number,
        public description:string,
        public cityId:number,
        public city:CityDTO
    ){}
}