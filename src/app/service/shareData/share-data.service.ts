import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { PhieuDNVTService } from '../PhieuDNVT/phieu-dnvt.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ShareDataService {
  private message = new BehaviorSubject(1);
  getMess = this.message.asObservable();

  //khai báo biến lưu trả ds phiếu
  dsPhieu:any =[];
  counts:any;


  constructor( private  servicePhieu: PhieuDNVTService, private router: Router) { }

  setTB(id:any){
    this.message.next(id)
  }
}
