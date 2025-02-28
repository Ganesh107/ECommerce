import { Component } from '@angular/core';

@Component({
  selector: 'app-authentication',
  standalone: false,
  templateUrl: './authentication.component.html',
})
export class AuthenticationComponent {
  userName: string = ''
  password: string = ''
  disableButton: boolean = false
  buttonColor:string = 'lightgrey'
  inValidUserNameError: string = ''
  inValidPasswordError: string = ''

  handleOnChange(){
    if(this.userName == '' || this.password == ''){
      this.disableButton = true
      this.buttonColor = 'lightgrey'
    }
    else if(this.userName !== '' && this.password !== ''){
      this.buttonColor = '#2563eb'
      this.disableButton = false
    }
  }

  loginUser(){
    console.log('clicked', this.userName, this.password)
  }
}
