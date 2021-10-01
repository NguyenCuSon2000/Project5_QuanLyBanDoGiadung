import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './shared/component/page-not-found/page-not-found.component';


const routes: Routes = [
  { path: '', loadChildren: () => import('./pages//pages.module').then(m => m.PagesModule)} ,
  {
    path: '**',
    component: PageNotFoundComponent,
    redirectTo: ''
  },  
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
