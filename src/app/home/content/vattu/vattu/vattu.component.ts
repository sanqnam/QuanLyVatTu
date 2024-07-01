import { Component, OnInit,Input } from '@angular/core';
import { FormBuilder, Validator, Validators,ReactiveFormsModule, FormGroup, FormControl} from '@angular/forms';  
import { VattuService } from 'src/app/service/vattu/vattu.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute} from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgImageSliderModule } from 'ng-image-slider';
import { ShareDataService } from 'src/app/service/shareData/share-data.service';

@Component({
  selector: 'app-vattu',
  templateUrl: './vattu.component.html',
  styleUrls: ['./vattu.component.css']
})
export class VattuComponent implements OnInit{
  title='Vật tư'
  search:any;
  DsVatTu:any=[]
  vattu:any=[];
  idVatTu:any=[];
  val:Object = {}
    
  imageObject: Array<object> = [];
  urls: Array<String> = [];
  width: Number = 500;
  height: Number = 500;

  POSTS: any;
  page:any = 1;
  count:any = 0;
  tableSize: number = 20;
  tableSizes: any = [30,40,50];


  constructor(private service:VattuService, private spinner: NgxSpinnerService, 
    private toastr: ToastrService, private imgSlie: NgImageSliderModule,
    private fb: FormBuilder, private router:Router, private route:ActivatedRoute, private shareData:ShareDataService){      

  }
  ngOnInit():void{
    
  }
  

  loadVatTu(page:any,size:any,search:any){
    if(this.search== ""){
      this.DsVatTu=null;
      this.count=0
      console.log("dsvattu null", this.DsVatTu)
    }else{      
      this.service.Search(page,size,search).subscribe((data:any)=>{
        this.DsVatTu=data.value;
        console.log("dsvattu !null", this.DsVatTu)
        for(let vattu of this.DsVatTu){
          if(vattu.hinhAnhVatTus.length >= 1){
            for(let hinh of vattu.hinhAnhVatTus){
              var obj = {
                image: hinh.urlHinhAnh
              };
              this.imageObject.push(obj)
            }
          }
        }
        this.count=data.serializerSettings;
      })
    }
  }
  Search(){
    this.page=1;
    this.loadVatTu(this.page,this.tableSize,this.search);
  }
  onTableDataChange (event: any){
    this.page = event;
    this.loadVatTu(this.page,this.tableSize,this.search);    
  }
  AddQuanTam(idVatTu:any){
    this.val = {
      idUser:Number(localStorage.getItem('idUser')),
      idVatTu: idVatTu
  }
    this.service.AddQuanTam(this.val).subscribe(res =>{
      if(res == 1){
        this.toastr.success("Đã thêm vào danh sách quan tâm",'Thành Công',  {
          timeOut: 2000,
        });
      }else{
        this.toastr.error("Vật tư đã tồn tại trong danh sách",'không thành Công', {
          timeOut: 2000,
        });
      }
    })
  }
}

