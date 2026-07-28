"use client";
import React, { useState } from "react";
import Image from "next/image";

type Props = {
  imageUrl: string;
};
export default function MzadImage({ imageUrl }: Props) {
  const [isLoading, setLoading] = useState(true);
  return (
    <Image
      src={imageUrl}
      alt="horse image"
      fill
      priority
      sizes="(max-width: 768px) 100vw, (max-width: 1200px) 50vw, 33vw"
      className={`fill object-center group-hover:scale-110 ease-in-out duration-700
      ${isLoading ? "blur-3xl" : "blur-0"}`}
      onLoad={() => setLoading(false)}
    />
  );
}
