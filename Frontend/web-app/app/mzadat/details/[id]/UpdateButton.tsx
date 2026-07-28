"use client";
import { Button } from "flowbite-react";
import Link from "next/link";
import React from "react";

type Props = { id: string };
export default function UpdateButton({ id }: Props) {
  return (
    <Button outline color="purple">
      <Link href={`/mzadat/update/${id}`}>Update</Link>
    </Button>
  );
}
