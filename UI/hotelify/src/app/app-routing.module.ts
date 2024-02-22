import { BlogpostListComponent } from './features/blog-post/blogpost-list/blogpost-list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HotelListComponent } from './features/hotel/hotel-list/hotel-list.component';
import { AddHotelComponent } from './features/hotel/add-hotel/add-hotel.component';
import { EditHotelComponent } from './features/hotel/edit-hotel/edit-hotel.component';
import { AddBlogpostComponent } from './features/blog-post/add-blogpost/add-blogpost.component';

const routes: Routes = [
  {
    path: 'admin/hotels',
    component: HotelListComponent,
  },
  {
    path: 'admin/hotels/add',
    component: AddHotelComponent,
  },
  {
    path: 'admin/hotels/:id',
    component: EditHotelComponent,
  },
  {
    path: 'admin/blogposts',
    component: BlogpostListComponent,
  },
  {
    path: 'admin/blogposts/add',
    component: AddBlogpostComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
