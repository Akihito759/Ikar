import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpCommunicationService {
  constructor(private http: HttpClient) { }

  playAndSave(data: any){
    console.log(data);
     return this.http.post(`${environment.url}/api/Recording/StartAndSave`,data);
  }
}
