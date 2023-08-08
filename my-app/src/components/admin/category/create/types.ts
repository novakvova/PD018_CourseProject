import { SyntheticEvent } from "react";

export interface ICategoryCreate {
  title: string;
  image: File | null;
  details: string;
}

export interface ICategoryCreateErrror {
  title: string[] | undefined;
  details: string[] | undefined;
  image: string[] | undefined;
}

export interface ICategoryCreateErrrorBackend {
  Title: string[] | undefined;
  Details: string[] | undefined;
  Image: string[] | undefined;
}

export interface ICategoryCreateErrorResponce {
  errors: ICategoryCreateErrrorBackend;
  status: number;
  title: string;
}
