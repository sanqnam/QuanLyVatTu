import { Component,OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { NguoimuaService } from 'src/app/service/nguoimua/nguoimua.service';
import { ExcelService } from 'src/app/service/xuatfile/excel.service';
import * as alasql from 'alasql';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-phieu-mua-hoan-thanh',
  templateUrl: './phieu-mua-hoan-thanh.component.html',
  styleUrls: ['./phieu-mua-hoan-thanh.component.css']
})
export class PhieuMuaHoanThanhComponent implements OnInit{
  idUser: any = localStorage.getItem("idUser");
  DsPhieu: any = [];
  DsChiTietPhieu: any = [];
  counts:any
  showPhieuMua:any
  dsChiTiet:any
  excel:any
  formFilter!:FormGroup
  month:any

  page: any = 1;
  tableSize: number = 10;
  constructor(private service: NguoimuaService, private spinner: NgxSpinnerService, private toastr: ToastrService, private router: Router, private route: ActivatedRoute,private excelService: ExcelService, private fb: FormBuilder) { 

    this.closeResult =''

  }
  ngOnInit(): void {

    this.LoadPhieu(this.page, this.idUser)
  }
  LoadPhieu(page: any,  idUser: any,) {
    this.service.GetPhieuHoanThanh(page,  idUser).subscribe(
      (data: any) => {
        this.DsPhieu = data.value;
        this.counts = data.serializerSettings;
        console.log("dddddđ", data)
      },
      err => {
        console.error('err', err);
        if (err.status == 403) {
          this.router.navigate(['/error']);
        };
      }
    )
  }
  
  formatCurrency(value: number): string {
    if (!value) return '0đ';
    const formattedValue = value.toLocaleString('vi-VN');
    return `${formattedValue}đ`;
  }
  onTableDataChange(event: any) {
    this.page = event;
    this.LoadPhieu(this.page, this.idUser);
  }
  loadChiTietPhieu(id: any) {
    this.service.GetChiTietPhieuMua(id).subscribe((res: any) => {
      console.log("dasta chitiets", res)
      this.dsChiTiet = res.value
      this.showPhieuMua = true;
    })
  }

  private _success = new Subject<string>();
  staticAlertClosed = false;
  successMessage:any
  peopleByCity:Object[]=[]
  closeResult: string;

  public changeMessage() {
    this._success.next(`${new Date()} - Message successfully changed.`);
  }
  input()
  {
    var val = (<HTMLInputElement>document.getElementById("month")).value;
    console.log('ma',val)
    this.month= val
  }
  exportJson(): void {
    if(this.month==undefined|| this.month ==null ||this.month=='' )
    {
      alert("yêu cầu nhập tháng để xuất");      
    }else{
      this.service.XuatHoaDonTheoThang(this.month, this.idUser).subscribe((res:any)=>{
        console.log(res.value)
        this.excel = [res.value]
        console.log("file", this.excel)
        var ress = alasql(`SEARCH / AS @data \ chiTietHoaDons / AS @items \ RETURN(@items->tenVatTu as TenVatTu, @items->soLuong as SoLuong, @items->donGia as DonGia, @items->vat as VAT, @items->donViTinh as DonViTinh,@items->donViCungCap as DonViCungCap) \ FROM ?`, [this.excel])
      console.log("xuất file", ress)      
      let ten = 'Hoa dơn tháng '+ this.month
      this.excelService.exportAsExcelFile(ress, ten);
      })
    }

    
  }

}
