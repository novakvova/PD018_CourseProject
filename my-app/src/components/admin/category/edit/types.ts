export interface ICategoryEdit {
  id: number;
  title: string;
  image: File | null | string;
  details: string;
}

export interface ICategoryEditErrror {
  title: string;
  details: string;
  image: string;
}
