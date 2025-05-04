import { Component, inject } from '@angular/core';
import { SharedService } from '../shared.service';
import { environment } from '../../assets/environments/environment';

@Component({
  selector: 'app-authentication',
  standalone: false,
  templateUrl: './authentication.component.html',
})
export class AuthenticationComponent {
  userName: string = '';
  password: string = '';
  disableButton: boolean = false;
  buttonColor:string = 'lightgrey';
  invalidUserName:boolean = false;
  invalidPassword: boolean = false;
  sharedService = inject(SharedService)

  handleOnChange(){
    this.invalidUserName = false
    if(this.userName == '' || this.password == ''){
      this.disableButton = true;
      this.buttonColor = 'lightgrey';
    }
    else if(this.userName !== '' && this.password !== ''){
      this.buttonColor = '#2563eb';
      this.disableButton = false;
    }
  }

  loginUser(){
    if(!this.sharedService.isInputValid(this.userName)){
      this.invalidUserName = true;
      this.disableButton = true;
      this.buttonColor = 'lightgrey';
    }
    else{
      const payload = {
        "email": this.userName,
        "password": this.password
      } 
      let url: string =  environment["authorizeURL"];
      const httpObserver = {
        next: (res: any) => {
          if (res.statusCode === 200) {
            console.log("Success:", res.data);
          }
        },
        error: (err: any) => {
          console.error("HTTP Error:", err);
        }
      };
      this.sharedService.httpPost(url, payload).subscribe(httpObserver);
    }
  }
}
