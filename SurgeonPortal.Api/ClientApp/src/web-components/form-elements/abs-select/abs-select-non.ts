export class AbsSelectNon extends HTMLElement {
  constructor() {
    super();
    // this.shadow = this.attachShadow({ mode: 'open' });
    this.attachShadow({ mode: 'open' });
    if (this.shadowRoot) {
      this.shadowRoot.innerHTML = `<div>TEST</div><sl-select><slot></slot></sl-select>`;
    }
  }
}

customElements.define('abs-select-non', AbsSelectNon);
