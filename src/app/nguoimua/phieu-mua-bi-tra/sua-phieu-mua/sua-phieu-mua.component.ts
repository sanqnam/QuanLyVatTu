import { Component, OnInit, Input } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { NguoimuaService } from 'src/app/service/nguoimua/nguoimua.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-sua-phieu-mua',
  templateUrl: './sua-phieu-mua.component.html',
  styleUrls: ['./sua-phieu-mua.component.css']
})
export class SuaPhieuMuaComponent implements OnInit {
  @Input() dsChiTiet: any = []
  @Input() phieu: any
  isDuyet: any = false;
  formSua!: FormGroup
  formInput!: FormGroup
  lyDo: any;
  idPhieu: any;
  idUser: any = localStorage.getItem("idUser")
  role: any = localStorage.getItem("maChucVu")
  khongDuyet: any = 1;
  hinhArr: any = [];
  isShowimg: boolean = false
  ma: any
  displayMa: boolean = false;
  arr: any = []
  click: boolean = true
  isLyDo:boolean = false

  constructor(private service: NguoimuaService, private spinner: NgxSpinnerService, private toastr: ToastrService, private fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.formSua = this.fb.group({
      idPhieu: ['', [Validators.required]],
      idUser: ['', [Validators.required]],
      lyDoSuaPhieu: ['', [Validators.required]],
      chiTietPhieus: []
    })
  }
  buildDS(ds: any) {
    if (this.click) {
      for (let i = 0; ds.length > i; i++) {    
        console.log('chi tiet', ds[i])
        this.arr.push(this.BuildForms(ds[i]))
      }
      console.log("arr2", ds)
      console.log("arr", this.fb.array(this.arr).value)
    }
    this.click = false
  }
  BuildForms(phieu: any): FormGroup {
    return this.fb.group({
      idChiTietPhieu: [phieu.idChiTietPhieu],
      donGia: [phieu.donGia],
      vat: [phieu.vat],
      donViCungCap: [phieu.donViCungCap],
      soLuongMuaThem: [phieu.soluong],
    })
  }
  InputGia(i: any, event: any) {
    console.log("inpu", i, "ss", event);
    this.arr[i].get('donGia').setValue(event)
    console.log('ar i', this.arr[i].donGia)
  }
  InputSoluong(i: any, event: any) {
    console.log("inpu", i, "ss", event);
    this.arr[i].get('soLuongMuaThem').setValue(event)
  }
  InputDonViCungCap(i: any, event: any) {
    console.log("inpu", i, "ss", event);
    this.arr[i].get('donViCungCap').setValue(event)
  }
  InputVat(i: any, event: any) {
    console.log("inpu", i, "ss", event);
    this.arr[i].get('vat').setValue(event)
  }
  inputLyDo(){
    var val = (<HTMLInputElement>document.getElementById("lyDo")).value;
    console.log("PhieuTra", val);
    this.lyDo = val
  }
  Send(){
    if(this.lyDo == ''||this.lyDo == undefined){
      this.isLyDo = true
    }else{

      this.formSua.patchValue({
        idPhieu: this.phieu.idPhieuTrinhMua,
        idUser: this.idUser,
        lyDoSuaPhieu: this.lyDo,
        chiTietPhieus: this.fb.array(this.arr).value
      })
      console.log("send ", this.formSua.value)
      this.spinner.show()
      this.service.PhieuCanSua(this.formSua.value).subscribe((res:any)=>{
        this.spinner.hide()
        this.toastr.success('Thành công', 'Đã sửa', {
        });
        console.log("res",res)
      })
    }
  }
}
