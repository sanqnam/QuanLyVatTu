import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, Validator, Validators,ReactiveFormsModule, FormGroup, FormControl} from '@angular/forms';  
import { VattuService } from 'src/app/service/vattu/vattu.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute} from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgImageSliderModule } from 'ng-image-slider';
import { ShareDataService } from 'src/app/service/shareData/share-data.service';

@Component({
  selector: 'app-vtquantam',
  templateUrl: './vtquantam.component.html',
  styleUrls: ['./vtquantam.component.css']
})
export class VtquantamComponent implements OnInit{
  @Output() DataVatTu: EventEmitter<any> = new EventEmitter();
  title:string = "Danh sách chờ"
  search:any;
  DsVatTu:any=[]
  vattu:any=[];
  idVatTu:any=[];
  idUser:number = Number(localStorage.getItem('idUser'))
  checked:string = "";

  dsPhieu: any = [];
  
  POSTS: any;
  page:any = 1;
  count:any = 0;
  tableSize: number = 20;
  tableSizes: any = [30,40,50];

  constructor(private service:VattuService, private spinner: NgxSpinnerService, 
    private toastr: ToastrService, private imgSlie: NgImageSliderModule,
    private fb: FormBuilder, private router:Router, private route:ActivatedRoute, 
    private shareData:ShareDataService){      
    }
  ngOnInit():void{
    this.LoadVatTu()
    this.reset()
  }
  LoadVatTu(){
    this.service.GetDsCho(this.idUser).subscribe((data:any) =>{
    this.DsVatTu = data.value;
    console.log("ds quan tâm ", this.DsVatTu);
    })
  }
  reset(){
    this.dsPhieu =[]

  }
  Search(){

  }
  onTableDataChange (event: any){
    this.page = event;
    //this.loadVatTu(this.page,this.tableSize,this.search);    
  }
  CheckBox(val:any){
    var obj = val
    let find = this.dsPhieu.find((o:any)=>o.idVatTu == obj.idVatTu) 
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
}
