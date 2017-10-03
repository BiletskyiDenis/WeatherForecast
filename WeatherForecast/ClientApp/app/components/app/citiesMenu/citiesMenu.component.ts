import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { ICityWeatherData } from "../services/IForecast";
import { WeatherService } from "../services/weather.service";

@Component({
    selector: 'city-menu',
    templateUrl: './citiesMenu.component.html',
    styleUrls: ['./citiesMenu.component.css'],
})

export class CitiesMenu {

    @Input() cities: ICityWeatherData[];
    @Output() onSelected = new EventEmitter<ICityWeatherData>();
    @Output() onDeleteClick = new EventEmitter<ICityWeatherData>();

    showDeleteButtons = false;

    constructor(private weatherService: WeatherService) { }

    trackByCity(index: number, city: ICityWeatherData): string {
        return city.cityName;
    }

    onClick(city: ICityWeatherData): void {

        if (city.daysOfWeek != null) {
            this.onSelected.emit(city);
            this.weatherService.setSelectedId(city.cityWeatherId)
        }

    }

    onShowDeleteButtons(): void {
        this.showDeleteButtons = !this.showDeleteButtons;
    }

    onDeleteCity(city: ICityWeatherData): void {
        this.onDeleteClick.emit(city);
    }

}