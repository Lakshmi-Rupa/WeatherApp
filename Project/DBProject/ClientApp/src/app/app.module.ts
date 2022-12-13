import { CommonModule } from "@angular/common";
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatDividerModule } from "@angular/material/divider";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatMenuModule } from "@angular/material/menu";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatTableModule } from "@angular/material/table";
import { MatTooltipModule } from "@angular/material/tooltip";
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { WeatherSearchComponent } from './weather-search/weather-search.component';
import { WeatherForecastComponent } from './weather-forecast/weather-forecast.component';
import { WeatherService } from "./shared/services/weather.service";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { WeatherFilterComponent } from './weather-filter/weather-filter.component';
import { MatSelectModule } from '@angular/material/select';
import { DropdownService } from "./shared/services/dropdown.service";
import { WeatherGridComponent } from './weather-grid/weather-grid.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { EditWeatherGridComponent } from './edit-weather-grid/edit-weather-grid.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    WeatherSearchComponent,
    WeatherForecastComponent,
    WeatherFilterComponent,
    WeatherGridComponent,
    EditWeatherGridComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatFormFieldModule,
    MatButtonModule,
    MatTooltipModule,
    MatDividerModule,
    MatIconModule,
    MatMenuModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'weather-search', component: WeatherSearchComponent },
      { path: 'weather-forecast/:details', component: WeatherForecastComponent },
      { path: 'weather-forecast', component: WeatherForecastComponent },
      { path: 'weather-filter', component: WeatherFilterComponent },
      { path: 'weather-grid', component: WeatherGridComponent },
      { path: 'edit-weather-grid', component: EditWeatherGridComponent },
    ], { relativeLinkResolution: 'legacy' })
  ],
  exports: [
   
  ],
  providers: [WeatherService, DropdownService],
  bootstrap: [AppComponent]
})
export class AppModule { }
