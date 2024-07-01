import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ThukhoService } from 'src/app/service/thukho.service';

@Component({
  selector: 'app-phieu-cap-vat-tu',
  templateUrl: './phieu-cap-vat-tu.component.html',
  styleUrls: ['./phieu-cap-vat-tu.component.css']
})
export class PhieuCapVatTuComponent implements OnInit {
  dsPhieu: any = [];
  detialPhieu: any = [];
  cap: any;
  filter: any = [];
  filterForm!: FormGroup;
  val: object = {};
  phieuMua:any;


  //phantrang;
  pg: any = 1;
  count: any = 0;
  tableSize: number = 10;
  tableSizes: any = [10, 15, 20, 25, 30];

  constructor(private service: ThukhoService, private spinner: NgxSpinnerService, private toastr: ToastrService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    if (this.filter.length == 0) {
      this.filter.size = this.tableSize;
    }
    this.filterForm = new FormGroup({
      size: new FormControl(''),
    });
    this.LoadPhieuYeuCau(this.pg, this.tableSize);
  }
  LoadPhieuYeuCau(pg: any, size: any) {
    this.service.GetAllPhieuCapVatTu(pg, size).subscribe((data: any) => {
      this.dsPhieu = data.value;
      this.count = data.serializerSettings;
      console.log('dsPhieu', data);
    }, err => {
      console.error('err', err);
      if (err.status == 403) {
        this.router.navigate(['/error']);
      };
    })
  }
  onTableDataChange(event: any) {
    this.pg = event;
    this.LoadPhieuYeuCau(this.pg, this.filter.size);
  }
  chiTiet(phieu: any) {
    this.LoadChiTietPhieuYeuCau(phieu);
  }
  filterSelect() {
    this.filter = this.filterForm.value;
    this.pg = 1;
    this.LoadPhieuYeuCau(this.pg, this.filter.size);
  }
  LoadChiTietPhieuYeuCau(idPhieu: any) {
    this.service.GetAllDetailPhieuCapVatTu(idPhieu).subscribe((data: any) => {
      this.detialPhieu = data.value;
      console.log("detailPhieu", data);
    })
  }
  SubmitCap(detail: any) {
    this.service.CapVatTu(detail.idVatTu, detail.idUser, detail.idChiTietPhieu).subscribe(res => {
      this.toastr.success("cấp thành công");
      this.cap = res;
      console.log("detial câp", res);
    })
  }
  SubmitMua(detial:any){
    this.service.GetYeuCauMua(detial).subscribe(res=>{
      this.phieuMua = res;
      console.log("phieu mua", this.phieuMua);
    })
  }
  SubmitAddMaVT(detail: any) {    
    this.service.AddMaVatTu(detail.idVatTu, detail.idUser, detail.idChiTietPhieu).subscribe(res => {
      this.cap = res;
      this.toastr.success("cấp thành công");
      console.log("Add cap ma vt", res);
    })
  }
}
