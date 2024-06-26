import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Country } from '../interfaces/country.model';

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  private backendUrl: string;

  constructor(private http: HttpClient) {
    this.backendUrl = `http://localhost:5000/api/country`;
  }

  getCountries(): Observable<Country[]> {

    return this.http.get<Country[]>(this.backendUrl);
  }
}
