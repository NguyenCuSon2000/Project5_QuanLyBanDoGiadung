import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from '../../../core/base-component';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import { AuthenticationService } from 'src/app/core/authentication.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent extends BaseComponent implements OnInit {
  public categories:any;
  public products_group:any;
  public frmSearch:any;
  total:any;
  constructor(private fb: FormBuilder, private router: Router, injector: Injector, private authenticationService: AuthenticationService) {
    super(injector);
  }
  
  ngOnInit(): void {
    this.frmSearch = this.fb.group({
      'tenSanPham': ['']
    });
    this._api.get('/api/LoaiSanPham/get-all').takeUntil(this.unsubscribe).subscribe(res => {
      this.categories = res;
    }); 
    
    this._api.get('/api/NhomSanPham/get-all').takeUntil(this.unsubscribe).subscribe(res => {
      this.products_group = res;
    }); 
    
    this._cart.items.subscribe((res) => {
      this.total = res? res.length:0;
    });
  }
  search(){
    this.router.navigate(['/search/'+ this.frmSearch.get('tenSanPham').value])
  }
  onLogOut(){
    this.authenticationService.logout();
    alert("Bạn đã đăng xuất thành công");    
  }
}
