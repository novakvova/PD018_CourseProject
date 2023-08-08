import { LegacyRef, useEffect, useRef, useState } from "react";
import { Link, Outlet, useNavigate, useParams } from "react-router-dom";
import { ICategoryGetResult, ICategoryItem } from "./types";
import classNames from "classnames";
import { randomUUID } from "crypto";
import ReactLoading from "react-loading";
import { APP_ENV } from "../../../../env";
import { http_common } from "../../../../services/tokenService";

const CategoryListPage = () => {
  const deleteDialog = useRef();
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [list, setList] = useState<ICategoryItem[]>([
    // {
    //     id: 1,
    //     name: "SSD",
    //     description: "Для швикдих людей"
    // }
  ]);
  const [data, setData] = useState<ICategoryGetResult>();

  const { page } = useParams();
  let localpage;

  useEffect(() => {
    // if user has defined page in url - use it. Either use first page
    if (page == undefined || page == null) localpage = 1;
    else localpage = page;

    console.log("try to get categories from server page " + localpage);
    setIsLoading(true);
    http_common
      .get<ICategoryGetResult>(
        `${APP_ENV.BASE_URL}api/category/Search?page=${localpage}`
      )
      .then((resp) => {
        setIsLoading(false);
        console.log("Сервак дав дані", resp);
        setList(resp.data.categories);
        setData(resp.data);
      })
      .catch((e) => {
        console.log("get categories from server error: ", e);
        setIsLoading(false);
      });

    console.log("use Effect end");
  }, [page]);

  console.log("Render component");

  var paginationData: React.JSX.Element[] = [];
  const totalPages = data?.pages as number;
  const currentPage = data?.currentPage as number;
  for (let page = 1; page <= totalPages; page++) {
    paginationData.push(
      <>
        <li
          key={Math.random()}
          className={classNames("page-item", {
            active: page == currentPage,
          })}
        >
          <Link to={`/admin/category/page/${page}`} className="page-link">
            {page}
          </Link>
        </li>
      </>
    );
  }

  console.log("paginationData", paginationData);

  const viewData = list.map((category) => (
    <tr key={category.id}>
      <td>{category.id}</td>
      <td>{category.title}</td>
      <td>
        <img
          src={APP_ENV.BASE_URL + "api/Files/Get/" + category.image + "/50"}
          width={50}
        />
      </td>
      <td>
        <Link
          to={`/admin/category/edit/${category.id}`}
          className="btn btn-primary m-1"
        >
          Edit
        </Link>
        <Link
          to={`/admin/category/delete/${category.id}`}
          className="btn btn-danger m-1"
        >
          Delete
        </Link>
      </td>
    </tr>
  ));
  //console.error("Сало");

  return (
    <>
      <h1 className="text-center">Список категорій</h1>
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
        <div className="onLoad">
          <Link to="/admin/category/create" className="btn btn-success">
            Додати
          </Link>
          <table className="table">
            <thead>
              <tr>
                <th scope="col">Id</th>
                <th scope="col" style={{ width: "20vw" }}>
                  Назва
                </th>
                <th scope="col">Фото</th>
                <th scope="col"></th>
              </tr>
            </thead>
            <tbody>{viewData}</tbody>
          </table>
          <ul className="pagination justify-content-center">
            {paginationData}
          </ul>
        </div>
      )}
    </>
  );
};

export default CategoryListPage;
