import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SliderComponent } from './slider/slider.component';
import { ProductSliderComponent } from './product-slider/product-slider.component';
import { BlogSliderComponent } from './blog-slider/blog-slider.component';
import { HomeComponent } from './home.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

export const homeRoute: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
];

@NgModule({
  declarations: [
    HomeComponent,
    SliderComponent,
    ProductSliderComponent,
    BlogSliderComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(homeRoute),
    HttpClientModule,
  ]
})
export class HomeModule { }
