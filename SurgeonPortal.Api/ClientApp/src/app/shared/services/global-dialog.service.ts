import {
  ComponentRef,
  Injectable,
  Type,
  ViewContainerRef,
} from '@angular/core';

@Injectable({ providedIn: 'root' })
export class GlobalDialogService {
  private _dialog!: any;
  private _componentRef!: ComponentRef<any>;

  public viewContainerRef?: ViewContainerRef;

  // setter used by other components to pass in a ViewContainerRef
  // TODO: [Joe] I'd love to make this purely standalone if possible
  set setViewContainerRef(vcr: ViewContainerRef) {
    this.viewContainerRef = vcr;
  }

  constructor() {
    // create initial dialog element
    this._dialog = document.createElement('sl-dialog');
    // set an id attribute so that we can get at the dialog from outside of the service if needed
    this._dialog.setAttribute('id', 'global-dialog');
    // set the aria-modal attribute to better support screen readers
    this._dialog.setAttribute('aria-modal', 'true');

    // set any event listeners that should be universal (and not type specific) here
    // this._dialog.addEventListener('sl-initial-focus', () => {
    //   console.log('sl-initial-focus');
    // });
  }

  // handle the Success/Error dialog
  showSuccessError(title: string, message: string, isSuccess: boolean) {
    // If another global dialog is open, close it
    if (document.body.contains(this._dialog)) {
      this.hide();
    }

    // create button text and icon based on success or error
    const buttonText = isSuccess ? 'Continue' : 'Okay';
    const icon = isSuccess ? 'fa-circle-check' : 'fa-circle-xmark';
    const iconClass = isSuccess ? 'success-icon' : 'error-icon';

    // create the dialog content
    this._dialog.innerHTML = `<div class="global-dialog flex flex-column justify-content-center align-items-center">
      <i class="fa-regular ${icon} ${iconClass}"></i>
      <h2 class="font-normal mt-3 mb-1">${title}</h2>
      <p class="font-bold mt-0 mb-1" *ngIf="message">${message}</p>
      <sl-button
        slot="footer"
        class="mt-5"
        style="width: 337px"
        variant="primary"
        >${buttonText}</sl-button
      >
    </div>`;

    // add the click event listener to the button and set initial focus
    const button = this._dialog.querySelector('sl-button');
    button.setAttribute('autofocus', '');
    button.addEventListener('click', () => {
      this.hide();
    });

    // add click event listener to the dialog overlay so that the close cleans up the DOM
    this._dialog.addEventListener('sl-request-close', () => {
      this.hide();
    });

    // set modal specific attributes (this can be done with param options)
    this._dialog.setAttribute('style', '--width: unset');
    this._dialog.setAttribute('no-header', 'true');

    // add the dialog to the DOM and show
    document.body.appendChild(this._dialog);
    this._dialog.show();
  }

  // handle the Confirmation dialog
  showConfirmation(title: string, message: string): Promise<boolean> {
    // If another global dialog is open, close it
    if (document.body.contains(this._dialog)) {
      this.hide();
    }

    // create dialog content inside of a promise so that click events can use a .then() after the promise resolves
    return new Promise((resolve) => {
      // create the dialog content
      this._dialog.innerHTML = `<div class="global-dialog flex flex-column justify-content-center align-items-center">
        <i class="fa-solid fa-triangle-exclamation confirmation-icon"></i>
        <h2 class="font-normal mt-3 mb-1">${title}</h2>
        <p class="font-bold mt-0 mb-1" *ngIf="message">${message}</p>
        <div class="mt-5" slot="footer">
        <sl-button id="declineButton" variant="default" outline style="width: 200px">
        <i slot="prefix" class="fa-solid fa-xmark"></i>
        No
        </sl-button>
        <sl-button id="confirmButton" variant="primary" style="width: 200px">
        <i slot="prefix" class="fa-solid fa-check"></i>
        Yes
        </sl-button>
        </div>
      </div>`;

      // add the click event listener to the decline button and set initial focus
      const declineButton = this._dialog.querySelector('#declineButton');
      declineButton.setAttribute('autofocus', '');
      declineButton.addEventListener('click', () => {
        this.hide();
        resolve(false);
      });

      // add the click event listener to the confirm button
      const confirmButton = this._dialog.querySelector('#confirmButton');
      confirmButton.addEventListener('click', () => {
        this.hide();
        resolve(true);
      });

      // add click event listener to the dialog overlay to prevent default close
      this._dialog.addEventListener('sl-request-close', (event: any) => {
        event.preventDefault();
      });

      // set modal specific attributes (this can be done with param options)
      this._dialog.setAttribute('style', '--width: unset');
      this._dialog.setAttribute('no-header', 'true');

      // add the dialog to the DOM and show
      document.body.appendChild(this._dialog);
      this._dialog.show();
    });
  }

