import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PhieuSuaService } from 'src/app/service/PhieuSua/phieu-sua.service';

@Component({
  selector: 'app-nv-phu-trach',
  templateUrl: './nv-phu-trach.component.html',
  styleUrls: ['./nv-phu-trach.component.css']
})
export class NvPhuTrachComponent implements OnInit{
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
  dsImg:any =[]
  currentIndex=0;
  
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
    this.dsImg = [];
    this.LoadPhieu( this.idUser, this.page, this.tableSize);  
  }
  LoadPhieu( idUser:any,page:any, size:any,){
    this.service.ShowVTCanSuaByNVSua(idUser, page, size).subscribe(
      (data:any)=>{
        this.DsPhieu=data.value;
        this.counts=data.serializerSettings;
        console.log("dsphiey ",this.DsPhieu)
      },
      err => {
        console.error('err',err);
        if(err.status == 403){
          this.router.navigate(['/error']);
        };
      }
    )
  }
  
  showHinh(idCTPhieu:any){
    this.service.GetImgByPhieu(idCTPhieu.idChiTietPhieuSua).subscribe((res:any)=>{
      
      if( res.value.length == 0){
        this.dsImg = null;      }
      else{
        console.log("resimg", res)
        this.dsImg = res.value;
      }
      console.log("dsimg", this.dsImg)
    })
  }

  onTableDataChange (event: any){
    console.log("even", event);
    this.page = event;
    this.LoadPhieu( this.idUser, this.page, this.tableSize);    
  }
  filterSelect(){
    this.tableSize = this.filterForm.get('size')?.value;
    this.LoadPhieu( this.idUser, this.page, this.tableSize); 
  }
}
