import { GridEyeDataModel } from './../models/GirdEyeDataModel';
import { Injectable } from '@angular/core';
import * as SignalR from '@aspnet/signalr'
import { environment } from '../../environments/environment'
import { DataFlowService} from './data-flow.service'

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hub: SignalR.HubConnection;

  constructor(private dataFlowService: DataFlowService) { }

  public connetct = () => {
    this.hub = new SignalR.HubConnectionBuilder().withUrl(`${environment.url}/dataHub`).build();
    this.addListeners();
    this.hub.start().then(() => {
      console.log('starting connection');
      this.Ping();
    }).catch((err) => console.warn('connetion err', err));
  }

  private addListeners = () => {
    this.hub.on("Message", (message:string) =>{
      if(message != "pong"){
        let object: GridEyeDataModel = JSON.parse(message);
        this.dataFlowService.setFramesWithMetaData(object);
      }
    } );
  }

  private Ping() {
    this.hub.invoke("CommunicationTest", "ping");
  }
}
