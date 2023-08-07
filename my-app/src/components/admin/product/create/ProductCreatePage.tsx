import classNames from "classnames";
import { LegacyRef, useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { IProductCreate, IProductCreateError } from "./types";
import ReactLoading from "react-loading";
import { APP_ENV } from "../../../../env";
import { http_common } from "../../../../services/tokenService";
import * as yup from "yup";
import { useFormik } from "formik";
import { AxiosError } from "axios";
import { ICategoryGetResult, ICategoryItem } from "../../category/list/types";
import { IProductItem } from "../list/types";
import { config } from "process";
const ProductCreatePage = () => {
  const navigator = useNavigate();
  const fileSelectInputRef = useRef<HTMLInputElement>();

  const [isProcessing, setIsProcessing] = useState<boolean>(false);
  const [imageError, setImageError] = useState<string>();
  const [initValues, setInitValues] = useState<IProductCreate>({
    title: "",
    details: "",
    category_id: undefined,
    price: 0,
    images: [],
  });
  const [images, setImages] = useState<File[]>([]);
  const [imagesUrl, setImagesUrl] = useState<string[]>([]);
  const [categories, setCategories] = useState<ICategoryItem[]>([]);

  const productCreateSchema = yup.object({
    name: yup.string().required("Enter name"),
    description: yup.string().required("Enter description"), // todo add regex validation
    category_id: yup.number().required("Enter category"),
    price: yup.number().required("Enter price"),
  });

  const loadMoreCategoriesAsync = async () => {
    const result = await http_common.get<ICategoryGetResult>(
      `${APP_ENV.BASE_URL}api/category/search?page=${1}&pageSize=100`
    );
    setCategories(result.data.categories);
  };

  useEffect(() => {
    setIsProcessing(true);
    const fetchData = async () => {
      try {
        console.log("create tedas");

        await loadMoreCategoriesAsync();
      } catch (error) {
        setIsProcessing(false);
        console.log("get categories list error: ", error);
      }
    };
    fetchData();
    setIsProcessing(false);
  }, []);

  const [responceError, setResponceError] = useState<IProductCreateError>();

  const onSubmitFormikData = async (values: IProductCreate) => {
    try {
      var formData = new FormData();
      formData.append("title", values.title);
      formData.append("details", values.details);
      formData.append("price", values.price.toString());
      formData.append("categoryId", values.category_id?.toString() as string);
      images.forEach((i) => {
        formData.append("images", i);
      });
      var resp = await http_common.post(
        `${APP_ENV.BASE_URL}api/product/create`,
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );

      var created = resp.data as IProductItem;
      console.log("resp = ", created);

      // const formData = new FormData();
      // images.forEach((i) => {
      //   formData.append("images[]", i);
      // });
      // var uploadImagesResult = await http_common.post(
      //   `api/product/${created.id}/images`,
      //   formData,
      //   {
      //     headers: {
      //       "Content-Type": "multipart/form-data",
      //     },
      //   }
      // );
      // console.log("images upload result:", uploadImagesResult);

      navigator("..");
      await setIsProcessing(false);
    } catch (e: any) {
      const axiosError = e as AxiosError;
      const error = axiosError.response?.data as IProductCreateError;
      console.log("product create server error", error);
      setResponceError(error);
      errors.category_id = error.category_id?.join(", ");
      errors.details = error.Details?.join(", ");
      errors.title = error.Title?.join(", ");
      errors.price = error.Price?.join(", ");
      await setIsProcessing(false);
    }
  };
  const formik = useFormik({
    initialValues: initValues,
    // validationSchema: productCreateSchema,
    onSubmit: onSubmitFormikData,
  });
  const { values, errors, touched, handleSubmit, handleChange } = formik;

  // images
  const onImageChangeHandler = async (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    const files = e.target.files;
    if (!files || !files.length) {
      return;
    }

    const file = files[0];
    if (!/^image\/\w+/.test(file.type)) {
      setImageError("Select correct image!");
      return;
    }
    const url = URL.createObjectURL(file);
    setImages([...images, file]);
    setImagesUrl([...imagesUrl, url]);
    console.log("images:", images, imagesUrl);
  };
  const onAddImageClick = async () => {
    await fileSelectInputRef.current?.click();
  };
  const removeImage = (index: number) => {
    const updatedImages = [...images];
    updatedImages.splice(index, 1);
    setImages(updatedImages);

    const updatedImagesUrl = [...imagesUrl];
    updatedImagesUrl.splice(index, 1);
    setImagesUrl(updatedImagesUrl);
  };

  // view data
  const categoriesData = categories?.map((l) => (
    <option value={l.id} key={Math.random()}>
      {l.title}
    </option>
  ));
  const imagesPreviewData = imagesUrl?.map((url, index) => (
    <div className="img" key={url}>
      <button
        type="button"
        className="btn-close position-relative z-index-100 top-0 start-100"
        aria-label="Remove image"
        title="Remove image"
        onClick={() => removeImage(index)}
      ></button>

      <div className="card m-2" style={{ width: "14rem", height: "14rem" }}>
        <img src={url} className="card-img-top" alt={"image"}></img>
      </div>
    </div>
  ));

  return (
    <>
      <h1 className="text-center">Створити товар</h1>
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
        <form className="col-md-6 offset-md-3" onSubmit={handleSubmit}>
          <div className="mb-3">
            <label htmlFor="title" className="form-label">
              Назва
            </label>
            <input
              type="text"
              className={classNames("form-control", {
                "is-invalid": errors.title,
              })}
              id="title"
              name="title"
              value={values.title}
              onChange={handleChange}
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
              value={values.details}
              onChange={handleChange}
            />
            {errors.details && (
              <div className="invalid-feedback">{errors.details}</div>
            )}
          </div>
          <div className="mb-3">
            <label htmlFor="price" className="form-label">
              Ціна
            </label>
            <input
              type="number"
              className={classNames("form-control", {
                "is-invalid": errors.price,
              })}
              id="price"
              name="price"
              value={values.price}
              onChange={handleChange}
            />
            {errors.price && (
              <div className="invalid-feedback">{errors.price}</div>
            )}
          </div>
          <div className="mb-3">
            <label htmlFor="category_id" className="form-label">
              Категорія
            </label>
            <select
              required={true}
              onChange={handleChange}
              value={values.category_id}
              id="category_id"
              name="category_id"
              className="form-select"
              aria-label="Default select example"
            >
              <option disabled={true} defaultChecked={true}>
                Виберіть категорію...
              </option>
              {categoriesData}
            </select>
            {errors.category_id && (
              <div className="invalid-feedback">{errors.category_id}</div>
            )}
          </div>
          <div className="mb-3">
            <div className="form-control">
              <label htmlFor="name" className="form-label">
                Картинки
              </label>
              <button
                type="button"
                onClick={onAddImageClick}
                className="btn btn-secondary"
              >
                Додати картинку
              </button>
              {/* hidden file input */}
              <input
                type="file"
                accept="image/*"
                className={classNames("form-control d-none")}
                id="image"
                name="image"
                ref={fileSelectInputRef as LegacyRef<HTMLInputElement>}
                onChange={onImageChangeHandler}
              />
              <div className="container">
                <div className="d-flex flex-wrap">{imagesPreviewData}</div>
              </div>
              {imageError && (
                <div className="invalid-feedback">{imageError}</div>
              )}
            </div>
          </div>
          <button type="submit" className="btn btn-success">
            Додати
          </button>
        </form>
      )}
    </>
  );
};
export default ProductCreatePage;
