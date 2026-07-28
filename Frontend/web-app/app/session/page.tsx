import React from "react";
import { getCurrentSession, getTokenWorkaround } from "../actions/authActions";
import Heading from "../components/Heading";
import AuthTest from "./AuthTest";

export default async function session() {
  const session = await getCurrentSession();
  const token = await getTokenWorkaround();
  return (
    <div>
      <Heading title="Session Details" />
      <div
        className="bg-gray-200 border-2 border-purple-500 p-2 
          w-[50%] text-lg"
      >
        <h3>Session data</h3>
        <pre>{JSON.stringify(session, null, 2)}</pre>
      </div>
      <div className="mt-4">
        <AuthTest />
      </div>
      <div
        className="bg-gray-200 border-2 border-purple-500 p-2 
          w-[50%] text-lg mt-4"
      >
        <h3>Token data</h3>
        <pre className="overflow-auto">{JSON.stringify(token, null, 2)}</pre>
      </div>
    </div>
  );
}
