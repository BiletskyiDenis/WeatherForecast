export interface IChartData {

    xdata: string[];
    data: IChartSubData[];  
}

export interface IChartSubData {

    name: string;
    data: number[];
    marker: { symbol: 'square' };
}

export interface ISearchResult {
    id: number;
    cityName: string;
    icon: string;
    temp: number;
    country: string;
}

export interface ICityWeatherData {
    cityWeatherId: number;
    cityName: string;
    country: string;
    currentDay: any;
    daysOfHourly: any;
    daysOfWeek: any;
}