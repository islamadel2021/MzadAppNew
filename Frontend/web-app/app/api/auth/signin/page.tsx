import EmptyFilter from "@/app/components/EmptyFilter";
import React from "react";

export default function Page({
  searchParams
}: {
  searchParams: { callbackUrl: string };
}) {
  return (
    <EmptyFilter
      title="You need to login to access this page"
      subtitle="Please login to continue"
      showLogin
      callbackUrl={searchParams.callbackUrl}
    />
  );
}
