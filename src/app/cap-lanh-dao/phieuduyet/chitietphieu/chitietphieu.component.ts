import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ConsoleLogger } from '@microsoft/signalr/dist/esm/Utils';
import { controllers } from 'chart.js';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhieuDNVTService } from 'src/app/service/PhieuDNVT/phieu-dnvt.service';

@Component({
  selector: 'app-chitietphieu',
  templateUrl: './chitietphieu.component.html',
  styleUrls: ['./chitietphieu.component.css']
})
export class ChitietphieuComponent implements OnInit {

  @Input('chiTiet') chiTietPhieu: any = [];
  @Input('phieu') phieu: any = [];
  @Input('display') display: any;
  DsChiTiet: any = [];
  DsVattu = this.chiTietPhieu.chiTietPhieusList;
  //res :any =[];
  formDuyet!: FormGroup
  val: any;
  idPhieu: any = this.phieu.idPhieuDeNghi;
  chucVu: any = localStorage.getItem('maChuVu');
  isDuyet: any = false;
  countDs: number = 0;
  idUser: any = localStorage.getItem('idUser');
  lyDo:any

  // set diều kiện phải nhập 
  isLydo: boolean = false
  isTra: boolean = false

  khongDuyet: any = 1;

  ma: any
  displayMa: boolean = false;


  constructor(private service: PhieuDNVTService, private spinner: NgxSpinnerService, private toastr: ToastrService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }


  ngOnInit(): void {
    console.log("idphieu", this.idPhieu);
    this.formDuyet = new FormGroup({
      IdPhieuDeNghi: new FormControl('', Validators.required),
      IdPhongBan: new FormControl('', Validators.required),
      LyDoTraPhieu: new FormControl(''),
      MaBiMat: new FormControl(''),
      Status: new FormControl('', Validators.required),
      ChiTietPhieus: new FormControl('', Validators.required),
    })
  }
  onInput(index: any) {
    var val = (<HTMLInputElement>document.getElementById("vatTu" + index)).value;
    this.chiTietPhieu[index].soLuongThayDoi = val;
    console.log('ds moi', this.chiTietPhieu)
  }
  inputMa() {

    var val = (<HTMLInputElement>document.getElementById("maBiMat")).value;
    console.log("var ma", val);
    this.ma = val;
  }
  submit() {
    this.formDuyet.patchValue({
      IdPhieuDeNghi: this.chiTietPhieu[0].idPhieuDeNghi,
      IdPhongBan: this.phieu.idPhongBan,
      LyDoTraPhieu: '',
      MaBiMat: this.ma,
      ChiTietPhieus: this.chiTietPhieu,
    })
    console.log('show form', this.formDuyet.value)
    console.log('show chiteti', this.chiTietPhieu)
  }
  submitTra() {
    this.formDuyet.patchValue({
      IdPhieuDeNghi: this.chiTietPhieu[0].idPhieuDeNghi,
      IdPhongBan: this.phieu.idPhongBan,
      LyDoTraPhieu: this.phieu.LyDoTraPhieu,
      MaBiMat: this.ma,
      ChiTietPhieus: this.chiTietPhieu,
    })
    console.log('show form', this.formDuyet.value)
  }
  setup() {
    this.khongDuyet = 2
    this.isTra = true
  }

  InputLyDo() {
    var val = (<HTMLInputElement>document.getElementById("lyDo")).value;
    this.phieu.LyDoTraPhieu = val;
    this.lyDo= val
    console.log("PhieuTra", this.phieu);
  }
  Send() {
    console.log("ly do", this.lyDo)
    if (this.isDuyet == false && this.lyDo == null || this.isDuyet == false && this.lyDo == 'undefined' ||this.isDuyet == false && this.lyDo == '') {
      this.isLydo = true
      console.log(" looix sdafsafdsaf")
    } else if (this.ma == '' || this.ma == 'undefined' || this.ma == null) {
      console.log("không ma", this.ma)
      this.displayMa = true;
      this.isLydo = false
    }
    else {
      console.log("ma in duye ", this.ma)
      if (this.isDuyet) {
        this.submit();
      }
      else {
        this.submitTra();
      }
      var val = this.formDuyet.value
      console.log("form ", val)
      this.spinner.show();
      this.service.DuyetPhieu(val, this.phieu.idPhieuDeNghi, this.isDuyet, this.idUser).subscribe((res: any) => {
        console.log("res", res);
        setTimeout(() => {
          if (res.result.statusCode == 200) {
            this.spinner.hide();
            this.reset()
            this.toastr.success('Thành công', 'Đã xử lý phiếu', {
            });
          }
          else if (res.result.statusCode == 400) {
            this.spinner.hide();
            this.toastr.error(' Yêu cầu nhập lại để duyệt', 'Nhập sai mã ');
          }
        });
      })
      console.log("display 2", this.display);
    }
  }
  reset() {
    this.isDuyet = false
    this.displayMa = false
    this.khongDuyet = 1
    this.isTra = false
    this.ma = ''
  }
  isStatus() {
    this.isDuyet = true;
    this.Send()
  }
}
