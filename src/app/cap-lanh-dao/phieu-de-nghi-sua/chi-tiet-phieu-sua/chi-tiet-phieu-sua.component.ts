import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhieuSuaService } from 'src/app/service/PhieuSua/phieu-sua.service';

@Component({
  selector: 'app-chi-tiet-phieu-sua',
  templateUrl: './chi-tiet-phieu-sua.component.html',
  styleUrls: ['./chi-tiet-phieu-sua.component.css']
})
export class ChiTietPhieuSuaComponent implements OnInit{
@Input('chiTiet') chiTietPhieu:any =[]
@Input('phieu')phieu :any;
isDuyet: any = false;
formDuyet!: FormGroup
lyDo:any;
idPhieu:any;

khongDuyet: any = 1;
hinhArr:any =[];
isShowimg:boolean = false
ma:any
displayMa:boolean = false;

constructor(private service: PhieuSuaService, private spinner: NgxSpinnerService, private toastr: ToastrService, private fb: FormBuilder) { }
  ngOnInit(): void {
  }
  SendHinh(){
    
  }
  inputMa(){
    var val = (<HTMLInputElement>document.getElementById("maBiMat")).value;
    console.log("var ma", val);
    this.ma = val;
  }
  InputLyDo() {
    var val = (<HTMLInputElement>document.getElementById("lyDo")).value;
    this.lyDo = val;
    console.log("PhieuTra", this.lyDo);
  }
  Hinh(ct:any){
    console.log("hinh" , ct)
    this.service.GetImgByPhieu(ct.idChiTietPhieuSua).subscribe(res=>{
    this.hinhArr= res
    console.log("hinhg arr", this.hinhArr)
    })
  }
  Send() {
    this.idPhieu = this.phieu.idPhieuSuaChua
    console.log("id phieu", this.phieu.idPhieuSuaChua)
    this.spinner.show();
    this.service.DuyetPhieuSua(this.isDuyet, this.idPhieu,this.lyDo,this.ma).subscribe((res: any) => {
      console.log("res", res);
      setTimeout(() => {
        if (res.result.statusCode == 200  ) {
          this.spinner.hide();
          this.toastr.success('Thành công', 'Đã xử lý phiếu', {
          });
        }else if(res.result.statusCode == 400){
          this.spinner.hide();
          this.toastr.error(' Yêu cầu nhập lại để duyệt','Nhập sai mã ');
        }
      });
    })
  }
  action(){
    this.isShowimg = true
    console.log("trạngthai", this.isShowimg)
  }
  isStatus() {
    this.isDuyet = true;
  } 
}
