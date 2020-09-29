export class ListPagination {
    public set currentPage(page: number) { this._currentPage = page; }
    public set pageSize(pageSize: number) {  this._pageSize = pageSize; }
    public set allItems(allItems: number) { this._allItems = allItems; }

    public get currentPage(): number { return this._currentPage; }
    public get pageSize(): number { return this._pageSize; }
    public get allItems(): number { return this._allItems; }
    public get pagesCount(): number { return Math.ceil(this.allItems / this.pageSize);}

    constructor(
        private _currentPage: number = 1,
        private _pageSize: number = 10,
        private _allItems: number = 10
    ) {  }

    public static parse(pagingJson: string): ListPagination
    {
        if (pagingJson == null || pagingJson == "") {
            return new ListPagination();
        }

        const paging = JSON.parse(pagingJson);
        
        const paginationInfo = new ListPagination(
            paging.currentPage,
            paging.itemsPerPage,
            paging.allItems
        );

        return paginationInfo;
    }
}