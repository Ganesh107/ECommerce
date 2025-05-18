import { Component, ElementRef, ViewChild, Renderer2, inject, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-navbar',
  standalone: false,
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnDestroy{
  private renderer = inject(Renderer2)
  private debounceTimeOut: any;
  @ViewChild('nav') navContainer!: ElementRef;
  @ViewChild('modal') modal!: ElementRef;
  showLeftButton: boolean = false;
  displayModal: boolean = false;
  isHovering = false;
  modalCategory = "";

  handleScroll(direction: string): void{
    const container = this.navContainer.nativeElement;
    const limit = direction == 'left' ? 200 : -200;
    container.scrollBy({ left: limit, behavior: 'smooth' });
    this.showLeftButton = direction === 'left' ? true : false;
  }

  showModal(state: boolean, event: MouseEvent, currCategory: HTMLElement | ElementRef, category: string): void{
    clearTimeout(this.debounceTimeOut); // Debouncing to avoid flickering
    const nativeElement = currCategory instanceof ElementRef ? currCategory.nativeElement : currCategory;
    const currSelection = nativeElement;
    if(state)
      this.renderer.addClass(currSelection, 'border-b-2');

    if(this.isHoveringOverModal(event)){
      return;
    };
    
    if(!state && !this.isHovering){
      this.displayModal = false;
      this.renderer.removeClass(currSelection, 'border-b-2');
      return;
    }

    this.debounceTimeOut = setTimeout(() => {
      this.displayModal = true;
      this.modalCategory = category;
    }, 200);
  }

  handleHover(val: boolean): void{
    this.isHovering = val;
    if(!val){
      this.displayModal = false;
      const navEle = this.navContainer.nativeElement.children;
      for (const element of navEle) {
        this.renderer.removeClass(element, 'border-b-2');
      }
    }
  }

  isHoveringOverModal(event: MouseEvent): boolean{
    const relatedTarget = event.relatedTarget as Node;
    return this.modal.nativeElement.contains(relatedTarget)
  }

  ngOnDestroy(): void {
    clearTimeout(this.debounceTimeOut);
  }

}
