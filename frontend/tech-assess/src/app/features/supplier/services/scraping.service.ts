import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ScrapeResult } from '../interfaces/scrape-result.model';
import { Source } from '../interfaces/source.enum';

@Injectable({
  providedIn: 'root'
})
export class ScrapingService {
  private backendUrl: string;

  constructor(private http: HttpClient) {
    this.backendUrl = `http://localhost:5000/api/scraping`;
  }

  getScrapedData(supplierName: string,
    sources: {
      oldCheckbox: boolean, wbCheckbox: boolean, ofacCheckbox: boolean
    }): Observable<ScrapeResult[]> {

    let params = new HttpParams().set("supplierName", supplierName)
    if (sources.ofacCheckbox) params = params.append("sources", Source.OLD.toString());
    if (sources.wbCheckbox) params = params.append("sources", Source.WB.toString());
    if (sources.ofacCheckbox) params = params.append("sources", Source.OFAC.toString());
    
    return this.http.get<ScrapeResult[]>(this.backendUrl, { params });
  }
}
