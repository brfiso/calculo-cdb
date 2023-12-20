import { Injectable } from '@angular/core';import { HttpClient, HttpClientModule, HttpHeaders } from '@angular/common/http';
import { CalculoModel } from './models/app.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = 'https://localhost:7229/api';

  constructor(private http: HttpClient) {}
  
  calcular(calculo: CalculoModel): Observable<any> {
    var headers = new HttpHeaders({
      'Access-Control-Allow-Headers':'*'
    });

    return this.http.post(`${this.apiUrl}/cdb/calcular`, calculo, {headers: headers});
  }
}
