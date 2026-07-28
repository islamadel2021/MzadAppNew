"use client";
import { placeTenderForMzad } from "@/app/actions/MzadActions";
import { useTenderStore } from "@/hooks/useTenderStore";
import React from "react";
import { FieldValues, useForm } from "react-hook-form";
import { toast } from "react-hot-toast";

type Props = {
  mzadId: string;
  highTender: number;
};
export default function TenderForm({ mzadId, highTender }: Props) {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors }
  } = useForm();
  const addTender = useTenderStore((state) => state.addTender);

  function onSubmit(data: FieldValues) {
    if (data.amount <= highTender) {
      reset();
      return toast.error(`Tender must be at least ${highTender + 1} EGP`);
    }

    placeTenderForMzad(mzadId, +data.amount)
      .then((tender) => {
        if (tender.error) throw tender.error;
        addTender(tender);
        reset();
      })
      .catch((err) => toast.error(err.message));
  }

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="flex items-center border-2 rounded-lg py-2"
    >
      <input
        type="number"
        {...register("amount")}
        className="input-style"
        placeholder={`Enter your tender (minimum tender is ${
          highTender + 1
        } EGP)`}
      />
    </form>
  );
}
