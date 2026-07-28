import { getDetails } from "@/app/actions/MzadActions";
import Heading from "@/app/components/Heading";
import React from "react";
import MzadForm from "../../MzadForm";

export default async function Update({ params }: { params: { id: string } }) {
  const data = await getDetails(params.id);
  return (
    <div className="mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg">
      <Heading
        title="Update your Mzad"
        subtitle="Please update the details of your mzad"
      />
      <MzadForm mzad={data} />
    </div>
  );
}
