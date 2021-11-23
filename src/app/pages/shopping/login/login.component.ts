import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/core/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  frmLogin:FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error='';
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
    ) {
      // redirect to home if already logged in
      if (this.authenticationService.userValue) {
        this.router.navigate(['/auth/login']);
      }
    }
    
    ngOnInit(): void {
      this.frmLogin = this.formBuilder.group({
        TaiKhoan: ['', Validators.required],
        MatKhau: ['', Validators.required],
        remember: [''],
      });
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
      this.authenticationService
      .login(this.f.TaiKhoan.value, this.f.MatKhau.value)
      .pipe(first())
      .subscribe(
        (data) => {
          this.router.navigate([this.returnUrl]);
        },
        (error) => {
          alert("Đăng nhập thất bại");
          this.loading = false;   
        }
        );
      }

}
