import { Component,Input } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { NguoimuaService } from 'src/app/service/nguoimua/nguoimua.service';

@Component({
  selector: 'app-chi-tiet-nhap',
  templateUrl: './chi-tiet-nhap.component.html',
  styleUrls: ['./chi-tiet-nhap.component.css']
})
export class ChiTietNhapComponent {
  idUser:any = localStorage.getItem("idUser")

  constructor(private service: NguoimuaService, private spinner: NgxSpinnerService, private toastr: ToastrService) { }
  @Input() chiTiet:any =[]
  @Input()phieu:any
  
}
