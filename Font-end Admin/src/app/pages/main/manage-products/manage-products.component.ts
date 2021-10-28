import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from '../../../core/base-component';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import { ActivatedRoute, Router } from '@angular/router';
import { SanPham } from 'src/app/shared/Models/SanPham';
import { AbstractControl, ReactiveFormsModule, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { NgForm } from '@angular/forms';
interface Category {
  maLoai: string,
  tenLoai: string
}
interface Brand {
  maHang: string,
  tenHang: string
}
@Component({
  selector: 'app-manage-products',
  templateUrl: './manage-products.component.html',
  styleUrls: ['./manage-products.component.css']
})
export class ManageProductsComponent extends BaseComponent implements OnInit {
  public product: any;
  public products: any;
  public page = 1;
  public pageSize = 10;
  public totalItems: any;
  frmCheckOut!: FormGroup;
  public categories: Category[];
  selectedCategory: Category;
  public brands: Brand[];
  selectedBrand: Brand;
  file1: any = null;
  image1?: string;
  formProduct: FormBuilder;
  constructor(injector: Injector, private route: ActivatedRoute, private router: Router) {
    super(injector);
    this.product = new SanPham();
  }
  isEdit: boolean = false;
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this._api.post('/api/SanPham/product-all-paginate', { page: this.page, pageSize: this.pageSize }).takeUntil(this.unsubscribe).subscribe(res => {
        this.products = res.data;
        this.totalItems = res.totalItems;
        setTimeout(() => {
          this.loadScripts();
        });
      });
    });

    this._api.get('/api/LoaiSanPham/get-all').takeUntil(this.unsubscribe).subscribe(res => {
      this.categories = res;
    });

    this._api.get('/api/HangSanPham/get-all').takeUntil(this.unsubscribe).subscribe(res => {
      this.brands = res;
    });

  }
  displayAdd: boolean = false;
  showAdd() {
    this.displayAdd = true;


  }
  CancelEdit() {
    this.product = "";
    this.displayAdd = false;
    this.isEdit = false
  }
  onChange1(event: any) {
    this.file1 = event.target.files[0];
    var reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (e: any) => {
      this.image1 = e.target.result;
    }
  }
  edit(product: SanPham) {
    this.product = product;
    console.log(this.product);
    //console.log(this.product.maSanPham);
  
    this.isEdit = true;

  }
  loadPage(page) {
    this._route.params.subscribe(params => {
      this._api.post('/api/SanPham/product-all-paginate', { page: page, pageSize: this.pageSize }).takeUntil(this.unsubscribe).subscribe(res => {
        this.products = res.data;
        this.totalItems = res.totalItems;
      }, err => { });
    });
  }
  resetform(form) {
    if (form != null)
      form.resetForm();
  }
  AddNewProduct(form: NgForm) {

    console.log(form.value);
    try {
      this._api.post('/api/SanPham/create-product', form.value).takeUntil(this.unsubscribe).subscribe(res => {
        console.log(" add ok");
        this.resetform(form);
      }, err => { console.log(err); });

      // product.photosmall=this.file2.name;
      // this._api.addnews(news).subscribe(res=>{
      // this.news?.push(res);
      // alert("Thêm Thành Công");
      //  this.router.navigate(['/news_management']);
      // });
    }
    catch (error) {
      console.log(error);
    }
  }
  SaveData() {
  
    try {
      console.log(this.product);
     
      this._api.post('/api/SanPham/update-product',this.product).takeUntil(this.unsubscribe).subscribe(res => {
        console.log("edit ok");
         this.isEdit = false;
      }, err => { console.log(err); });
     
      // product.photosmall=this.file2.name;
      // this._api.addnews(news).subscribe(res=>{
      // this.news?.push(res);
      // alert("Thêm Thành Công");
      //  this.router.navigate(['/news_management']);
      // });
    }
    catch (error) {
      console.log(error);
    }
  }

}
