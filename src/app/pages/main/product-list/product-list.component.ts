import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../../core/base-component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent extends BaseComponent implements OnInit {
  public list_item: any;
  public page = 1;
  public pageSize = 3;
  public totalItems:any;
  public x:any;
  public quanlity: Number[]|any;

  
  constructor(injector: Injector,private route: ActivatedRoute, private router: Router) { 
    super(injector);
    this.quanlity = [5, 10, 15];
  }

  ngOnInit(): void {
    window.scroll(0,0);
    this.route.params.subscribe(params => {
      let id = params['id'];
      this._api.post('/api/SanPham/get-product-by-category',{page: this.page, pageSize: this.pageSize, maloai: id}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list_item = res.data;
        this.totalItems = res.totalItems;
        setTimeout(() => {
          this.loadScripts();
        });
      });
    });
  }

  loadPage(page) { 
    this._route.params.subscribe(params => {
      let id = params['id'];
      this._api.post('/api/SanPham/get-product-by-category', { page: page, pageSize: this.pageSize, maloai: id}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list_item = res.data;
        this.totalItems = res.totalItems;
        }, err => { });       
   });   
  }

  addToCart(it) { 
    this._cart.addToCart(it);
    alert('Thêm thành công!'); 
  }
}
