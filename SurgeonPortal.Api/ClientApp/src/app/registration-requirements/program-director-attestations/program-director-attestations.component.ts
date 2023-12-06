import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'abs-program-director-attestations',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    InputTextModule,
    ButtonModule,
  ],
  templateUrl: './program-director-attestations.component.html',
  styleUrls: ['./program-director-attestations.component.scss'],
})
export class ProgramDirectorAttestationsComponent {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  @Input() modalName!: string;

  constructor() {
    // constructor
  }

  programDirectorAttestationsFrom: FormGroup = new FormGroup({
    emailAddress: new FormControl('', [Validators.required, Validators.email]),
    programDirectorName: new FormControl('', [Validators.required]),
  });

  saveAttestation() {
    console.log('saveAttestation');
    console.log(
      'programDirectorAttestationsFrom',
      this.programDirectorAttestationsFrom.getRawValue()
    );
  }

  close() {
    this.closeDialog.emit({ action: this.modalName });
  }
}
