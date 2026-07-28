"use client";
import { Button } from "flowbite-react";
import { signIn } from "next-auth/react";
import React from "react";

export default function LoginButton() {
  return (
    <Button
      outline
      color="light"
      size={"lg"}
      className="focus:ring-0"
      onClick={() => {
        signIn("id-server", {
          callbackUrl: "/"
        });
      }}
    >
      Login
    </Button>
  );
}
