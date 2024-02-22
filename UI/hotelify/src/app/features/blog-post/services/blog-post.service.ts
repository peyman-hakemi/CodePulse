import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';

@Injectable({
  providedIn: 'root',
})
export class BlogPostService {
  constructor(private http: HttpClient) {}

  createBlogPost(blogPost: AddBlogPost): Observable<BlogPost> {
    return this.http.post<BlogPost>(
      `${environment.API_BASE_URL}/api/BlogPosts`,
      blogPost
    );
  }

  getAllBlogPosts(): Observable<BlogPost[]> {
    return this.http.get<BlogPost[]>(
      `${environment.API_BASE_URL}/api/BlogPosts`
    );
  }
}
