import { Component, OnDestroy } from '@angular/core';
import { AddHotelRequest } from '../models/add-hotel-request.module';
import { HotelService } from '../services/hotel.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-hotel',
  templateUrl: './add-hotel.component.html',
  styleUrls: ['./add-hotel.component.css'],
})
export class AddHotelComponent implements OnDestroy {
  model: AddHotelRequest;

  private addHotelSubscription?: Subscription;

  constructor(private hotelService: HotelService, private router: Router) {
    this.model = {
      name: '',
      description: '',
    };
  }
  ngOnDestroy(): void {
    this.addHotelSubscription?.unsubscribe();
  }

  onFormSubmit() {
    this.addHotelSubscription = this.hotelService
      .addHotel(this.model)
      .subscribe({
        next: () => this.router.navigateByUrl('/admin/hotels'),
      });
  }
}
