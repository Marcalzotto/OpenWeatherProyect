import {Injectable} from '@angular/core';
import {BranchOfficeDTO} from '../DTOs/BranchOfficeDTO';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../environments/environment';
import {Operation} from 'fast-json-patch';

@Injectable()
export class OfficeService
{
    private url:string = "";
    constructor(private _http:HttpClient) 
    {}

    getByCountryId(countryId, includeCities):Observable<BranchOfficeDTO[]>{
        
        this.url = `${environment.environment}/api/offices`;

        let params = new HttpParams();

        params = params.append('countryId', countryId);
        params = params.append('includeCities', includeCities);

        let httpOptions = {
            params: params
        }
 
        return this._http.get<BranchOfficeDTO[]>(this.url,httpOptions);
    }

    
    createOffice(office:BranchOfficeDTO):Observable<BranchOfficeDTO>
    {
        this.url = `${environment.environment}/api/offices`;
        
        return this._http.post<BranchOfficeDTO>(this.url, office);
    }

    getById(id:string):Observable<BranchOfficeDTO>
    {
        this.url = `${environment.environment}/api/offices/${id}`;

        return this._http.get<BranchOfficeDTO>(this.url);       
    }
    
    patchOffice(id:number, operations:Operation[])
    {
        this.url = `${environment.environment}/api/offices/${id}`;
        return this._http.patch(this.url, operations);
    }

    deleteOffice(id:number)
    {
        this.url = `${environment.environment}/api/offices/${id}`;
        return this._http.delete(this.url);
    }
}