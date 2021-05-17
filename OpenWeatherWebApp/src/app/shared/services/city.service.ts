import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CityDTO } from '../DTOs/CityDTO';
import { environment } from '../../../environments/environment';

@Injectable()
export class CityService {

  private url:string = "";
  constructor(private _http:HttpClient) { }

  getCitiesWithOutOffice(countryId:number):Observable<CityDTO[]>
  {
    this.url = `${environment.environment}/api/countries/${countryId}/cities`;
    return this._http.get<CityDTO[]>(this.url);
  }
}
