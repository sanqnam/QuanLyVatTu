import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PageService {

  constructor(private http: HttpClient) { }
  apiUrl = 'https://localhost:7006/api/';

  // Login 
  getPage(url:any,tableSize:any){
    return this.http.get(this.apiUrl+url+'/GetPage/'+tableSize);
  }
}
