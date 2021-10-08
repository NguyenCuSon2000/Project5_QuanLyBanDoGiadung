import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout/checkout.component';
import { RouterModule } from '@angular/router';
import { CartComponent } from './cart/cart.component';



@NgModule({
  declarations: [
    CheckoutComponent,
    CartComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: 'cart',
        component: CartComponent
      },
      {
        path:'checkout',
        component: CheckoutComponent
      },
    ])
  ]
})
export class ShoppingModule { }
