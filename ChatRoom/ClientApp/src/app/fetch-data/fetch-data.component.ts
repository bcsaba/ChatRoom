import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public forecasts: HourlyChatEvent[] = [];
  public chatRooms: ChatRoom[] = [];
  contactForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.contactForm = this.fb.group({
      room: [null]
    });
    
    http.get<ChatRoom[]>(baseUrl + 'ChatRoom').subscribe(result => {
      this.chatRooms = result;
    });

    http.get<HourlyChatEvent[]>(baseUrl + 'ChatRoom/2/hourlyevents').subscribe(result => {
      this.processRoomEvents(result);
    }, error => console.error(error));
  }

  private processRoomEvents(result: HourlyChatEvent[]) {
    this.forecasts = result;

    var prevHourPart = "";
    this.forecasts.forEach(element => {
      if (element.hourPart == prevHourPart) {
        element.sameHourAsPrevious = true;
      } else {
        element.sameHourAsPrevious = false;
        prevHourPart = element.hourPart;
      }
    });
  }

  ngOnInit() {
 
    this.contactForm = this.fb.group({
      room: [2]
    });

    this.contactForm.get("room")!.valueChanges
    .subscribe(f=> {
      this.onChatRoomSelectionChange(f);
    });
  }

  onChatRoomSelectionChange(roomId: number){
    console.log(roomId);
    this.http.get<HourlyChatEvent[]>(this.baseUrl + `ChatRoom/${roomId}/hourlyevents`).subscribe(result => {
      this.processRoomEvents(result);
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
  sameHourAsPrevious: boolean;
}

interface ChatRoom {
  chatRoomId: number;
  name: string;
}
