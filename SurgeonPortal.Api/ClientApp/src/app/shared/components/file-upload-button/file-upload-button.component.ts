import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'abs-file-upload-button',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './file-upload-button.component.html',
  styleUrls: ['./file-upload-button.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class FileUploadButtonComponent {
  @Input() inputId!: string;
  @Input() inputName!: string;
  @Input() buttonText!: string;

  @Output() fileUploaded: EventEmitter<any> = new EventEmitter();

  triggerFileUpload(inputId: string) {
    const fileInput: any | null = document.querySelector('#' + inputId);
    fileInput?.click();
  }
  uploadFile(event: any) {
    const file: File = event.target.files[0];
    const uploadFile = {
      file: file,
      fileName: file.name,
      fileType: this.covertMimeToIcon(file.type),
      uploadDate: new Date(),
    };

    this.fileUploaded.emit({ file: uploadFile, fileInput: this.inputId });
  }

  // TODO: [Joe] add more supported file types (or refine to a list of specifically allowed file types)
  // TODO: [Joe] consider moving out of the upload button component so that it can be used where file icons are being displayed
  covertMimeToIcon(mimeType: string) {
    switch (mimeType) {
      case 'application/pdf':
        return 'pdf';
      case 'image/jpeg':
      case 'image/png':
      case 'image/gif':
        return 'image';
      case 'audio/mpeg':
      case 'audio/mp3':
      case 'audio/wav':
        return 'audio';
      case 'video/mp4':
      case 'video/webm':
      case 'video/ogg':
        return 'video';
      case 'application/msword':
        return 'word';
      default:
        return 'lines';
    }
  }
}
