import { ErrorHandler, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VattuService {

  constructor(private http: HttpClient) { }
  apiUrl = 'https://localhost:7006/api/';

  Search(page:any, size:any, search:any){
    return this.http.get(this.apiUrl+'VatTu/Search/'+page+'/'+size+'/'+search);
  }
  AddQuanTam(val:any){
    return this.http.post<any>(this.apiUrl+'VTQuanTam/AddQuanTam/', val);
  }

  GetDsCho(idUser:any){
    return this.http.get(this.apiUrl+'VTQuanTam/GetAll/'+idUser);
  }  
  getPhongBan(idPhongBan:any){
    return this.http.get(this.apiUrl+'User/TruongPhong/'+idPhongBan);
  }

  TaoPhieu(val:any){
    return this.http.post(this.apiUrl+'PhieuDeNghVatTu/TaoPhieu/',val)
  }
  AddVatTu(val:any){
    return this.http.post(this.apiUrl +'ThuKho/AddVatTu',val)
  }
  GetVTSuDung(idUser:any){
    return this.http.get(this.apiUrl+'VatTu/VatTuSuDung/'+idUser);
  }
  SearchVatTuDangYeuCau(search:any, pg:any, idPhongBan:any){
    return this.http.get(this.apiUrl + 'VatTu/SearchVatTuDangYeuCau/'+ search+'/'+ pg + '/'+idPhongBan);
  }
  GetVatTuDangYeuCau(idPhongBan:any, pg:any){
    return this.http.get(this.apiUrl+'VatTu/GetVatTuDangYeuCau/'+idPhongBan+'/'+pg)
  }
  GetAllBySearch(idPhongBan:any, ten:any,vattu:any, pg:any, size:any){
    return this.http.get(this.apiUrl+'VatTu/GetAllBySearch/'+idPhongBan+'/'+ten+'/'+vattu+'/'+pg+'/'+size)
  }
  GetAllByIdPhongBan(idPhongBan:any,  pg:any, size:any){
    return this.http.get(this.apiUrl+'VatTu/GetAllByIdPhongBan/'+idPhongBan+'/'+pg+'/'+size)
  }
}