  // handle the Confirmation dialog
  showConfirmationWithWarning(
    title: string,
    message: string,
    warning: string
  ): Promise<boolean> {
    // If another global dialog is open, close it
    if (document.body.contains(this._dialog)) {
      this.hide();
    }

    // create dialog content inside of a promise so that click events can use a .then() after the promise resolves
    return new Promise((resolve) => {
      const dialogLabel = `<div class="flex justify-content-between align-items-center" slot="label">
      <span class="text-2xl">${title}</span></div>`;
      const dialogContent = `<div class="global-dialog flex flex-column">
        <p class="mt-0 mb-2">${message}</p>
        <p class="mt-0 mb-2 text-warning"><i class="fa-solid fa-triangle-exclamation mr-2"></i>${warning}</p>
        <div class="mt-5 flex justify-content-end" slot="footer">
        <sl-button id="declineButton" variant="text" outline style="width: 100px">
        Cancel
        </sl-button>
        <sl-button id="confirmButton" variant="primary" style="margin-left: .5rem; width: 337px">
        Confirm
        </sl-button>
        </div>
      </div>`;

      // create the dialog content
      this._dialog.innerHTML = dialogLabel + dialogContent;

      // add the click event listener to the decline button and set initial focus
      const declineButton = this._dialog.querySelector('#declineButton');
      declineButton.setAttribute('autofocus', '');
      declineButton.addEventListener('click', () => {
        this.hide();
        resolve(false);
      });

      // add the click event listener to the confirm button
      const confirmButton = this._dialog.querySelector('#confirmButton');
      confirmButton.addEventListener('click', () => {
        this.hide();
        resolve(true);
      });

      // add click event listener to the dialog overlay to prevent default close
      this._dialog.addEventListener('sl-request-close', (event: any) => {
        event.preventDefault();
      });

      // set modal specific attributes (this can be done with param options)
      this._dialog.setAttribute('style', '--width: 50%');
      this._dialog.removeAttribute('no-header');

      // add the dialog to the DOM and show
      document.body.appendChild(this._dialog);
      this._dialog.show();
    });
  }

