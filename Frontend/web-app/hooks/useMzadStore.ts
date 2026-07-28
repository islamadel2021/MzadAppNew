import { Mzad, PagedResult } from "@/types";
import { create } from "zustand";

type State = {
  mzadat: Mzad[];
  totalCount: number;
  pageCount: number;
};

type Actions = {
  setData: (data: PagedResult<Mzad>) => void;
  setCurrentPrice: (MzadId: string, amount: number) => void;
};

const initialState: State = {
  mzadat: [],
  pageCount: 0,
  totalCount: 0
};

export const useMzadStore = create<State & Actions>((set) => ({
  ...initialState,

  setData: (data: PagedResult<Mzad>) => {
    set(() => ({
      mzadat: data.results,
      totalCount: data.totalCount,
      pageCount: data.pageCount
    }));
  },

  setCurrentPrice: (mzadId: string, amount: number) => {
    set((state) => ({
      mzadat: state.mzadat.map((mzad) =>
        mzad.id === mzadId ? { ...mzad, currentHighTender: amount } : mzad
      )
    }));
  }
}));
