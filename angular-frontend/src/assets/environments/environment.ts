let finalEnv: any = {
    production: false
}

function loadJSON(filePath: any){
    const json: any = loadTextFile(filePath);
    return JSON.parse(json)
}

function loadTextFile(filePath: any) {
    const xmlhttp = new XMLHttpRequest();
    xmlhttp.open("GET", filePath+'?_='+new Date().getTime(),false);
    xmlhttp.send();
    if (xmlhttp.status == 200) {
      return xmlhttp.responseText;
    }
    else {
      return null;
    }
  }

function loadVariables(finalEnv: any){
    let env: any = loadJSON('assets/configuration/config.json');
    for(let key in env){
        finalEnv[key] = env[key]
    }
    return finalEnv;
}

export const environment = loadVariables(finalEnv);