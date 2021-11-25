import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { WeatherGrid } from '../shared/models/WeatherGrid';
import { DropdownService } from '../shared/services/dropdown.service';

@Component({
  selector: 'app-weather-grid',
  templateUrl: './weather-grid.component.html',
  styleUrls: ['./weather-grid.component.css']
})
export class WeatherGridComponent implements OnInit {

  displayedColumns: string[] = ["edit", "name", "country", "cloudiness", "temperature", "weatherCondition", "windSpeed"];
  opened = false;
  dataSource: MatTableDataSource<WeatherGrid>;
  public _userDetail: WeatherGrid[];

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator | undefined;
  @ViewChild(MatSort, { static: true }) sort: MatSort | undefined;

  constructor(private router: Router,
    private dropDownService: DropdownService) { }

  ngOnInit() {
    this.getUsers();
    this.dataSource = new MatTableDataSource<WeatherGrid>(this._userDetail);
  }
  getUsers() {
    this.dropDownService.getWeatherGrid().subscribe(
      d => {
        this._userDetail = d;
        this.dataSource = new MatTableDataSource<WeatherGrid>(this._userDetail);
        this.dataSource.paginator = this.paginator as MatPaginator;
        this.dataSource.sort = this.sort as MatSort;
      }
    );
  }

}
