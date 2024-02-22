import { Component, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { HotelService } from '../../hotel/services/hotel.service';
import { Hotel } from '../../hotel/models/hotel.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css'],
})
export class AddBlogpostComponent implements OnInit {
  model: AddBlogPost;
  hotels$?: Observable<Hotel[]>;

  constructor(
    private blogPostService: BlogPostService,
    private router: Router,
    private hotelService: HotelService
  ) {
    this.model = {
      title: '',
      shortDescription: '',
      urlHandle: '',
      content: '',
      featuredImageUrl: '',
      author: '',
      isVisible: true,
      publishDate: new Date(),
      hotels: [],
    };
  }
  ngOnInit(): void {
    this.hotels$ = this.hotelService.getAllHotels();
  }

  onFormSubmit(): void {
    this.blogPostService.createBlogPost(this.model).subscribe({
      next: (response) => {
        console.log(response);
        this.router.navigateByUrl('/admin/blogposts');
      },
    });
  }
}
