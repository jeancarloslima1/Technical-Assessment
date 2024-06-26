import { ScrapeData } from "./scrape-data.model";

export interface WBData extends ScrapeData {
    firmName: string;
    address: string;
    country: string;
    fromDate: string;
    toDate: string;
    grounds: string;
}