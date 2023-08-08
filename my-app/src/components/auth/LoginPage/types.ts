export interface ILoginRequest {
  email: string;
  password: string;
}

export interface ILoginRequestError {
  email: string;
  password: string;
  error: string;
}

export interface ILoginResponce {
  token: string;
  firstName: string;
}
