export interface PaginatedList<T> {
    items: T[];
    pageIndex: number;
    totalPages: number;
    totalItems: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;    
}