import { Injectable } from "@angular/core";
import { IChartData } from "../services/IForecast";
import { ICityWeatherData } from "../services/IForecast";
import { ISearchResult } from "../services/IForecast";
import { IWeekData } from "../services/IForecast";
import { Http, Response, Headers } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class WeatherService {

    constructor(private _http: Http) { }

    getCities() {
        return this.httpPostM('/forecast/GetAll');
    }

    getAllData() {
        return this.httpPostM('/forecast/GetFullData');
    }

    searchCity(name: string) {
        return this.httpPostM('/forecast/Search', 'name=' + name);
    }

    addCity(data: ISearchResult) {
        return this.httpPost('/forecast/AddCity', 'id=' + data.id + '&name=' + data.cityName);
    }

    getCount() {
        return this.httpPostM('/forecast/GetCount');
    }

    checkForUpdate() {
        return this.httpPostM('/forecast/CheckForUpdate');
    }

    getSelectedId() {
        return this.httpPostM('/forecast/GetSelectedId');
    }

    setSelectedId(id: number) {
        return this.httpPost('/forecast/SetSelectedId', 'id=' + id).subscribe(
            data => { },
            error => alert(error)
        );
    }

    deleteCity(id: number) {
        return this.httpPost('/forecast/DeleteCity', 'id=' + id);
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
        var data = this.getWeekData(weatherData);
        var xdata: string[] = [];
        for (var i = 0; i < data.dateTime.length; i++) {
            xdata.push(this.getDateTime(data.dateTime[i]).getDate().toString())
        }

        return {
            xdata: xdata,
            data: [
                { marker: { symbol: 'square' }, data: data.tempMax, name: 'Max' },
                { marker: { symbol: 'square' }, data: data.tempMin, name: 'Min' },
            ]
        }
    }

    getWeekForecast(weatherData: ICityWeatherData): any[] {
        var data = this.getWeekData(weatherData);
        var weekData: any[] = [];

        for (var i = 0; i < data.dateTime.length; i++) {
            weekData.push({ dateTime: data.dateTime[i], tempMax: data.tempMax[i], tempMin: data.tempMin[i], icon: data.icon[i] })
        }

        weekData[weekData.length - 1].tempMax += weekData[weekData.length - 2].tempMax;
        weekData[weekData.length - 1].tempMax /= 2;
        weekData[weekData.length - 1].icon = weekData[weekData.length - 1].icon.replace("n", "d");

        return weekData;
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

    getWeekData(weatherData: ICityWeatherData): IWeekData {

        var dateTime: number[] = [];
        var tempMax: number[] = [];
        var tempMin: number[] = [];
        var pressure: number[] = [];
        var humidity: number[] = [];
        var icon: string[] = [];
        var data: any;

        for (var i = 0; i < weatherData.daysOfWeek.length; i++) {
            var data = weatherData.daysOfWeek[i];

            if (!this.checkLessDate(weatherData.currentDay.dateTime, data.dateTime)) {
                continue;
            }
            dateTime.push(data.dateTime);
            pressure.push(data.pressure);
            icon.push(data.icon);
            tempMax.push(Math.floor(data.temp));
            tempMin.push(Math.floor(data.tempMin));
            humidity.push(data.humidity);
            if (dateTime.length == 7) {
                break;
            }
        }

        return { dateTime: dateTime, humidity: humidity, icon: icon, pressure: pressure, tempMax: tempMax, tempMin: tempMin }
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

    checkLessDate(dateTime1: number, dateTime2: number): boolean {
        var day1 = new Date(dateTime1 * 1000).getDate();
        var day2 = new Date(dateTime2 * 1000).getDate();

        var month1 = new Date(dateTime1 * 1000).getMonth();
        var month2 = new Date(dateTime2 * 1000).getMonth();

        return (month1 == month2 && day1 <= day2);
    }

}

