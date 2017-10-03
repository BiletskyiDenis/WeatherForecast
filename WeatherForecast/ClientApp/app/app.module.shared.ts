import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';

import { AddButton } from './components/app/addButton/addButton.component';
import { SearchForm } from './components/app/searchForm/searchForm.component';
import { CitiesMenu } from './components/app/citiesMenu/citiesMenu.component';
import { MoreInfo } from './components/app/moreInfo/moreInfo.component';
import { Chart } from './components/app/moreInfo/chart.component';
import { WeatherWidget } from './components/app/weatherWidget/weatherWidget.component';
import { RoundDegreePipe } from './components/app/pipe/Pipes'
import { ShortNamePipe } from './components/app/pipe/Pipes'
import { TimePipe } from './components/app/pipe/Pipes'
import { DayPipe } from './components/app/pipe/Pipes'
import { DayNamePipe } from './components/app/pipe/Pipes'
import { MonthNamePipe } from './components/app/pipe/Pipes'
import { YearNamePipe } from './components/app/pipe/Pipes'

import { WeatherService } from './components/app/services/weather.service';


@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        AddButton,
        SearchForm,
        CitiesMenu,
        MoreInfo,
        WeatherWidget,
        Chart,
        RoundDegreePipe,
        ShortNamePipe,
        TimePipe,
        DayPipe,
        DayNamePipe,
        MonthNamePipe,
        YearNamePipe
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        WeatherService
    ]
})
export class AppModuleShared {
}
