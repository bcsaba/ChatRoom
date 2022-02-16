import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: HourlyChatEvent[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<HourlyChatEvent[]>(baseUrl + 'ChatRoom/2/hourlyevents').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface HourlyChatEvent {
  eventTypeId:  number;
  name: string;
  countType: number;
  userInAction: number;
  targetUser: number;
  hourPart: string;
}
