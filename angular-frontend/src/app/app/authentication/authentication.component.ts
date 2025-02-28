import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-authentication',
  standalone: false,
  templateUrl: './authentication.component.html',
})
export class AuthenticationComponent implements OnInit {
  @Input() loginImage: any;
  ngOnInit(): void {
    this.loginImage = 'src/assets/images/login.png'
  }
}
