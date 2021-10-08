import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { ProductListComponent } from './product-list/product-list.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [HomeComponent, ProductListComponent, AboutUsComponent, ContactUsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: HomeComponent
      },
      {
        path:'product-list/:id',
        component: ProductListComponent
      },
      {
        path:'about-us',
        component: AboutUsComponent
      },
      {
        path:'contact-us',
        component: ContactUsComponent
      },
    ]), 
  ]
})
export class MainModule { }
