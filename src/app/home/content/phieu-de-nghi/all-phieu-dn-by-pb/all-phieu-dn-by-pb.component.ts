import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhieuDNVTService } from 'src/app/service/PhieuDNVT/phieu-dnvt.service';

@Component({
  selector: 'app-all-phieu-dn-by-pb',
  templateUrl: './all-phieu-dn-by-pb.component.html',
  styleUrls: ['./all-phieu-dn-by-pb.component.css']
})
export class AllPhieuDnByPbComponent implements OnInit{


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
  sortOrder:any=2;
  counts:any = 0;
  tableSize: number = 10;
  tableSizes: any = [10,15,20,25,30];
  choose:any = 1;

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
    this.LoadPhieu(this.page, this.filter.size,this.idPhongBan, this.sortOrder, this.choose);
  }
  LoadPhieu(page:any, size:any, phongBan:any, sortOrder:any,  choose:any){
    // this.filter=this.filterForm.value;
    this.service.GetAllTheoPhongBan(page,size,phongBan, sortOrder, choose).subscribe(
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
    this.LoadPhieu(this.page, this.tableSize,this.idPhongBan, this.sortOrder,this.choose);    
  }
  filterSelect(){
    this.tableSize = this.filterForm.get('size')?.value;
    this.sortOrder = this.filterForm.get('sortOrder')?.value;
    this.choose = this.filterForm.get('choose')?.value;
    console.log(" size ", this.tableSize, "--- oder ", this.sortOrder,"--- hosss", this.choose)
    this.LoadPhieu(this.page, this.tableSize,this.idPhongBan, this.sortOrder,  this.choose);
  }
}
