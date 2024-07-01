import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { VattuService } from 'src/app/service/vattu/vattu.service';

@Component({
  selector: 'app-vat-tu-su-dung',
  templateUrl: './vat-tu-su-dung.component.html',
  styleUrls: ['./vat-tu-su-dung.component.css']
})
export class VatTuSuDungComponent implements OnInit {
  @Output() DataVatTu: EventEmitter<any> = new EventEmitter();

  dsPhieu: any = [];
  filterForm!:FormGroup;
  filter:any=[];
  searchTenNguoiDung:any;
  searchTenVatTu:any
  // phân trang
  POSTS: any;
  page:any = 1;
  count:any = 0;
  tableSize: number = 10;
  tableSizes: any = [10,15,20,25,30];
  idPhongBan:any = localStorage.getItem("idPhongBan")


  dsVatTu: any = [];
  idUser = localStorage.getItem('idUser')
  constructor(private service: VattuService, private spinner: NgxSpinnerService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute, private toastr: ToastrService) {
  }
  ngOnInit(): void {
    if(this.filter.length == 0){
      this.filter.size= this.tableSize;
      console.log("hllllll",this.filter.size )
     
    }
    this.filterForm = new FormGroup({
      size: new FormControl(''),
    });   
    
    this.LoadVTSuDung(this.idPhongBan, this.page, this.filter.size);
    this.dsPhieu = [];
  }
  LoadSearch(idPhongBan:any, ten:any, vattu:any, pg:any, size:any) {
    this.service.GetAllBySearch(idPhongBan,ten,vattu,pg,size).subscribe((data: any) => {
      this.dsVatTu = data.value;
      this.count=data.serializerSettings;
      console.log("ds vatjt tu sd", data);
    })
  }
  LoadVTSuDung(idPhongBan:any, pg:any, size:any){
    this.service.GetAllByIdPhongBan(idPhongBan,pg,size).subscribe((data: any) => {
      this.dsVatTu = data.value;
      this.count=data.serializerSettings;
      console.log("ds vatjt tu sd", data);
    })
  }
  filterSelect(){
    this.filter=this.filterForm.value;
    this.page = 1;
    this.LoadVTSuDung(this.idPhongBan, this.page, this.filter.size);  
  }
  CheckBox(val:any){
    var obj = val
    let find = this.dsPhieu.find((o:any)=>o.idChiTietVatTu == obj.idChiTietVatTu) 
    if(find == null){
      if(this.dsPhieu.length+1 <=10){
        this.dsPhieu.push(obj)  
      }else{
        alert('Số lượng vật tư đề nghị không lớn hơn 10')
      }
    }else{
      this.dsPhieu.splice(this.dsPhieu.indexOf(obj),1);
    }
  }
  SenData(){
    this.DataVatTu = this.dsPhieu;  
    console.log("dsdata",this.DataVatTu)
    console.log("dsdata",this.dsPhieu)
  }
  // SearchTenNguoiDung(){
  //   console.log(" tên nguoi dung", this.searchTenNguoiDung)
  //   if(this.searchTenNguoiDung == null || this.searchTenNguoiDung.trim() ===''){
  //     this.ngOnInit();
  //   }else{
  //     this.filter=this.filterForm.value;
  //     if(this.filter.size==null)
  //     { 
  //       this.filter.size= this.tableSize;
  //     }
  //     this.LoadSearch(this.idPhongBan, this.searchTenNguoiDung, this.searchTenVatTu, this.page, this.filter.size);  
  //   }
  // } 
  SearchTenVatTu(){
    console.log(" tên vật tư", this.searchTenVatTu)
    if(this.searchTenNguoiDung != null||this.searchTenVatTu !=null){
      this.filter = this.filterForm.value;
      if(this.filter.size =='')
      {
        this.filter.size= this.tableSize;
      }
      if((this.searchTenNguoiDung == null || this.searchTenNguoiDung.trim()==='') && this.searchTenVatTu !='')
        {
          var nulls = 'undefined';
          this.LoadSearch(this.idPhongBan,nulls, this.searchTenVatTu, this.page, this.filter.size);
        }
        else if((this.searchTenVatTu == null || this.searchTenVatTu.trim()==='')&& this.searchTenNguoiDung!=''){
          var nulls ='undefined';
          this.LoadSearch(this.idPhongBan, this.searchTenNguoiDung, nulls, this.page, this.filter.size);
        }
        else if(this.searchTenNguoiDung !='' && this.searchTenVatTu !=''){
          this.LoadSearch(this.idPhongBan, this.searchTenNguoiDung, this.searchTenVatTu, this.page, this.filter.size);
        }
        else{
          if(this.filter.size =='')
            {
              this.filter.size= this.tableSize;
            }
          this.ngOnInit();
        }}

  } 
  onTableDataChange (event: any){
    console.log("enve", event);
    this.page = event;
    if(this.searchTenNguoiDung == null || this.searchTenNguoiDung.trim()===''||this.searchTenVatTu ==null||this.searchTenVatTu.trim()===''){
      this.LoadVTSuDung(this.idPhongBan, this.page, this.filter.size)
    }
    else{
      this.LoadSearch(this.idPhongBan, this.searchTenNguoiDung, this.searchTenVatTu, this.page, this.filter.size);
    }
        
  }
}
