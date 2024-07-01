import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validator, Validators } from '@angular/forms';  
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/service/user/user.service';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { group } from '@angular/animations';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit{
  
  disabled = true;
  isDisabled:boolean = true;
  profile:any=[];
  idUser:any;
  editProfile!:FormGroup;
  val:any;
  url="";
  id:any= localStorage.getItem("idUser");
  currentRole:any
  displayLanhDao:any

  displayNew:any= false
  displayOld:any= false
  displayRe:any= false
  displayTong:any = false
  display:any= false
  displayQuanTri:any
  groupMaBiMat!:FormGroup;
  sendUrl(url:string){
    this.url = url;
    console.log("urlNew", this.url);
    this.editProfile.patchValue({
      hinhDaiDien:this.url
    })
  }
  constructor(private fb: FormBuilder, private toastr: ToastrService,
      private service: UserService, private router:Router){
       }
      
  ngOnInit(): void {
    this.idUser = new Number(localStorage.getItem('idUser'));
    this.LoadProfile();
    this.editProfile = new FormGroup({
      username: new FormControl(''),
      hoTen: new FormControl(''),
      email: new FormControl(''),
      dienThoai: new FormControl(''),
      diaChi: new FormControl(''),
      aboutMe: new FormControl(''),
      idChucVu: new FormControl(''),//{ value: "", disabled: true }, Validators.required),
      idPhongBan: new FormControl(''),//{ value: "", disabled: true }, Validators.required),
      hinhDaiDien:new FormControl(''),  
      isActive:new FormControl('')    
    });
    this.groupMaBiMat =this.fb.group({
      oldMa: ['', Validators.required],
      newMa: ['', Validators.required],
      reNewMa: ['', Validators.required]
    })
  }

  LoadProfile(){    
    this.service.GetByIdUser(this.idUser).subscribe((res:any)=>{
      this.profile = res;
      console.log(this.profile);
      if(this.profile.aboutMe == null){
        this.profile.aboutMe = "Thông tin đang cập nhật";
      }
      this.editProfile.patchValue({
        username:res.username,
        hoTen:res.hoTen,
        email:res.email,
        dienThoai:res.dienThoai,
        diaChi:res.diaChi,
        aboutMe:res.aboutMe,
        hinhDaiDien:res.hinhDaiDien,
        idPhongBan:res.idPhongBan,
        idChucVu:res.idChucVu,
        isActive:res.isActive
      })
    })
    this.MenuDisplay();
  }
  MenuDisplay(){
    this.currentRole = localStorage.getItem('maChucVu');
    this.displayLanhDao = ['PP', 'TP', 'GD', 'PGD'].includes(this.currentRole);

  }
  submit(){ 
    this.editProfile.get('username')?.enable();
    this.editProfile.get('isActive')?.enable();
    this.val = this.editProfile.value;
    console.log(this.val);
    this.service.UpdateUser(this.idUser,this.val).subscribe(res =>{
      if(res=="Đã chỉnh sửa thông tin"){
        this.toastr.success('Cập nhật thông tin thành công ','Successfully', {
          timeOut: 1000,
        }) 
        
        if(confirm("Đăng nhập lại để làm mới thông tin")== true){
          localStorage.clear();
          this.router.navigate(['/login'])
        }else{
          sessionStorage.clear();
          this.LoadProfile();
          const idUser:string = this.idUser;
          const username:string = this.profile.username;
          const hoten:string = this.profile.hoTen;
          const maPhongBan:string = this.profile.maPhongBan;
          const maChucVu:string = this.profile.maChucVu;
          const hinhDaiDien:string = this.profile.hinhDaiDien;
          localStorage.setItem('idUser',idUser);
          localStorage.setItem('username',username);
          localStorage.setItem('hoten',hoten);
          localStorage.setItem('maPhongBan',maPhongBan);
          localStorage.setItem('maChucVu',maChucVu);      
          localStorage.setItem('hinhDaiDien',hinhDaiDien);  
          this.router.navigate(['/']);  
        }       
      }
      
    })
  }
  public createImgPath = (serverPath: string) => { 
    return `${serverPath}`;
  }

  public sendToHeader(){

  }
  sendToMa(){
    let val = this.groupMaBiMat.value
    var newMa = this.groupMaBiMat.get('newMa')?.value;
    let oldMa = this.groupMaBiMat.get('oldMa')?.value;
    let reNewMa = this.groupMaBiMat.get('reNewMa')?.value;
    console.log(newMa, oldMa,reNewMa)
    console.log("ma bi mat", val);
    this.setDisplay(newMa, oldMa, reNewMa)
    if(reNewMa !== newMa){
      this.groupMaBiMat.get('reNewMa')?.setErrors({ notMatch: true });
      this.display = true
      console.log("in tree", this.display)
      this.displayTong = true
    }else{
      this.display = false;
      this.displayTong = false
    }
    if(this.displayTong == false){
     this.service.DoiMaBiMat(this.id,val).subscribe((res:any)=>{
      console.log("res ",res)
     })
    }
    
  }
  setDisplay(newm:any, old:any, renew:any){
    console.log( "maaaa",newm);
    this.displayNew = newm === '';
    this.displayOld = old === '';
    this.displayRe = renew === '';
    this.displayTong = this.displayNew || this.displayOld || this.displayRe;
}
  
}