"use client";
import { useParamsStore } from "@/hooks/useParamsStore";
import React, { useState } from "react";
import { FaSearch } from "react-icons/fa";

export default function Search() {
  const setSearchValue = useParamsStore((state) => state.setSearchValue);
  const searchValue = useParamsStore((state) => state.searchValue);
  const setParams = useParamsStore((state) => state.setParams);
  const onChange = (e: any) => setSearchValue(e.target.value);
  const search = () => setParams({ searchTerm: searchValue });
  return (
    <div className="flex w-[40%] items-center border-1 rounded-full py-1 -ml-20 shadow-sm bg-white">
      <input
        value={searchValue}
        onChange={onChange}
        onKeyDown={(e: any) => {
          if (e.key === "Enter") {
            search();
          }
        }}
        type="text"
        placeholder="Search for Mzadat by seller, breed or color"
        className="input-style"
      />
      <button onClick={search}>
        <FaSearch
          size={40}
          className="bg-purple-600 text-white rounded-full p-2 cursor-pointer mx-2"
        />
      </button>
    </div>
  );
}
