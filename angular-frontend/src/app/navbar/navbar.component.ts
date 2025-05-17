import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-navbar',
  standalone: false,
  templateUrl: './navbar.component.html'
})
export class NavbarComponent {
  @ViewChild('nav') navContainer!: ElementRef;
  showLeftButton: boolean = false;
  
  handleScroll(direction: string): void{
    const container = this.navContainer.nativeElement;
    const limit = direction == 'left' ? 200 : -200;
    container.scrollBy({ left: limit, behavior: 'smooth' });
    this.showLeftButton = direction === 'left' ? true : false;
  }
}
