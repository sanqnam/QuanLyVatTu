import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { NguoimuaService } from 'src/app/service/nguoimua/nguoimua.service';
import { VattuService } from 'src/app/service/vattu/vattu.service';

@Component({
  selector: 'app-tao-phieu-mua',
  templateUrl: './tao-phieu-mua.component.html',
  styleUrls: ['./tao-phieu-mua.component.css']
})
export class TaoPhieuMuaComponent implements OnInit {
  @Input('dsPhieu')
  dsPhieu: any = [];
  PhieuVT!: FormGroup;
  ChiTietPhieus = new FormArray([]);
  isClick: boolean = false
  countDs: number = 0;
  idPhongBan: any = localStorage.getItem("idPhongBan");
  dsPhongBan: any = [];
  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private service: NguoimuaService, private router: Router, private spinner: NgxSpinnerService, private serviceVT: VattuService) { }

  ngOnInit(): void {
    if (this.dsPhieu.length == 0) {
      this.PhieuVT = this.fb.group({
        idThuTruong: [''],
        idLanhDao: [''],
        idPhongBan:[''],
        ChiTietPhieus: this.fb.group({
          idVatTu: [''],
          tenVatTu: [''],
          maVatTu: [''],
          DonViTinhDeNghi: [''],
          soLuongMuaThem: [''],
          donGia: [''],
          vAT: [''],
          donViCungCap: [''],
          IdChiTietPhieu:['']
        })
      })
    }
    this.getPhongBan();
  }

  adDsVatTu() {
    if (this.dsPhieu.length > this.countDs || this.dsPhieu.length < this.countDs) {
      this.spinner.show();
      setTimeout(() => {
        let arr = [];
        this.countDs = this.dsPhieu.length;
        this.isClick = true;
        for (let i = 0; i < this.dsPhieu.length; i++) {
          arr.push(this.BuildForm(this.dsPhieu[i]))
        }
        this.getArrayControls()
        this.spinner.hide();
        this.PhieuVT = this.fb.group({
          idUser: [localStorage.getItem('idUser')],
          idThuTruong: [''],
          idPhongBan: [localStorage.getItem('idPhongBan')],
          idLanhDao: [''],
          ChiTietPhieus: this.fb.array(arr)
        })
      });
    }
  }
  BuildForm(vattu: any): FormGroup {
    return this.fb.group({
      idVatTu: [vattu.idVatTu],
      tenVatTu: [vattu.tenVatTu],
      maVatTu: [vattu.maVatTu],
      donViTinhDeNghi: [vattu.donViTinh],
      soLuongMuaThem: [vattu.soLuongTongCanMua],
      donGia: new FormControl(''),
      vAT: new FormControl(''),
      donViCungCap: new FormControl(''),
      idChiTietPhieu:[vattu.idChiTietPhieu]
    })
  }
  getArrayControls() {
    if (this.countDs >= 1) {
      var cont = (<FormArray>this.PhieuVT.get('ChiTietPhieus')).controls;
      return cont;
    }
    return;
  }
  TaoPhieu() {
    console.log("phieu tao", this.PhieuVT.value);
    var val = this.PhieuVT.value;
    console.log("phieu", val);
    this.service.TaoPhieuMua(val).subscribe((res: any) => {
      console.log("res", res);
      this.spinner.show();
      setTimeout(() => {
        if (res.success== true) {
          this.spinner.hide();
          this.dsPhieu = null;
          this.toastr.success('Thành công', 'Đã tạo phiếu', {
            timeOut: 5000,
          });
        }
      });
    })
  }
  getPhongBan() {
    this.serviceVT.getPhongBan(this.idPhongBan).subscribe(res => {
      this.dsPhongBan = res;
    })
  }
}
