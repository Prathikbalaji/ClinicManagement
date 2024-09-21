import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetReceptionistsComponent } from './get-receptionists.component';

describe('GetReceptionistsComponent', () => {
  let component: GetReceptionistsComponent;
  let fixture: ComponentFixture<GetReceptionistsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GetReceptionistsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetReceptionistsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
