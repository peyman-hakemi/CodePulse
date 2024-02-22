import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogPostService } from '../services/blog-post.service';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { HotelService } from '../../hotel/services/hotel.service';
import { Hotel } from '../../hotel/models/hotel.model';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css'],
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  routeSubscription?: Subscription;
  model?: BlogPost;
  hotels$?: Observable<Hotel[]>;
  selectedHotels?: string[];

  constructor(
    private blogPostService: BlogPostService,
    private route: ActivatedRoute,
    private hotelService: HotelService
  ) {}

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.hotels$ = this.hotelService.getAllHotels();

    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id) {
          this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response) => {
              this.model = response;
              this.selectedHotels = response.hotels.map((h) => h.id);
            },
          });
        }
      },
    });
  }

  onFormSubmit() {
    // const blogPost: BlogPost = {...this.model, hotels: this.selectedHotels}
    // this.blogPostService.editBlogPost(this.id, {...})
  }
}
