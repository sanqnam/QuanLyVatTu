import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validator, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { HttpResponse, HttpStatusCode } from '@angular/common/http';
import { ChucvuService } from 'src/app/service/chucvu/chucvu.service';

@Component({
  selector: 'app-ad-chuc-vu',
  templateUrl: './ad-chuc-vu.component.html',
  styleUrls: ['./ad-chuc-vu.component.css']
})
export class AdChucVuComponent implements OnInit {
  addChucVu!: FormGroup;
  val: any;
  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private service: ChucvuService, private router: Router, private spinner: NgxSpinnerService) {

  }

  ngOnInit(): void {
    this.addChucVu = this.fb.group({
      tenChucVu: ['', [Validators.required]],
      maChucVu: ['', [Validators.required]]
    })

  }
  submit() {
    this.val = this.addChucVu.value;
    console.log("addChucVu", this.val)
    this.spinner.show();
    setTimeout(() => {
      this.spinner.hide();
      this.service.AddChucVu(this.val).subscribe(res => {
        this.toastr.success('Tạo chức vụ thành công', res, { timeOut: 2000, });
      },
        err => {
          console.error('err', err);
          if (err.status == 400) {
            this.toastr.error('Chức vụ đã tôn tại', 'Error', { timeOut: 10000, });
          }
        }
      )
    }, 1000);
  }

}
