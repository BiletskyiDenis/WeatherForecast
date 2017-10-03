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

export interface IWeekData {
    tempMax: number[];
    tempMin: number[];
    pressure: number[];
    humidity: number[];
    icon: string[];
    dateTime: number[];
}

export interface ICityWeatherData {
    cityWeatherId: number;
    cityName: string;
    country: string;
    currentDay: any;
    daysOfHourly: any;
    daysOfWeek: any;
}