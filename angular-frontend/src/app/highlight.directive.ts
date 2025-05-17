import { Directive, ElementRef, HostListener, inject, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appHighlight]',
  standalone: false
})
export class HighlightDirective {
  private ele = inject(ElementRef)
  private renderer = inject(Renderer2)

  @HostListener('mouseenter')
  onMouseEnter(){
    this.renderer.addClass(this.ele.nativeElement, 'border-b-2')
  }

  @HostListener('mouseleave')
  onMouseleave(){
    this.renderer.removeClass(this.ele.nativeElement, 'border-b-2')
  }
}
