import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { NguoimuaService } from 'src/app/service/nguoimua/nguoimua.service';

@Component({
  selector: 'app-phieu-mua',
  templateUrl: './phieu-mua.component.html',
  styleUrls: ['./phieu-mua.component.css']
})
export class PhieuMuaComponent implements OnInit{
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
  dsChiTiet:any=[]
  showPhieuMua:boolean= false
  phieuMua:any
  showDK:boolean=false

  filter:any=[];
  filterForm!:FormGroup;

  POSTS: any;
  page:any = 1;
  sortOrder:any=1;
  counts:any = 0;
  tableSize: number = 10;
  tableSizes: any = [10,15,20,25,30];


  constructor( private service:NguoimuaService, private spinner:NgxSpinnerService, private toastr:ToastrService, private fb:FormBuilder, private router:Router, private route:ActivatedRoute){}

  ngOnInit(): void {
    if(this.filter.length == 0){
      this.filter.size= this.tableSize;
      this.filter.sortOrder = 1;
    }   
    this.filterForm = new FormGroup({
      size: new FormControl(''),
      sortOrder: new FormControl(''),
    });    
    this.LoadPhieu(this.page, this.filter.size,this.idPhongBan, this.sortOrder, this.idUser, this.chucVu);
    this.showPhieuMua = false
  }
  LoadPhieu(page:any, size:any, phongBan:any, sortOrder:any, idUser:any, chucVu:any){
    this.service.GetPhieuMua(page,size,phongBan, sortOrder, idUser,chucVu).subscribe(
      (data:any)=>{
        this.DsPhieu=data.value;
        this.counts=data.serializerSettings;
        console.log("dddddđ",this.DsPhieu)
      },
      err => {
        console.error('err',err);
        if(err.status == 403){
          this.router.navigate(['/error']);
        };
      }
    )
  }
  combinedLoad(id:any){
  this.loadPhieubyid(id)
  this.loadChiTietPhieu(id)
  }
  loadChiTietPhieu(id:any){
  this.service.GetChiTietPhieuMua(id).subscribe((res:any)=>{
  console.log("dasta chitiets",res)
  this.dsChiTiet =res.value
  this.showPhieuMua = true

} 
)  }
 loadPhieubyid(id:any){
  this.service.GetPhieuById(id).subscribe(res=>{
   this.phieuMua = res
   console.log(this.phieuMua)
   if(this.phieuMua.soLanSuaPhieu == null)
   {
    this.showDK= false
   }else{
    this.showDK= true
   }
  })
 }
  formatCurrency(value: number): string {
    if (!value) return '0đ';
    const formattedValue = value.toLocaleString('vi-VN');    
    return `${formattedValue}đ`;
  }
  onTableDataChange(event: any){
    this.page = event;
    this.LoadPhieu(this.page, this.tableSize,this.idPhongBan, this.sortOrder, this.idUser, this.chucVu);    
  }
  filterSelect(){
    this.tableSize = this.filterForm.get('size')?.value;
    this.sortOrder = this.filterForm.get('sortOrder')?.value;
    this.LoadPhieu(this.page, this.tableSize,this.idPhongBan, this.sortOrder, this.idUser, this.chucVu);
  }
}
