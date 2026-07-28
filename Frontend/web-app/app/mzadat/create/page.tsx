import Heading from "@/app/components/Heading";
import React from "react";
import MzadForm from "../MzadForm";

export default function Create() {
  return (
    <div className="mx-auto mt-2 max-w-[60%] shadow-lg p-7 bg-white rounded-lg">
      <Heading
        title="Sell your horse"
        subtitle="Please enter the details of your Mzad"
      />
      <MzadForm />
    </div>
  );
}
