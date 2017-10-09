import { Component } from '@angular/core'
import { ISearchResult } from "./services/IForecast"
import { ICityWeatherData } from "./services/IForecast"
import { WeatherService } from "./services/weather.service"

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
})
export class AppComponent {

    showWeatherWidget: boolean;
    showSearchForm: boolean;
    showMoreInfo: boolean;

    blurScreen: boolean;
    existsCities: boolean;
    firstChek: boolean;
    updating: boolean;

    cities: ICityWeatherData[] = [];
    selectedCity: ICityWeatherData;
    emptyCity: ICityWeatherData;
    selectedId: number;

    constructor(private weatherService: WeatherService) { }

     ngOnInit() {
         this.chekCities();
    }

    chekCities(): void {
        this.weatherService.getCount().subscribe(
            data => {
                this.firstChek = true;
                if (data.count > 0) {
                    this.existsCities = true;
                }
            },
            error => alert(error),
            () => this.getSelectedCityId()
        )
    }

    checkUpdate(): void {
        this.updating = true;
        this.weatherService.checkForUpdate().subscribe(
            data => {
                if (data.dataUpdate) {
                    this.getCities();
                }
            });
    }

    getCities(): void {
        var citySet: boolean = false;
        if (!this.existsCities) {
            return;
        }
        this.weatherService.getCities().subscribe(
            data => {
                if (data.length > 0) {
                    this.cities = <ICityWeatherData[]>data;

                    for (var i = 0; i < this.cities.length; i++) {
                        if (this.cities[i].cityWeatherId == this.selectedId) {
                            this.selectedCity = this.cities[i];
                            citySet = true;
                            break;
                        }
                    }

                    if (!citySet) {
                        this.selectedCity = this.cities[0];
                    }

                    this.showWeatherWidget = true;

                    this.checkUpdate();
                }
            },
            error => alert(error)
        )
    }

    getSelectedCityId(): void {
        this.weatherService.getSelectedId().subscribe(
            data => this.selectedId = data,
            error => alert(error),
            () => this.getCities()
        )
    }

    onDeleteCity($event: ICityWeatherData): void {
        this.cities = this.cities.filter(x => x != $event)
        if (this.cities.length > 0) {
            if (this.selectedId == $event.cityWeatherId) {
                this.selectedCity = this.cities[0];
                this.selectedId = this.cities[0].cityWeatherId;
            }
        } else {
            this.existsCities = false;
            this.selectedId = 0;
            this.selectedCity = this.emptyCity;
            this.showMoreInfo = false;
        }

        this.weatherService.deleteCity($event.cityWeatherId).subscribe(
            data => { },
            error => alert(error)
        )
    }

    onClickMenuItem($event: ICityWeatherData): void {
        if ($event != null) {
            this.selectedCity = $event;
            this.selectedId = $event.cityWeatherId;
        } else {
            this.selectedCity = this.cities[0];
            this.selectedId = this.cities[0].cityWeatherId;
        }
    }

    onClickAddButton(): void {
        this.showSearchForm = true;
        this.blurScreen = true;
    }

    onCloseSearchForm(): void {
        this.showSearchForm = false;
        this.blurScreen = false;
    }

    onShowMoreInfo(): void {
        this.showMoreInfo = !this.showMoreInfo;
    }

    onAddNewCity($event: ISearchResult): void {
        var city = this.cities.find(x => x.cityWeatherId == $event.id);

        if (city != null) {
            return;
        }
        var tmpUpdate: ISearchResult = $event;

        this.cities.push({
            cityName: tmpUpdate.cityName,
            cityWeatherId: tmpUpdate.id,
            country: tmpUpdate.country,
            currentDay: { temp: tmpUpdate.temp, icon: tmpUpdate.icon },
            daysOfHourly: null,
            daysOfWeek: null
        });

        this.existsCities = true;

        this.weatherService.addCity($event).subscribe(
            data => {

                if (tmpUpdate.cityName != null) {
                    this.firstChek = true;
                    this.existsCities = true;
                    this.selectedId = tmpUpdate.id;
                }
                this.getCities();

            },
            error => alert(error));
    }
}
