import { Component, Injector, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { DiachiService } from '../../../core/service/diachi.service';
import { BaseComponent } from '../../../core/base-component';
import { first } from 'rxjs/operators';

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
  items:any;
  total:any;
  ngayDat:any;
  diachi: any;
  constructor(injector: Injector, private _DCService: DiachiService,) { 
    super(injector);
  }
  // public categories: Category[];
  // selectedCategory: Category;
  // public brands: Brand[];
  // selectedBrand: Brand;
  public TinhTP: TinhTP[] | any;
  public QH: QH[] | any;
  public XP: XP[] | any;
  
  public selectedTinhTP: TinhTP ;
  public selectedQH: QH ;
  public selectedXP: XP ;
  
  ngOnInit(): void {
    window.scrollTo(0,0);
    var today = new Date();
    this.ngayDat = today;
    this.frmCheckout = new FormGroup({
      txtHo: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50),Validators.pattern(this.namePattern)]),
      txtTen: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50),Validators.pattern(this.namePattern)]),
      // txtDiaChi: new FormControl('',[Validators.required]),
      tinh: new FormControl(''),
      huyen: new FormControl(''),
      xa: new FormControl(''),
      txtSDT: new FormControl('', [Validators.required, Validators.pattern(this.mobilePattern)]),
      txtEmail: new FormControl('', [this.CustomEmailValidator]),
    });
    
    this._cart.items.subscribe((res) => {
      this.items = res;
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
    let fullname = value.txtHo + ' ' + value.txtTen;
    let address = value.tinh.ten + ' - ' + value.huyen.ten + ' - ' + value.xa.ten;
    console.log(address);
    let hoadon = {
      hoTen: fullname, 
      diaChiNhan:address,
      SDTNhan:value.txtSDT,
      email:value.txtEmail, 
      tinhTrang: "Chờ xử lý",
      tongTien: this.total,
      ngayDat:  this.ngayDat,
      listjson_chitiet:this.items};
      debugger;
      this._api.post('/api/DonHang/create-hoadon', hoadon).takeUntil(this.unsubscribe).subscribe(res => {
        alert('Tạo thành công');
        this._cart.clearCart();
      }, err => { });      
    }
    
  }
  