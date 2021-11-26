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

  displayedColumns: string[] = ["edit", "delete", "name", "country", "cloudiness", "temperature", "weatherCondition", "windSpeed", "createdDate", "updatedDate"];
  opened = false;
  dataSource: MatTableDataSource<WeatherGrid>;
  public _weatherDetails: WeatherGrid[];

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator | undefined;
  @ViewChild(MatSort, { static: true }) sort: MatSort | undefined;

  constructor(private router: Router,
    private dropDownService: DropdownService) { }

  ngOnInit() {
    this.getWeatherGrid();
    this.dataSource = new MatTableDataSource<WeatherGrid>(this._weatherDetails);
  }
  getWeatherGrid() {
    this.dropDownService.getWeatherGrid().subscribe(
      d => {
        this._weatherDetails = d;
        this.dataSource = new MatTableDataSource<WeatherGrid>(this._weatherDetails);
        this.dataSource.paginator = this.paginator as MatPaginator;
        this.dataSource.sort = this.sort as MatSort;
      }
    );
  }

  deleteById(cityId: number) {
    if (confirm("Are you sure you want to delete this ?")) {
      this.dropDownService.deleteWeatherGridById(cityId).subscribe(user => {
        this.getWeatherGrid();
      });
      }
      else {
        return false;
      }
    }

}
