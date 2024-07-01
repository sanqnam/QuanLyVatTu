import { Component, Input, OnInit} from '@angular/core';
import { FormBuilder, Validator, Validators,ReactiveFormsModule} from '@angular/forms';  
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/service/user/user.service';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { HttpResponse, HttpStatusCode } from '@angular/common/http';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  @Input('PhongBan')
    dsPhongBan:any=[];
  @Input('ChucVu')
    dsChucVu:any=[];
  addUser!:FormGroup;
  val:any;
  isDisable:boolean = true;
  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private service: UserService, private router:Router,private spinner: NgxSpinnerService){ 
     }
  ngOnInit(): void {    
      this.addUser = new FormGroup({
        username: new FormControl(''),
        hoTen: new FormControl(''),
        email: new FormControl(''),
        dienThoai: new FormControl(''),
        diaChi: new FormControl(''),
        idChucVu: new FormControl(''),
        idPhongBan: new FormControl(''),
        isActive: new FormControl (''),
        hinhDaiDien:new FormControl(''),
      });
  }
  submit(){ 
    this.val = this.addUser.value;
    console.log('AddUser',this.val)
    this.spinner.show();
    setTimeout(() => {
      this.spinner.hide();
      this.service.AddUser(this.val).subscribe(
        res=>{
        this.toastr.success('Tạo tài khoản thành công', res, {
          timeOut: 2000,
          });
        },
        err => {
          console.error('err',err);
          if(err.status == 400){
            this.toastr.error('Username hoặc Email đã tồn tại', 'Error', {
              timeOut: 10000,
              });
          }
      })   
    }, 1000);
  }
}
