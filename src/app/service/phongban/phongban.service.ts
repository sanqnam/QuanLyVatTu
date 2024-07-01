import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PhongBanService {

  constructor(private http: HttpClient) { }
  apiUrl = 'https://localhost:7006/api/PhongBan/';

  // Login 
  GetAll() {
    return this.http.get(this.apiUrl);
  }
  GetByIdPhongBan(idPhongBan: number) {
    return this.http.get<any>(this.apiUrl+ 'IdPhongBan'+ idPhongBan);
  }
  Search(search:any){
    return this.http.get(this.apiUrl + 'Search/'+ search);
  }
  Update(idPhongBan:number, val:any){
    return this.http.put<any>(this.apiUrl + 'Update/'+idPhongBan , val);
  }
  AddPhongBan(val:any){
    return this.http.post<any>(this.apiUrl, val);
  }
  GetAllPBSua(){
    return this.http.get(this.apiUrl+'GetAllPBSua/');
  }
}
