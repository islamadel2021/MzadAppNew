"use client";
import { deleteMzad } from "@/app/actions/MzadActions";
import { Button } from "flowbite-react";
import { useRouter } from "next/navigation";
import React from "react";
import toast from "react-hot-toast";

type Props = { id: string };
export default function DeleteButton({ id }: Props) {
  const router = useRouter();
  const doDelete = () => {
    deleteMzad(id)
      .then(() => {
        router.push("/");
        toast.success("Mzad deleted successfully");
      })
      .catch((err) => {
        toast.error(err.message);
      });
  };
  return (
    <Button onClick={doDelete} outline color="failure">
      Delete
    </Button>
  );
}
