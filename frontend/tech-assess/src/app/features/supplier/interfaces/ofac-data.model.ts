import { ScrapeData } from "./scrape-data.model";

export interface OFACData extends ScrapeData {
    name: string;
    address: string;
    type: string;
    programs: string;
    list: string;
    score: string;
}