import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from "@angular/forms";
import { MatTable } from "@angular/material/table";
import { MatFormField } from "@angular/material/form-field";
import { MatInput } from "@angular/material/input";
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-weather-search',
  templateUrl: './weather-search.component.html',
  styleUrls: ['./weather-search.component.css']
})
export class WeatherSearchComponent implements OnInit {

  constructor(
    private fb: FormBuilder) { }
  form: FormGroup;

  ngOnInit(): void {


    this.form = this.fb.group({
      
    });
  }

}
