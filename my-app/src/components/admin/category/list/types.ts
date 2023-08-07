export interface ICategoryItem {
  id: number;
  title: string;
  image: string;
}

export interface ICategoryGetResult {
  categories: ICategoryItem[];
  currentPage: number;
  pages: number;
  total: number;
}
