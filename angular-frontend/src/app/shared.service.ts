import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class SharedService { 
    public emailRegex: RegExp = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/
    public mobileRegex: RegExp = /^(\+91)?[6-9]\d{9}$/
    
    isInputValid(input: string){
        
    }
}