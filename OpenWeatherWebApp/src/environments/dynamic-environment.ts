declare var window: any;

export class DynamicEnvironment{
    public get environment(){

        this.checkConfig();
        return window.config.environment;    
    }

    private checkConfig(){
        if(window.config === null || window.config === undefined){
            var req = new XMLHttpRequest();
            req.open('GET', `assets/app-config.json?t=${Date.now()}`, false);
            req.send(null);
            window.config = JSON.parse(req.response);
        }
    }
}