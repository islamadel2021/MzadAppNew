import { getDetails, getTendersForMzad } from "@/app/actions/MzadActions";
import Heading from "@/app/components/Heading";
import React from "react";
import CountdownTimer from "../../CountdownTimer";
import MzadImage from "../../MzadImage";
import DetailedSpecs from "./DetailedSpecs";
import { getCurrentUser } from "@/app/actions/authActions";
import UpdateButton from "./UpdateButton";
import DeleteButton from "./DeleteButton";
import TenderItem from "./TenderItem";
import TendersList from "./TendersList";
import { User } from "next-auth";

export default async function Details({ params }: { params: { id: string } }) {
  const mzad = await getDetails(params.id);
  const user = await getCurrentUser();
  return (
    <div>
      <div className="flex justify-between">
        <div className="flex items-center gap-3">
          <Heading title={`${mzad.name}`} />
          {user?.username === mzad.seller.toLowerCase() && (
            <>
              <UpdateButton id={params.id} />
              <DeleteButton id={params.id} />
            </>
          )}
        </div>
        <div className="flex gap-3">
          <h3 className="text-2xl font-semibold">Time remaining:</h3>
          <CountdownTimer mzadEnd={mzad.mzadEnd} />
        </div>
      </div>

      <div className="grid grid-cols-2 gap-6 mt-3">
        <div className="w-full bg-gray-200 aspect-h-10 aspect-w-16 rounded-lg overflow-hidden">
          <MzadImage imageUrl={mzad.imageUrl} />
        </div>
        <TendersList user={user as User} mzad={mzad} />
      </div>
      <div className="mt-3 grid grid-cols-1 rounded-lg">
        <DetailedSpecs mzad={mzad} />
      </div>
    </div>
  );
}
