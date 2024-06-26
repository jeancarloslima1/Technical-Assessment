import { ScrapeData } from "./scrape-data.model";

export interface OLDData extends ScrapeData {
    entity: string;
    jurisdiction: string;
    linkedTo: string;
    dataFrom: string;
}