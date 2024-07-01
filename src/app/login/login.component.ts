import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validator, Validators } from '@angular/forms';  
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../service/auth/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  chucvu:any=[]
  notification:string="";
  constructor(private builder: FormBuilder, private toastr: ToastrService,
    private service: AuthService, private router:Router, private spinner: NgxSpinnerService){} 

  loginForm = this.builder.group({
    Username:this.builder.control('',Validators.required),
    MatKhau:this.builder.control('',Validators.required)
  })
  ngOnInit(): void {
    this.LoadChucVu()
    this.checkLogin()
  }
  LoadChucVu(){
    this.chucvu=['GD','PGD','TP','PP','QTri','T.Kho']
  }
  checkLogin(){
    if(localStorage.getItem('token')!=null){
      this.router.navigate(['/']); 
    };
  }
  loginSubmit(){
    this.spinner.show();
    if(this.loginForm.valid){
      setTimeout(() => {
        this.service.Login(this.loginForm.value).subscribe((result:any) =>{ 
          this.spinner.hide();
          if(result == 1){
            this.notification = "Username hoặc Mật khẩu không chính xác";
            this.toastr.error('Đăng nhập không thành công', this.notification, {
              timeOut: 3000,
            });
          }else if(result == 2){
            this.notification = "Tài khoản bị khóa bởi Admin";
            this.toastr.error('Đăng nhập không thành công', this.notification, {
              timeOut: 3000,
            });
          }else {
            localStorage.setItem('token',result);
            this.service.GetName(this.loginForm.value).subscribe((res:any)=>{
              const idUser:string = res.idUser;
              const username:string = res.username;
              const hoten:string = res.hoTen;
              const maPhongBan:string = res.maPhongBan;
              const idPhongBan:string = res.idPhongBan;
              const maChucVu:string = res.maChucVu;
              const hinhDaiDien:string = res.hinhDaiDien;
              localStorage.setItem('idUser',idUser);
              localStorage.setItem('username',username);
              localStorage.setItem('hoten',hoten);
              localStorage.setItem('maPhongBan',maPhongBan);
              localStorage.setItem('idPhongBan',idPhongBan);
              localStorage.setItem('maChucVu',maChucVu);      
              localStorage.setItem('hinhDaiDien',hinhDaiDien);     
              if(localStorage.length != null){
                for(let i=0; this.chucvu.length > i;i++){
                  console.log("chuc vu4444", this.chucvu[i]);
                  if(maChucVu == this.chucvu[i]){
                    console.log("chuc vu", this.chucvu[i]);
                    this.router.navigate(['/dashboard']);     
                    this.toastr.success('Đăng nhập thành công','Login Successfully', {
                      timeOut: 1000,
                    })  
                    return;
                  }
                }
                  console.log("chuc vu2222", maChucVu);
                  this.router.navigate(['/']);     
                  this.toastr.success('Đăng nhập thành công','Login Successfully', {
                    timeOut: 1000,
                  })  
              } 
            });
          }     
        })
      },1000);
    }else{
      this.toastr.warning('Nhập đầy đủ thông tin để đăng nhập');
    }
  }
}
