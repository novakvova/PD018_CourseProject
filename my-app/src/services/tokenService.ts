import { Navigate, useNavigate } from "react-router-dom";
import axios, { AxiosError } from "axios";
import jwt from "jsonwebtoken";
import jwtDecode from "jwt-decode";
import { APP_ENV } from "../env";
import { useDispatch } from "react-redux";
import { AuthUserActionType, IUser } from "../components/auth/types";
import { error } from "console";

export const storeToken = (token: string) => {
  console.log("store token");
  localStorage.setItem("token", `Bearer ${token}`);
  http_common.defaults.headers["Authorization"] = getToken();
};

export const getToken = () => {
  return localStorage.getItem("token");
};

export const removeToken = () => {
  delete http_common.defaults.headers["Authorization"];
  return localStorage.removeItem("token");
};

export const decodeToken = (token: string) => {
  return jwtDecode(token);
  // return jwt.decode(token);
};

export const isAdmin = (user: IUser | undefined): boolean => {
  return (
    user != null &&
    user.roles != null &&
    (user?.roles as string[]).find((r) => r == "Administrator") != null
  );
};

export const isSignedIn = (): boolean => {
  let t = getToken();
  // todo add overdue check
  return t != null && t != "" && t != undefined;
};

export var http_common = axios.create({
  baseURL: APP_ENV.BASE_URL,
  headers: {
    Authorization: `${getToken()}`,
  },
});

http_common.interceptors.response.use(
  (responce) => responce,
  (error: AxiosError) => {
    console.log("catched", error);
    if (error.code == "ERR_NETWORK") {
      window.location.href = "/auth/login";
    }
    return Promise.reject(error);
  }
);
