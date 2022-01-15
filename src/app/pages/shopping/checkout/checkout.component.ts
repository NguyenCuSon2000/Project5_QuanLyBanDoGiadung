import { Component, Injector, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { DiachiService } from '../../../core/service/diachi.service';
import { BaseComponent } from '../../../core/base-component';
import { first } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/core/authentication.service';
import { Router } from '@angular/router';

interface TinhTP {
  maTP: string,
  Ten: string,
  Loai: string
}

interface QH {
  maQH: string,
  Ten: string,
  Loai: string,
  ViTri: string,
  MaTinh: string
}

interface XP {
  maXP: string,
  Ten: string,
  Loai: string,
  ViTri: string,
  MaQH: string
}


@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent extends BaseComponent implements OnInit {
  public frmCheckout: FormGroup;
  namePattern= "^([a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌÓỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳýỵỷỹ\s]+)$";
  mobilePattern="^[0-9 _-]{10,12}$";
  public items:any;
  public products:any[];
  public arrSoLuong = [];
  public total:number;
  public ngayDat:any;
  public diachi: string;
  public user:any;
  public i:number;
  constructor(
    injector: Injector,
    private _DCService: DiachiService,
    private authenticationService: AuthenticationService,
    private router: Router) { 
      super(injector);
    }
    
    public TinhTP: TinhTP[] | any;
    public QH: QH[] | any;
    public XP: XP[] | any;
    
    public selectedTinhTP: TinhTP ;
    public selectedQH: QH ;
    public selectedXP: XP ;
    
    ngOnInit(): void {
      window.scrollTo(0,0);
      this.i = 0;
      var today = new Date();
      this.ngayDat = today;
      this.frmCheckout = new FormGroup({
        txtHoTen: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
        tinh: new FormControl('', [Validators.required]),
        huyen: new FormControl('', [Validators.required]),
        xa: new FormControl('', [Validators.required]),
        txtDiaChi: new FormControl('', [Validators.required]),
        txtSDT: new FormControl('', [Validators.required, Validators.pattern(this.mobilePattern)]),
        txtEmail: new FormControl('', [this.CustomEmailValidator]),
      });
      
      this._cart.items.subscribe((res) => {
        this.items = res;
        // this.products = res;
        this.total = 0;
        for(let x of this.items){ 
          x.soLuong = +x.quantity;
          x.money = x.quantity * x.giaBan;
          this.total += x.quantity * x.giaBan;
        } 
      });
      
      this._DCService
      .GetTinhTP()
      .pipe(first())
      .subscribe((res) => {
        this.TinhTP = res;
        console.log(this.TinhTP); 
      });
      
      this.authenticationService.user.subscribe((res) => {
        this.user = res;
        console.log(this.user);
      })
      
      // let keys: any[];
      // for (let i: number = 0; i < this.products.length; i++) {
      //   for (const key in this.products[i]) {
      //     console.log(key);
      //     console.log(this.products[i][key]);
      //   };
      // }
      // this.products.forEach(obj => {
      //   Object.entries(obj).forEach(([key, value]) => {
      //     console.log(value);
      //   });
      // });
      this.products.forEach((obj) => {
        "<tr>" +
        "<td>"+console.log(obj.tenSanPham)+"</td>" +
        "</tr>"
      });
     
    }
    selectQH() {
      
      this._DCService
      .GetQH(this.selectedTinhTP.maTP)
      .pipe(first())
      .subscribe((res) => {
        this.QH = res;
      });
    }
    
    selectXP() {
      this._DCService
      .GetXP(this.selectedQH.maQH)
      .pipe(first())
      .subscribe((res) => {
        this.XP = res;
      });
    }
    getDiaChi(){
      this.diachi = this.selectedXP.Ten + ', ' + this.selectedQH.Ten + ', ' + this.selectedTinhTP.Ten;
    }
    public CustomEmailValidator(control: AbstractControl): ValidationErrors | null {
      if ((control.value || '').toString() == '') {
        return null;
      }
      return Validators.email(control);
    }
    
    public txtMatKhauCheckValidator(control) {
      var filteredStrings = { search: control.value, select: '@#!$%&*' }
      var result = (filteredStrings.select.match(new RegExp('[' +
      filteredStrings.search + ']', 'g')) || []).join('');
      if ((control.value.length < 6 || result == '') && control.value) {
        return { nameX: true };
      }
    }
    
    public onSubmit(value: any) {
      // let fullname = value.txtHo + ' ' + value.txtTen;
      let address = value.xa.ten + ' - ' + value.huyen.ten + ' - ' + value.tinh.ten;
      let diaChiNhan = '(' + value.txtDiaChi + ' ) ' + address;
      console.log(value.txtDiaChi + ' - ' + address);
      let hoadon = {
        hoTen: value.txtHoTen, 
        diaChiNhan: diaChiNhan,
        SDTNhan:value.txtSDT,
        email:value.txtEmail, 
        tinhTrang: "Chờ xử lý",
        tongTien: this.total,
        ngayDat:  this.ngayDat,
        listjson_chitiet:this.items
      };
      this._api.post('/api/DonHang/create-hoadon', hoadon).takeUntil(this.unsubscribe).subscribe(res => {
        debugger;
        this._api.post('/api/Email/Send', {
          ToEmail: value.txtEmail,
          Subject: "Cửa hàng bán đồ gia dụng HONO",
          Body: 
          "<h3>Ngày đặt: </h3> " + this.ngayDat.toLocaleDateString() +
          "<h3>Người đặt: </h3>"   + value.txtHoTen +
          "<h3>Số điện thoại:  </h3>" + value.txtSDT +
          "<h3>Email: </h3>"  + value.txtEmail + 
          "<h3>Địa chỉ nhận:  </h3>"  + diaChiNhan +
          // "<h3>Sản phẩm đã đặt</h3>" +
          // "<table class='table-styling product' border=1 cellpadding=3 cellspacing=2>" +
          // "<thead>"+
          // "<tr>" +
          // "<th>STT</th>" +
          // "<th>Tên sản phẩm</th>" +
          // "<th>Số lượng</th>" +
          // "<th>Đơn giá (VND)</th>" +
          // "<th>Thành tiền (VND)</th>" +
          // "</tr>" +
          // "</thead>"+
          // "<tbody>" +
          //   this.items.forEach((obj) => {
          //   debugger;
          //     "<tr>" +
          //       "<td>"+(this.i + 1)+"</td>" +
          //       "<td>"+obj.tenSanPham+"</td>" +
          //       "<td>"+obj.quantity+"</td>" +
          //       "<td>"+obj.giaBan+"</td>" +
          //       "<td>"+obj.money+"</td>" +
          //     "</tr>"
          //  })
          // +
          // "</tbody>"+
          // "</table>" +
          "<h3>Tổng tiền:</h3>" + this.total.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + " " + "VNĐ" +
          "<h3>Cảm ơn bạn đã mua hàng nhé</h3>"
        }).takeUntil(this.unsubscribe).subscribe(res => {
        }, err => {
          alert('Có lỗi trong quá trình thực hiện');
        })
        alert('Đặt hàng thành công');
        this._cart.clearCart();
        this.router.navigate(['/']);
      }, err => { });      
    }
  }
  