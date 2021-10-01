import { AfterViewInit, Component, Injector, OnInit, Renderer2 } from '@angular/core';
import { BaseComponent } from 'src/app/core/base-component';
import { HousingService } from 'src/app/services/housing.service';

import { ICategory } from './ICategory.interface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent extends BaseComponent  implements OnInit, AfterViewInit {
  
  categories: Array<ICategory>;
  constructor(injector: Injector, private housingService: HousingService ) {
    super(injector);
  }
  ngOnInit(): void {
  window.scroll(0,0);
  this.housingService.getAllCategories().subscribe(
    data=>{
      this.categories = data;
    }, error => {
      console.log(error);
    }
    );
    
  }
  
  ngAfterViewInit() { 
    this.loadScripts(); 
  }
}
  