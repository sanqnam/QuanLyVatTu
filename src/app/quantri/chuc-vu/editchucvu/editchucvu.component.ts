import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ChucvuService } from 'src/app/service/chucvu/chucvu.service';

@Component({
  selector: 'app-editchucvu',
  templateUrl: './editchucvu.component.html',
  styleUrls: ['./editchucvu.component.css']
})
export class EditchucvuComponent implements OnInit{
  @Input('chiTietChucVu')
  chucVu:any=[];
  editChucVu!:FormGroup;
  val:any;
  constructor(private fb:FormBuilder, private toaste: ToastrService, private service: ChucvuService, private router: Router, private spinner: NgxSpinnerService){}

  ngOnInit(): void {
    this.editChucVu = this.fb.group({
      tenChucVu : ['', [Validators.required]],
      maChucVu: ['',[Validators.required]]
    })
  }
  submit(){
    this.val = this.editChucVu.value;
    console.log("val", this.val)
    this.spinner.show();
    console.log("idchuvu", this.chucVu.idChucVu);
    setTimeout(()=>{
      this.spinner.hide();
      this.service.Update(this.chucVu.idChucVu, this.val).subscribe(res =>{
        console.log(res);
        this.toaste.success('sửa thành công '+this.chucVu.tenChucVu,res,{
          timeOut:2000,
        })
      })
    },1000)
  }
}
