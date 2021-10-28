import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopbarComponent } from './components/topbar/topbar.component';
import { LeftSlidebarComponent } from './components/left-slidebar/left-slidebar.component';
import { RouterModule } from '@angular/router';


@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  declarations: [TopbarComponent, LeftSlidebarComponent],
  exports: [
    TopbarComponent, LeftSlidebarComponent
  ]
})
export class SharedModule { }
