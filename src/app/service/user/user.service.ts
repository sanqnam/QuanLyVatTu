import { ErrorHandler, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  apiUrl = 'https://localhost:7006/api/';

  // Login 
  GetAll(page:any, size:any, phongBan:any,chucVu:any){
    return this.http.get(this.apiUrl+'User/GetAll/'+page+'/'+size+'/'+phongBan+'/'+chucVu);
  }
  Search(page:any, size:any, search:any){
    return this.http.get(this.apiUrl+'User/Search/'+page+'/'+size+'/'+search);
  }
  GetByIdPhongBan(idPhongBan:number){
    return this.http.get<any>(this.apiUrl+'User/PhongBan/'+idPhongBan);
  }
  GetByIdUser(idUser:any){
    return this.http.get<any>(this.apiUrl+'User/'+idUser);
  }
  UpdateUser(idUser:any, val:any){
    return this.http.put<any>(this.apiUrl+'User/'+idUser, val)
  }  
  SetConnectionId(idUser:any, val:any){
    return this.http.get<any>(this.apiUrl+'User/SetConnectionId/'+idUser+'/'+val);
  }
  AddUser(val:any){
    return this.http.post<any>(this.apiUrl+'User/AddUser/', val);
  }  
  ActiveUser(idUser:any){
    return this.http.get<any>(this.apiUrl+'User/Active/'+idUser);
  }
  GetChucVu(){
    return this.http.get<any>(this.apiUrl+'ChucVu/');
  }
  ResetPass(idUser:any){
    return this.http.get<any>(this.apiUrl+'User/ResetPass/'+idUser);
  }
  getPage(tableSize:any){
    return this.http.get(this.apiUrl+'User/GetPage/'+tableSize);
  }
  GetNotiCount(idUser:any){
    return this.http.get<any>(this.apiUrl+'Notificas/GetCountNoti/'+idUser);
  }
  DoiMaBiMat(idUser:any, val:any){
    return this.http.put<any>(this.apiUrl+'User/DoiMaBiMat/'+idUser, val)
  }
  GetUserSuaByPhongBan(idPhongBan:number){
    return this.http.get<any>(this.apiUrl+'User/GetUserSuaByPhongBan/'+idPhongBan);
  }
}


