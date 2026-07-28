import { Mzad } from "@/types";
import Image from "next/image";
import Link from "next/link";
import React from "react";

type Props = {
  mzad: Mzad;
};

export default function MzadCreatedToast({ mzad }: Props) {
  return (
    <Link
      href={`/mzadat/details/${mzad.id}`}
      className="flex flex-col items-center"
    >
      <div className="flex flex-row items-center gap-2">
        <Image
          src={mzad.imageUrl}
          alt="image"
          height={80}
          width={80}
          className="rounded-lg w-auto h-auto"
        />
        <span>
          New Mzad! {mzad.name} {mzad.yearOfBirth} has been added
        </span>
      </div>
    </Link>
  );
}
