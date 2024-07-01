import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhieuSuaService } from 'src/app/service/PhieuSua/phieu-sua.service';

@Component({
  selector: 'app-phieu-dang-xu-ly',
  templateUrl: './phieu-dang-xu-ly.component.html',
  styleUrls: ['./phieu-dang-xu-ly.component.css']
})
export class PhieuDangXuLyComponent implements OnInit{
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
    this.service.GetAllPhieuDangCho(idUser, page, size, sortOrder, chosse).subscribe((res:any)=>{
      console.log("res", res)
      this.dsPhieu = res.value
      this.counts = res.serializerSettings
    })
  }
  chiTiet(phieu:any){
    this.phieu = phieu;
    this.idPhieu = phieu.idPhieuSuaChua;
    console.log("phieuư", this.idPhieu);
    this.service.GetVatTuByPhieu(this.idPhieu).subscribe((res:any)=>{
      console.log("res", res)
      this.chiTietPhieu =res.value;
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
