import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PhongBanService } from 'src/app/service/phongban/phongban.service';

@Component({
  selector: 'app-phongban',
  templateUrl: './phongban.component.html',
  styleUrls: ['./phongban.component.css']
})
export class PhongbanComponent implements OnInit{

  res:any;
  DsPhongBan:any =[];
  search: any =[];
  phongban:any=[];


  constructor(private service: PhongBanService, private router: Router, private fb: FormBuilder, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.LoadChucVu();
  }
  LoadChucVu() {
    this.service.GetAll().subscribe(res=> {
      this.DsPhongBan = res;
      console.log('chucvu',res)
    }, err =>{
      console.error('err', err);
      if(err.status ==403){
        this.router.navigate(['/error']);
      }
    }
    )
  }

  Search() {
    if (this.search === "") {
      this.ngOnInit();
    } else {
      this.service.Search(this.search).subscribe((res: any) => {
        this.DsPhongBan = res;
        console.log("res ", res);
      });
    }
  }

  chiTietPhongBan(phongban:any){
    this.phongban=phongban;
    console.log("phong ban", this.phongban);
  }
}
