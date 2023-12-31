import classNames from "classnames";
import { ChangeEvent, FormEvent, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  ICategoryCreate,
  ICategoryCreateErrorResponce,
  ICategoryCreateErrror,
} from "./types";
import ReactLoading from "react-loading";
import { APP_ENV } from "../../../../env";
import { http_common } from "../../../../services/tokenService";
import CropperDialog from "../../../common/CropperDialog";
import { AxiosError } from "axios";
const CategoryCreatePage = () => {
  const navigator = useNavigate();
  const [isProcessing, setIsProcessing] = useState<boolean>(false);
  const [dto, setDto] = useState<ICategoryCreate>({
    title: "",
    details: "",
    image: null,
  });

  const [errors, setErrors] = useState<ICategoryCreateErrror>({
    title: undefined,
    details: undefined,
    image: undefined,
  });

  const onChangeHandler = (e: ChangeEvent<HTMLInputElement>) => {
    setDto({ ...dto, [e.target.title]: e.target.value });
  };

  const onSubmitHandler = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setErrors({ title: undefined, details: undefined, image: undefined });

    try {
      setIsProcessing(true);
      await http_common.post(`${APP_ENV.BASE_URL}api/category/create`, dto, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      setIsProcessing(false);
      navigator("..");
    } catch (er: any) {
      const error = er as AxiosError;

      console.log(error);
      setIsProcessing(false);

      if (error.code == "ERR_NETWORK") {
        console.log("network error", error);
      }
      if (error.code == "ERR_BAD_REQUEST") {
        const responce = er.response.data as ICategoryCreateErrorResponce;
        var errs: ICategoryCreateErrror = {
          title: responce.errors.Title,
          details: responce.errors.Details,
          image: responce.errors.Image,
        };
        setErrors(errs);
      }
    }
  };
  const onImageChangeHandler = (f: File) => {
    console.log("image input handle change", f);
    if (f != null) {
      onImageSaveHandler(f);
    }
  };
  const onImageSaveHandler = (file: File) => {
    console.log("image save handle", file);
    setDto({ ...dto, image: file });
  };

  console.log("Server error ", errors);
  console.log("status", errors.title != undefined);

  return (
    <>
      <h1 className="text-center">Створити категорію</h1>
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
        <form className="col-md-6 offset-md-3" onSubmit={onSubmitHandler}>
          <div className="mb-3">
            <label htmlFor="title" className="form-label">
              Наза
            </label>
            <input
              type="text"
              className={classNames("form-control", {
                "is-invalid":
                  errors.title != undefined && errors.title?.length > 0,
              })}
              id="title"
              title="title"
              value={dto.title}
              onChange={onChangeHandler}
            />
            {errors.title != undefined && errors.title?.length > 0 && (
              <div className="invalid-feedback">
                {errors.title.join() as string}
              </div>
            )}
          </div>
          <div className="mb-3">
            <label htmlFor="details" className="form-label">
              Опис
            </label>
            <input
              type="text"
              id="details"
              className={classNames("form-control", {
                "is-invalid": errors.details,
              })}
              title="details"
              value={dto.details}
              onChange={onChangeHandler}
            />
            {errors.details && (
              <div className="invalid-feedback">{errors.details}</div>
            )}
          </div>
          <div className="mb-3">
            <label htmlFor="image" className="form-label">
              Image
            </label>

            <CropperDialog
              onSave={onImageChangeHandler}
              error={errors.image?.join() as string}
            ></CropperDialog>
          </div>
          <button type="submit" className="btn btn-primary">
            Додати
          </button>
        </form>
      )}
    </>
  );
};
export default CategoryCreatePage;
