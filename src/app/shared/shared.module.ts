import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from './component/header/header.component';
import { FooterComponent } from './component/footer/footer.component';
import { PageNotFoundComponent } from './component/page-not-found/page-not-found.component';
import { AddCartSectionComponent } from './component/add-cart-section/add-cart-section.component';
import { WishlishComponent } from './component/wishlish/wishlish.component';
import { AddCartModalComponent } from './component/add-cart-modal/add-cart-modal.component';
import { QuickViewModalComponent } from './component/quick-view-modal/quick-view-modal.component';
import { ReactiveFormsModule } from '@angular/forms';




@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    PageNotFoundComponent,
    AddCartSectionComponent,
    WishlishComponent,
    AddCartModalComponent,
    QuickViewModalComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule
  ],
  exports: [
    HeaderComponent,
    FooterComponent
  ]
})
export class SharedModule { }
