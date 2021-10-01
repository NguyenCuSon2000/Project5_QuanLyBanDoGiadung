import { AfterViewInit, Injector, Renderer2 } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

export class BaseComponent  {
    // public route: ActivatedRoute;
    public renderer: Renderer2
    constructor(injector: Injector) {
        
        this.renderer = injector.get(Renderer2);
    }
     
    public loadScripts() {
        this.renderExternalScript('assets/js/main.js').onload = () => {
        }
      }
    public renderExternalScript(src: string): HTMLScriptElement {
        const script = document.createElement('script');
        script.type = 'text/javascript';
        script.src = src;
        script.async = true;
        script.defer = true;
        this.renderer.appendChild(document.body, script);
        return script;
      }
}
