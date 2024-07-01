import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhongBanService } from 'src/app/service/phongban/phongban.service';

@Component({
  selector: 'app-edit-phong-ban',
  templateUrl: './edit-phong-ban.component.html',
  styleUrls: ['./edit-phong-ban.component.css']
})
export class EditPhongBanComponent implements OnInit{
  @Input('chiTietPhongBan')
  phongBan:any=[];
  editPhongBan!:FormGroup;
  val:any;
  constructor(private fb:FormBuilder, private toaste: ToastrService, private service: PhongBanService, private router: Router, private spinner: NgxSpinnerService){}

  ngOnInit(): void {
    this.editPhongBan = this.fb.group({
      tenPhongBan : ['', [Validators.required]],
      maPhongBan: ['',[Validators.required]]
    })
  }

  submit(){
    this.val = this.editPhongBan.value;
    console.log("val", this.val)
    this.spinner.show();
    console.log("idchuvu", this.phongBan.idPhongBan);
    setTimeout(()=>{
      this.spinner.hide();
      this.service.Update(this.phongBan.idPhongBan, this.val).subscribe(res =>{
        console.log(res);
        this.toaste.success('sửa thành công '+this.phongBan.tenChucVu,res,{
          timeOut:2000,
        })
      })
    },1000)
  }
}
