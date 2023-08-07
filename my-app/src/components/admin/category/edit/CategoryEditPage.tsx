import classNames from "classnames";
import { ChangeEvent, FormEvent, useEffect, useRef, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import {
  ICategoryEdit,
  ICategoryEditErrorResponce,
  ICategoryEditErrror,
} from "./types";
import { ICategoryItem } from "../list/types";
import Cropper from "cropperjs";
import "cropperjs/dist/cropper.min.css";
import ReactLoading from "react-loading";
import { APP_ENV } from "../../../../env";
import { http_common } from "../../../../services/tokenService";
import CropperDialog from "../../../common/CropperDialog";
import { AxiosError } from "axios";

const CategoryEditPage = () => {
  const navigator = useNavigate();

  const { id } = useParams();

  const [isLoading, setIsLoading] = useState<boolean>(false);

  const [editCategory, setEditCategory] = useState<ICategoryEdit>({
    id: -1,
    title: "",
    details: "",
    image: null,
  });

  const [toSendCategory, setToSendCategory] = useState<any>({});

  const [errors, setErrors] = useState<ICategoryEditErrror>({
    title: undefined,
    details: undefined,
    image: undefined,
  });

  useEffect(() => {
    setIsLoading(true);
    http_common
      .get<ICategoryItem>(`${APP_ENV.BASE_URL}api/category/get/${id}`)
      .then((resp: any) => {
        let initCategory = resp.data;
        console.log("Сервак дав 1 category", initCategory);
        setIsLoading(false);
        setEditCategory({
          id: initCategory.id,
          title: initCategory.title,
          details: initCategory.details,
          image:
            APP_ENV.BASE_URL + "api/Files/Get/" + initCategory.image + "/1200",
        });
      })
      .catch((e: any) => {
        setIsLoading(false);
        console.log("get category by id error: ", e);
      });

    console.log("use Effect working");
  }, []);

  const onChangeHandler = (e: ChangeEvent<HTMLInputElement>) => {
    setEditCategory({ ...editCategory, [e.target.name]: e.target.value });
    setToSendCategory({ ...toSendCategory, [e.target.name]: e.target.value });
  };

  const onSubmitHandler = (e: FormEvent<HTMLFormElement>) => {
    console.log("CategoryEditPAge onSUbmitHAndler");
    e.preventDefault();
    setIsLoading(true);
    setErrors({ title: undefined, details: undefined, image: undefined });
    toSendCategory.categoryId = id;
    http_common
      .patch(`${APP_ENV.BASE_URL}api/category/update`, toSendCategory, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      .then((resp) => {
        setIsLoading(false);
        navigator("../..");
      })
      .catch((er: any) => {
        const error = er as AxiosError;

        console.log(error);
        setIsLoading(false);
        if (error.code == "ERR_NETWORK") {
          console.log("network error", error);
        }
        if (error.code == "ERR_BAD_REQUEST") {
          const responce = er.response.data as ICategoryEditErrorResponce;
          var errs: ICategoryEditErrror = {
            title: responce.errors.Title,
            details: responce.errors.Details,
            image: responce.errors.ImageContent,
          };
          setErrors(errs);
        }
      });
    //console.log("Submit data", dto);
  };

  const onImageChangeHandler = (f: File) => {
    console.log("image input handle change", f);
    if (f != null) {
      onImageSaveHandler(f);
    }
  };
  const onImageSaveHandler = (file: File) => {
    console.log("image save handle", file);
    setEditCategory({ ...editCategory, image: file });
    setToSendCategory({ ...toSendCategory, image: file });
  };
  return (
    <>
      <h1 className="text-center">Edit категорію {id}</h1>
      {isLoading && (
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
      {!isLoading && (
        <form
          className={classNames("col-md-6 offset-md-3")}
          onSubmit={onSubmitHandler}
        >
          <div className="mb-3">
            <label className="form-label">ID</label>
            <input
              disabled={true}
              type="text"
              className={classNames("form-control", {})}
              value={editCategory.id}
            />
          </div>
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
              name="title"
              value={editCategory.title}
              onChange={onChangeHandler}
            />
            {errors.title != undefined && errors.title?.length > 0 && (
              <div className="invalid-feedback">{errors.title.join()}</div>
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
                "is-invalid":
                  errors.details != undefined && errors.details?.length > 0,
              })}
              name="details"
              value={editCategory.details}
              onChange={onChangeHandler}
            />
            {errors.details != undefined && errors.details?.length > 0 && (
              <div className="invalid-feedback">{errors.details.join()}</div>
            )}
          </div>
          <div className="mb-3">
            <label htmlFor="image" className="form-label">
              Image
            </label>
            <CropperDialog
              imageUri={editCategory.image as string}
              onSave={onImageChangeHandler}
              error={errors.image?.join() as string}
            ></CropperDialog>
          </div>
          <button type="submit" className="btn btn-danger me-1">
            Save edit
          </button>
          <Link to={"../.."} className="btn btn-secondary">
            Cancel
          </Link>
        </form>
      )}
    </>
  );
};
export default CategoryEditPage;
