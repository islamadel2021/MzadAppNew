"use client";
import { Pagination } from "flowbite-react";
import React, { useState } from "react";

type Props = {
  currentPage: number;
  pageCount: number;
  pageChanged: (pageNumber: number) => void;
};
export default function AppPagination({
  currentPage,
  pageCount,
  pageChanged,
}: Props) {
  return (
    <Pagination
      currentPage={currentPage}
      totalPages={pageCount}
      onPageChange={(e) => pageChanged(e)}
      showIcons
      layout="pagination"
      className="mb-2"
    />
  );
}
