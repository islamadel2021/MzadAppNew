"use client";

import { useMzadStore } from "@/hooks/useMzadStore";
import { useTenderStore } from "@/hooks/useTenderStore";
import { Mzad, MzadFinished, Tender } from "@/types";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { User } from "next-auth";
import { ReactNode, useEffect, useState } from "react";
import toast from "react-hot-toast";
import MzadCreatedToast from "../components/MzadCreatedToast";
import { getDetails } from "../actions/MzadActions";
import MzadFinishedToast from "../components/MzadFinishedToast";

type Props = {
  children: ReactNode;
  user: User | null;
};

export default function SignalRProvider({ children, user }: Props) {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const setCurrentPrice = useMzadStore((state) => state.setCurrentPrice);
  const addTender = useTenderStore((state) => state.addTender);
  const apiUrl =
    process.env.NODE_ENV === "production"
      ? "https://api.mzadapp.store/notification"
      : process.env.NEXT_PUBLIC_NOTIFY_URL;
  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(apiUrl!)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, [apiUrl]);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log("Connected to notification hub");
          connection.on("TenderPlaced", (tender: Tender) => {
            console.log("Tender placed event received");
            if (tender.tenderStatus.includes("Accepted")) {
              setCurrentPrice(tender.mzadId, tender.amount);
            }
            addTender(tender);
          });
          connection.on("MzadCreated", (mzad: Mzad) => {
            if (user?.username.toLowerCase() !== mzad.seller.toLowerCase()) {
              return toast(<MzadCreatedToast mzad={mzad} />, {
                duration: 3000
              });
            }
          });
          connection.on("MzadFinished", (mzadFinished: MzadFinished) => {
            const mzad = getDetails(mzadFinished.mzadId);
            return toast.promise(
              mzad,
              {
                loading: "Loading",
                success: (mzad) => (
                  <MzadFinishedToast mzadFinished={mzadFinished} mzad={mzad} />
                ),
                error: (err) => "Mzad finished!"
              },
              { success: { duration: 3000, icon: null } }
            );
          });
        })
        .catch((err) => console.log(err));
    }
    return () => {
      connection?.stop();
    };
  }, [connection, setCurrentPrice]);

  return children;
}
