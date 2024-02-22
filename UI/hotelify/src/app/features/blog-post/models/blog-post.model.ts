import { Hotel } from "../../hotel/models/hotel.model";

export interface BlogPost {
  title: string;
  shortDescription: string;
  content: string;
  featuredImageUrl: string;
  urlHandle: string;
  author: string;
  publishDate: Date;
  isVisible: boolean;
  id: string;
  hotels: Hotel[]
}
