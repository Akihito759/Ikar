
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
  privateCounter = 0;

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
      this.privateCounter++;
    });
  }

  count() {
    let seconds = 10;
    timer(0, 1000).pipe(take(seconds)).subscribe(x =>
      this.counterSource.next(seconds--));
  }

  onReset(){
    this.privateCounter = 0;
  }

  

  constructor(private http: HttpCommunicationService) { }

  ngOnInit(): void {
  }

}
