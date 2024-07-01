import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { NguoimuaService } from 'src/app/service/nguoimua/nguoimua.service';

@Component({
  selector: 'app-nhap-kho',
  templateUrl: './nhap-kho.component.html',
  styleUrls: ['./nhap-kho.component.css']
})
export class NhapKhoComponent implements OnInit {

  DsPhieu: any = [];
  phieu: any = [];
  idPhieu: any;
  idUser: any = localStorage.getItem("idUser");
  idPhongBan: any = localStorage.getItem("idPhongBan");
  chucVu: any = localStorage.getItem("maChucVu");
  phieuTheoId: any
  dsChiTiet: any
  showXuLy: boolean = false
  id: any
  isXacNhan = false
  buildForm: any = FormGroup
  arr: any = []

  tableSize: any = 10
  page: any = 1;
  counts: any

  constructor(private service: NguoimuaService, private spinner: NgxSpinnerService, private toastr: ToastrService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.buildForm = this.fb.group({
      idPhieu: '',
      ChiTietNhaps: []
    })
    this.LoadPhieu(this.page)

  }
  LoadPhieu(page: any) {
    this.service.GetPhieuChoThuKho(page).subscribe(
      (data: any) => {
        this.DsPhieu = data.value;
        this.counts = data.serializerSettings;
        console.log("dddddđ", this.DsPhieu)
      },
      err => {
        console.error('err', err);
        if (err.status == 403) {
          this.router.navigate(['/error']);
        };
      }
    )
  }
  XuLy(id: any) {
    console.log("id", id)
    this.isXacNhan = true
    this.buildForm.patchValue({
      idPhieu: id,
      ChiTietNhaps : this.fb.array(this.arr).value
    })
    this.spinner.show
    this.service.XacNhanThuKho( this.buildForm.value, this.isXacNhan).subscribe(res => {
      console.log(res)
      setTimeout(() => {
        this.spinner.hide()
        this.toastr.success('Thành công', 'Dã nhập kho', {
        });
        
      }, );
      this.reset()

    })
  }
  reset(){
    this.ngOnInit()
    this.arr=[]
    this.isXacNhan = false
  }
  loadForm(ds: any) {
    console.log("dsfomr", ds)
    for (let i = 0; ds.length > i; i++) {
      this.arr.push(this.BuildArr(ds[i]))
    }
  }
  BuildArr(i: any): FormGroup {
    return this.fb.group({
      idVatTu: i.idVatTu,
      soLuong: i.soluong,
    })
  }
  loadChiTietPhieu(id: any) {
    this.id = id
    this.service.GetChiTietPhieuMua(id).subscribe((res: any) => {
      console.log("dasta chitiets", res)
      this.dsChiTiet = res.value
      this.loadForm(this.dsChiTiet)
      this.showXuLy = true;
    })
  }

  onTableDataChange(event: any) {
    this.page = event;
    this.LoadPhieu(this.page);
  }

}
