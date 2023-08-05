import classNames from "classnames";
import { ChangeEvent, FormEvent, useEffect, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { ICategoryEdit, ICategoryEditErrror } from "./types";
import { ICategoryItem } from "../list/types";
import Cropper from "cropperjs";
import "cropperjs/dist/cropper.min.css";
import ReactLoading from "react-loading";
import { APP_ENV } from "../../../../env";
import { http_common } from "../../../../services/tokenService";
import CropperDialog from "../../../common/CropperDialog";

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
    title: "",
    details: "",
    image: "",
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
    setEditCategory({ ...editCategory, [e.target.title]: e.target.value });
    setToSendCategory({ ...toSendCategory, [e.target.title]: e.target.value });
  };

  const onSubmitHandler = (e: FormEvent<HTMLFormElement>) => {
    console.log("CategoryEditPAge onSUbmitHAndler");
    e.preventDefault();
    setIsLoading(true);
    setErrors({ title: "", details: "", image: "" });
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
        const errors = er.response.data as ICategoryEditErrror;
        setErrors(errors);
        console.log("Server update error ", errors);
        setIsLoading(false);
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
              className={classNames("form-control", {
                "is-invalid": errors.title,
              })}
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
                "is-invalid": errors.title,
              })}
              id="title"
              name="title"
              value={editCategory.title}
              onChange={onChangeHandler}
            />
            {errors.title && (
              <div className="invalid-feedback">{errors.title}</div>
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
              name="details"
              value={editCategory.details}
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
              imageUri={editCategory.image as string}
              onSave={onImageChangeHandler}
              error={errors.image}
            ></CropperDialog>
          </div>
          <button type="submit" className="btn btn-danger">
            Save edit
          </button>
        </form>
      )}
    </>
  );
};
export default CategoryEditPage;
