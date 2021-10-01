import { Component, Input, OnInit } from '@angular/core';
import { ICategory } from '../ICategory.interface';

@Component({
  selector: 'app-product-slider',
  templateUrl: './product-slider.component.html',
  styleUrls: ['./product-slider.component.css']
})
export class ProductSliderComponent implements OnInit {
  @Input() categories:ICategory
  constructor() { }

  ngOnInit(): void {
  }

}
