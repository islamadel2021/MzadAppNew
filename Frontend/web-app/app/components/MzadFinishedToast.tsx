import { Mzad, MzadFinished } from "@/types";
import Image from "next/image";
import Link from "next/link";
import React from "react";

type Props = {
  mzadFinished: MzadFinished;
  mzad: Mzad;
};

export default function MzadFinishedToast({ mzadFinished, mzad }: Props) {
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
        <div className="flex flex-col">
          <span>Mzad for {mzad.name} has finished</span>
          {mzadFinished.horseSold && mzadFinished.amount ? (
            <p>
              Congrats to {mzadFinished.winner} who has won this mzad for{" "}
              {mzadFinished.amount} EGP
            </p>
          ) : (
            <p>This horse has not sold</p>
          )}
        </div>
      </div>
    </Link>
  );
}
