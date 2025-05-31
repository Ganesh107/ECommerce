import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-homepage',
  standalone: false,
  templateUrl: './homepage.component.html'
})
export class HomepageComponent implements OnInit, OnDestroy{
  currentIndex = 0;
  intervalId: any;
  carouselImages: string[] = [
    "assets/images/carousel1.jpg",
    "assets/images/carousel2.jpg",
    "assets/images/carousel1.jpg",
    "assets/images/carousel2.jpg"
  ]

  ngOnInit(): void {
    this.startCarouselAutoPlay()
  }

  startCarouselAutoPlay(): void{
    this.intervalId = setInterval(() => {
      this.next();
    }, 3000);
  }

  next(): void{
    this.currentIndex = (this.currentIndex + 1) % this.carouselImages.length;
  }

  prev(): void{
    this.currentIndex = ((this.currentIndex - 1) + this.carouselImages.length) % this.carouselImages.length;
  }

  ngOnDestroy(): void {
    clearInterval(this.intervalId);
  }
}
