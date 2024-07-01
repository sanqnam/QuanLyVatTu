import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConsoleLogger } from '@microsoft/signalr/dist/esm/Utils';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhieuSuaService } from 'src/app/service/PhieuSua/phieu-sua.service';
import { PhongBanService } from 'src/app/service/phongban/phongban.service';
import { VattuService } from 'src/app/service/vattu/vattu.service';

@Component({
  selector: 'app-add-phieu-sua',
  templateUrl: './add-phieu-sua.component.html',
  styleUrls: ['./add-phieu-sua.component.css']
})
export class AddPhieuSuaComponent implements OnInit {

  @Input('dsPhieu')
  dsPhieu: any = []
  @Input('clicks') clicks :any 
  PhieuVT!: FormGroup;
  ChiTietPhieuSuas = new FormArray([]);
  isClick: boolean = false
  countDs: number = 0;
  idPhongBan: any = localStorage.getItem("idPhongBan");
  idUser: any = localStorage.getItem("idUser");
  dsPhongBan: any = [];
  url: any[] = [{}];
  vals: any = [];
  obj: object = []
  arrr: any = []
  isClicks:boolean = false
  dsThuTruong:any=[]

  isLyDo:boolean = false
  isTP:boolean = false
  isTPNhan:boolean = false

  constructor(private fb: FormBuilder, private toastr: ToastrService, private serviceSua: PhieuSuaService, private router: Router, private spinner: NgxSpinnerService, private serviceVT: VattuService ,private servicePB:PhongBanService){}
  ngOnInit(): void {
    if (this.dsPhieu.length == 0) {
      this.PhieuVT = this.fb.group({
        idThuTruongNhan: ['', [Validators.required]],
        idPhongBan:'',
        idUser: [''],
        idPhongNhan: '',
        lyDo: [''],
        ChiTietPhieuSuas:[''],
      })
    }
    this.getPhongBan();
    this.getTruongPhong();
    this.clicks = false;    
  }
  BuildForms(phieu: any): FormGroup {
    return this.fb.group({
      idChiTietVatTu: [phieu],
      moTaTinhTrang: [''],
      url: [],
    })
  }
  hello(){
    if(!this.clicks){
      console.log("ds phieu is click", this.dsPhieu);
      for(let i=0; this.dsPhieu.length >i; i++){
        var id = this.dsPhieu[i].idChiTietVatTu
        this.arrr.push(this.BuildForms(id));
      }
      this.clicks= true;
    }
  }
  onInput(i:any){
    var val = (<HTMLInputElement>document.getElementById("moTa"+i)).value;
    this.arrr[i].value.moTaTinhTrang = val;
  }
  sendUrl(even: any, i: any) {
      this.arrr[i].value.url = even;
  }
  send(){
    this.PhieuVT.patchValue({
      idUser: localStorage.getItem("idUser"),
      idPhongBan: localStorage.getItem("idPhongBan"),
      ChiTietPhieuSuas:this.fb.array(this.arrr).value
    })
    var val = this.PhieuVT.value;
    console.log("phieu", val);

    if( val.lyDo == '')
    {
      this.isLyDo= true
    } else if(val.idPhongNhan == ''){
      this.isLyDo = false
      this.isTP = true
      console.log("hsfjskdfskfsdf")
    } else if(val.idThuTruongNhan == ''){
      this.isTP = false
      this.isTPNhan = true
    }else{
      this.isTPNhan = false 
      this.TaoPhieu(val)
    }
  }
  TaoPhieu(val:any) {

    this.serviceSua.AddPhieuSua(val).subscribe((res: any) => {
      console.log("res", res);
      this.spinner.show();
      setTimeout(() => {
        if (res.value == "tao xong") {
          this.spinner.hide();
          this.toastr.success('Thành công', 'Đã tạo phiếu', {
            timeOut: 5000,
          });

         this.resetForm();
        }
      });
    })
  }
  resetForm(): void {
   this.PhieuVT.patchValue({
    lyDo: [''],
    ChiTietPhieuSuas:['']
   })
   this.clicks =false;
    this.arrr = [];
    this.dsPhieu = [];
  }
  getPhongBan() {
    this.servicePB.GetAllPBSua().subscribe((res:any) => {
      this.dsPhongBan = res.value;
      console.log(this.dsPhongBan)
    })
  }
  getTruongPhong(){
this.serviceVT.getPhongBan(this.idPhongBan).subscribe(res=>{
this.dsThuTruong = res
})
  }
}
