import { Component, Input } from "@angular/core";
import { ICityWeatherData } from "../services/IForecast";
import { OnInit } from "@angular/core";

declare var Highcharts: any;

@Component({
    selector: 'more-info',
    templateUrl: './moreInfo.component.html',
    styleUrls: ['./moreInfo.component.css']
})
    
export class MoreInfo {

    @Input() selectedCity: ICityWeatherData;

    getTime(dateTime:number):string {
        return new Date(dateTime * 1000).toString().split(" ")[4].substring(0, 5);
    }
}