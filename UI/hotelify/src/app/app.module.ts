import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HotelListComponent } from './features/hotel/hotel-list/hotel-list.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';

import {
  DxButtonModule,
  DxDataGridModule,
  DxListModule,
} from 'devextreme-angular';
import { MarkdownModule } from 'ngx-markdown';
import { AddHotelComponent } from './features/hotel/add-hotel/add-hotel.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { EditHotelComponent } from './features/hotel/edit-hotel/edit-hotel.component';
import { BlogpostListComponent } from './features/blog-post/blogpost-list/blogpost-list.component';
import { AddBlogpostComponent } from './features/blog-post/add-blogpost/add-blogpost.component';
import { EditBlogpostComponent } from './features/blog-post/edit-blogpost/edit-blogpost.component';

@NgModule({
  declarations: [
    AppComponent,
    HotelListComponent,
    NavbarComponent,
    AddHotelComponent,
    EditHotelComponent,
    BlogpostListComponent,
    AddBlogpostComponent,
    EditBlogpostComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DxListModule,
    FormsModule,
    HttpClientModule,
    DxDataGridModule,
    DxButtonModule,
    MarkdownModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
