import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from 'src/app/core/base-component';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent extends BaseComponent implements OnInit {
  public page = 1;
  public pageSize = 5;
  public products:any;
  public totalItems:Number;
  constructor(private injector: Injector,private route: ActivatedRoute) {
    super(injector);
   }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.page = 1;
      this.pageSize = 5;
      let tenSanPham = params['tenSanPham'];
      this._api.post('/api/SanPham/search', { page: this.page, pageSize: this.pageSize, tenSanPham: tenSanPham }).takeUntil(this.unsubscribe).subscribe(res => {
        this.products = res.data;
        this.totalItems = res.totalItems;
        this.pageSize = res.pageSize;
      });
    });
  }
  addToCart(it) { 
    this._cart.addToCart(it);
    alert('Thêm thành công!'); 
  }
}
