import { AfterViewInit, Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponent } from '../../../core/base-component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent extends BaseComponent implements OnInit, AfterViewInit {

  constructor(injector: Injector, private router: Router) { 
    super(injector);
  }
  ngOnInit(): void {
    window.scroll(0,0);
  }
  
  onHome() {
    this.router.navigate(['/'])
  }
  ngAfterViewInit() { 
    this.loadScripts(); 
   }
}