  // this is a proof of concept, if this works it should be broken out into specific modal types
  // currently replicating surgeon profile dialog from registration requirements page
  showComponentModal(
    component: Type<any>,
    title?: string | undefined,
    status?: string | undefined,
    saveButtonText?: string | undefined,
    cancelButtonText?: string | undefined,
    saveCallback?: () => void,
    cancelCallback?: () => void
  ) {
    // If another global dialog is open, close it
    if (document.body.contains(this._dialog)) {
      this.hide();
    }

    // if a ViewContainerRef was set by the component that is calling this service, create the component
    if (this.viewContainerRef) {
      this._componentRef = this.viewContainerRef.createComponent(component);
    }

    // initialize the dialog label content as empty
    let dialogLabel = ``;

    // if there is a title create the dialog label content
    if (title) {
      // if there is a status add it to the dialog label content
      if (status) {
        dialogLabel = `<div class="flex justify-content-between align-items-center" slot="label">
        <span class="text-2xl">${title}</span>
        <span class="text-base">
          Status:
          <span class="status ${status}">
            ${status.replace('-', ' ')}
          </span></span></div>`;
      }
      // else just add the title to the dialog label content
      else {
        dialogLabel = `<div class="flex justify-content-between align-items-center" slot="label">
        <span class="text-2xl">${title}</span></div>`;
      }
    }

    // initialize the dialog footer content as empty
    let dialogFooter = ``;
    // if there are any footer buttons create the dialog footer content
    if (saveButtonText || cancelButtonText) {
      dialogFooter = `<div class="flex justify-content-end" slot="footer">`;
      // if there is a cancel button, add it
      if (cancelButtonText) {
        dialogFooter =
          dialogFooter +
          `<sl-button id="cancelButton" variant="text">${cancelButtonText}</sl-button>`;
      }
      // if there is a save button, add it
      if (saveButtonText) {
        dialogFooter =
          dialogFooter +
          `<sl-button id="saveButton" variant="primary" style="width: 337px">
          ${saveButtonText}
          </sl-button>`;
      }
      dialogFooter = dialogFooter + `</div>`;
    }

    // create the dialog content
    // TODO: [Joe] should alternate content be shown if no ViewContainerRef was set?
    this._dialog.innerHTML = `${dialogLabel}
      <div class="global-dialog component-dialog">
      <ng-container></ng-container></div>
      ${dialogFooter}`;

    // if there is a cancel button
    const cancelButton = this._dialog.querySelector('#cancelButton');
    if (cancelButton) {
      // add the click event listener to the cancel button and set initial focus
      cancelButton.setAttribute('autofocus', '');
      cancelButton.addEventListener('click', () => {
        this.hide();
        // if there is a cancel callback, call it
        if (cancelCallback) {
          cancelCallback();
        }
      });
    }

    // if there is a save button
    const saveButton = this._dialog.querySelector('#saveButton');
    if (saveButton) {
      // if there is no other button, set initial focus
      if (!cancelButton) {
        saveButton.setAttribute('autofocus', '');
      }
      // add the click event listener to the save button
      saveButton.addEventListener('click', () => {
        this.hide();
        // if there is a save callback, call it
        if (saveCallback) {
          saveCallback();
        }
      });
    }

    // TODO: [Joe] need to handle the case where no component was created (no ViewContainerRef was set)
    if (this._componentRef) {
      const ngContainer = this._dialog.querySelector('ng-container');
      ngContainer.appendChild(this._componentRef.location.nativeElement);
    }

    // add click event listener to the dialog overlay to prevent default close
    this._dialog.addEventListener('sl-request-close', (event: any) => {
      event.preventDefault();
    });

    // set modal specific attributes (this can be done with param options)
    this._dialog.removeAttribute('no-header');
    this._dialog.setAttribute('class', 'status-header-dialog hide-close');
    this._dialog.setAttribute('style', '--width: 70%');

    // add the dialog to the DOM and show
    document.body.appendChild(this._dialog);
    this._dialog.show();
  }

  showLoading() {
    if (document.body.contains(this._dialog)) {
      this.hide();
    }

    // create the dialog content
    this._dialog.innerHTML = `<div class="global-dialog flex flex-column justify-content-center align-items-center">
    <sl-spinner style="font-size: 150px; --track-width: 10px;"></sl-spinner>
    </div>`;

    // add click event listener to the dialog overlay so that the close cleans up the DOM
    this._dialog.addEventListener('sl-request-close', () => {
      this.hide();
    });

    // set modal specific attributes (this can be done with param options)
    this._dialog.setAttribute('style', '--width: unset');
    this._dialog.setAttribute('no-header', 'true');
    this._dialog.setAttribute('class', 'loading');

    // add the dialog to the DOM and show
    document.body.appendChild(this._dialog);

    this._dialog.show();
  }

  // can be used to trigger a close from outside of dialog service
  closeOpenDialog() {
    if (document.body.contains(this._dialog)) {
      this.hide();
    }
  }

  private hide() {
    // if there is a component, destroy it
    if (this._componentRef) {
      this._componentRef.destroy();
    }

    // hide and remove the dialog
    if (document.body.contains(this._dialog)) {
      this._dialog.hide();
      document.body.removeChild(this._dialog);
    }
  }
}
