import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder } from '@angular/forms';
import { User } from 'oidc-client';

@Component({
  selector: 'app-events-by-minute',
  templateUrl: './events-by-minute.component.html',
  styleUrls: ['./events-by-minute.component.css']
})
export class EventsByMinuteComponent implements OnInit {
  public roomEvents: MinuteChatEvent[] = [];
  public chatRooms: ChatRoom[] = [];
  contactForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { 
    this.contactForm = this.fb.group({
      room: [null]
    });

    http.get<ChatRoom[]>(baseUrl + 'ChatRoom').subscribe(result => {
      this.chatRooms = result;
    });

    http.get<MinuteChatEvent[]>(baseUrl + 'ChatRoom/2/minutebyminuteevents').subscribe(result => {
      this.processRoomEvents(result);
    }, error => console.error(error));
  }

  private processRoomEvents(result: MinuteChatEvent[]) {
    this.roomEvents = result;
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
    this.http.get<MinuteChatEvent[]>(this.baseUrl + `ChatRoom/${roomId}/minutebyminuteevents`).subscribe(result => {
      this.processRoomEvents(result);
    }, error => console.error(error));
  }
}

interface RoomEventType {
  id: number;
  name: string;
}

interface ChatUser {
  id: number;
  firstName: string;
  lastName: string;
  nickName: string;
}

interface Comment {
  id: number;
  commentString: string;
}

interface MinuteChatEvent {
  id: number;
  roomEventType: RoomEventType;
  eventTime: string;
  user: ChatUser;
  comment: Comment;
  targetUser: ChatUser;
}

interface ChatRoom {
  chatRoomId: number;
  name: string;
}