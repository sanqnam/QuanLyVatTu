import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { VattuService } from 'src/app/service/vattu/vattu.service';

@Component({
  selector: 'app-vat-tu-dang-yeu-cau',
  templateUrl: './vat-tu-dang-yeu-cau.component.html',
  styleUrls: ['./vat-tu-dang-yeu-cau.component.css']
})
export class VatTuDangYeuCauComponent implements OnInit {

  dsVatTu: any = [];
  counts: any;
  idPhongBan = localStorage.getItem('idPhongBan');

  search: any;
  pg: any = 1;

  constructor(private service: VattuService, private spinner: NgxSpinnerService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute, private toastr: ToastrService) { }
  ngOnInit(): void {
    this.LoadVatTu(this.idPhongBan, this.pg);
  }
  LoadVatTu(idPhongBan: any, pg: any) {
    this.service.GetVatTuDangYeuCau(idPhongBan, pg).subscribe((data: any) => {
      this.dsVatTu = data.value;
      this.counts = data.serializerSettings;
      console.log("count ds vt dang yêu cau", data);
    })
  }
  onTableDataChange(event: any) {
    console.log("even", event);

    this.pg = event;
    if (this.search == null || this.search.trim() === '') {
      this.LoadVatTu(this.idPhongBan, this.pg);
    }
    else {
      this.LoadSearch(this.search, this.pg, this.idPhongBan)
    }
  }
  LoadSearch(search: any, pg: any, idPhongBan: any) {
    this.service.SearchVatTuDangYeuCau(search, pg, idPhongBan).subscribe((data: any) => {
      this.dsVatTu = data.value;
      this.counts = data.serializerSettings;
      console.log("count ds vt dang yêu cau", data);
    })
  }
  Search() {
    if (this.search == null || this.search.trim() === '') {
      this.ngOnInit();
    }
    else {
      this.LoadSearch(this.search, this.pg, this.idPhongBan)
    }
  }
}
