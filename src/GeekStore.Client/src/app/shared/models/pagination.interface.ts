export interface Pagination<T> {
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  pageCount: number;
  data?: T;
}
