import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable, count } from 'rxjs';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { ConsoleLogger } from '@microsoft/signalr/dist/esm/Utils';

@Injectable({
  providedIn: 'root'
})
export class uploadService implements OnInit {
  // href:any
  private baseUrl = 'https://localhost:7006/api/WriteFile';

  constructor(private http: HttpClient, private router:Router, private location: Location) {
  }
  ngOnInit() {

  }

  upload(file: File, folder:any): Observable<HttpEvent<any>> {
    const formData: FormData = new FormData();
    formData.append('file', file);
    console.log("form data", formData)
    const req = new HttpRequest('POST', `${this.baseUrl}/`+'UploadFile/'+folder, formData, {
      reportProgress: true,
      responseType: 'json',
    });
    return this.http.request(req);
  }
  uploadList(files: File[], folder:any):Observable<HttpEvent<any>>{
    const formData: FormData = new FormData();
    for(let i=0; files.length > i ; i++){
      formData.append('files', files[i]);
    }
    console.log("form data", formData)
    const req = new HttpRequest('POST', `${this.baseUrl}/`+'UploadFiles/'+folder, formData, {
      reportProgress: true,
      responseType: 'json',
    });
    return this.http.request(req);
  }
}