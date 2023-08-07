import { SyntheticEvent } from "react";

export interface ICategoryCreate {
  title: string;
  image: File | null;
  details: string;
}

export interface ICategoryCreateErrror {
  title: string;
  details: string;
  image: string;
}
