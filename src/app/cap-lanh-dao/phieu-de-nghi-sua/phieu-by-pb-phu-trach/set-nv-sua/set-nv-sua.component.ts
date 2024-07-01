import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhieuSuaService } from 'src/app/service/PhieuSua/phieu-sua.service';
import { UserService } from 'src/app/service/user/user.service';
import tippy from 'tippy.js';

@Component({
  selector: 'app-set-nv-sua',
  templateUrl: './set-nv-sua.component.html',
  styleUrls: ['./set-nv-sua.component.css']
})
export class SetNvSuaComponent implements OnInit{
  @Input('chiTiet') chiTietPhieu:any =[]
  @Input('phieu')phieu :any;
  isDuyet: any = false;
  formSet!: FormGroup
  lyDo:any;
  idPhieu:any;
  userSua:any;
  dsUser:any = []
  idPB :any = localStorage.getItem("idPhongBan");
  idUser :any = localStorage.getItem("idUser");

  
  khongDuyet: any = 1;
  dsImg:any =[];
  isShowimg:boolean = false
  
  constructor(private service: PhieuSuaService, private spinner: NgxSpinnerService, private toastr: ToastrService, private fb: FormBuilder, private serviceUSer:UserService) { }
    ngOnInit(): void {
      this.formSet = new FormGroup({
        userSua: new FormControl(''),
      })
      this.GetUserByPB()
      
    }
    Hinh(ct:any){
      console.log("hinh" , ct)
      this.service.GetImgByPhieu(ct.idChiTietPhieuSua).subscribe((res:any)=>{
      this.dsImg= res.value
      console.log("hinhg arr", this.dsImg)
      this.initTippy();
      })
      
  
    }
    initTippy(): void {
      console.log("tippy", this.dsImg)
      const images = this.dsImg.map((img:any) => `<img src="${img.url}" style="max-width: 200px; max-height: 200px;" />`);
      if(images.length > 0){
        tippy('.show-images', {
          content: images.join(''), // Nội dung của tooltip là danh sách hình ảnh
          allowHTML: true,
          interactive: true, // Cho phép tương tác với tooltip
          placement: 'top', // Vị trí hiển thị tooltip (ở đây là bên phải)
          trigger: ' mouseenter  ', // Kích hoạt tooltip khi click
        });
      }
      else{
        tippy('.show-images', {
          content: 'Không có hình', // Nội dung của tooltip là danh sách hình ảnh
          allowHTML: true,
          interactive: true, // Cho phép tương tác với tooltip
          placement: 'top', 
          trigger: 'mouseenter',
        });
      }
    }
    Submit(phieu:any) {
      this.userSua = this.formSet.get('userSua')?.value;
      console.log("sdfjsldfjsdjfsjdf---->", this.userSua)
      console.log("id chi tiết phieseu sãu", )
      this.idPhieu = phieu.idChiTietPhieuSua
      console.log("id phieu", this.idPhieu)
      this.service.SetNhanVienSua(this.userSua, this.idPhieu, this.idUser).subscribe((res: any) => {
        console.log("res", res);
        if(res =="xong"){
          this.toastr.success("set thành công");
          this.userSua = null;
        }
      })
    }
    action(){
      this.isShowimg = true
      console.log("trạngthai", this.isShowimg)
    }
    isStatus() {
      this.isDuyet = true;
    } 
    GetUserByPB(){
      this.serviceUSer.GetByIdPhongBan(this.idPB).subscribe(res=>{
        this.dsUser = res;
        console.log("ds user", this.dsUser);
      })
    }
}
