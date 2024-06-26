export interface Supplier {
  id: number;
  legalName: string;
  tradeName: string;
  taxIdentificationNumber: string;
  phoneNumber: string;
  email: string;
  website: string;
  physicalAddress: string;
  countryId: number;
  annualRevenueUSD: number;
  lastEdited: Date;
}