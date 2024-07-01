import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { uploadService } from '../service/file/upload.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent implements OnInit {
  @Output() public sendUrl = new EventEmitter<string>();
  selectedFiles?: FileList;
  currentFile?: File;
  progress = 0;
  message = '';
  preview = '';
  url="";
  imageInfos?: Observable<any>;
  serverUrl="https://localhost:7006/";

  public href: string = "";
  
  constructor(private uploadService: uploadService, private router:Router,private toastr: ToastrService) {}

  ngOnInit() {
    this.href = this.router.url.replace(this.router.url,'/');
    if(this.href='profile'){
      this.href='user';
    }
    
  }
  
  selectFile(event: any): void {
    this.message = '';
    this.preview = '';
    this.progress = 0;
    this.selectedFiles = event.target.files;
    console.log("file", this,this.selectedFiles)
    if (this.selectedFiles) {
      const file: File | null = this.selectedFiles.item(0);
      if (file) {
        this.preview = '';
        this.currentFile = file;
        console.log("curenfile", this.currentFile)
        const reader = new FileReader();
        reader.onload = (e: any) => {
          // console.log(e.target.result);
          this.preview = e.target.result;
        };
        reader.readAsDataURL(this.currentFile);
      }
    }
  }
  upload(): void {
    this.progress = 0;
    console.log("select file", this.selectedFiles)
    if (this.selectedFiles) {
      const file: File | null = this.selectedFiles.item(0);
      if (file) {
        this.currentFile = file;
        console.log("select file 2", this.currentFile)
        this.uploadService.upload(this.currentFile, this.href).subscribe({
          next: (event: any) => {
            console.log('event',event)
            if(event.type >=4){
              this.url = this.serverUrl+String(event.body.value);
              this.sendUrl.emit(this.url);
            }  
            if (event.type === HttpEventType.UploadProgress) {
              this.progress = Math.round((100 * event.loaded) / event.total);
              this.toastr.success('Upload Thành Công','Successfully'); 
            } else if (event instanceof HttpResponse) {
              this.message = event.body.message;
            }            
          },
          error: (err: any) => {
            console.log('err',err);
            this.progress = 0;
            if (err.error && err.error.message) {
              this.message = err.error.message;
            } else {
              this.toastr.error('Upload Không Thành Công','Error'); 
            }
            this.currentFile = undefined;
          },
        });
      }
      this.selectedFiles = undefined;
    }
  }


}