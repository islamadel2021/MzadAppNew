"use client";
import { useTenderStore } from "@/hooks/useTenderStore";
import { usePathname } from "next/navigation";
import React from "react";
import Countdown, { zeroPad } from "react-countdown";

type Props = {
  mzadEnd: string;
};
const renderer = ({
  days,
  hours,
  minutes,
  seconds,
  completed
}: {
  days: number;
  hours: number;
  minutes: number;
  seconds: number;
  completed: boolean;
}) => {
  return (
    <div
      className={`
                text-white py-1 px-3 
                rounded flex justify-center
                max-w-[7vw]
                ${
                  completed
                    ? "bg-red-600"
                    : days === 0 && hours < 24
                    ? "bg-amber-600"
                    : "bg-green-600"
                }
            `}
    >
      {completed ? (
        <span>Finished</span>
      ) : (
        <span suppressHydrationWarning>
          {zeroPad(days)}:{zeroPad(hours)}:{zeroPad(minutes)}:{zeroPad(seconds)}
        </span>
      )}
    </div>
  );
};

export default function CountdownTimer({ mzadEnd }: Props) {
  const setOpen = useTenderStore((state) => state.setOpen);
  const pathname = usePathname();
  const mzadFinished = () => {
    if (pathname.startsWith("/mzadat/details")) {
      setOpen(false);
    }
  };
  return (
    <div>
      <Countdown date={mzadEnd} renderer={renderer} onComplete={mzadFinished} />
    </div>
  );
}
