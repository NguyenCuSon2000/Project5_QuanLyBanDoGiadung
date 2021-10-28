import { AfterViewInit, Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/core/base-component';

@Component({
  selector: 'app-left-slidebar',
  templateUrl: './left-slidebar.component.html',
  styleUrls: ['./left-slidebar.component.css']
})
export class LeftSlidebarComponent extends BaseComponent implements OnInit, AfterViewInit{
  
  constructor(injector: Injector, private router: Router) {
    super(injector);
  }
  ngOnInit() {}
  onLogin(){
    this.router.navigate(['/auth/login']);
  }
  onRegister(){
    this.router.navigate(['/auth/register']);
  }
  ngAfterViewInit() { 
    this.loadScripts(); 
  }

}