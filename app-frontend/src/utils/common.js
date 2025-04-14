import { authorizeurl } from "./constants"

export function httpPost(payload, url){
    const jsonPayload = JSON.stringify(payload)
    const metaData = {
        method: "POST",
        headers:{
            "Content-Type": "application/json"
        },
        body: jsonPayload
    }
    return fetch(url, metaData)
}

export function isInputMobileNumber(input){
    if(input === '') return false
    for (const char of input) {
        if(isNaN(char))
            return false
    }
    return true
}

export function validateLoginInput(input, isEmail){
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    const mobileRegex = /^(\+91)?[6-9]\d{9}$/;
    let res = false
    if(isEmail)
        res = emailRegex.test(input.email)
    else
        res = mobileRegex.test(input.email)
    return res
}
