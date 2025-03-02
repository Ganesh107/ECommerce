import { Injectable } from "@angular/core";
import { HttpClient, HttpBackend, HttpHeaders } from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class SharedService { 
    private emailRegex: RegExp = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    private mobileRegex: RegExp = /^(\+91)?[6-9]\d{9}$/;
    private httpClient: HttpClient;

    constructor(private http: HttpClient, handler: HttpBackend){
        this.httpClient = new HttpClient(handler)
    }

    isInputValid(input: string){
        let isEmail = false;
        for (let c of input){
            if (isNaN(Number(c))){
                isEmail = true;
                break;
            }
        }

        if(isEmail){
            return this.emailRegex.test(input);
        }
        else{
            return this.mobileRegex.test(input);
        }
    }

    httpPost(url: string, payload: any, isTokenAvailable = false){
        let httpHeader: any;
        let token = 'Bearer ' + localStorage.getItem("jwtToken");

        httpHeader = new HttpHeaders({
            "Content-Type": "application/json;charset=UTF-8",
            "Accept": "application/json",
            "Cache-Control": "no-store",
            "Pragma": "no-cache",
            "X-Frame-Options": "SAMEORIGIN",
            "Authorization": token
        });

        return this.http.post(url, payload, { headers: httpHeader });
    }
}