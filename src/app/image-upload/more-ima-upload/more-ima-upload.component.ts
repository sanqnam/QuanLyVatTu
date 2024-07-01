import { HttpEventType,HttpResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { uploadService } from 'src/app/service/file/upload.service';

@Component({
  selector: 'app-more-ima-upload',
  templateUrl: './more-ima-upload.component.html',
  styleUrls: ['./more-ima-upload.component.css']
})
export class MoreImaUploadComponent  implements OnInit{
  @Output() public sendUrl = new EventEmitter<string>();
  
  selectedFiles?: FileList;
  currentFile?: File;
  progress = 0;
  message = '';
  preview :any;
  previews :any = [];
  url:any=[];
  imageInfos?: Observable<any>;
  serverUrl="https://localhost:7006/";

  public href: string = "";
  
  constructor(private uploadService: uploadService, private router:Router,private toastr: ToastrService) {}

  ngOnInit() {
    this.href = this.router.url.replace(this.router.url,'/');
    if(this.href='profile'){
      this.href='user';
    }
    else if(this.href ='vattusudung'){
      this.href ='VatTuSuaChua'
    }
  }
  
  selectFile(event: any): void {
    this.message = '';
    this.preview = '';
    this.progress = 0;
    this.selectedFiles = event.target.files;
    if (this.selectedFiles) {
      for(let i=0; this.selectedFiles.length > i ;i++){
        const file: File | null = this.selectedFiles.item(i);
        if (file) {
          this.currentFile = file;
          const reader = new FileReader();
          reader.onload = (e: any) => {
            this.preview= e.target.result;
            this.previews.push( this.preview);
            console.log("preview",this.previews);
          };
          reader.readAsDataURL(this.currentFile);
        }
      }
      console.log("preview")
    }
  }
  upload(): void {
    this.progress = 0;
    const files :File[]=[];
    if (this.selectedFiles) {
      for(let i =0; this.selectedFiles.length > i ; i ++){
        const file: File | null = this.selectedFiles.item(i);
        if (file) {
          files.push(file);
        }
      }
        this.uploadService.uploadList(files, this.href).subscribe({
          next: (event: any) => {
            console.log('event',event)
            if(event.type >=4){
              this.url = event.body.value; 
              this.sendUrl.emit(this.url);
              console.log("log url", this.url);
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


      this.selectedFiles = undefined;
    }
  }
}
