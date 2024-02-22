import { UpdateHotelRequest } from './../models/update-hotel-request.models';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { HotelService } from '../services/hotel.service';
import { Hotel } from '../models/hotel.model';

@Component({
  selector: 'app-edit-hotel',
  templateUrl: './edit-hotel.component.html',
  styleUrls: ['./edit-hotel.component.css'],
})
export class EditHotelComponent implements OnInit, OnDestroy {
  id: string | null = null;
  paramsSubscription?: Subscription;
  editHotelSubscription?: Subscription;
  hotel?: Hotel;

  constructor(
    private route: ActivatedRoute,
    private hotelService: HotelService,
    private router: Router
  ) {}

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editHotelSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.hotelService.getHotelById(this.id).subscribe({
            next: (response) => (this.hotel = response),
          });
        }
      },
    });
  }

  onFormSubmit(): void {
    const updateHotelRequest: UpdateHotelRequest = {
      availableFrom: this.hotel?.availableFrom,
      description: this.hotel?.description ?? '',
      name: this.hotel?.name ?? '',
    };
    if (this.hotel)
      this.editHotelSubscription = this.hotelService
        .editHotel(this.hotel.id, updateHotelRequest)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/hotels');
          },
        });
  }
  onDelete(): void {
    if (this.hotel?.id) {
      this.hotelService.deleteHotel(this.hotel.id).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/hotels');
        },
      });
    }
  }
}
