import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validator, Validators } from '@angular/forms';  
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/service/auth/auth.service'; 
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { UserService } from 'src/app/service/user/user.service';

import { NotificasService } from 'src/app/service/notifi/notificas.service';
import { SignalrService } from 'src/app/service/signalr/signalr.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit{
  @Output() dataToggle: EventEmitter<boolean> = new EventEmitter();
  hoTen:any;
  chucVu:any;
  phongBan:any;
  hinhDaiDien:any;
  idUser:any;
  toggled:any = 1;
  nameId:string = "sidebar"
  toggle:boolean = false;
  search:any;
  searchForm!:FormGroup; 
  dsNoti:any=[];
  // private connection: HubConnection;
  url="";
  thongBao:any = "";
  public href: string = "";
  noti:any= Object;
  constructor(private builder: FormBuilder,  private router:Router,private UserService:UserService, private signalr: SignalrService, private serviceNoti: NotificasService){      
      // this.shareData.getSearch.subscribe(sea => )

  }
  
   ngOnInit():void {
    this.getUserInfor();
    this.signalr.notifi.subscribe((noti:any)=>{
    this.noti = noti;
   })
   console.log("notifi", this.noti);
   this.GetNotiCount(this.idUser);
  }
  sendUrl(){
    this.href = this.router.url.substring(1);
    console.log('href',this.href)
    this.search = this.searchForm.value;
    console.log('header url input',this.url);    
    //this.shareData.setSearch(this.search);
  }
  toggleSidebar(){
    if(this.toggle == false){
      this.toggle = true;
      this.dataToggle.emit(this.toggle);
    }else{
      this.toggle = false;
      this.dataToggle.emit(this.toggle);
    }
  }
  LoadNoti(idNhan:any){
    this.serviceNoti.GetNotiByNguoiNhan(idNhan).subscribe(res=>{
      this.dsNoti = res;
      console.log("ds noti", this.dsNoti);
    })
  }

  Logout(){
    localStorage.clear();
    sessionStorage.clear();
    this.router.navigateByUrl('/login');
  }
  getUserInfor(){
    this.idUser = localStorage.getItem('idUser');
    this.hoTen = localStorage.getItem('hoten');
    this.hinhDaiDien = localStorage.getItem('hinhDaiDien'); 
    this.chucVu = localStorage.getItem('maChucVu');
    this.phongBan = localStorage.getItem('maPhongBan');
  }
  getComponent(){
    this.href = this.router.url.substring(1);
    console.log('href',this.router.url)
  }
 GetNotiCount(idUser:any){
  this.UserService.GetNotiCount(idUser).subscribe(res=>{
    this.noti = res
    console.log("count thong bao", this.noti)
  })
 }
 SetStatus(idNoti:any){
  this.serviceNoti.SetStatus(idNoti).subscribe(res=>{
    console.log(res);
  })
 }
  
}
