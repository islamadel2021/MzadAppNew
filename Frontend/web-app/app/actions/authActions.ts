import { getServerSession } from "next-auth";
import { authOptions } from "../api/auth/[...nextauth]/route";
import { NextApiRequest } from "next";
import { cookies, headers } from "next/headers";
import { getToken } from "next-auth/jwt";

export async function getCurrentSession() {
  return await getServerSession(authOptions);
}
export async function getCurrentUser() {
  try {
    const session = await getCurrentSession();
    return session?.user;
  } catch (error) {
    return null;
  }
}

export async function getTokenWorkaround() {
  const req = {
    headers: Object.fromEntries(headers() as Headers),
    cookies: Object.fromEntries(
      cookies()
        .getAll()
        .map((c) => [c.name, c.value])
    )
  } as NextApiRequest;

  return await getToken({ req });
}
