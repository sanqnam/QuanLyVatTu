import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PhieuSuaService } from 'src/app/service/PhieuSua/phieu-sua.service';
import { UserService } from 'src/app/service/user/user.service';

@Component({
  selector: 'app-phieu-by-pb-phu-trach',
  templateUrl: './phieu-by-pb-phu-trach.component.html',
  styleUrls: ['./phieu-by-pb-phu-trach.component.css']
})
export class PhieuByPbPhuTrachComponent {
  res:any
  DsPhieu:any=[];
  phieu:any=[];
  idPhieu:any;
  DsChiTietPhieu:any=[];
  chiTietPhieu:any =[];
  filterform!:FormGroup;
  idUser :any =localStorage.getItem("idUser");
  idPhongBan :any = localStorage.getItem("idPhongBan");
  chucVu:any = localStorage.getItem("maChucVu");
  idTinhTrangPhieu = this.phieu.idTinhTrangPhieu;
  display:any;
  dsImg:any = []

  currentIndex:any = 0
  
  filter:any=[];
  filterForm!:FormGroup;
  formSet!:FormGroup;

  POSTS: any;
  page:any = 1;
  sortOrder:any=1;
  counts:any = 0;
  tableSize: number = 10;
  tableSizes: any = [10,15,20,25,30];


  userSua:any
  dsUser:any


  constructor(private service: PhieuSuaService, private fb:FormBuilder, private router:Router, private route:ActivatedRoute ,private serviceUSer:UserService,private toastr: ToastrService){}

  ngOnInit(): void {
    if(this.filter.length == 0){
      this.filter.size= this.tableSize;
      this.filter.sortOrder = 1;
    }   
    this.filterForm = new FormGroup({
      size: new FormControl(''),
      sortOrder: new FormControl(''),
    });    
    this.formSet = new FormGroup({
      userSua: new FormControl(''),
    })
    this.GetUserByPB()
    this.LoadPhieu(this.idPhongBan, this.chucVu, this.page, this.tableSize, this.sortOrder);   
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
  loadHinh(phieu:any){
    console.log("phiếu  ooo", phieu)
    this.service.GetImgByPhieu(phieu.idChiTietPhieuSua).subscribe((res:any)=>{
      console.log("res", res)
      if( res.value.length == 0){
        this.dsImg = 0}
      else{
        console.log("resimg", res)
        this.dsImg = res.value;
      }
      console.log("dsimg", this.dsImg)
    })
  }

  LoadPhieu(idPhongBan:any, role:any,page:any, size:any,orderShort:any){
    this.service.AllPhieuSuaByPhongBanPhuTrach(idPhongBan,role, page, size, orderShort).subscribe(
      (data:any)=>{
        this.DsPhieu=data.value;
        this.counts=data.serializerSettings;
        console.log("dsphieueueu",this.DsPhieu)
      },
      err => {
        console.error('err',err);
        if(err.status == 403){
          this.router.navigate(['/error']);
        };
      }
    )
  }
  chiTiet(phieu:any){
    this.phieu = phieu;
    console.log("phieu", this.phieu)
    this.service.GetVatTuByPhieu(phieu.idPhieuSuaChua).subscribe((res:any)=>{
      this.chiTietPhieu =res.value;
      console.log("phieu", this.chiTietPhieu)
    })

  }

  
  onTableDataChange (event: any){
    console.log("even", event);
    this.page = event;
    this.LoadPhieu(this.idPhongBan, this.chucVu, this.page, this.tableSize, this.sortOrder);    
  }
  filterSelect(){
    this.tableSize = this.filterForm.get('size')?.value;
    this.LoadPhieu(this.idPhongBan, this.chucVu, this.page, this.tableSize, this.sortOrder); 
  }

  GetUserByPB(){
    this.serviceUSer.GetUserSuaByPhongBan(this.idPhongBan).subscribe(res=>{
      this.dsUser = res;
      console.log("ds user", this.dsUser);
    })
  }
}
