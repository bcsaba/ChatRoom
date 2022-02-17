import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-hourly-events',
  templateUrl: './hourly-events.component.html'
})
export class HourlyEventsComponent implements OnInit {
  public roomEvents: HourlyChatEvent[] = [];
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
    this.roomEvents = result;

    var prevHourPart = "";
    this.roomEvents.forEach(element => {
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
    this.http.get<HourlyChatEvent[]>(this.baseUrl + `ChatRoom/${roomId}/hourlyevents`).subscribe(result => {
      this.processRoomEvents(result);
    }, error => console.error(error));
  }
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
