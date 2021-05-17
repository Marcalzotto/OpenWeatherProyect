import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CountryDTO} from '../DTOs/CountryDTO';
import {environment} from '../../../environments/environment';


@Injectable()
export class CountryService
{
    private url:string = `${environment.environment}/api/countries`;

    constructor(private _http:HttpClient) 
    {}
    
    getAll():Observable<CountryDTO[]>
    {
        return this._http.get<CountryDTO[]>(this.url);
    }
}