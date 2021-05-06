import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';

import {WeatherConditionDTO} from '../DTOs/WeatherConditionDTO';
import {UnitsDTO} from '../DTOs/UnitsDTO';
import { CityDTO } from '../DTOs/CityDTO';
import { BranchOfficeDTO } from '../DTOs/BranchOfficeDTO';
import {WeatherTypeDTO} from '../DTOs/WeatherTypeDTO'

//create cost array of Countries here
// const CONDITIONS = [
//     new WeatherConditionDTO(1,"stations",1005,100,null,null,1.5,300,null,50,
//     null,1.4,null,null,1560343627, 1560396563, -25200, 1560350645, 99991, 200, new Date(), 
//     new UnitsDTO(5,10,15,2,4,6,6,12,18,4,8,16), new CityDTO(99991,"Caballito",null,-3,-3,1,null,
//     new BranchOfficeDTO(1, "Caballito office", 99999, null)),
//     [new WeatherTypeDTO(802,"Clear","Clear Sky","01d",1,null)
//     ]),
//     new WeatherConditionDTO(2,"stations",1005,100,null,null,1.5,300,null,50,
//     null,1.4,null,null,1560343627, 1560396563, -25200, 1560350645, 99991, 200, new Date(), 
//     new UnitsDTO(5,10,15,2,4,6,6,12,18,4,8,16), new CityDTO(99991,"Caballito",null,-3,-3,1,null,
//     new BranchOfficeDTO(1, "Caballito office", 99999, null)),
//     [new WeatherTypeDTO(804,"Rain","Rainy","02d",2,null),
//      new WeatherTypeDTO(804,"Snow","Heavy Snow","04d",2,null)
//     ]),
//     new WeatherConditionDTO(3,"stations",1005,100,null,null,1.5,300,null,50,
//     null,1.4,null,null,1560343627, 1560396563, -25200, 1560350645, 99991, 200, new Date(), 
//     new UnitsDTO(5,10,15,2,4,6,6,12,18,4,8,16), new CityDTO(99991,"Caballito",null,-3,-3,1,null,
//     new BranchOfficeDTO(1, "Caballito office", 99999, null)),
//     [new WeatherTypeDTO(802,"Clear","Clear Sky","01d",3,null)
//     ]),
//     new WeatherConditionDTO(4,"stations",1005,100,null,null,1.5,300,null,50,
//     null,1.4,null,null,1560343627, 1560396563, -25200, 1560350645, 99991, 200, new Date(), 
//     new UnitsDTO(5,10,15,2,4,6,6,12,18,4,8,16), new CityDTO(99991,"Caballito",null,-3,-3,1,null,
//     new BranchOfficeDTO(1, "Caballito office", 99999, null)),
//     [new WeatherTypeDTO(802,"Clear","Clear Sky","01d",4,null)
//     ]),
//     new WeatherConditionDTO(5,"stations",1005,100,null,null,1.5,300,null,50,
//     null,1.4,null,null,1560343627, 1560396563, -25200, 1560350645, 99991, 200, new Date(), 
//     new UnitsDTO(5,10,15,2,4,6,6,12,18,4,8,16), new CityDTO(99991,"Caballito",null,-3,-3,1,null,
//     new BranchOfficeDTO(1, "Caballito office", 99999, null)),
//     [new WeatherTypeDTO(802,"Clear","Clear Sky","01d",5,null)
//     ]),
//     new WeatherConditionDTO(5,"stations",1005,100,null,null,1.5,300,null,50,
//     null,1.4,null,null,1560343627, 1560396563, -25200, 1560350645, 99991, 200, new Date(), 
//     new UnitsDTO(5,10,15,2,4,6,6,12,18,4,8,16), new CityDTO(99991,"Caballito",null,-3,-3,1,null,
//     new BranchOfficeDTO(1, "Caballito office", 99999, null)),
//     [new WeatherTypeDTO(802,"Clear","Clear Sky","01d",5,null)
//     ]),
//     new WeatherConditionDTO(5,"stations",1005,100,null,null,1.5,300,null,50,
//     null,1.4,null,null,1560343627, 1560396563, -25200, 1560350645, 99991, 200, new Date(), 
//     new UnitsDTO(5,10,15,2,4,6,6,12,18,4,8,16), new CityDTO(99991,"Caballito",null,-3,-3,1,null,
//     new BranchOfficeDTO(1, "Caballito office", 99999, null)),
//     [new WeatherTypeDTO(802,"Clear","Clear Sky","01d",5,null)
//     ]),  
// ] 

@Injectable()
export class WeatherConditionService
{
    private url:string = "";
    constructor(private _http:HttpClient) {}
    
    // getAll():PromiseLike<any[]>
    // {
    //     return Promise.resolve<WeatherConditionDTO[]>(CONDITIONS);
    // }

    getAll(cities:number[]):Observable<WeatherConditionDTO[]>{
        
        this.url = `${environment.environment}/api/weather-conditions`;
        
        let params = new HttpParams();
        
        cities.forEach((value)=>{
            params = params.append('id',value.toString());
        });

        return this._http.get<WeatherConditionDTO[]>(this.url, {params});
    }

    getHistorical(cities:number[], dateFrom:string, dateTo:string):Observable<WeatherConditionDTO[]>{

        this.url = `${environment.environment}/api/weather-conditions/history`;
        
        let params = new HttpParams();

        cities.forEach((value)=>{
            params = params.append('id',value.toString());
        });

        params = params.append('dateFrom',dateFrom);
        params = params.append('dateTo', dateTo);
        
        return this._http.get<WeatherConditionDTO[]>(this.url, {params});

    }
}