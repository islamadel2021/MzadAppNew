import { Mzad } from "@/types";
import Link from "next/link";
import React from "react";
import CountdownTimer from "./CountdownTimer";
import MzadImage from "./MzadImage";
import CurrentPrice from "./CurrentPrice";
type Props = {
  mzad: Mzad;
};
export default function MzadCard({ mzad }: Props) {
  return (
    <Link href={`/mzadat/details/${mzad.id}`} className="group">
      <div className="w-full bg-gray-200 aspect-w-16 aspect-h-10 rounded-lg overflow-hidden">
        <div>
          <MzadImage imageUrl={mzad.imageUrl} />
          <div className="absolute bottom-1 left-1">
            <CountdownTimer mzadEnd={mzad.mzadEnd} />
          </div>
          <div className="absolute top-1 right-1">
            <CurrentPrice
              amount={mzad.currentHighTender}
              reservePrice={mzad.reservePrice}
            />
          </div>
        </div>
      </div>
      <div className="flex justify-between items-center m-1 text-lg font-extrabold text-gray-600">
        <h3>{mzad.name}</h3>
        <p>{mzad.yearOfBirth}</p>
      </div>
    </Link>
  );
}
