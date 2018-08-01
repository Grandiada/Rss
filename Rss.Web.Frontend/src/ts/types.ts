import * as ko from "knockout"

export interface StartupData {
    totalPages: number;
}

export interface News {
    title: string;
    description: string;
    source: string;
    date: string;
}


export class Page {

    pageNumber: number;

    constructor(page: number) {
        this.pageNumber = page;
    }
}

export class PageViewModel {
    selectedPageNumber = ko.observable<number>(1);
    pages: KnockoutObservableArray<Page>;

    constructor(totalPages: number) {
        this.pages = ko.observableArray<Page>();

        for (var i = 0; i < totalPages; i++) {
            this.pages.push(new Page(i + 1));
        }
    }


    select = (pageSelected: Page) => {
        this.selectedPageNumber(pageSelected.pageNumber);
    }
}

export interface LoadDataAjaxRequest {
    sourceName: string;
    page: number;
    sortOrder: string;
}

export interface LoadDataAjaxRespone {
    totalPages: number;
    news: News[];
}