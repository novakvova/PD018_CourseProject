export interface ICategoryEdit {
  id: number;
  title: string;
  image: File | null | string;
  details: string;
}

export interface ICategoryEditErrror {
  title: string[] | undefined;
  details: string[] | undefined;
  image: string[] | undefined;
}

export interface ICategoryEditErrrorBackend {
  Title: string[] | undefined;
  Details: string[] | undefined;
  ImageContent: string[] | undefined;
}

export interface ICategoryEditErrorResponce {
  errors: ICategoryEditErrrorBackend;
  status: number;
  title: string;
}
