import { StartupData, News, PageViewModel, Page, LoadDataAjaxRequest, LoadDataAjaxRespone } from "./types";
import * as ko from "knockout";
import * as $ from "jquery";

export class Ui {

    _news = ko.observableArray<News>();
    _pageViewModel: KnockoutObservable<PageViewModel>;
    _sourceName = ko.observable<string>("Все");
    _orderType = ko.observable<string>("DateOrder");

    constructor(private startupData: StartupData) {
        this._pageViewModel = ko.observable(new PageViewModel(startupData.totalPages));
        this.filterButton();
    }

    select = (page: Page) => {
        this._pageViewModel().select(page);

        const data: LoadDataAjaxRequest = {
            page: this._pageViewModel().selectedPageNumber(),
            sortOrder: this._orderType(),
            sourceName: this._sourceName()
        }

        this.loadData(data);
    }

    filterButton = () => {

        const data: LoadDataAjaxRequest = {
            page: 1,
            sortOrder: this._orderType(),
            sourceName: this._sourceName()
        }

        this.loadData(data);
    }

    loadData = (data: LoadDataAjaxRequest) => {

        $.ajax({
            url: `/get-data/`,
            method: 'GET',
            data: data,

            success: (response: LoadDataAjaxRespone) => {
                this._news.removeAll();
                this._news.push(...response.news);
                if (data.page === 1)
                    this._pageViewModel(new PageViewModel(response.totalPages));
            },
            error: error => {
                const fullError = JSON.stringify(error);
                console.log(fullError);
            }
        });
    }

}

