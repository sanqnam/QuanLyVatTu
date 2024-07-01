import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ChucvuService {

  apiUrl = 'https://localhost:7006/api/';

  constructor(private http: HttpClient) { }

  GetAll() {
    return this.http.get(this.apiUrl + 'ChucVu');
  }
  Search(search: any) {
    return this.http.get(this.apiUrl + 'ChucVu/search/' + search)
  }
  GetById(IdPhongBan: number) {
    return this.http.get<any>(this.apiUrl + 'ChucVu/' + IdPhongBan)
  }
  Update(Id: any, val: any) {
    return this.http.put<any>(this.apiUrl + 'ChucVu/Update/' + Id, val)
  }
  AddChucVu(val: any) {
    return this.http.post<any>(this.apiUrl + 'ChucVu/AddChucVu/', val)
  }
}
