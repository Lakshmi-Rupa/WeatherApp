import Coord from "./Coord";

export class City {
  cityId: number;
  id: number;
  name: string;
  coord: Coord;
  country: string;
  population: number;
  timezone: number;
  createdDate?: Date | string;
  updatedDate?: Date | string;
  deleteIndicator?: boolean;
}
