import { ThermalColorPallete } from './helpers/ThermalColorPallete';
import { element } from 'protractor';
import { DataFlowService } from './services/data-flow.service';
import { Component, ViewChild, ElementRef } from '@angular/core';
import { SignalRService } from '../app/services/signal-r.service'
import { Observable } from 'rxjs';
import { GridEyeDataModel } from './models/GirdEyeDataModel';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  @ViewChild('canvas', { static: true })
  canvas: ElementRef<HTMLCanvasElement>;
  @ViewChild('scale', { static: true })
  canvasScale: ElementRef<HTMLCanvasElement>;

  private ctx: CanvasRenderingContext2D;
  private ctxScale: CanvasRenderingContext2D;
  public dataFlow$: Observable<GridEyeDataModel>;

  constructor(private signalRService: SignalRService, private dataFlowService: DataFlowService) {
    signalRService.connetct();
    this.dataFlow$ = this.dataFlowService.frames$;
  }
  
  ngOnInit() {
    this.ctx = this.canvas.nativeElement.getContext('2d');
    this.ctxScale = this.canvasScale.nativeElement.getContext('2d');
    this.drawSacle();
    if (this.canvas.nativeElement.width != this.canvas.nativeElement.height) {
      console.error("Canvas width must be same as height it must be square!");
    }
    this.dataFlow$.subscribe(data => this.drawFrame(data.Temperature));
  }

  public refresh() {
    this.ctx.clearRect(0, 0, this.canvas.nativeElement.width, this.canvas.nativeElement.height);
  }

  private drawSacle() {
    ThermalColorPallete.ironPallete.forEach((color, index) => {
      this.ctxScale.fillStyle = color;
      this.ctxScale.fillRect(0, ThermalColorPallete.ironPallete.length - index, this.canvasScale.nativeElement.width, 1);
    });

  }

  private drawFrame(frame: number[]) {
    this.refresh();
    frame.forEach((element, index) => {
      let pixelSize = this.canvas.nativeElement.width / 8;
      this.ctx.fillStyle = ThermalColorPallete.getRGB(element);
      //draw from left to right lines
      this.ctx.fillRect(index % 8 * pixelSize, Math.floor(index / 8) * pixelSize, pixelSize, pixelSize);
      this.ctx.font = "10px Arial";
      this.ctx.fillStyle = "White";
      this.ctx.fillText(element.toFixed(2), index % 8 * pixelSize + pixelSize / 4, Math.floor(index / 8) * pixelSize + pixelSize / 2);
    });
  }

  ngOnDestroy() {
    this.dataFlow$ = null;
  }

}
