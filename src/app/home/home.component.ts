import { Component } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { UserService } from 'src/app/service/user/user.service';
import { NotifService } from '../service/signalr/notif.service';
import { Router } from '@angular/router';
import { SignalrService } from '../service/signalr/signalr.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent { 
  public user: any = "";
  public message: string = "";
  public connectionId: any;
  public toId:string="";
  public toggleClass:any;
  toggle:any;
  search:any;
  idUser:any;
  val:any;
  public href: string = "";
  thongBao:any =[];
  token: any = localStorage.getItem('token')
  messages: { user: string, message: string }[] = [];
  public username: any = localStorage.getItem('username');
  public toUser:string="";


  constructor(private service:UserService, private router:Router, private signalr:SignalrService) {
    }
  async ngOnInit() {  
    this.idUser = localStorage.getItem('idUser');
    this.user = localStorage. getItem('hoten');
    this.signalr.startConnection(this.token);
    this.signalr.messageReceiced.subscribe((rep)=>{
    this.messages.push(rep);
    
    })
  }
  async sendMessage() {
    if (!this.user || !this.message) return;
    await this.signalr.sendMessage(this.username,this.user, this.message, this.toUser);
    this.message = '';      
  }
  changeToggle(toggle:boolean){
    if(toggle==true){
      this.toggleClass="toggle-sidebar"
    }else{
      this.toggleClass=""
    }
  }
  setConnect(id:any, val:any){
    this.service.SetConnectionId(id, val).subscribe(res =>{
    });
  }
}


