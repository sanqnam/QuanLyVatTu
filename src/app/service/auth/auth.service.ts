import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }
  apiUrl = 'https://localhost:7006/api/';

  // Login 
  Login(data:any){
    return this.http.post(this.apiUrl+'Login/Login/',data,{responseType: 'text'});
  }
  GetName(data:any){
    return this.http.post(this.apiUrl+'Login/Getname/',data);
  }
}
