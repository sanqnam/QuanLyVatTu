import { Component, OnInit,Input } from '@angular/core';
import { FormBuilder, Validator, Validators,ReactiveFormsModule, FormGroup, FormControl} from '@angular/forms';  
import { UserService } from 'src/app/service/user/user.service';
import { ToastrService } from 'ngx-toastr';
import { PhongBanService } from 'src/app/service/phongban/phongban.service';
import { Router, ActivatedRoute} from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ShareDataService } from 'src/app/service/shareData/share-data.service';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit{


  title='Danh Sách User'
  togSearch:boolean=false;
  searchClass:string="search-bar";

  res:any
  DsUser:any=[];
  user:any=[];
  isDisabled:boolean = true;
  dsPhongBan:any=[];
  phongBan:any=0;
  dsChucVu:any=[];
  chucVu:any=0;
  lable:string="";
  filter:any=[];
  filterForm!:FormGroup;
  search:any;
      // phan trang
      //title = 'phantrang';
      POSTS: any;
      page:any = 1;
      count:any = 0;
      tableSize: number = 10;
      tableSizes: any = [10,15,20,25,30];
     //!-- End Phan Trang
     // Search
     //!-- End Search
  

  constructor(private service:UserService, private pbService:PhongBanService, 
    private spinner: NgxSpinnerService, private toastr: ToastrService,private fb: FormBuilder,
    private router:Router, private route:ActivatedRoute, private shareData:ShareDataService){      
      // this.shareData.getSearch.subscribe(msg => this.search = msg);
  }
  
  ngOnInit():void{
    if(this.filter.length == 0){
      this.filter.size= this.tableSize;
      this.filter.idPhongBan = 0;
      this.filter.idChucVu = 0;
    }
    this.filterForm = new FormGroup({
      size: new FormControl(''),
      idChucVu: new FormControl(''),
      idPhongBan: new FormControl(''),
    });    
    this.LoadUsers(this.page, this.filter.size,this.filter.idPhongBan, this.filter.idChucVu);
    this.GetPhongBan();
    this.GetChucVu();  
    
  }

  LoadUsers(page:any, size:any, phongBan:any, chucVu:any){
    // this.filter=this.filterForm.value;
    this.service.GetAll(page,size,phongBan, chucVu).subscribe(
      (data:any)=>{
        this.DsUser=data.value;
        this.count=data.serializerSettings;
        console.log('user',data)
      },
      err => {
        console.error('err',err);
        if(err.status == 403){
          this.router.navigate(['/error']);
        };
      }
    )
  }
  activeUser(idUser:any, username:any, isActive:any){
    console.log('1',this.filter)
    if(isActive==1){
      this.lable = "Xử lý Vô Hiệu tài khoản: ";
    }else{
      this.lable = "Xử lý Kích Hoạt tài khoản: ";
    }
    if(confirm(this.lable+username)== true){
      this.spinner.show();
      setTimeout(() => {
        this.spinner.hide();
        this.service.ActiveUser(idUser).subscribe(data=>{
          if(data==0){
            this.LoadUsers(this.page, this.filter.size,this.filter.idPhongBan, this.filter.idChucVu);
            this.toastr.warning('Tài khoản: '+username,'Đã vô hiệu', {
              timeOut: 2000,
            });
          }else if(data==1){
            this.LoadUsers(this.page, this.filter.size,this.filter.idPhongBan, this.filter.idChucVu);
            this.toastr.success('Tài khoản: '+username,'Đã kích hoạt', {
              timeOut: 2000,
            });          
          }
        })
      }, 1000);
    }
  }
  chiTietUser(user:any){
    this.user=user;
  }
  GetPhongBan(){
    this.pbService.GetAll().subscribe(pbData =>{
      this.dsPhongBan=pbData;
    })
  }
  GetChucVu(){    
    this.service.GetChucVu().subscribe(cvData =>{
      this.dsChucVu=cvData;
    })
  }
  resetPass(idUser:any,username:any){
    if(confirm('Đặt lại mật khẩu cho: '+username)== true){
      this.service.ResetPass(idUser).subscribe(res =>{
        this.toastr.success(res+username,'Thành Công', {
          timeOut: 1000,
        });
      })
    }
  }
  // phan trang
  onTableDataChange (event: any){
    this.page = event;
    this.LoadUsers(this.page, this.filter.size,this.filter.idPhongBan, this.filter.idChucVu);    
  }
  filterSelect(){
    this.filter=this.filterForm.value;
    this.page = 1;
    this.LoadUsers(this.page, this.filter.size,this.filter.idPhongBan, this.filter.idChucVu);
  }
  toggleSearch(){
    if(this.togSearch==false){
      this.togSearch=true;
      this.searchClass="search-bar search-bar-show";
    }else{
      this.togSearch=false;
      this.searchClass="search-bar";
    }
  }
  Search(){
    if(this.search== ""){
      this.ngOnInit();
    }else{
      this.filter=this.filterForm.value;
      if(this.filter.size=='')
      {
        this.filter.size= this.tableSize;
      }
      this.service.Search(this.page,this.filter.size,this.search).subscribe((data:any)=>{
        this.DsUser=data.value;
        this.count=data.serializerSettings;
      })
    }
  } 
}
