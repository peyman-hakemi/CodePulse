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

  getBlogPostById(id: string): Observable<BlogPost> {
    return this.http.get<BlogPost>(
      `${environment.API_BASE_URL}/api/BlogPosts/${id}`
    );
  }

  editBlogPost(id: string, blogPost: BlogPost): Observable<BlogPost> {
    return this.http.put<BlogPost>(
      `${environment.API_BASE_URL}.api/BlogPosts/${id}`,
      blogPost
    );
  }
}
