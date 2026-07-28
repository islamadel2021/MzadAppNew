"use client";
import { getTendersForMzad } from "@/app/actions/MzadActions";
import Heading from "@/app/components/Heading";
import { useTenderStore } from "@/hooks/useTenderStore";
import { Mzad, Tender } from "@/types";
import { Spinner } from "flowbite-react";
import { User } from "next-auth";
import React, { useEffect, useState } from "react";
import toast from "react-hot-toast";
import TenderItem from "./TenderItem";
import EmptyFilter from "@/app/components/EmptyFilter";
import TenderForm from "./TenderForm";

type Props = {
  user: User;
  mzad: Mzad;
};
export default function TendersList({ user, mzad }: Props) {
  const [loading, setLoading] = useState(true);
  const tenders = useTenderStore((state) => state.tenders);
  const setTenders = useTenderStore((state) => state.setTenders);
  const open = useTenderStore((state) => state.open);
  const setOpen = useTenderStore((state) => state.setOpen);
  const openForTenders = new Date(mzad.mzadEnd) > new Date();
  const highTender = tenders.reduce(
    (prev, current) => (prev > current.amount ? prev : current.amount),
    0
  );
  useEffect(() => {
    getTendersForMzad(mzad.id)
      .then((res: any) => {
        if (res.error) throw res.error;
        setTenders(res as Tender[]);
      })
      .catch((err: any) => {
        toast.error(err.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, [mzad.id, setTenders, setLoading]);

  useEffect(() => {
    setOpen(openForTenders);
  }, [openForTenders, setOpen]);
  if (loading) {
    return (
      <div className="text-center">
        <Spinner size={"xl"} color="purple" className="mx-52 my-52" />
      </div>
    );
  }
  return (
    <div className="border-2 rounded-lg p-2 shadow-md">
      <div className="bg-white">
        <div className="sticky top-0 bg-white">
          <Heading
            title={
              tenders.length === 0
                ? "Tenders"
                : `Current high tender is ${highTender} EGP`
            }
          />
        </div>
      </div>
      <div className="overflow-auto h-[300px] flex flex-col-reverse px-2">
        {tenders.length === 0 ? (
          <EmptyFilter
            title="No tenders for this mzad"
            subtitle="You can place a tender here"
          />
        ) : (
          <>
            {tenders.map((tender) => (
              <TenderItem key={tender.id} tender={tender} />
            ))}
          </>
        )}
      </div>
      <div className="px-2 pb-2 text-gray-600">
        {!open ? (
          <div className="flex items-center justify-center p-2 text-lg font-semibold">
            This mzad is finished or closed for tenders
          </div>
        ) : !user ? (
          <div className="flex items-center justify-center p-2 text-lg font-semibold">
            Please login to place a tender
          </div>
        ) : user &&
          user.username.toLowerCase() === mzad.seller.toLowerCase() ? (
          <div className="flex items-center justify-center p-2 text-lg font-semibold">
            You cannot place a tender on your own mzad
          </div>
        ) : (
          <TenderForm mzadId={mzad.id} highTender={highTender} />
        )}
      </div>
    </div>
  );
}
