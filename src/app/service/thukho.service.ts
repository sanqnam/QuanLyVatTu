import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ThukhoService {

  constructor(private http: HttpClient) { }
  apiUrl = 'https://localhost:7006/api/ThuKho/';

  GetAllVatTu(pg: any, size: any) {
    return this.http.get(this.apiUrl + 'GetAllVatTuTrongKho/' + pg + '/' + size);
  }
  GetAllPhieuCapVatTu(pg: any, size: any) {
    return this.http.get(this.apiUrl + 'GetAllPhieuCapVatTu/' + pg + '/' + size);
  }
  GetAllDetailPhieuCapVatTu(idPhieu: any) {
    return this.http.get(this.apiUrl + 'GetAllDetailPhieuCapVatTu/' + idPhieu);
  }
  CapVatTu(idVatTu: any, idUser: any, idPhieu: any) {
    return this.http.get(this.apiUrl + 'CapVatTu/' + idVatTu + '/' + idUser + '/' + idPhieu);
  }
  YeuCauMua(val:any){
    return this.http.post(this.apiUrl+'TaoYeuCauMua', val);
  }
  GetYeuCauMua(idPhieu:any){
    return this.http.get(this.apiUrl + 'GetYeuCauMuaVT/'+idPhieu);
  }
  AddMaVatTu(idVatTu: any, idUser: any, idPhieu: any){
    return this.http.get(this.apiUrl + 'AddMaVatTu/' + idVatTu + '/' + idUser + '/' + idPhieu);
  }

}
