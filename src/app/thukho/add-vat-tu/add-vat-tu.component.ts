import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ThukhoService } from 'src/app/service/thukho.service';
import { VattuService } from 'src/app/service/vattu/vattu.service';

@Component({
  selector: 'app-add-vat-tu',
  templateUrl: './add-vat-tu.component.html',
  styleUrls: ['./add-vat-tu.component.css']
})
export class AddVatTuComponent implements OnInit{
  addVatTu!:FormGroup;
  val:any;
  constructor(private service: VattuService,   private toastr: ToastrService, private fb: FormBuilder,private router: Router, private route: ActivatedRoute,private spinner: NgxSpinnerService) {
  }
  ngOnInit(): void {
    this.addVatTu = this.fb.group({
      tenVatTu: ['',[Validators.required]],
      maVatTu: ['',[Validators.required]],
      donViTinh:['',[Validators.required]],
      viTri: ['',[Validators.required]],
      soLuongTonKho:['',[Validators.required]],
      thongSo:['',[Validators.required]],
    })
  }
  Submit(){
    this.val = this.addVatTu.value;
    console.log("addVatTu", this.val)
    this.spinner.show();
    setTimeout(() => {
      this.spinner.hide();
      this.service.AddVatTu(this.val).subscribe(res => {
        this.toastr.success('Thêm vât tư thành công') 
        console.log("res",res);
      },err => {
          console.error('errd', err);
          if (err.status == 400) {
            this.toastr.error('vật tư đã tôn tại', 'Error');
          }
        }
      )
    }, 100);
  }
}
