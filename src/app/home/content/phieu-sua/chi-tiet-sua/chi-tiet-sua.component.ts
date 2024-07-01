import { Component,Input, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PhieuSuaService } from 'src/app/service/PhieuSua/phieu-sua.service';

@Component({
  selector: 'app-chi-tiet-sua',
  templateUrl: './chi-tiet-sua.component.html',
  styleUrls: ['./chi-tiet-sua.component.css']
})
export class ChiTietSuaComponent implements OnInit{
  @Input('chiTiet') chiTietPhieu:any =[]

  
  constructor(private service: PhieuSuaService, private spinner: NgxSpinnerService, private toastr: ToastrService) { }
    ngOnInit(): void {
    }
    SendHinh(){
      
    }

    // Hinh(ct:any){
    //   console.log("hinh" , ct)
    //   this.service.GetImgByPhieu(ct.idChiTietPhieuSua).subscribe(res=>{
    //   this.hinhArr= res
    //   console.log("hinhg arr", this.hinhArr)
    //   })
    // }
    
}
