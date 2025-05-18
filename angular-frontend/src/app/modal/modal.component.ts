import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-modal',
  standalone: false,
  templateUrl: './modal.component.html',
})
export class ModalComponent {
  @Input() modalType = "";
}
