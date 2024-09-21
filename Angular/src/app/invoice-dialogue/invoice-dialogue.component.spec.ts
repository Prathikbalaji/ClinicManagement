import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvoiceDialogueComponent } from './invoice-dialogue.component';

describe('InvoiceDialogueComponent', () => {
  let component: InvoiceDialogueComponent;
  let fixture: ComponentFixture<InvoiceDialogueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvoiceDialogueComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvoiceDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
