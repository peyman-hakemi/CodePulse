import { Injectable } from '@angular/core';
import { AddHotelRequest } from '../models/add-hotel-request.module';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Hotel } from '../models/hotel.model';
import { environment } from 'src/environments/environment';
import { UpdateHotelRequest } from '../models/update-hotel-request.models';

@Injectable({
  providedIn: 'root',
})
export class HotelService {
  constructor(private http: HttpClient) {}

  addHotel(model: AddHotelRequest): Observable<void> {
    return this.http.post<void>(
      `${environment.API_BASE_URL}/api/Hotels`,
      model
    );
  }
  getAllHotels(): Observable<Hotel[]> {
    return this.http.get<Array<Hotel>>(
      `${environment.API_BASE_URL}/api/Hotels`
    );
  }

  getHotelById(id: string): Observable<Hotel> {
    return this.http.get<Hotel>(`${environment.API_BASE_URL}/api/hotels/${id}`);
  }

  editHotel(id: string, hotel: UpdateHotelRequest): Observable<Hotel> {
    return this.http.put<Hotel>(
      `${environment.API_BASE_URL}/api/hotels/${id}`,
      hotel
    );
  }

  deleteHotel(id: string): Observable<Hotel> {
    return this.http.delete<Hotel>(
      `${environment.API_BASE_URL}/api/hotels/${id}`
    );
  }
}
