import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PhieuSuaService {

  constructor(private http:HttpClient) { }
  apiUrl = 'https://localhost:7006/api/PhieuDeNghiSua/';


  AddPhieuSua(val: any){
    return this.http.post(this.apiUrl+'AddPhieuSua/', val)
  }
  GetAllPhieu(idPhongBan:any, idNguoiDuyet:any , pg:any, size:any){
    return this.http.get(this.apiUrl + 'GetAllPhieu/'+idPhongBan + '/'+idNguoiDuyet +'/'+pg+'/'+size)
  }
  GetImgByPhieu(idChiTietPhieu:any){
    return this.http.get(this.apiUrl + 'GetImgByPhieu/'+idChiTietPhieu)
  }
  GetVatTuByPhieu(idphieu:any){
    return this.http.get(this.apiUrl + 'GetVatTuByPhieu/'+idphieu)
  }
  DuyetPhieuSua(duyet:any, idPhieu:any, lydo:any,maBiMat:any){
    return this.http.get(this.apiUrl + 'DuyetPhieuSua/'+duyet+'/'+idPhieu+'/'+lydo+'/'+maBiMat)
  }
  SetDeNghiThanhLy( idPhieu:any){
    return this.http.get(this.apiUrl + 'SetDeNghiThanhLy/'+idPhieu)
  }
  SetSuaXong( idPhieu:any){
    return this.http.get(this.apiUrl + 'SetSuaXong/'+idPhieu)
  }
  SetNhanVienSua(idUser:any, idPhieu:any, idUserGui:any){
    return this.http.get(this.apiUrl + 'SetNhanVienSua/'+idUser+'/'+idPhieu+'/'+idUserGui)
  }
  ShowVTCanSuaByNVSua( idUser:any, pg:number,  size:number ){
    return this.http.get(this.apiUrl + 'ShowVTCanSuaByNVSua/'+idUser+'/'+pg+'/'+size)
  }
  AllPhieuSuaByPhongBanPhuTrach(idPhongBan:any, role:any, pg:any, size:any, orderShort:any){
    return this.http.get(this.apiUrl + 'AllPhieuSuaByPhongBanPhuTrach/'+idPhongBan+'/'+role+'/'+pg+'/'+size+'/'+orderShort)
  }
  GetAllStatusPhieuByPB(idPhongBan:any, size:any, pg:any, status:any){
    return this.http.get(this.apiUrl + 'GetAllStatusPhieuByPB/'+idPhongBan+'/'+size+'/'+pg+'/'+status)
  }
  // phiếu sửa đang chờ duyệt
  GetAllPhieuDangCho(idUser:any, pg:any, size:any, oderSort:any, chosse:any){
    return this.http.get(this.apiUrl + 'GetAllPhieuDangCho/'+idUser+'/'+pg+'/'+size+'/'+oderSort+'/'+chosse)
  }
  // phiếu sửa đã hoàn thành
  GetAllPhieuSussces(idUser:any, pg:any, size:any, oderSort:any, chosse:any){
    return this.http.get(this.apiUrl + 'GetAllPhieuSussces/'+idUser+'/'+pg+'/'+size+'/'+oderSort+'/'+chosse)
  }
}
