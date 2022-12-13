class Coord {
  coordId: number;
  cityId: number;
  lon: number;
  lat: number;

  constructor(lon, lat) {
    this.lon = lon;
    this.lat = lat;
  }
}

export default Coord;
