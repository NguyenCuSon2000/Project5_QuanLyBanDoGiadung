import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from '../../../core/base-component';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import { ActivatedRoute, Router } from '@angular/router';

interface ProductsGroup {
  maNhom: string,
  tenNhom: string
}
@Component({
  selector: 'app-manage-categories',
  templateUrl: './manage-categories.component.html',
  styleUrls: ['./manage-categories.component.css']
})
export class ManageCategoriesComponent extends BaseComponent implements OnInit {
  public categories: any;
  public page = 1;
  public pageSize = 3;
  public totalItems:any;
  public productsGroup: ProductsGroup[];
  selectedProductGroup: ProductsGroup;
  constructor(injector: Injector,private route: ActivatedRoute, private router: Router) {
    super(injector);
   }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this._api.post('/api/LoaiSanPham/category-all-paginate',{page: this.page, pageSize: this.pageSize}).takeUntil(this.unsubscribe).subscribe(res => {
        this.categories = res.data;
        this.totalItems = res.totalItems;
        setTimeout(() => {
          this.loadScripts();
        });
      });
    });

    this._api.get('/api/NhomSanPham/get-all').takeUntil(this.unsubscribe).subscribe(res => {
      this.productsGroup = res;
    }); 
  }
  displayAdd: boolean = false;
  showAdd() {
      this.displayAdd = true;
  }
  loadPage(page) { 
    this._route.params.subscribe(params => {
      this._api.post('/api/LoaiSanPham/category-all-paginate', { page: page, pageSize: this.pageSize}).takeUntil(this.unsubscribe).subscribe(res => {
        this.categories = res.data;
        this.totalItems = res.totalItems;
        }, err => { });       
   });   
  }
}
