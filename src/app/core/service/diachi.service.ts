import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};
@Injectable({
  providedIn: 'root'
})
export class DiachiService {

  private API_URL = 'http://localhost:8961/api/DiaChi';
  constructor(private readonly http: HttpClient) {}
  GetTinhTP() {
    const url = `${this.API_URL}/get-province-all`;
    return this.http.get(url);
  }
  GetQH(id: any) {
    const url = `${this.API_URL}/get-qh-by-matinh/${id}`;
    return this.http.get(url);
  }
  GetXP(id: any) {
    const url = `${this.API_URL}/get-xp-by-maqh/${id}`;
    return this.http.get(url);
  }

}
