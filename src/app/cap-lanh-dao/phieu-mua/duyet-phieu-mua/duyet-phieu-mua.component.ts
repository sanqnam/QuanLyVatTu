import { Component, Input ,OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { NguoimuaService } from 'src/app/service/nguoimua/nguoimua.service';

@Component({
  selector: 'app-duyet-phieu-mua',
  templateUrl: './duyet-phieu-mua.component.html',
  styleUrls: ['./duyet-phieu-mua.component.css']
})
export class DuyetPhieuMuaComponent implements OnInit{
  @Input() chiTiet:any =[]
  @Input()phieu:any
  isDuyet: any = false;
  formDuyet!: FormGroup
  lyDo:any;
  idPhieu:any;
  idUser:any = localStorage.getItem("idUser")
  role:any = localStorage.getItem("maChucVu")
  khongDuyet: any = 1;
  hinhArr:any =[];
  isShowimg:boolean = false
  ma:any
  displayMa:boolean = false;
  
  constructor(private service: NguoimuaService, private spinner: NgxSpinnerService, private toastr: ToastrService,private fb:FormBuilder) { }
    ngOnInit(): void {
      this.formDuyet=this.fb.group({
        idPhieu: ['', [Validators.required]],
        idUser: ['', [Validators.required]],
        lyDoKhongDuyet: ['', [Validators.required]],
        maBiMat: ['', [Validators.required]],
        role: ['', [Validators.required]],        
      })
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
    Send() {
      if(this.ma == ''||this.ma == undefined){
        this.displayMa = true
      }
      else{
        console.log("phieu duyett", this.phieu)
        this.formDuyet.patchValue({
          idPhieu:this.phieu.idPhieuTrinhMua,
          idUser: this.idUser,
          lyDoKhongDuyet:this.lyDo,
          maBiMat:this.ma,
          role:this.role
        })
        console.log("fome duyêtm", this.formDuyet.value);
        this.spinner.show();
        this.service.DuyetPhieuMua(this.formDuyet.value,this.isDuyet).subscribe((res: any) => {
          console.log("res", res);
          setTimeout(() => {
            if (res.statusCode == 200  ) {
              this.spinner.hide();
              this.toastr.success('Thành công', 'Đã xử lý phiếu', {
              });
              this.ReLoad()
            }else if(res.statusCode == 400){
              this.spinner.hide();
              this.toastr.error(' Yêu cầu nhập lại để duyệt','Nhập sai mã ');
            }
            
          });
        })
      }    
    }
    ReLoad(){
      this.lyDo='';
      this.isDuyet=false;
      this.khongDuyet =1
      this.khongDuyet=1;
      this.displayMa =false
      this.formDuyet.patchValue({
          idPhieu:'',
          idUser: '',
          lyDoKhongDuyet:'',
          maBiMat:'',
          role:''
      })
    }
    action(){
      this.isShowimg = true
      console.log("trạngthai", this.isShowimg)
    }
    isStatus() {
      this.isDuyet = true;
      this.Send();
    } 
  
}
