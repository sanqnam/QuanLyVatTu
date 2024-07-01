import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class NotifService {
  private connection: HubConnection;
  public connectionId: any;
  constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl('https://localhost:7006/chat')
      .withAutomaticReconnect() //[0, 1, 3,5,10,13,15000,18000,21000,30000]
      .build();
   }
   async ngOnInit() {   }
    connServer(){
      try {        
        this.connection.start();
        console.log('Connected to SignalR hub in service');
        this.connectionId = this.connection.connectionId;
        
        //this.connectionId = this.connection.connectionId;
        console.log('2-->',this.connectionId);
        // localStorage.setItem('connHub', this.connectionId);
        
        // this.setConnect(this.idUser, this.val);
      } catch (error) {
        console.error('Failed to connect to SignalR hub', error);
      }
    }
    reConnServer(){
      this.connection.onreconnecting(error => {
        console.assert(this.connection.state === HubConnectionState.Reconnecting);
        console.log("Reconnecting...", error);
      });
      
      this.connection.onreconnected(connectionId => {
        // alert('re')
          console.assert(this.connection.state === HubConnectionState.Connected);
          // this.connectionId = this.connection.connectionId;
          // this.val = connectionId;
          console.log("Re -->:", this.connection.state);
          // localStorage.setItem('connHub',this.connectionId);
          console.log("Reconnected with connectionId:", connectionId);
          // this.setConnect(this.idUser, this.val);
      });
    }
    
}
