import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from "@angular/forms";
import { MatTable } from "@angular/material/table";
import { MatFormField } from "@angular/material/form-field";
import { MatInput } from "@angular/material/input";
import { MatFormFieldModule } from '@angular/material/form-field';
import { ThemePalette } from '@angular/material/core';
import { Router } from '@angular/router';
import { DataType } from '../shared/data.type';
import { slideAnimation } from "../shared/animations/animations";
import { delay } from '../shared/delay';
import { City } from '../shared/models/city';
import { DropdownService } from '../shared/services/dropdown.service';
import { Clouds } from '../shared/models/Clouds';
import { Main } from '../shared/models/main';
import { WeatherModel } from '../shared/models/weather.model';
import { LongWeatherForecastModel } from '../shared/models/longWeatherForecastModel';
import { LongWeatherForecastListItemModel } from '../shared/models/longWeatherForecastListItemModel';
import { WeatherService } from '../shared/services/weather.service';

interface TableRow {
  day: string;
  tempC: number;
  pressure: number;
}

@Component({
  selector: 'app-weather-filter',
  templateUrl: './weather-filter.component.html',
  styleUrls: ['./weather-filter.component.css'],
  animations: [slideAnimation]
})
export class WeatherFilterComponent implements OnInit {
  formfilter: FormGroup;

  constructor(private router: Router,
    private fb: FormBuilder,
    private dropDownService: DropdownService,
    private weatherService: WeatherService) { }

  @Input() color: ThemePalette = "primary";
  showLoadingSpinner: boolean = true;
  selectCity = "";
  selectCloudiness = "";
  selectTemperature = "";
  public _cityNames: City[];
  public _cloudiness: Clouds[];
  public _temperature: Main[];
  weatherDetails: WeatherModel = new WeatherModel();
  longWeatherModel: LongWeatherForecastModel;
  currentWeatherDetails: LongWeatherForecastListItemModel;
  tableData: TableRow[] = [];
  displayedColumns: string[] = ["Day", "TempC", "Pressure"];
  dataSource;
  public displayGrid: boolean = false;

  ngOnInit(): void {
    this.formfilter = this.fb.group({
      cityName: [''],
      cloudAll: [''],
      temperature: [''],
    });
    this.getCitydd();
    this.getCloudiness();
    this.getTemperature();
  }


  getCitydd() {
    this.dropDownService.getCityDropdown().subscribe(
      d => {
        this._cityNames = d;
      }
    );
  }

  getCloudiness() {
    this.dropDownService.getCloudsDropdown().subscribe(
      d => {
        this._cloudiness = d;
      }
    );
  }

  getTemperature() {
    this.dropDownService.getMainDropdown().subscribe(
      d => {
        this._temperature = d;
      }
    );
  }

  get6DaysWeatherForecast(cityName: string) {
    console.log(cityName);
    this.displayGrid = true;
    this.tableData = [];
    this.weatherService
      .get6DaysWeatherForecast(cityName)
      .subscribe((res) => {
        this.longWeatherModel = res.responseBody;
        this.longWeatherModel.list.forEach((element) => {
          this.tableData.push({
            pressure: element.main.pressure,
            day: element.dayName,
            tempC: element.main.tempC,
          });
          this.dataSource = this.tableData;
          this.currentWeatherDetails = this.longWeatherModel.list[0];
        });
      });
  }
}
