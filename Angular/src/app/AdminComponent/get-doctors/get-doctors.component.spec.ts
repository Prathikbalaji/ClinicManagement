import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetDoctorsComponent } from './get-doctors.component';

describe('GetDoctorsComponent', () => {
  let component: GetDoctorsComponent;
  let fixture: ComponentFixture<GetDoctorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GetDoctorsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetDoctorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
