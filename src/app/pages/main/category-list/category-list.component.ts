import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../../core/base-component';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent extends BaseComponent implements OnInit {
  public list_category: any;
  public page = 1;
  public pageSize = 4;
  public totalItems:any;
  constructor(injector: Injector,private route: ActivatedRoute, private router: Router) { 
    super(injector);
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let id = params['id'];
      let name = params['name'];
      this._api.post('/api/LoaiSanPham/get-category-by-group',{page: this.page, pageSize: this.pageSize, manhom: id}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list_category = res.data;
        this.totalItems = res.totalItems;
        debugger;
        setTimeout(() => {
          this.loadScripts();
        });
      });
    });
  }

  loadPage(page) { 
    this._route.params.subscribe(params => {
      let id = params['id'];
      let name = params['name'];
      this._api.post('/api/LoaiSanPham/get-category-by-group', { page: page, pageSize: this.pageSize, manhom: id}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list_category = res.data;
        this.totalItems = res.totalItems;
        }, err => { });       
   });   
  }
}
