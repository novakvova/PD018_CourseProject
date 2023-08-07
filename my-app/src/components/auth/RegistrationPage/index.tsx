import classNames from "classnames";
import { ChangeEvent, useEffect, useState } from "react";
import ReactLoading from "react-loading";
import { useNavigate } from "react-router-dom";
import { APP_ENV } from "../../../env";
import {
  http_common,
  isSignedIn,
  storeToken,
} from "../../../services/tokenService";
import {
  IRegistrationRequest,
  IRegistrationRequestError,
  IRegistrationResponce,
} from "./types";
import CropperDialog from "../../common/CropperDialog";
import * as yup from "yup";
import { useFormik } from "formik";

const RegistrationPage = () => {
  const navigator = useNavigate();

  useEffect(() => {
    if (isSignedIn() == true) {
      navigator("/");
    }
  });

  const [isProcessing, setIsProcessing] = useState<boolean>(false);

  const initValues: IRegistrationRequest = {
    email: "",
    password: "",
    firstName: "",
    lastName: "",
  };

  const [responceError, setResponceError] = useState<string>();

  const registrationSchema = yup.object({
    email: yup.string().required("Enter email").email("Пошта вказана не вірно"),
    password: yup.string().required("Enter password"),
    firstName: yup.string().required("Enter first name"),
    lastName: yup.string().required("Enter last name"),
  });

  const onSubmitFormikData = async (values: IRegistrationRequest) => {
    try {
      await setIsProcessing(true);
      var resp = await http_common.post(`api/auth/signup`, values, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      var respData = resp.data as IRegistrationResponce;
      console.log("resp = ", respData);
      navigator("../login");
      await setIsProcessing(false);
    } catch (e: any) {
      setResponceError(e);
      await setIsProcessing(false);
    }
  };
  const formik = useFormik({
    initialValues: initValues,
    validationSchema: registrationSchema,
    onSubmit: onSubmitFormikData,
  });

  const { values, errors, touched, handleSubmit, handleChange } = formik;

  return (
    <div className="d-flex justify-content-center">
      <div className="w-50">
        <h1 className="text-center">Registration</h1>
        {isProcessing && (
          <div className="">
            <div className="row">
              <div className="col"></div>
              <div className="col">
                <div className="d-flex justify-content-center">
                  <ReactLoading
                    type="bars"
                    color="gray"
                    height={"50%"}
                    width={"50%"}
                  ></ReactLoading>
                </div>
              </div>
              <div className="col"></div>
            </div>
          </div>
        )}
        {!isProcessing && (
          <form onSubmit={handleSubmit}>
            <div className="wrapper">
              <div className="mb-3">
                <label htmlFor="firstName" className="form-label">
                  FirstName
                </label>
                <input
                  type="text"
                  className={classNames("form-control", {
                    "is-invalid": errors.firstName && touched.firstName,
                  })}
                  id="firstName"
                  name="firstName"
                  value={values.firstName}
                  onChange={handleChange}
                />
                {errors.firstName && (
                  <div className="invalid-feedback">{errors.firstName}</div>
                )}
              </div>
              <div className="mb-3">
                <label htmlFor="lastName" className="form-label">
                  LastName
                </label>
                <input
                  type="text"
                  className={classNames("form-control", {
                    "is-invalid": errors.lastName && touched.lastName,
                  })}
                  id="lastName"
                  name="lastName"
                  value={values.lastName}
                  onChange={handleChange}
                />
                {errors.lastName && (
                  <div className="invalid-feedback">{errors.lastName}</div>
                )}
              </div>
              <div className="mb-3">
                <label htmlFor="email" className="form-label">
                  Email
                </label>
                <input
                  type="email"
                  className={classNames("form-control", {
                    "is-invalid": errors.email && touched.email,
                  })}
                  id="email"
                  name="email"
                  value={values.email}
                  onChange={handleChange}
                />
                {errors.email && (
                  <div className="invalid-feedback">{errors.email}</div>
                )}
              </div>
              <div className="mb-3">
                <label htmlFor="password" className="form-label">
                  Password
                </label>
                <input
                  type="password"
                  id="password"
                  className={classNames("form-control", {
                    "is-invalid": errors.password && touched.password,
                  })}
                  name="password"
                  value={values.password}
                  onChange={handleChange}
                />
                {errors.password && (
                  <div className="invalid-feedback">{errors.password}</div>
                )}
              </div>
              <div className="mb-3">
                <button type="submit" className="btn btn-primary w-100">
                  Sign up
                </button>
              </div>

              {responceError && (
                <div className="text-danger">{responceError}</div>
              )}
            </div>
          </form>
        )}
      </div>
    </div>
  );
};

export default RegistrationPage;
