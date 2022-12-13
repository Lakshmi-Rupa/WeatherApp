import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditWeatherGridComponent } from './edit-weather-grid.component';

describe('EditWeatherGridComponent', () => {
  let component: EditWeatherGridComponent;
  let fixture: ComponentFixture<EditWeatherGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditWeatherGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditWeatherGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
