import { Component,OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { NguoimuaService } from 'src/app/service/nguoimua/nguoimua.service';

@Component({
  selector: 'app-de-nghi-nhap-kho',
  templateUrl: './de-nghi-nhap-kho.component.html',
  styleUrls: ['./de-nghi-nhap-kho.component.css']
})
export class DeNghiNhapKhoComponent implements OnInit{
  idUser: any = localStorage.getItem("idUser");
  DsPhieu: any = [];
  DsChiTietPhieu: any = [];
  counts:any
  showPhieuMua:any
  dsChiTiet:any
  id:any

  page: any = 1;
  tableSize: number = 10;

  constructor(private service: NguoimuaService, private spinner: NgxSpinnerService, private toastr: ToastrService, private router: Router, private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.LoadPhieu(this.page, this.idUser)
  }
  LoadPhieu(page: any,  idUser: any,) {
    this.service.GetPhieuNhapKho(page,  idUser).subscribe(
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
  Submit(){
    console.log("idPhieu",this.id)
    this.service.YeuCauNhapKho(this.id).subscribe((res:any)=>{
      console.log('res', res)
      this.spinner.show()
      setTimeout(() => {
        if (res.statusCode == 200  ) {
          this.spinner.hide();
          this.toastr.success('Thành công', 'Dã đề nghị nhập kho', {
          });
          this.Reset()
        }else if(res.statusCode == 400){
          this.spinner.hide();
          this.toastr.error(' Yêu cầu nhập lại để duyệt','Nhập sai mã ');
        }})

    })
  }
  Reset(){
    this.ngOnInit()
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
    this.id= id
    this.service.GetChiTietPhieuMua(id).subscribe((res: any) => {
      console.log("dasta chitiets", res)
      this.dsChiTiet = res.value
      this.showPhieuMua = true;
    })
  }
}
