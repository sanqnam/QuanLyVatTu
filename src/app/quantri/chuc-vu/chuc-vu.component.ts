import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ChucvuService } from 'src/app/service/chucvu/chucvu.service';
import { PhongBanService } from 'src/app/service/phongban/phongban.service';
import { ShareDataService } from 'src/app/service/shareData/share-data.service';


@Component({
  selector: 'app-chuc-vu',
  templateUrl: './chuc-vu.component.html',
  styleUrls: ['./chuc-vu.component.css']
})
export class ChucVuComponent implements OnInit {
  res: any;
  DsChucVu: any = [];
  filter: any=[];
  search:any=[];
  filterForm!:FormGroup;
  searchClass:string="search-bar";
  chucVu:any=[];


  constructor(private service: ChucvuService, private router: Router, private fb: FormBuilder, private route: ActivatedRoute, private shareData: ShareDataService) { }
  ngOnInit(): void {
    this.LoadChucVu();
  }
  LoadChucVu() {
    this.service.GetAll().subscribe(res=> {
      this.DsChucVu = res;
      console.log('chucvu',res)
    }, err =>{
      console.error('err', err);
      if(err.status == 403){
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
        this.DsChucVu = res;
        console.log("res ", res);
      });
    }
  }
  chiTietChucVu(chuVu:any){
    this.chucVu=chuVu;
  }

}
