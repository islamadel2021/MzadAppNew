import { Tender } from "@/types";
import { format } from "date-fns";
import React from "react";
import titleize from "titleize";

type Props = {
  tender: Tender;
};

export default function TenderItem({ tender }: Props) {
  function getTenderInfo() {
    let bgColor = "";
    let text = "";
    switch (tender.tenderStatus) {
      case "Accepted":
        bgColor = "bg-green-300";
        text = "Tender accepted";
        break;
      case "AcceptedBelowReserve":
        bgColor = "bg-amber-300";
        text = "Reserve not met";
        break;
      case "TooLow":
        bgColor = "bg-red-300";
        text = "Tender was too low";
        break;
      default:
        bgColor = "bg-red-200";
        text = "Tender not Accepted";
        break;
    }
    return { bgColor, text };
  }

  return (
    <div
      className={`
            border-gray-300 border-2 px-3 py-2 rounded-lg
            flex justify-between items-center mb-2
            ${getTenderInfo().bgColor}
        `}
    >
      <div className="flex flex-col">
        <span>Tender Owner: {titleize(tender.tenderOwner)}</span>
        <span className="text-gray-700 text-sm">
          Time: {format(new Date(tender.tenderTime), "dd MMM yyyy h:mm a")}
        </span>
      </div>
      <div className="flex flex-col text-right">
        <div className="text-xl font-semibold">{tender.amount} EGP</div>
        <div className="flex flex-row items-center">
          <span>{getTenderInfo().text}</span>
        </div>
      </div>
    </div>
  );
}
