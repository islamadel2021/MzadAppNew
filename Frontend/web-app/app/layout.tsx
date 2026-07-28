import type { Metadata } from "next";
import "./globals.css";
import Navbar from "./nav/Navbar";
import ToastProvider from "./Providers/ToastProvider";
import SignalRProvider from "./Providers/SignalRProvider";
import { getCurrentUser } from "./actions/authActions";
import { User } from "next-auth";

export const metadata: Metadata = {
  title: "MzadApp",
  description: "Arabian Horses Mzad"
};

export default async function RootLayout({
  children
}: {
  children: React.ReactNode;
}) {
  const user = await getCurrentUser();
  return (
    <html lang="en">
      <body>
        <ToastProvider />
        <Navbar />
        <main className="mx-auto p-3">
          <SignalRProvider user={user as User}>{children}</SignalRProvider>
        </main>
      </body>
    </html>
  );
}
