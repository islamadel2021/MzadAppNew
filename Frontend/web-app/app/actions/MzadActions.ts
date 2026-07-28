"use server";
import { Mzad, PagedResult, Tender } from "@/types";
import { getTokenWorkaround } from "./authActions";
import { fetchWrapper } from "./FetchWrapper";
import { FieldValues } from "react-hook-form";
import { revalidatePath } from "next/cache";

export async function getData(url: string): Promise<PagedResult<Mzad>> {
  return await fetchWrapper.get(`filter/${url}`);
}

export async function createMzad(data: FieldValues) {
  return await fetchWrapper.post("mzad", data);
}

export async function updateMzad(data: FieldValues, id: string) {
  const res = await fetchWrapper.put(`mzad/${id}`, data);
  revalidatePath(`/mzadat/${id}`);
  return res;
}

export async function getDetails(id: string): Promise<Mzad> {
  return await fetchWrapper.get(`mzad/${id}`);
}

export async function deleteMzad(id: string) {
  return await fetchWrapper.del(`mzad/${id}`);
}

export async function getTendersForMzad(id: string): Promise<Tender[]> {
  return await fetchWrapper.get(`tender/${id}`);
}

export async function placeTenderForMzad(mzadId: string, amount: number) {
  return await fetchWrapper.post(
    `tender?mzadId=${mzadId}&amount=${amount}`,
    {}
  );
}

export async function UpdateMzadTest() {
  const data = {
    YearOfBirth: 2020
  };

  const token = await getTokenWorkaround();
  const res = await fetch(
    process.env.BASE_URL + "mzad/f01b70b4-8a1c-4e20-8ba2-9b41c5fe37dc",
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token?.access_token}`
      },
      body: JSON.stringify(data)
    }
  );

  if (!res.ok) return { status: res.status, message: res.statusText };
  return res.statusText;
}
