import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  token:any;
  hubConnection:any;

  public messageReceiced = new Subject<{user: string , message: string}>();
  public notifi = new Subject<number>();
  constructor() { }
  startConnection = (token:any)=>{
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl('https://localhost:7006/chat', {accessTokenFactory:()=>token})
    .withAutomaticReconnect()
    .build();
    this.hubConnection
    .start()
    .then(()=>console.log('connextion started'))
    .catch((err:any) => console.log("lỗi  kết nối" + err));
    this.ListenerMessage();
    this.ListenerNotifica();
  }
   ListenerMessage = () =>{
    this.hubConnection.on('ReceiveMessage', (user: string, message:string)=>{
      this.messageReceiced.next({user, message});
    })
   }   
   ListenerNotifica = () =>{
    this.hubConnection.on('ReceiveNotifica',(noti:any)=>{
      this.notifi.next(noti);
      console.log('notiffi log ', noti)
    })
   }
   sendMessage = (formUser:string,user:string,message: string,toUser:any) => {
    this.hubConnection.invoke('SendMessage', formUser, user, message, toUser)
      .catch((err:any) => console.error(err));
  }
}
