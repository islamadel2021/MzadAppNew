export type PagedResult<T> = {
  results: T[];
  pageCount: number;
  totalCount: number;
};

export type Mzad = {
  reservePrice: number;
  seller: string;
  winner?: string;
  soldAmount: number;
  currentHighTender: number;
  createdAt: string;
  updatedAt: string;
  mzadEnd: string;
  status: string;
  name: string;
  father: string;
  mother: string;
  breed: string;
  yearOfBirth: number;
  color: string;
  imageUrl: string;
  id: string;
};

export type Tender = {
  id: string;
  mzadId: string;
  tenderOwner: string;
  amount: number;
  tenderTime: string;
  tenderStatus: string;
};

export type MzadFinished = {
  mzadId: string;
  horseSold: boolean;
  winner: string;
  seller: string;
  amount: number;
};
