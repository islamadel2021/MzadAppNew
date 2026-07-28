"use server";
import React from "react";
import Search from "./Search";
import Logo from "./Logo";
import LoginButton from "./LoginButton";
import { getCurrentUser } from "../actions/authActions";
import UserActions from "./UserActions";
import { cookies } from "next/headers";
export default async function Navbar() {
  const user = await getCurrentUser();
  return (
    <header className="navbar-container">
      <Logo />
      <Search />
      {user ? <UserActions user={user} /> : <LoginButton />}
    </header>
  );
}

export async function deleteCookie() {
  cookies().delete(".AspNetCore.Identity.Application");
}
