import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NguoimuaService {

  constructor(private http: HttpClient) { }
  apiUrl = 'https://localhost:7006/api/NguoiMua/';

  GetVatTuChoMua(pg:any){
 return this.http.get(this.apiUrl+'GetVatTuChoMua/'+pg)
  }
  SearchVatTuChoMua(pg:any, search:any){
 return this.http.get(this.apiUrl+'SearchVatTuChoMua/'+pg+'/'+search)
  }
  TaoPhieuMua(val:any){
    return this.http.post(this.apiUrl +'TaoPhieuMua/',val);
  }
  GetPhieuMua(pg:number, size: number, idPhongBan:number, soreOrder:any, idUser:number, role:string){
    return this.http.get<any>(this.apiUrl+ 'GetAllPhieuTrinhMua/'+pg+'/'+size+'/'+idPhongBan+'/'+soreOrder+'/'+idUser+'/'+role);
  }
  GetChiTietPhieuMua(id:any){
    return this.http.get(this.apiUrl +'GetChiTietPhieuMua/'+id);
  }
  GetPhieuById(id:any){
    return this.http.get(this.apiUrl +'GetPhieuById/'+id);
  }
  DuyetPhieuMua(val:any,duyet:any){
    return this.http.put(this.apiUrl +'DuyetPhieuMua/'+duyet,val);
  }
  PhieuCanSua(val:any){
    return this.http.put(this.apiUrl +'PhieuCanSua/',val);
  }
  ShowPhieuKhongDuyet(pg:any,size:any,idUser:any){
    return this.http.get(this.apiUrl +'ShowPhieuKhongDuyet/'+pg+'/'+size+'/'+idUser);
  }
  GetPhieuSuaTra(idPhieu:any,idUser:any){
    return this.http.get(this.apiUrl +'GetPhieuSuaTra/'+idPhieu+'/'+idUser);
  }
  GetPhieuHoanThanh(pg:any,idUser:any){
    return this.http.get(this.apiUrl +'GetPhieuHoanThanh/'+pg+'/'+idUser);
  }
  GetPhieuNhapKho(pg:any,idUser:any){
    return this.http.get(this.apiUrl +'GetPhieuNhapKho/'+pg+'/'+idUser);
  }
  YeuCauNhapKho(idPhieu:any){
    return this.http.get(this.apiUrl +'YeuCauNhapKho/'+idPhieu);
  }
    // của thu kho
  GetPhieuChoThuKho(pg:any){
    return this.http.get(this.apiUrl +'GetPhieuChoThuKho/'+pg);
  }
  XacNhanThuKho(val:any,isXacNhan:any){
    return this.http.put(this.apiUrl +'XacNhanThuKho/'+isXacNhan,val);
  }
  XuatHoaDonTheoThang(month:any, idUser:any){
    return this.http.get(this.apiUrl +'XuatHoaDonTheoThang/'+month+'/'+idUser);
  }
}

