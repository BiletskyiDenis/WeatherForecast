import { Component, Input, Output, EventEmitter } from "@angular/core";
import { ICityWeatherData } from "../services/IForecast";
import { WeatherService } from "../services/weather.service";

@Component({
    selector: 'weather-widget',
    templateUrl: './weatherWidget.component.html',
    styleUrls: ['./weatherWidget.component.css', './moreInfoButton.css'],
})
export class WeatherWidget {

    @Input() cities: ICityWeatherData[];

    @Input() showMoreInfo: boolean;

    private _selectedCity: ICityWeatherData;

    @Input()
    set selectedCity(selectedCity: ICityWeatherData) {
        this._selectedCity = selectedCity;
    }
    get selectedCity(): ICityWeatherData{
        return this._selectedCity;
    }

    @Output() onShowMoreInfo = new EventEmitter();
    @Output() onSelected = new EventEmitter<ICityWeatherData>();
    @Output() onDeleteClick = new EventEmitter<ICityWeatherData>();

    constructor(private weatherService: WeatherService) { }

    onShow(): void {
        
        this.onShowMoreInfo.emit();
    }

    onClick(city: ICityWeatherData): void {
        this.onSelected.emit(city);
    }

    onDeleteCity(): void {
        this.onDeleteClick.emit(this.selectedCity);
    }
}