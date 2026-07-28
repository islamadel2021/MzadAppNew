"use client";
import { Button, Dropdown } from "flowbite-react";
import { User } from "next-auth";
import { signOut } from "next-auth/react";
import Link from "next/link";
import React from "react";
import { AiFillTrophy, AiOutlineLogout } from "react-icons/ai";
import { LiaHorseHeadSolid } from "react-icons/lia";
import { HiCog, HiUser } from "react-icons/hi";
import { usePathname, useRouter } from "next/navigation";
import { useParamsStore } from "@/hooks/useParamsStore";
import titleize from "titleize";
import { deleteCookie } from "./Navbar";

type Props = { user: Partial<User> };

export default function UserActions({ user }: Props) {
  const router = useRouter();
  const pathname = usePathname();
  const setParams = useParamsStore((state) => state.setParams);
  const reset = useParamsStore((state) => state.reset);
  const setSeller = () => {
    reset();
    setParams({ seller: titleize(`${user.username}`), winner: undefined });
    if (pathname !== "/") router.push("/");
  };
  const setWinner = () => {
    reset();
    setParams({ winner: titleize(`${user.username}`), seller: undefined });
    if (pathname !== "/") router.push("/");
  };
  return (
    <Dropdown inline label={`Welcome ${user.name}`}>
      <Dropdown.Item icon={HiUser} onClick={setSeller}>
        Mzadaty
      </Dropdown.Item>
      <Dropdown.Item icon={AiFillTrophy} onClick={setWinner}>
        Mzadat won
      </Dropdown.Item>
      <Dropdown.Item icon={LiaHorseHeadSolid}>
        <Link href="/mzadat/create">Sell my horse</Link>
      </Dropdown.Item>
      <Dropdown.Item icon={HiCog}>
        <Link href="/session">Session (dev only)</Link>
      </Dropdown.Item>
      <Dropdown.Divider />
      <Dropdown.Item
        icon={AiOutlineLogout}
        onClick={() => {
          signOut({ callbackUrl: "/" });
          deleteCookie();
        }}
      >
        Logout
      </Dropdown.Item>
    </Dropdown>
  );
}
