import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validator, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { VattuService } from 'src/app/service/vattu/vattu.service';
import { FormControl, FormGroup, NgForm, FormArray } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { HttpResponse, HttpStatusCode } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-add-phieu-dn',
  templateUrl: './add-phieu-dn.component.html',
  styleUrls: ['./add-phieu-dn.component.css']
})
export class AddPhieuDNComponent implements OnInit {

  @Input('dsPhieu')
  dsPhieu: any = [];
  PhieuVT!: FormGroup;
  ChiTietPhieus = new FormArray([]);
  isClick: boolean = false
  countDs: number = 0;
  idPhongBan: any = localStorage.getItem("idPhongBan");
  dsPhongBan: any = [];
  Status:any;
  arr: any = []
  isTao:boolean=false

  // set điều kiện bắt buộc phải nhập
  isLyDo:boolean = false
  isTP:boolean = false
  isLD:boolean = false


  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private service: VattuService, private router: Router, private spinner: NgxSpinnerService) { }
  ngOnInit(): void {
    if (this.dsPhieu.length == 0) {
      this.PhieuVT = this.fb.group({
        LyDoLapPhieu: '',
        idThuTruong: '',
        idLanhDao: '',
        idUser:'',
        idPhongBan:'',
        ChiTietPhieus: []
      })
    }
    this.getPhongBan();
    
  }
  adDsVatTu() {
    if(!this.isTao){
      console.log('phieu', this.dsPhieu)
          this.isClick = true;
          for (let i = 0; i < this.dsPhieu.length; i++) {
            console.log('phieu2', this.dsPhieu[i])
           this.arr.push(this.BuildForm(this.dsPhieu[i]))
          }     
          console.log(this.fb.array(this.arr).value)
      this.isTao = true
    }

  }
  InputSoluong(i: any) {
    var val = (<HTMLInputElement>document.getElementById("soLuong"+i)).value;
    console.log(val)
    this.arr[i].value.soLuongDeNghi = val;
  }
  InputGhiChu(i: any) {
    var val = (<HTMLInputElement>document.getElementById("ghiChu"+i)).value;
    this.arr[i].value.ghiChuNguoiDung = val;
  }
  BuildForm(vattu: any): FormGroup {
    this.Status=vattu.status
    return this.fb.group({
      idVatTu: vattu.idVatTu,
      tenVatTu: vattu.tenVatTu,
      maVatTu: vattu.maVatTu,
      donViTinhDeNghi: vattu.donViTinh,
      soLuongDeNghi: '',
      ghiChuNguoiDung: ''
    })    
  }
  send(){
    console.log("phieu tao", this.PhieuVT.value);
    this.PhieuVT.patchValue({
      idUser: localStorage.getItem('idUser'),
      idPhongBan: localStorage.getItem('idPhongBan'),
      ChiTietPhieus: this.fb.array(this.arr).value
    })
    var val = this.PhieuVT.value;
    console.log("phieu", val);
    if(val.LyDoLapPhieu == null || val.LyDoLapPhieu == '')
    {
      this.isLyDo= true
    } else if(val.idThuTruong == null || val.idThuTruong == ''){
      this.isLyDo = false
      this.isTP = true
    } else if( val.idLanhDao == null || val.idLanhDao==''){
      this.isTP = false
      this.isLD = true
      this.isLyDo = false
    }else{
      this.isLD = false 
      this.TaoPhieu(val)
    }
  }

  TaoPhieu(val:any) {

    this.service.TaoPhieu(val).subscribe((res: any) => {
      console.log("res", res);
      this.spinner.show();
      setTimeout(() => {
        if (res.success == true) {
          this.spinner.hide();
          this.dsPhieu = null;
          this.toastr.success('Thành công', 'Đã tạo phiếu', {
            timeOut: 5000,
          });
          this.PhieuVT.patchValue({
            LyDoLapPhieu: [''],
            ChiTietPhieus: []             
          })    
        }
      });
    })
  }
  getPhongBan() {
    this.service.getPhongBan(this.idPhongBan).subscribe(res => {
      this.dsPhongBan = res;
    })
  }
}
