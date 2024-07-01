import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { PhieuDNVTService } from 'src/app/service/PhieuDNVT/phieu-dnvt.service';
import { ShareDataService } from 'src/app/service/shareData/share-data.service';

@Component({
  selector: 'app-phieuduyet',
  templateUrl: './phieuduyet.component.html',
  styleUrls: ['./phieuduyet.component.css']
})
export class PhieuduyetComponent  implements OnInit{

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

  filter:any=[];
  filterForm!:FormGroup;

  POSTS: any;
  page:any = 1;
  sortOrder:any=1;
  counts:any = 0;
  tableSize: number = 10;
  tableSizes: any = [10,15,20,25,30];
  
  constructor(private service:PhieuDNVTService, private spinner:NgxSpinnerService, private toastr:ToastrService, private fb:FormBuilder, private router:Router, private route:ActivatedRoute, private shareData: ShareDataService){}

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
    if(this.chucVu == 'PGD'|| this.chucVu =='GD'){
      this.display = 1;
    }
    else{
      this.display =2;
    }
  }
  LoadPhieu(page:any, size:any, phongBan:any, sortOrder:any, idUser:any, chucVu:any){
    // this.filter=this.filterForm.value;
    this.service.GetAllPhieuDuyet(page,size,phongBan, sortOrder, idUser,chucVu).subscribe(
      (data:any)=>{
        this.DsPhieu=data.value;
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
    console.log("phieu", this.phieu);
    console.log("phieuư", phieu.idPhieuDeNghi);
    this.service.GetById(phieu.idPhieuDeNghi).subscribe(res=>{
      this.chiTietPhieu =res;
      console.log("phieuưwww", this.chiTietPhieu);
    })
    console.log("display", this.display);
  }
  onTableDataChange (event: any){
    console.log("even", event);
    this.page = event;
    this.LoadPhieu(this.page, this.tableSize,this.idPhongBan, this.sortOrder, this.idUser, this.chucVu);    
  }
  filterSelect(){
    this.tableSize = this.filterForm.get('size')?.value;
    this.sortOrder = this.filterForm.get('sortOrder')?.value;
    this.LoadPhieu(this.page, this.tableSize,this.idPhongBan, this.sortOrder, this.idUser, this.chucVu);
  }
}
