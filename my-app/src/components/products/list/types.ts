import { ICategoryItem } from "../../admin/category/list/types";

export interface IProductImageItem {
  name: string;
  priority: number;
}

export interface IProductItem {
  id: number;
  images: string[];
  title: string;
  category: ICategoryItem;
  price: number;
}

export interface IProductGetResult {
  products: IProductItem[];
  currentPage: number;
  pages: number;
  total: number;
}
