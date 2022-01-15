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
  public products_group: any;
  public arrays: any;
  public page = 1;
  public pageSize = 15;
  public totalItems:any;
  
  
  
  constructor(injector: Injector,private route: ActivatedRoute, private router: Router) { 
    super(injector);
    
  }
  
  ngOnInit(): void {
    window.scroll(0,0);
    this.route.params.subscribe(params => {
      let id = params['id'];
      this._api.post('/api/SanPham/get-product-by-category',{page: this.page, pageSize: this.pageSize, maloai: id}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list_item = res.data;
        this.arrays = res.data;
        this.totalItems = res.totalItems;
        setTimeout(() => {
          this.loadScripts();
        });
      });
    });
    this.getGroupProduct();
  }

  getGroupProduct(){
    this._api.get('/api/NhomSanPham/get-all').takeUntil(this.unsubscribe).subscribe(res => {
      this.products_group = res;
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


  checkboxArray: any = [
    {
      type: "checkbox",
      price: 500000
    },
    {
      type: "checkbox",
      price: 1000000
    },
    {
      type: "checkbox",
      price: 3000000
    },
    {
      type: "checkbox",
      price: 5000000
    },
    {
      type: "checkbox",
      price: 10000000
    },
  ]
  
  tempArray:any = [];
  newArray:any = [];
  onChange(event:any){
    if(event.target.checked){
      this.tempArray = this.arrays.filter((e:any) => e.giaBan >= event.target.value);
      this.list_item = [];
      this.newArray.push(this.tempArray);
      for (let i = 0; i < this.newArray.length; i++) {
        var firstArray = this.newArray[i];
        for (let i = 0; i < firstArray.length; i++) {
          var obj = firstArray[i];
          this.list_item.push(obj);
        }
      }
    }
    else{
      this.tempArray = this.list_item.filter((e:any) => e.giaBan < event.target.value);
      this.newArray = [];
      this.list_item = [];
      this.newArray.push(this.tempArray);
      for (let i = 0; i < this.newArray.length; i++) {
        var firstArray = this.newArray[i];
        for (let i = 0; i < firstArray.length; i++) {
          var obj = firstArray[i];
          this.list_item.push(obj);
        }
      }
    }
  }
  
}
