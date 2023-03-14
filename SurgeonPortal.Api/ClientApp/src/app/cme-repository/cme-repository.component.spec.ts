import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CmeRepositoryComponent } from './cme-repository.component';

describe('CmeRepositoryComponent', () => {
  let component: CmeRepositoryComponent;
  let fixture: ComponentFixture<CmeRepositoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CmeRepositoryComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CmeRepositoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
