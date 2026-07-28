import React from "react";

type Props = {
  amount?: number;
  reservePrice: number;
};

export default function CurrentPrice({ amount, reservePrice }: Props) {
  const text = amount ? amount + " EGP" : "No Tenders";
  const color = amount
    ? amount > reservePrice
      ? "bg-green-600"
      : "bg-amber-600"
    : "bg-red-600";

  return (
    <div
      className={`text-white py-1 px-2 rounded flex justify-center ${color}`}
    >
      {text}
    </div>
  );
}
