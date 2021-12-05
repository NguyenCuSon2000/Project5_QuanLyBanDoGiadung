import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '../../../core/base-component';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent extends BaseComponent implements OnInit {
  public product:any;
  constructor(injector: Injector,private route: ActivatedRoute) { 
    super(injector);
  }

  ngOnInit(): void {
    window.scroll(0,0);
     this.route.params.subscribe(params => {
      let id = params['id'];
      this._api.get('/api/SanPham/get-by-id/' + id).takeUntil(this.unsubscribe).subscribe(res => {
        this.product = res;
        setTimeout(() => {
          this.loadScripts();
        });
      });
    });
  }
  addToCart(it) { 
    this._cart.addToCart(it);
    alert('Thêm thành công!'); 
  }
}
