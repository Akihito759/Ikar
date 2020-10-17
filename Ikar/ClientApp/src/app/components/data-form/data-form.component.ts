
import { HttpCommunicationService } from './../../services/http-communication.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { RSA_X931_PADDING } from 'constants';
import { interval, observable, Subject, timer } from 'rxjs';
import { take, map } from 'rxjs/operators';

@Component({
  selector: 'app-data-form',
  templateUrl: './data-form.component.html',
  styleUrls: ['./data-form.component.scss']
})
export class DataFormComponent implements OnInit {
  isLoading = false;
  counterSource = new Subject<number>();

  private roomTemperature;
  private sessionId;

  counter$ = this.counterSource.asObservable();
  data = new FormGroup({
    isFall: new FormControl(''),
    isLie: new FormControl(''),
    isFullCloth: new FormControl(''),
    sessionId: new FormControl(''),
    temperature: new FormControl('')
  })

  onSubmit() {
    const value = this.data.value;
    let dto = {
      Temperature: value['temperature'],
      RecordingSessionId: value['sessionId'],
      Fall: Boolean(value['isFall']),
      Lie: Boolean(value['isLie']),
      FullCloth: Boolean(value['isFullCloth']),
    }
    this.isLoading = true;
    this.count();
    this.http.playAndSave(dto).subscribe(() => {
      this.isLoading = false;
      this.saveValues();
      this.data.reset();
      this.setSavedValues();
    });
  }

  count() {
    let seconds = 10;
    timer(0, 1000).pipe(take(seconds)).subscribe(x =>
      this.counterSource.next(seconds--));
  }

  saveValues(){
    let value = this.data.value;
    this.roomTemperature = value['temperature'];
    this.sessionId = value['sessionId'];
  }

  setSavedValues(){
    this.data.setValue({
      temperature: this.roomTemperature,
      sessionId: this.sessionId,
      isFall: '',
      isLie: '',
      isFullCloth:'',
    })
  }

  

  constructor(private http: HttpCommunicationService) { }

  ngOnInit(): void {
  }

}
