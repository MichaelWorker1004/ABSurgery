import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class GlobalToastService {
  private showToast(
    message: string,
    type: string,
    duration: number,
    containerId: string
  ) {
    const container = document.querySelector(containerId);
    let toastIcon = 'info-circle';
    let toastType = 'primary';
    if (type === 'success') {
      toastIcon = 'check2-circle';
      toastType = 'success';
    } else if (type === 'warning') {
      toastIcon = 'exclamation-triangle';
      toastType = 'warning';
    } else if (type === 'error') {
      toastIcon = 'exclamation-octagon';
      toastType = 'danger';
    }
    const toast = document.createElement('sl-alert');
    toast.setAttribute('variant', toastType);
    if (duration > 0) {
      toast.setAttribute('duration', duration.toString());
    }
    toast.setAttribute('open', 'true');
    toast.setAttribute('closable', 'true');

    toast.innerHTML = `<sl-icon slot="icon" name="${toastIcon}"></sl-icon>
        <p class="toast__title">${type}</p>
        <p class="toast__message">${message}</p>`;

    if (container) {
      container.appendChild(toast);
    } else {
      document.body.appendChild(toast);
    }
  }

  showInfo(message: string, duration = 5000, containerId = '#toast-container') {
    this.showToast(message, 'info', duration, containerId);
  }

  showSuccess(
    message: string,
    duration = 5000,
    containerId = '#toast-container'
  ) {
    this.showToast(message, 'success', duration, containerId);
  }

  showWarning(
    message: string,
    duration = 5000,
    containerId = '#toast-container'
  ) {
    this.showToast(message, 'warning', duration, containerId);
  }

  showError(
    message: string,
    duration = 5000,
    containerId = '#toast-container'
  ) {
    this.showToast(message, 'error', duration, containerId);
  }

  showCustom(
    message: string,
    type: string,
    duration = 5000,
    containerId = '#toast-container'
  ) {
    this.showToast(message, type, duration, containerId);
  }
}
