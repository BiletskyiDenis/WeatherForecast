import { Injectable } from "@angular/core";
import { IChartData } from "../services/IForecast";
import { ICityWeatherData } from "../services/IForecast";
import { ISearchResult } from "../services/IForecast";
import { Http, Response, Headers } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class WeatherService {

    constructor(private _http: Http) { }

    getCities() {
        return this.httpPostM('/api/forecast/GetAll');
    }

    searchCity(name: string) {
        return this.httpPostM('/api/forecast/Search', 'name=' + name);
    }

    addCity(data: ISearchResult) {
        return this.httpPost('/api/forecast/AddCity', 'id=' + data.id + '&name=' + data.cityName);
    }

    getCount() {
        return this.httpPostM('/api/forecast/GetCount');
    }

    checkForUpdate() {
        return this.httpPostM('/api/forecast/CheckForUpdate');
    }

    getSelectedId() {
        return this.httpPostM('/api/forecast/GetSelectedId');
    }

    setSelectedId(id: number) {
        return this.httpPost('/api/forecast/SetSelectedId', 'id=' + id).subscribe(
            data => { },
            error => alert(error)
        );
    }

    deleteCity(id: number) {
        return this.httpPost('/api/forecast/DeleteCity', 'id=' + id);
    }

    httpPost(url: string, params: string = '') {
        var headers = new Headers();
        headers.append('Content-Type', 'application/x-www-form-urlencoded');
        return this._http.post(url, params, { headers: headers });
    }

    httpPostM(url: string, params: string = '') {
        return this.httpPost(url, params).map(res => res.json());
    }

    getCahartWeekData(weatherData: ICityWeatherData): IChartData {

        var tempMax: number[] = [];
        var tempMin: number[] = [];
        var xdata: string[] = [];

        for (var i = 0; i < weatherData.daysOfWeek.length; i++) {
            var data = weatherData.daysOfWeek[i];

            xdata.push(this.getDateTime(data.dateTime).getDate().toString());
            tempMax.push(Math.floor(data.temp));
            tempMin.push(Math.floor(data.tempMin));
        }

        return {
            xdata: xdata,
            data: [
                { marker: { symbol: 'square' }, data: tempMax, name: 'Max' },
                { marker: { symbol: 'square' }, data: tempMin, name: 'Min' },
            ]
        }
    }

    getCahrtHourDailyData(weatherData: ICityWeatherData): IChartData {
        var time: string[] = [];
        var temp: number[] = [];
        var data: any;

        for (var i = 1; i < 11; i++) {
            data = weatherData.daysOfHourly[i];
            time.push(this.getTimeHHMM(data.dateTime));
            temp.push(data.temp);
        }

        return {
            xdata: time,
            data: [
                { marker: { symbol: 'square' }, data: temp, name: 'Temperature' },
            ]
        }
    }

    getDay(dateTime: number): number {
        return new Date(dateTime * 1000).getDate();
    }

    getDateTime(dateTime: number): Date {
        return new Date(dateTime * 1000);
    }

    getTimeHHMM(dateTime: number): string {

        return new Date(dateTime * 1000).toString().split(" ")[4].substring(0, 5);
    }
}

