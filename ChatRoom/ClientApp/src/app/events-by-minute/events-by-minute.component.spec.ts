import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventsByMinuteComponent } from './events-by-minute.component';

describe('EventsByMinuteComponent', () => {
  let component: EventsByMinuteComponent;
  let fixture: ComponentFixture<EventsByMinuteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventsByMinuteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventsByMinuteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
