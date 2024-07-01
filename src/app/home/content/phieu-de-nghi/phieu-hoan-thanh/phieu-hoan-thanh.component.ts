import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhieuDNVTService } from 'src/app/service/PhieuDNVT/phieu-dnvt.service';

@Component({
  selector: 'app-phieu-hoan-thanh',
  templateUrl: './phieu-hoan-thanh.component.html',
  styleUrls: ['./phieu-hoan-thanh.component.css']
})
export class PhieuHoanThanhComponent implements OnInit{

  idUser :any =localStorage.getItem("idUser");
  idPhongBan :any = localStorage.getItem("idPhongBan");
  chucVu:any = localStorage.getItem("maChucVu");
  dsPhieu:any=[];
  phieu:any=[];
  idPhieu:any=[];
  chiTietPhieu:any =[];
  

  filter:any=[];
  filterForm!:FormGroup;

  POSTS: any;
  page:any = 1;
  sortOrder:any=1;
  counts:any = 0;
  tableSize: number = 10;
  tableSizes: any = [10,15,20,25,30];
  choose:any=1;
  // choose == 1 thì show phiếu đã xử lý, còn == 2 thì showw phiếu trả

  constructor(private service:PhieuDNVTService, private spinner:NgxSpinnerService, private toastr:ToastrService, private fb:FormBuilder, private router:Router, private route:ActivatedRoute)
  {}
  ngOnInit(): void {
    if(this.filter.length == 0){
      this.filter.size= this.tableSize;
      this.filter.sortOrder = 1;
      this.filter.choose =1;
    }   
    this.filterForm = new FormGroup({
      size: new FormControl(''),
      sortOrder: new FormControl(''),
      choose: new FormControl(''),
    });    
    this.LoadPhieu(this.page, this.filter.size,this.idUser, this.sortOrder, 2, 1);
  }
  LoadPhieu(page:any, size:any, phongBan:any, sortOrder:any, status:any, choose:any){
    // this.filter=this.filterForm.value;
    this.service.GetAllSapXep(page,size,phongBan, sortOrder,status, choose).subscribe(
      (data:any)=>{
        this.dsPhieu=data.value;
        this.counts=data.serializerSettings;
        console.log('dsPhieu',data)
        console.log('count', this.counts)
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
    this.idPhieu = phieu.idPhieuDeNghi;
    console.log("phieuư", this.phieu);
    this.service.GetById(phieu.idPhieuDeNghi).subscribe(res=>{
      this.chiTietPhieu =res;
      console.log("phieuư", this.chiTietPhieu);
    })
  }
  onTableDataChange (event: any){
    console.log("even", event);
    this.page = event;
    this.LoadPhieu(this.page, this.tableSize,this.idUser, this.sortOrder, 2,this.choose);    
  }
  filterSelect(){
    this.tableSize = this.filterForm.get('size')?.value;
    this.sortOrder = this.filterForm.get('sortOrder')?.value;
    this.choose = this.filterForm.get('choose')?.value;
    this.LoadPhieu(this.page, this.tableSize,this.idUser, this.sortOrder, 2, this.choose);
  }
}
