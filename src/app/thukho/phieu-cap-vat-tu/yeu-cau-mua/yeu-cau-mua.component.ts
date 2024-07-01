import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ThukhoService } from 'src/app/service/thukho.service';

@Component({
  selector: 'app-yeu-cau-mua',
  templateUrl: './yeu-cau-mua.component.html',
  styleUrls: ['./yeu-cau-mua.component.css']
})
export class YeuCauMuaComponent implements OnInit{
@Input('VTMua') vtMua:any;
addMua!:FormGroup;
val:any;


  constructor(private service: ThukhoService, private spinner: NgxSpinnerService, private toastr: ToastrService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) {
  }
  ngOnInit(): void {
    this.addMua = this.fb.group({
      idChiTietPhieu:[''],
      soLuongMuaThem :['',[Validators.required]],
      giChuMuaThem:['',[Validators.required]],
    })
  }
  Submit(){
    this.addMua.patchValue({
      idChiTietPhieu:this.vtMua.idChiTietPhieu,
    })
    this.val = this.addMua.value;
    console.log("mua neee", this.val);
    this.spinner.show();
    setTimeout(()=>{
      this.spinner.hide();
      this.service.YeuCauMua(this.val).subscribe(res=>{
        this.toastr.success("Yêu cầu thành công");
        console.log("res", res);
      })
    })
  }
  reset(){
    this.addMua.patchValue({
      idChiTietPhieu:'',
      soLuongMuaThem :'',
      giChuMuaThem:'',
    })
  }
}
