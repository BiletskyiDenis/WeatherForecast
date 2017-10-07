import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { WeatherService } from "../services/weather.service";
import { IChartData } from "../services/IForecast";
import { ICityWeatherData } from "../services/IForecast";

declare var Highcharts: any

@Component({
    selector: 'chart',
    template: `<div id="chart" style="min-width: 310px; height: 400px; margin: 0 auto"></div>`,
})

export class Chart {
    @Input() title: string;
    @Input() subtitle: string;
    @Input() yAxisTitle: string;
    @Input() divContainer: string;
    @Input() typeChart: string;

    private _selectedCity: ICityWeatherData;
    private init: boolean;

    @Input()
    set selectedCity(selectedCity: ICityWeatherData) {
        this._selectedCity = selectedCity;
        if (this.init) {
            this.updateChart(selectedCity);
            this.showChart();
        }
    };
    get selectedCity(): ICityWeatherData {
        return this._selectedCity;
    }

    chartData: IChartData;

    constructor(private weatherService: WeatherService) { }

    ngOnInit() {
        this.init = true;
        this.setID();
        this.updateChart(this.selectedCity);
        this.showChart();
    }

    updateChart(selectedCity: ICityWeatherData): void {

        if (this.typeChart == "CURRENT_DAY") {
            this.chartData = this.weatherService.getCahrtHourDailyData(selectedCity);
        }

        if (this.typeChart == "CURRENT_WEEK") {
            this.chartData = this.weatherService.getCahartWeekData(selectedCity);
        }
    }

    setID(): void {
        var chartId = document.getElementById('chart');
        if (chartId != null) {
            chartId.id = this.divContainer;
        }
    }

    showChart(): void {
        Highcharts.chart(this.divContainer, {
            chart: { type: 'spline' },
            title: { text: this.title },
            subtitle: { text: this.subtitle },
            xAxis: { categories: this.chartData.xdata },
            yAxis: { title: { text: this.yAxisTitle } },
            tooltip: { crosshairs: true, shared: true },
            plotOptions: { spline: { marker: { radius: 4, lineColor: '#666666', lineWidth: 1 } } },
            series: this.chartData.data
        });
    }
}