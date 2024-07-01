import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PhieuDNVTService {

  constructor(private http:HttpClient) { }
  apiUrl = 'https://localhost:7006/api/';


  GetAllPhieuDeNghiVT(idUser:number,idPhongBan:number){
    return this.http.get(this.apiUrl+'PhieuDeNghVatTu/DsPhieu/'+idUser+'/'+ idPhongBan)
  }
  GetAllChiTietPhieuDeNghiVT(idUser:number, idPhongBan:number){
    return this.http.get<any>(this.apiUrl+'PhieuDeNghVatTu/DsPhieuChiTiet/'+idUser +'/'+idPhongBan)
  }
  // GetAllPhieuChiTiet(){
  //   return this.http.get<any>(this.apiUrl+'PhieuDeNghVatTu/ChiTietPhieu/')
  // }
  GetById(idPhieu:any){
    return this.http.get<any>(this.apiUrl+'PhieuDeNghVatTu/ChiTietPhieu/'+idPhieu)
  }
  GetAllPhieuDeNghi(idPhongBan:any){
    return this.http.get<any>(this.apiUrl + 'PhieuDeNghVatTu/DsPhieu/'+ idPhongBan)
  }
  DuyetPhieu(val:any, idPhieu:any, duyet: boolean, idUser:any){
    return this.http.put<any>(this.apiUrl+ 'PhieuDeNghVatTu/DuyetPhieu/'+idPhieu +'/'+duyet+'/'+idUser,val)
  }
  GetAllSapXep(pg:number, size: number, idPhongBan:number, soreOrder:any, status:any, choose:any){
    return this.http.get<any>(this.apiUrl+ 'PhieuDeNghVatTu/GetAllSapXep/'+pg+'/'+size+'/'+idPhongBan+'/'+soreOrder+'/'+status+'/'+choose);
  }
  GetAllTheoPhongBan(pg:number, size: number, idPhongBan:number, soreOrder:any, choose:any){
    return this.http.get<any>(this.apiUrl+ 'PhieuDeNghVatTu/GetAllSapXepTheoPhongBan/'+pg+'/'+size+'/'+idPhongBan+'/'+soreOrder+'/'+choose);
  }
  GetAllPhieuDuyet(pg:number, size: number, idPhongBan:number, soreOrder:any, idUser:number, role:string){
    return this.http.get<any>(this.apiUrl+ 'PhieuDeNghVatTu/GetAllPhieuDuyet/'+pg+'/'+size+'/'+idPhongBan+'/'+soreOrder+'/'+idUser+'/'+role);
  }
  GetAllPhieuTra(pg:number, size: number, idPhongBan:number, soreOrder:any){
    return this.http.get<any>(this.apiUrl+ 'PhieuDeNghVatTu/GetAllPhieuTra/'+pg+'/'+size+'/'+idPhongBan+'/'+soreOrder);
  }
}
