import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PhieuSuaService } from 'src/app/service/PhieuSua/phieu-sua.service';

@Component({
  selector: 'app-all-phieu-sua',
  templateUrl: './all-phieu-sua.component.html',
  styleUrls: ['./all-phieu-sua.component.css']
})
export class AllPhieuSuaComponent implements OnInit{
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
  status:any=1 ;
  
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
      this.filter.status = 1;
    }   
    this.filterForm = new FormGroup({
      size: new FormControl(''),
      status: new FormControl(''),
    });   
    this.LoadPhieu(this.idPhongBan, this.tableSize, this.page,  this.status); 
  }

  LoadPhieu(idPhongBan:any, size:any, pg:any,status:any){
    this.service.GetAllStatusPhieuByPB(idPhongBan, size, pg, status).subscribe(
      (data:any)=>{
        console.log("ds all phieu sua", data)
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
      console.log("phieu", this.chiTietPhieu)
    })

  }
  onTableDataChange (event: any){
    console.log("even", event);
    this.page = event;
    this.LoadPhieu(this.idPhongBan, this.tableSize, this.page,  this.status);    
  }
  filterSelect(){
    this.tableSize = this.filterForm.get('size')?.value;
    this.status = this.filterForm.get('status')?.value;
    this.LoadPhieu(this.idPhongBan, this.tableSize, this.page,  this.status); 
  }
}
  