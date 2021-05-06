import { BranchOfficeDTO } from './BranchOfficeDTO';
import {CountryDTO} from './CountryDTO';

export class CityDTO
{
    constructor(
        public id: number,
        public name:string,
        public state:string,
        public longitude:number,
        public latitude:number,
        public countryId:number,
        public country:CountryDTO,
        public branchOffice:BranchOfficeDTO
    ){}
    
}