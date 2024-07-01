import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PhieuSuaService } from 'src/app/service/PhieuSua/phieu-sua.service';

@Component({
  selector: 'app-phieu-da-hoan-thanh',
  templateUrl: './phieu-da-hoan-thanh.component.html',
  styleUrls: ['./phieu-da-hoan-thanh.component.css']
})
export class PhieuDaHoanThanhComponent {
  dsPhieu:any=[];
  phieu:any=[];
  idPhieu:any=[];
  DsChiTietPhieu:any=[];
  chiTietPhieu:any =[];
  filterform!:FormGroup;
  idUser :any =localStorage.getItem("idUser");
  idPhongBan :any = localStorage.getItem("idPhongBan");
  chucVu:any = localStorage.getItem("maChucVu");

  filter:any=[];
  filterForm!:FormGroup;

  choose:any = 1
  POSTS: any;
  page:any = 1;
  sortOrder:any=1;
  counts:any = 0;
  tableSize: number = 10;
  tableSizes: any = [10,15,20,25,30];


  constructor(private service:PhieuSuaService, private spinner:NgxSpinnerService, private toastr:ToastrService, private fb:FormBuilder, private router:Router, private route:ActivatedRoute){}


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
    this.LoadPhieu(this.idUser,this.page, this.filter.size, this.sortOrder, this.filter.choose);
  }
  LoadPhieu(idUser:any,page:any, size:any, sortOrder:any, chosse:any){
    this.service.GetAllPhieuSussces(idUser, page, size, sortOrder, chosse).subscribe((res:any)=>{
      console.log("res", res)
      this.dsPhieu = res.value
      this.counts = res.serializerSettings
    })
  }
  chiTiet(phieu:any){
    this.phieu = phieu;
    this.idPhieu = phieu.idPhieuDeNghi;
    console.log("phieuư", this.phieu);
    this.service.GetVatTuByPhieu(phieu.idPhieuDeNghi).subscribe(res=>{
      this.chiTietPhieu =res;
      console.log("phieuư", this.chiTietPhieu);
    })
  }
  
 
  onTableDataChange (event: any){
    console.log("even", event);
    this.page = event;
    this.LoadPhieu(this.idUser,this.page, this.tableSize, this.sortOrder, this.choose);  
  }
  filterSelect(){
    this.tableSize = this.filterForm.get('size')?.value;
    this.sortOrder = this.filterForm.get('sortOrder')?.value;
    this.choose = this.filterForm.get('choose')?.value;
    this.LoadPhieu(this.idUser,this.page, this.tableSize, this.sortOrder, this.choose);
  }
}
