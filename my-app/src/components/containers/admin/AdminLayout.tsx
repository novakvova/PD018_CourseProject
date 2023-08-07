import "./admin.scss";
import { Outlet, useLocation, useNavigate } from "react-router-dom";
import DefaultHeader from "./AdminHeader";
import { useEffect } from "react";
import { isAdmin, isSignedIn } from "../../../services/tokenService";
import { useSelector } from "react-redux";
import { IAuthUser } from "../../auth/types";
import AdminHeader from "./AdminHeader";
import AdminSidebar from "./AdminSidebar";

const AdminLayout = () => {
  const navigator = useNavigate();

  const { isAuth, user } = useSelector((store: any) => store.auth as IAuthUser);
  const location = useLocation();
  const currentRoute = location.pathname;

  useEffect(() => {
    console.log("AdminLayout useEffect");
    console.log("current route:", currentRoute);
    if (isAuth == false) {
      console.log("user is not authenticated to admin panel");
      navigator(`/auth/login?forwardTo=${currentRoute}`);
    } else if (isAdmin(user) == false) {
      console.log("user is not authorized to admin panel");
      navigator(`/`);
    } else {
      console.log("user can go to admin panel");
    }
  }, []);

  return (
    <>
      <AdminHeader />
      <div className="admin container">
        <div className="row">
          <AdminSidebar></AdminSidebar>
          <main className="col-sm-8 col-md-9 col-lg-10 px-4">
            <Outlet />
          </main>
        </div>
      </div>
    </>
  );
};
export default AdminLayout;
