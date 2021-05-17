import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';

import {WeatherConditionDTO} from '../DTOs/WeatherConditionDTO';

@Injectable()
export class WeatherConditionService
{
    private url:string = "";
    constructor(private _http:HttpClient) {}

    getAll(cities:number[]):Observable<WeatherConditionDTO[]>{
        
        //definimos la url
        this.url = `${environment.environment}/api/weather-conditions`;

        //definimos parametros
        let params = new HttpParams();
        
        cities.forEach((value)=>{
            params = params.append('id',value.toString());
        });

        //definimos httpOptions a enviar al servier en la peticion get
        let httpOptions = {
            params : params
        }

        return this._http.get<WeatherConditionDTO[]>(this.url,httpOptions);
    }

    getHistorical(cities:number[], dateFrom:string, dateTo:string):Observable<WeatherConditionDTO[]>{

        this.url = `${environment.environment}/api/weather-conditions/history`;

        let params = new HttpParams();

        cities.forEach((value)=>{
            params = params.append('id',value.toString());
        });

        params = params.append('dateFrom',dateFrom);
        params = params.append('dateTo', dateTo);
        
        let httpOptions = {
            params : params
        }

        return this._http.get<WeatherConditionDTO[]>(this.url, httpOptions);
    }
}