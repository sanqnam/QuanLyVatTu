import { Component, OnInit } from '@angular/core';
import { ThukhoService } from '../service/thukho.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VattuService } from '../service/vattu/vattu.service';

@Component({
  selector: 'app-thukho',
  templateUrl: './thukho.component.html',
  styleUrls: ['./thukho.component.css']
})
export class ThukhoComponent implements OnInit {

  dsVatTu: any = [];
  filter: any = [];
  filterForm!: FormGroup;
  search:any;
  dsSearch:any=[];

  //phantrang;
  pg: any = 1;
  count: any = 0;
  tableSize: number = 10;
  tableSizes: any = [10, 15, 20, 25, 30];

  constructor(private service: ThukhoService, private servictVatTu: VattuService, private spinner: NgxSpinnerService, private toastr: ToastrService, private fb: FormBuilder,
    private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    if (this.filter.length == 0) {
      this.filter.size = this.tableSize;
    }
    this.filterForm = new FormGroup({
      size: new FormControl(''),
    });
    this.LoadAllVatTu(this.pg, this.tableSize);
  }
  LoadAllVatTu(pg: any, size: any) {
    this.service.GetAllVatTu(pg, size).subscribe((data: any) => {
      this.dsVatTu = data.value;
      this.count = data.serializerSettings;
      console.log('dsVatTU', data);
    }, err => {
      console.error('err', err);
      if (err.status == 403) {
        this.router.navigate(['/error']);
      };
    })
  }
  onTableDataChange(event: any) {
    this.pg = event;
    if(this.search ==null || this.search.trim()===''){
      this.LoadAllVatTu(event, this.tableSize);
    }
    else{
      this.LoadSearch(this.pg, this.tableSize, this.search)
    }
  }
  chiTiet(vattu: any) {

  }
  filterSelect() {
    this.filter = this.filterForm.value;
    this.pg = 1;
    this.LoadAllVatTu(this.pg, this.filter.size);
  }
  Search() {
    if (this.search != "") {
      this.LoadSearch(this.pg, this.tableSize, this.search)
      
    } else {
      this.ngOnInit();
    }
  }
  LoadSearch(pg:any, size:any, search:any){
    this.servictVatTu.Search(pg, size, search).subscribe((res: any) => {
      this.dsVatTu = res.value; 
      this.count = res.serializerSettings;
      console.log("res ", res);
      console.log("dsvt ", this.dsVatTu);
    });
  }

}
