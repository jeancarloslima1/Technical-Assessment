import { ScrapeData } from "./scrape-data.model";

export interface ScrapeResult {
    source: string;
    hits: number;
    scrapeData: ScrapeData[];
}