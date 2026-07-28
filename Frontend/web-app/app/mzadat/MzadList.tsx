"use client";
import { Mzad, PagedResult } from "@/types";
import React, { useEffect, useState } from "react";
import MzadCard from "./MzadCard";
import AppPagination from "../components/AppPagination";
import { getData } from "../actions/MzadActions";
import Filters from "./Filters";
import { Spinner } from "flowbite-react";
import { useParamsStore } from "@/hooks/useParamsStore";
import queryString from "query-string";
import EmptyFilter from "../components/EmptyFilter";
import { useMzadStore } from "@/hooks/useMzadStore";

export default function MzadList() {
  const [loading, setLoading] = useState(true);
  const params = useParamsStore((state) => ({
    pageNumber: state.pageNumber,
    pageSize: state.pageSize,
    searchTerm: state.searchTerm,
    orderBy: state.orderBy,
    filterBy: state.filterBy,
    seller: state.seller,
    winner: state.winner
  }));
  const data = useMzadStore((state) => ({
    mzadat: state.mzadat,
    totalCount: state.totalCount,
    pageCount: state.pageCount
  }));
  const setData = useMzadStore((state) => state.setData);
  const setParams = useParamsStore((state) => state.setParams);
  const url = queryString.stringifyUrl({
    url: "",
    query: params
  });
  const setPageNumber = (pageNumber: number) => setParams({ pageNumber });
  useEffect(() => {
    getData(url).then((data) => {
      setData(data);
      setLoading(false);
    });
  }, [url, setData]);
  if (loading) {
    return (
      <div className="text-center">
        <Spinner size={"xl"} color="purple" className="mx-52 my-52" />
      </div>
    );
  }
  return (
    <>
      <Filters />
      {data.totalCount === 0 ? (
        <EmptyFilter showReset />
      ) : (
        <>
          <div className="grid lg:grid-cols-4 md:grid-cols-3 sm:grid-cols-2 gap-4">
            {data.mzadat.map((mzad) => (
              <MzadCard key={mzad.id} mzad={mzad} />
            ))}
          </div>
          <div className="flex justify-center mt-2">
            <AppPagination
              currentPage={params.pageNumber}
              pageCount={data.pageCount}
              pageChanged={setPageNumber}
            />
          </div>
        </>
      )}
    </>
  );
}
