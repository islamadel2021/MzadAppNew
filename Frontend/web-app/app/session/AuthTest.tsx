"use client";
import React, { useState } from "react";
import { UpdateMzadTest } from "../actions/MzadActions";
import { Button } from "flowbite-react";

export default function AuthTest() {
  const [result, setResult] = useState<any>();
  const doUpdate = () => {
    setResult(undefined);
    UpdateMzadTest().then((result) => {
      setResult(result);
    });
  };
  return (
    <div className="flex items-center gap-4">
      <Button className="focus:ring-0" color="purple" onClick={doUpdate}>
        Auth Test
      </Button>
      <div>{JSON.stringify(result, null, 2)}</div>
    </div>
  );
}
