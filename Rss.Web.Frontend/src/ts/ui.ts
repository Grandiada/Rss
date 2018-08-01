import { StartupData, ClientViewModel, ProductViewModel, ClientToAdd, ProductToAdd, Client, Product } from "./types";
import * as ko from "knockout";
import * as $ from "jquery";

export class Ui {

    _records = ko.observableArray<Client>();

    constructor(private startupData: StartupData) {
        if (startupData.clients.length > 0) {

        }
    }

    showButton = () => {

        $.ajax({
            url: `/ajax/`,
            method: 'POST',
            data: ("asdas"),

            success: () => {

            },
            error: error => {
                const fullError = JSON.stringify(error);
                console.log(fullError);
            }
        });
    }
}

