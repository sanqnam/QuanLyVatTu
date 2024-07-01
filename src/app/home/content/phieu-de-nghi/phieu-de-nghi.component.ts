
import { AfterContentChecked, Component, DoCheck, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { PhieuDNVTService } from 'src/app/service/PhieuDNVT/phieu-dnvt.service';
import { ShareDataService } from 'src/app/service/shareData/share-data.service';

@Component({
  selector: 'app-phieu-de-nghi',
  templateUrl: './phieu-de-nghi.component.html',
  styleUrls: ['./phieu-de-nghi.component.css']
})
export class PhieuDeNghiComponent implements OnInit{
  res:any
  dsPhieu:any=[];
  phieu:any=[];
  idPhieu:any=[];
  DsChiTietPhieu:any=[];
  chiTietPhieu:any =[];
  filterform!:FormGroup;
  idUser :any =localStorage.getItem("idUser");
  idPhongBan :any = localStorage.getItem("idPhongBan");
  chucVu:any = localStorage.getItem("maChucVu");
  status:any = 1;

  getTB:any;

  messageTB:any

 // count:any;
  nutDuyet:any;

  activeShow1:string='';
  activeShow2:string='';
  activeShow3:string='';

  filter:any=[];
  filterForm!:FormGroup;

  POSTS: any;
  page:any = 1;
  sortOrder:any=1;
  counts:any = 0;
  tableSize: number = 10;
  tableSizes: any = [10,15,20,25,30];

  private querySub: Subscription = new Subscription();

  constructor(private service:PhieuDNVTService, private spinner:NgxSpinnerService, private toastr:ToastrService, private fb:FormBuilder, private router:Router, private route:ActivatedRoute, private shareData: ShareDataService)
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
    this.LoadPhieu(this.page, this.filter.size,this.idPhongBan, this.sortOrder, this.status);
  }
  LoadPhieu(page:any, size:any, phongBan:any, sortOrder:any, status:any){

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
    this.LoadPhieu(this.page, this.tableSize,this.idPhongBan, this.sortOrder, this.status);    
  }
  filterSelect(){
    this.tableSize = this.filterForm.get('size')?.value;
    this.sortOrder = this.filterForm.get('sortOrder')?.value;
    this.LoadPhieu(this.page, this.tableSize,this.idPhongBan, this.sortOrder, this.status);
  }
  
  setStatus(status:any){
    this.status= status;
      }
}   
