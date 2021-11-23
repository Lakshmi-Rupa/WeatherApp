import { Injectable, Component, OnInit } from "@angular/core";
import { Router } from '@angular/router';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { City } from "../models/city";
import { Clouds } from "../models/Clouds";
import Coord from "../models/Coord";
import { Main } from "../models/main";
import { Sys } from "../models/Sys";
import { Weather } from "../models/Weather";
import { Wind } from "../models/Wind";
import { Inject } from "@angular/core";
import { WeatherGrid } from "../models/WeatherGrid";

@Injectable({ providedIn: 'root' })
export class DropdownService {
  constructor(private httpClient: HttpClient) { }

  public getCityDropdown(): Observable<City[]> {
    return this.httpClient.get<City[]>('/api/DropDown/cityDropdown');
  }

  public getCloudsDropdown(): Observable<Clouds[]> {
    return this.httpClient.get<Clouds[]>('/api/DropDown/cloudsDropdown');
  }

  public getCoordDropdown(): Observable<Coord[]> {
    return this.httpClient.get<Coord[]>('/api/DropDown/coordDropdown');
  }

  public getMainDropdown(): Observable<Main[]> {
    return this.httpClient.get<Main[]>('/api/DropDown/mainDropdown');
  }

  public getSysDropdown(): Observable<Sys[]> {
    return this.httpClient.get<Sys[]>('/api/DropDown/sysDropdown');
  }

  public getWeatherDropdown(): Observable<Weather[]> {
    return this.httpClient.get<Weather[]>('/api/DropDown/weatherDropdown');
  }

  public getWindDropdown(): Observable<Wind[]> {
    return this.httpClient.get<Wind[]>('/api/DropDown/windDropdown');
  }

  public getWeatherGrid(): Observable<WeatherGrid[]> {
    return this.httpClient.get<WeatherGrid[]>('/api/DropDown/getWeatherGrid');
  }

}
