import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceptionistHeaderComponent } from './receptionist-header.component';

describe('ReceptionistHeaderComponent', () => {
  let component: ReceptionistHeaderComponent;
  let fixture: ComponentFixture<ReceptionistHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceptionistHeaderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReceptionistHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
