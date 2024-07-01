import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { retry } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificasService {

  constructor(private http:HttpClient) { }
  apiUrl = 'https://localhost:7006/api/Notificas/';

  GetNotiByNguoiNhan(idNhan:any){
    return this.http.get(this.apiUrl + 'GetNotiByNguoiNhan/'+idNhan)
  }
  SetStatus(idNoti:any){
    return this.http.get(this.apiUrl +'SetStatus/'+idNoti)
  }
}
