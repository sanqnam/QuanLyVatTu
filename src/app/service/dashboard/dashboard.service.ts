import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  apiUrl = 'https://localhost:7006/api/dashboard/';
  constructor(private http: HttpClient) { }

  TotolPhieuVT(phongBan:any,){
    return this.http.get(this.apiUrl+'GetDashboard/'+phongBan)
  }

  PhieuVTByMonth(phongBan:any,month:any){
    return this.http.get(this.apiUrl+'PhieuVTByMonth/'+phongBan+'/'+month)
  }

  GetPhieuVTByPB(phongBan:any, month:any){
    return this.http.get(this.apiUrl+'GetPhieuVTByPB/'+phongBan+'/'+month)
  }
  // phiếu sửa
  CountPhieuSuaByMonth(phongBan:any, month:any){
    return this.http.get(this.apiUrl+'CountPhieuSuaByMonth/'+phongBan+'/'+month)
  }
  CountPhieuSuaByPB(phongBan:any){
    return this.http.get(this.apiUrl+'CountPhieuSuaByPB/'+phongBan)
  }
// quản trị nhân viên
  CountNhanVienTheoChucVu(chucVu:any, ){
    return this.http.get(this.apiUrl+'CountNhanVienTheoChucVu/'+chucVu)
  }
  CountAllNhanVienTheoChucVu( ){
    return this.http.get(this.apiUrl+'CountAllNhanVienTheoChucVu/')
  }
  CountTatCaNhanVien(){
    return this.http.get(this.apiUrl+'CountTatCaNhanVien/')
  }
  CountNhanVienTheoPB(phongBan:any, ){
    return this.http.get(this.apiUrl+'CountNhanVienTheoPB/'+phongBan)
  }
  CountAllChucVu(){
    return this.http.get(this.apiUrl+'CountAllChucVu/')
  }
// các vật tư trong phòngban

CountVatTuTrongPhong(pb:any){
  return this.http.get(this.apiUrl+'CountVatTuTrongPhong/'+pb)
}
CountAllVatTuSD(){
  return this.http.get(this.apiUrl+'CountAllVatTuSD/')
}

// phần quản trị của thủ kho
CountAllVatTuSuDungTheoPhong(){
  return this.http.get(this.apiUrl+'CountAllVatTuSuDungTheoPhong/')
}
CountTongVatTuSuDung(){
  return this.http.get(this.apiUrl+'CountTongVatTuSuDung/')
}
CoutTongVatTu(){
  return this.http.get(this.apiUrl+'CoutTongVatTu/')
}
// quản trị tiền

TongTienTrongNam(){
  return this.http.get(this.apiUrl+'TongTienTrongNam/')
}
TienTheoThang(){
  return this.http.get(this.apiUrl+'TienTheoThang/')
}
} 
