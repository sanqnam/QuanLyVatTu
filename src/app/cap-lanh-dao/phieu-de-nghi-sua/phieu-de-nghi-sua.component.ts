import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PhieuSuaService } from 'src/app/service/PhieuSua/phieu-sua.service';

@Component({
  selector: 'app-phieu-de-nghi-sua',
  templateUrl: './phieu-de-nghi-sua.component.html',
  styleUrls: ['./phieu-de-nghi-sua.component.css']
})
export class PhieuDeNghiSuaComponent implements OnInit{
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
  isDuyet:boolean=false
  
  // hình ảnh
  currentIndex :any =0;
  dsImg:any = []

  
  filter:any=[];
  filterForm!:FormGroup;

  POSTS: any;
  page:any = 1;
  sortOrder:any=1;
  counts:any = 0;
  tableSize: number = 10;
  tableSizes: any = [10,15,20,25,30];

  constructor(private service: PhieuSuaService, private fb:FormBuilder, private router:Router, private route:ActivatedRoute){}

  ngOnInit(): void {
    if(this.filter.length == 0){
      this.filter.size= this.tableSize;
    }   
    this.filterForm = new FormGroup({
      size: new FormControl(''),
    });    
    this.LoadPhieu(this.idPhongBan, this.idUser, this.page, this.tableSize);   
    this.isDuyet = false;
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

  XuLy(phieu:any){
    console.log("pheiu", phieu)
    this.idPhieu = phieu.idPhieuSuaChua;
    console.log("pheiu2", this.idPhieu)
  }
  LoadPhieu(idPhongBan:any, idUser:any,page:any, size:any,){
    this.service.GetAllPhieu(idPhongBan,idUser, page, size).subscribe(
      (data:any)=>{
        this.DsPhieu=data.value;
        this.counts=data.serializerSettings;
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
      console.log("phieussss", this.chiTietPhieu)
    })
  }
  onTableDataChange (event: any){
    console.log("even", event);
    this.page = event;
    this.LoadPhieu(this.idPhongBan, this.idUser, this.page, this.tableSize);    
  }
  filterSelect(){
    this.tableSize = this.filterForm.get('size')?.value;
    this.LoadPhieu(this.idPhongBan, this.idUser, this.page, this.tableSize); 
  }
}
