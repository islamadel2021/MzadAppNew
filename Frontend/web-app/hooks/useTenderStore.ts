import { Tender } from "@/types";
import { create } from "zustand";

type State = {
  tenders: Tender[];
  open: boolean;
};

type Actions = {
  setTenders: (tenders: Tender[]) => void;
  addTender: (tender: Tender) => void;
  setOpen: (value: boolean) => void;
};

export const useTenderStore = create<State & Actions>((set) => ({
  tenders: [],
  open: true,
  setTenders: (tenders: Tender[]) => {
    set(() => ({
      tenders
    }));
  },
  addTender: (tender: Tender) => {
    set((state) => ({
      tenders: !state.tenders.find((x) => x.id === tender.id)
        ? [tender, ...state.tenders]
        : [...state.tenders]
    }));
  },
  setOpen: (value: boolean) => set((state) => ({ open: value }))
}));
