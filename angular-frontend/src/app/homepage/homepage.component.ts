import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { SharedService } from '../shared.service';
import { environment } from '../../assets/environments/environment';

@Component({
  selector: 'app-homepage',
  standalone: false,
  templateUrl: './homepage.component.html'
})
export class HomepageComponent implements OnInit, OnDestroy{
  currentIndex = 0;
  intervalId: any;
  displayBanner = true;
  carouselImages: string[] = [
    "assets/images/carousel1.jpg",
    "assets/images/carousel2.jpg",
    "assets/images/carousel3.gif",
    "assets/images/carousel4.gif",
    "assets/images/carousel5.gif"
  ]
  products:[] = [];
  sharedService = inject(SharedService);

  ngOnInit(): void {
    this.startCarouselAutoPlay()
    this.sharedService.httpGet(environment["productServiceURL"] + '/GetProducts').subscribe({
      next: (res: any) => {
        if (res.statusCode === 200) {
          this.products = res.data;
        } else {
          console.error("Error fetching products:", res.message);
        }
      },
      error: (err: any) => {
        console.error("HTTP Error:", err);
      }
    })
  }

  startCarouselAutoPlay(): void{
    this.intervalId = setInterval(() => {
      this.next(false);
    }, 4000);
  }

  next(isButtonClick: boolean): void{
    if(isButtonClick){
      clearInterval(this.intervalId); // Avoid flickering
      this.startCarouselAutoPlay();
    } 
    this.currentIndex = (this.currentIndex + 1) % this.carouselImages.length;
  }

  prev(isButtonClick: boolean): void{
     if(isButtonClick){
      clearInterval(this.intervalId);
      this.startCarouselAutoPlay();
    }
    this.currentIndex = ((this.currentIndex - 1) + this.carouselImages.length) % this.carouselImages.length;
  }

  ngOnDestroy(): void {
    clearInterval(this.intervalId);
  }
}
