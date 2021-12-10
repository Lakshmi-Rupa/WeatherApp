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
import { Location } from '@angular/common';

@Component({
  selector: 'app-edit-weather-grid',
  templateUrl: './edit-weather-grid.component.html',
  styleUrls: ['./edit-weather-grid.component.css'],
  animations: [slideAnimation]
})
export class EditWeatherGridComponent implements OnInit {

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private location: Location ) { }

  @Input() color: ThemePalette = "primary";
  editForm: FormGroup;
  state: "slideIn" | "slideOut" = "slideIn";
  private externalURl = " http://bulk.openweathermap.org/sample/";
  private dataType: DataType = DataType.CityName;
  private data: string | string[] = "";
  public stData: any | undefined;
  showError: boolean = false;
  errorMessage: string = "";
  usingUserLocation: boolean = false;

  ngOnInit(): void {
    this.editForm = this.fb.group({
      cityName: [""],
      cityID: [""],
      cityLongitude: [""],
      cityLatitude: [""],
    });
    console.log("dsa");
    this.stData = this.location.getState() as any;
    console.log(this.stData);
    console.log(this.stData?.cityName);

    this.editForm.controls["cityName"].setValue(this.stData?.cityName);
  }

  goToCityIDs(): void {
    document.location.href = this.externalURl;
  }

  checkDataType(): DataType {
    var input: string[] = [];

    for (const field in this.editForm.controls) {
      if (this.editForm.controls[field].value != "") {
        input.push(field);
      }
    }
    if (!(Array.isArray(input) && input.length)) {
      this.errorMessage = "Please fill one of the form inputs";
      this.showError = true;
      return DataType.Invalid;
    }

    if (input[0] == "cityLongitude" && input[1] == "cityLatitude") {
      this.data = [
        this.editForm.controls[input[0]].value,
        this.editForm.controls[input[1]].value,
      ];

      return DataType.LongitudeAndLatitude;
    }

    if (input.length > 1) {
      this.errorMessage = "Please use only one of the following";
      this.showError = true;
      return DataType.Invalid;
    }

    this.showError = false;
    this.data = this.editForm.controls[input[0]].value;

    switch (input[0]) {
      case "cityName":
        return DataType.CityName;
      case "cityID":
        return DataType.CityID;
    }
  }

  //async useCityGeoLocation(lat, lon) {
  //  localStorage.setItem("dataType", "cityCoord");
  //  localStorage.setItem("lat", lat);
  //  localStorage.setItem("lon", lon);
  //  this.state = "slideOut";
  //  await delay(300);
  //  this.router.navigateByUrl("/weather-forecast/");
  //  return;
  //}

  //getLocation(): void {
  //  if (navigator.geolocation) {
  //    navigator.geolocation.getCurrentPosition((position) => {
  //      this.useCityGeoLocation(
  //        position.coords.latitude,
  //        position.coords.longitude
  //      );
  //    });
  //  }
  //  this.usingUserLocation = true;
  //  this.dataType = DataType.LongitudeAndLatitude;
  //}


  async onSubmit() {
    debugger;
    this.dataType = this.checkDataType();
    if (this.showError == true) return;
    switch (this.dataType) {
      case DataType.CityName:
        {
          localStorage.setItem("dataType", "cityName");
          var city = this.editForm.get("cityName").value;
          this.state = "slideOut";
          await delay(300);
          this.router.navigateByUrl("/weather-forecast/" + city);
          return;
        }
        break;
      case DataType.CityID:
        {
          localStorage.setItem("dataType", "cityID");
          var cityID = this.editForm.get("cityID").value;
          this.state = "slideOut";
          await delay(300);
          this.router.navigateByUrl("/weather-forecast/" + cityID);
          return;
        }
        break;
      //case DataType.LongitudeAndLatitude:
      //  {
      //    if (this.usingUserLocation == false) {
      //      this.useCityGeoLocation(
      //        this.editForm.get("cityLatitude").value,
      //        this.editForm.get("cityLongitude").value
      //      );
      //      return;
      //    }
      //  }
      //  break;
    }
  }

}
