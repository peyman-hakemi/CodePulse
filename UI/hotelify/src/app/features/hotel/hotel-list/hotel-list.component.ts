import { Component, OnInit, ViewChild } from '@angular/core';
import { HotelService } from '../services/hotel.service';
import { Hotel } from '../models/hotel.model';
import { Observable } from 'rxjs';
import { DxDataGridComponent } from 'devextreme-angular';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-hotel-list',
  templateUrl: './hotel-list.component.html',
  styleUrls: ['./hotel-list.component.css'],
})
export class HotelListComponent implements OnInit {
  @ViewChild('mainGrid', { static: false }) mainGrid: any = DxDataGridComponent;
  hotels$?: Observable<Hotel[]>;
  expanded: boolean = false;

  constructor(private hotelService: HotelService) {}

  ngOnInit(): void {
    this.hotels$ = this.hotelService.getAllHotels();
  }

  saveButton = (e: any) => {
    console.log('ee', e.event);

    // this.mainGrid.instance.saveEditData();
    // this.mainGrid.instance.refresh();
  };
  cancelButton = () => {
    this.mainGrid.instance.cancelEditData();
  };

  async updateRow(e: any) {
    try {
      const dialogResult = await this.confirmAsync(
        'Are you sure?',
        'Confirm changes'
      );
      if (dialogResult) {
        let params = new HttpParams();
        for (let key in e.newData) {
          params = params.set(key, e.newData[key]);
        }
        // const validationResult = await lastValueFrom(this.httpClient.get("https://url/to/your/validation/service", { params }));
        // if (validationResult.errorText) {
        //     throw validationResult.errorText;
        // } else {
        //     e.cancel = false;
        // }
      } else {
        e.cancel = true;
      }
    } catch (error) {
      console.error('Validation or confirmation error', error);
      e.cancel = Promise.reject(error);
    }
  }

  private confirmAsync(message: string, title?: string): Promise<boolean> {
    return new Promise<boolean>((resolve) => {
      const dialogResult = confirm(message);
      resolve(dialogResult);
    });
  }
}
