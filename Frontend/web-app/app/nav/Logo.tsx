"use client";
import { useParamsStore } from "@/hooks/useParamsStore";
import { usePathname, useRouter } from "next/navigation";
import React from "react";
import { LiaHorseHeadSolid } from "react-icons/lia";

export default function Logo() {
  const router = useRouter();
  const pathname = usePathname();
  const reset = useParamsStore((state) => state.reset);
  const doReset = () => {
    if (pathname !== "/") router.push("/");
    reset();
  };
  return (
    <div
      onClick={doReset}
      className="cursor-pointer flex items-center gap-2 text-2xl"
    >
      <LiaHorseHeadSolid size={40} />
      <div>Arabian Horses Mzad</div>
    </div>
  );
}
