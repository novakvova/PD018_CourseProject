import { SyntheticEvent } from "react";

export interface IProductCreate {
  category_id: number | undefined;
  title: string;
  price: number;
  details: string;
  images: any;
}

export interface IProductCreateError {
  category_id: string[];
  Title: string[];
  Price: string[];
  Details: string[];
  Images: File[];
}
