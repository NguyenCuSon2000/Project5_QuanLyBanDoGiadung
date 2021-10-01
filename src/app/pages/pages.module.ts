import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { CartComponent } from './cart/cart.component';
import { HomeComponent } from './home/home.component';
import { ProductDetailsComponent } from './product-details/product-details.component';


@NgModule({
  declarations: [
    HomeComponent,
    ProductListComponent,
    ProductDetailsComponent,
    AboutUsComponent,
    ContactUsComponent,
    CartComponent
  ],
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
      {
        path:'cart',
        component: CartComponent
      },
    ]), 
  ]
})
export class PagesModule { }
