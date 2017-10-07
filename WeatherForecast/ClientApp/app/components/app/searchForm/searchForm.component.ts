import { Component, Input, Output, EventEmitter } from "@angular/core";
import { WeatherService } from "../services/weather.service";
import { ISearchResult } from "../services/IForecast";

@Component({
    selector: 'searchForm',
    templateUrl: './searchForm.component.html',
    styleUrls: ['./searchForm.component.css'],
})

export class SearchForm {

    @Input() showForm: boolean;
    @Output() onClickSearchResultEvent = new EventEmitter<ISearchResult>();
    @Output() onCloseFormEvent = new EventEmitter();

    showResult: boolean;
    cityName: string;
    foundCities: ISearchResult[];
    request: boolean = true;

    serching: boolean;
    endSearch: boolean;

    constructor(private weatherService: WeatherService) { }

    onCloseForm(): void {
        this.cityName = '';
        this.showResult = false;
        this.foundCities = [];
        this.onCloseFormEvent.emit();
    }

    checkName(name: string): void {

        this.cityName = name;
        this.search();

    }

    addNewCity(city: ISearchResult): void {
        this.onCloseForm();
        this.onClickSearchResultEvent.emit(city);
    }

    submit(): void {
        this.search();
    }

    search(): void {
        if (this.cityName.length <= 1) {
            this.showResult = false;
            return;
        }
        this.showResult = false;
        if (this.request) {
            this.request = false;
            setTimeout(() => {
                this.request = true;
                this.serching = true;
                this.endSearch = false;
                this.weatherService.searchCity(this.cityName).subscribe(
                    searchData => {
                        this.foundCities = searchData;

                    },
                    error => {
                        this.showResult = false;
                        this.endSearchStyle();
                    },
                    () => {
                        this.showResult = true;
                        this.endSearchStyle();
                    }
                )
            }, 1000);
        }
    }

    endSearchStyle(): void {
        this.endSearch = true;
        setTimeout(() => { this.serching = false }, 500)
    }
}