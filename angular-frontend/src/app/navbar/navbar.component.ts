import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  standalone: false,
  templateUrl: './navbar.component.html'
})
export class NavbarComponent {
  arr: string[] = ["Electronics", "Men's Fashion", "Women's Fashion", "Kid's Fashion", "Home & Kitchen", 
    "Baby", "Toys", "Sports & Outdoors", "Stationary", "Automotive", "Books & Media", "Food"]
}
