import { Component, Input, OnInit} from '@angular/core';
import { FormBuilder, Validator, Validators,ReactiveFormsModule} from '@angular/forms';  
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/service/user/user.service';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  @Input('chiTietUser') 
    user:any=[];
  @Input('PhongBan')
    dsPhongBan:any=[];
  @Input('ChucVu')
    dsChucVu:any=[];

  editUser!:FormGroup;
  val:any;
  isDisable:boolean = true;
  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private service: UserService, private router:Router,private spinner: NgxSpinnerService){

     }

  ngOnInit(): void {    
    
      this.editUser = new FormGroup({
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
    
    this.val = this.editUser.value;
    this.spinner.show();
    setTimeout(() => {
      this.spinner.hide();
      this.service.UpdateUser(this.user.idUser,this.val).subscribe(res=>{
        console.log(res);
        this.toastr.success('Tài khoản: '+this.user.username, res, {
          timeOut: 2000,
        });
      })
    }, 1000);
  }

  
}
