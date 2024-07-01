import { Component, Input, OnInit,ElementRef, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhieuSuaService } from 'src/app/service/PhieuSua/phieu-sua.service';


@Component({
  selector: 'app-pupup-duyet-phieu',
  templateUrl: './pupup-duyet-phieu.component.html',
  styleUrls: ['./pupup-duyet-phieu.component.css']
})
export class PupupDuyetPhieuComponent implements OnInit{
@Input()isHien:any
@Input()idPhieu:any;
@ViewChild('maBiMatInput') maBiMatInput!: ElementRef<HTMLInputElement>;


  ma:any
  lyDo:any
  isDisplayMa:boolean = false
  isDuyet:boolean = true
  constructor(private service: PhieuSuaService,private spinner: NgxSpinnerService, private toastr: ToastrService,){
  }
  ngOnInit(): void {
 
  }
 Back(){
  this.isDuyet = true
  this.isHien = true;
 }
  inputMa(){   
    const val = this.maBiMatInput.nativeElement.value;
    console.log("var madđ", val);
    this.ma = val;
  }
  InputLyDo() {
    var val = (<HTMLInputElement>document.getElementById("lyDo")).value;
    this.lyDo = val;
    console.log("PhieuTra", this.lyDo);
  }
  InputNote() {
    var val = (<HTMLInputElement>document.getElementById("note")).value;
    this.lyDo = val;
    console.log("Note", this.lyDo);
  }
  Send() {
    console.log("id phieu", this.idPhieu)
    console.log("var ma", this.ma);
    if(this.ma == ''||this.ma == undefined){
      this.isDisplayMa = true
    }else{   
      this.isDisplayMa = false  
      console.log("id phieu", this.idPhieu)
      this.spinner.show();
      this.service.DuyetPhieuSua(this.isDuyet, this.idPhieu,this.lyDo,this.ma).subscribe((res: any) => {
        console.log("res", res);
        setTimeout(() => {
          if (res.result.statusCode == 200  ) {
            this.reset()
            this.spinner.hide();
            this.toastr.success('Thành công', 'Đã xử lý phiếu', {
            } 
          )

           
          }else if(res.result.statusCode == 400){
            this.spinner.hide();
            this.toastr.error(' Yêu cầu nhập lại để duyệt','Nhập sai mã ');
          }
        });
      })
    }    
  }
  reset(){
    this.isDuyet = true
    this.isHien = true;
    this.isDisplayMa = false   
    this.ma = ''
  }
}
