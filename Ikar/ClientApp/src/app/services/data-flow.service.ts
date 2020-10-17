import { GridEyeDataModel } from './../models/GirdEyeDataModel';
import { Injectable } from '@angular/core';
import { merge, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataFlowService {

  private liveDataSource$ = new Subject<GridEyeDataModel>();
  private recordDataSource$ = new Subject<GridEyeDataModel>();
  private isLive = true;

  public frames$ = merge(this.liveDataSource$, this.recordDataSource$);

  constructor() { }

  public setFramesWithMetaData(dataModel: GridEyeDataModel) {
    if(this.isLive){
      this.liveDataSource$.next(dataModel);
    }
  }

  public playRecord(frames: number[][]){
    this.isLive=false;
  }


}
