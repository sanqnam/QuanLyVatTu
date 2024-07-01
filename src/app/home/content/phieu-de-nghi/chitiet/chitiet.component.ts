import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhieuDNVTService } from 'src/app/service/PhieuDNVT/phieu-dnvt.service';

@Component({
  selector: 'app-chitiet',
  templateUrl: './chitiet.component.html',
  styleUrls: ['./chitiet.component.css']
})
export class ChitietComponent implements OnInit{
  @Input('chiTiet') chiTietPhieu:any=[];
  @Input('phieu') phieu:any;
  DsChiTiet:any =[];
  res :any =[];
  editDs!:FormGroup;
  val:any;
  idPhieu:any;
  
  
  
  
  chucVu:any = localStorage.getItem('maChuVu');
  
  constructor(private service:PhieuDNVTService, private spinner:NgxSpinnerService, private toastr:ToastrService, private fb:FormBuilder, private router:Router, private route:ActivatedRoute){}
  
  ngOnInit(): void {
    console.log("chi tiết phiếu NV",this.chiTietPhieu);
    
}
}
