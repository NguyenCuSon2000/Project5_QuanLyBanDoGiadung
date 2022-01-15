import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AfterViewInit, Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from '../../../core/base-component';


@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrls: ['./contact-us.component.css']
})
export class ContactUsComponent extends BaseComponent implements OnInit, AfterViewInit {
  frmContact: FormGroup;
  loading = false;
  constructor(
    injector: Injector,
    private formBuilder: FormBuilder,
    private http: HttpClient
    ) {
      super(injector);
    }

    ngOnInit(): void {
      window.scroll(0,0);
      this.frmContact = this.formBuilder.group({
        ToEmail: ['', Validators.required],
        Subject: ['', Validators.required],
        Body: ['', Validators.required],
      })
    }
    ngAfterViewInit() { 
      this.loadScripts(); 
    }
    
    onSend(value:any){
      if(this.frmContact.invalid) return;
      debugger;
      this._api.post('/api/Email/Send', {
        ToEmail: value.ToEmail,
        Subject: value.Subject,
        Body: value.Body,
      }).takeUntil(this.unsubscribe).subscribe(res => {
        alert("Liên hệ thành công");
      }, err => {
        alert('Có lỗi trong quá trình thực hiện');
      })
    }
  }
  