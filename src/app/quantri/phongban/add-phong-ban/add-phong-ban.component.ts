import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhongBanService } from 'src/app/service/phongban/phongban.service';

@Component({
  selector: 'app-add-phong-ban',
  templateUrl: './add-phong-ban.component.html',
  styleUrls: ['./add-phong-ban.component.css']
})
export class AddPhongBanComponent implements OnInit {

  addPhongBan!: FormGroup;
  val: any;
  constructor(private fb: FormBuilder, private toastr: ToastrService,
    private service: PhongBanService, private router: Router, private spinner: NgxSpinnerService) {

  }
  ngOnInit(): void {
    this.addPhongBan = this.fb.group({
      tenPhongBan: ['', [Validators.required]],
      maPhongBan: ['', [Validators.required]]
    })

  }
  submitPB() {
    this.val = this.addPhongBan.value;
    console.log("addPhongban", this.val)
    this.spinner.show();
    setTimeout(() => {
      this.spinner.hide();
      this.service.AddPhongBan(this.val).subscribe(res => {
        this.toastr.success('Tạo chức vụ thành công', res, 
        
        { timeOut: 2000, });
        console.log("res",res);
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
