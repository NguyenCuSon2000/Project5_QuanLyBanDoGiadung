import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/core/authentication.service';
import { BaseComponent } from 'src/app/core/base-component';
import { MustMatch } from 'src/app/core/helper/must-match.validator';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent implements OnInit {
  frmLogin:FormGroup;
  frmRegister:FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error='';
  constructor(
    injector: Injector,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService ) {
      // redirect to home if already logged in
      super(injector)
      if (this.authenticationService.userValue) {
        this.router.navigate(['/auth/login']);
      }
    }
    
    ngOnInit(): void {
      this.frmLogin = this.formBuilder.group({
        TaiKhoan: ['', Validators.required],
        MatKhau: ['', Validators.required],
        NhapLaiMatKhau: ['', Validators.required],
        remember: [''],
      },{
        validator: MustMatch('MatKhau', 'NhapLaiMatKhau')
      });
      
      this.frmRegister = this.formBuilder.group({
        HoTen: ['', Validators.required],
        NgaySinh: ['', Validators.required],
        DiaChi: ['', Validators.required],
        GioiTinh: ['', Validators.required],
        Email: ['', [Validators.required, Validators.email]],
        TaiKhoan: ['', Validators.required],
        MatKhau: ['', Validators.required],
        NhapLaiMatKhau: ['', Validators.required],
      },{
        validator: MustMatch('MatKhau', 'NhapLaiMatKhau')
      })
      
      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }
    
    get f() {
      return this.frmLogin.controls;
    }
    
    onSubmit(){
      this.submitted = true;
      
      // stop here if form is invalid
      if (this.frmLogin.invalid) {
        return;
      }
      this.loading = true;
      this.authenticationService.login(this.f.TaiKhoan.value, this.f.MatKhau.value).pipe(first()).subscribe((data) => {
        this.router.navigate([this.returnUrl]);
      }, (error) => {
        alert("Đăng nhập thất bại");
        this.loading = false;   
      });
    }

    onRegister(value:any){
      if(this.frmRegister.invalid) return;
      this._api.post('/api/NguoiDung/create-user', 
      {
        HoTen:value.HoTen, 
        NgaySinh:value.NgaySinh, 
        DiaChi:value.DiaChi, 
        GioiTinh:value.GioiTinh, 
        Email:value.Email, 
        TaiKhoan:value.TaiKhoan, 
        MatKhau: value.MatKhau,
        role: 'User'
      }).takeUntil(this.unsubscribe).subscribe(res => {
        alert('Đăng ký thành công');
        this.router.navigate(['/shopping/login']);
      }, err => { 
        alert('Có lỗi trong quá trình thực hiện');
      });    
    }

    public txtMatKhauCheckValidator(control) {
      var filteredStrings = { search: control.value, select: '@#!$%&*' }
      var result = (filteredStrings.select.match(new RegExp('[' +
      filteredStrings.search + ']', 'g')) || []).join('');
      if ((control.value.length < 6 || result == '') && control.value) {
        return { nameX: true };
      }
    }
  }
  