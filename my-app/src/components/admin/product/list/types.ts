import { ICategoryItem } from "../../category/list/types";

export interface IProductItem {
  id: number;
  title: string;
  details: string;
  image: string;
  category: ICategoryItem;
  updatedAt: string;
  createdAt: string;
  price: number;
}

export interface IProductGetResult {
  products: IProductItem[];
  currentPage: number;
  pages: number;
  total: number;
}
