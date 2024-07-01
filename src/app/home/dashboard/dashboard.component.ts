import { DeclarationListEmitMode } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DashboardService } from 'src/app/service/dashboard/dashboard.service';
import { NotificasService } from 'src/app/service/notifi/notificas.service';
import { SignalrService } from 'src/app/service/signalr/signalr.service';
import { UserService } from 'src/app/service/user/user.service';
import { Chart, registerables } from 'chart.js/auto';
Chart.register(...registerables)


declare function DashboardChar(): void
declare function DashboardQuanTri(data: any): any
declare function test(data: any): any

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  idUser: any = localStorage.getItem("idUser");
  PhongBan: any = localStorage.getItem("maPhongBan")
  tenPB: any;
  count: any;
  countSua: any;
  countVT: any;
  countNV: any
  data: any = []
  name: any = [];
  countCV: any


  //phan quyền
  displayQuanTri = false;
  displayLanhDao = false;
  currentRole: any;
  displayThuKho: any;
  displayTruongPhong: any;
  displayQuanTriTien: any

  totolPhieuVT: any
  constructor(private router: Router, private service: DashboardService, private serviceUser: UserService, private signalr: SignalrService, private serviceNoti: NotificasService) {

  }
  ngOnInit(): void {

    DashboardChar();
    this.MenuDisplay();
    if (this.displayQuanTriTien) {
         // quyền của lãnh dạo và trưởng phòng tài chính
         this.loadVatTuTrongPhong();
         this.loadTotolPhieuVT();
         this.loadPhieuSua()
         this.TongTienTheoThang()
         this.TongTien()
    }
    else if (this.displayQuanTri) {
      console.log("quyền quản tịo")
      this.loadThanhVien()
      this.loadCountAllNhanVienTheoChucVu()
      this.LoadCountAllChucVu()
    }
    else if (this.displayThuKho) {
      this.CountAllVatTuSuDungTheoPhong()
      this.LoadCountTongVatTuSuDung()
      this.LoadCoutTongVatTu()
    }
    else if (this.displayLanhDao) {
      console.log("quyền trưởng phòng")
      this.loadVatTuTrongPhong();
      this.loadTotolPhieuVT();
      this.loadPhieuSua()
    }

  }
  getRandomColor(): string {
    const letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
      color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
  }
  loadQuanTri(data: any, label: any) {
    const dataPoints = data;
    const name = label

    const backgroundColors = dataPoints.map(() => this.getRandomColor());
    new Chart('myChart', {
      type: 'bar',
      data: {
        labels: name,
        datasets: [{
          label: 'Số người',
          data: dataPoints,
          backgroundColor: backgroundColors,
          borderWidth: 1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: false
          }
        }
      }
    });
  }
  // phiếu đề nghị vật tư
  loadTotolPhieuVT() {
    console.log('hsdfsdlkfssssss', this.PhongBan)
    if (this.PhongBan == 'GD' || this.PhongBan == 'P.GD') {
      this.PhongBan = "LanhDao"
      console.log("lanh dạo22", this.PhongBan)
    }
    console.log("lanh dạo", this.PhongBan)
    this.service.TotolPhieuVT(this.PhongBan).subscribe(res => {
      this.count = res;
      console.log('hsdfsdlkf', res)
    })
  }
  LoadPhieuVTByMonth(index: any) {
    this.service.PhieuVTByMonth(this.PhongBan, index).subscribe(res => {
      this.count = res;
      console.log('month', res)
    })
  }
  // phiếu sửa
  loadPhieuSua() {
    this.service.CountPhieuSuaByPB(this.PhongBan).subscribe(res => {
      this.countSua = res;
    })
  }
  loadPhieuSuaThang(index: any) {
    this.service.CountPhieuSuaByMonth(this.PhongBan, index).subscribe(res => {
      this.countSua = res
    })
  }
  loadVatTuTrongPhong() {
    this.service.CountVatTuTrongPhong(this.PhongBan).subscribe(res => {
      this.countVT = res;
    })
  }
  currentPhong: any
  MenuDisplay() {
    this.currentRole = localStorage.getItem('maChucVu');
    this.currentPhong = localStorage.getItem("maPhongBan")
    this.displayQuanTri = this.currentRole == 'QTri';
    this.displayLanhDao = ['PP', 'TP'].includes(this.currentRole) && this.currentPhong != 'TC&KT';
    this.displayThuKho = this.currentRole == 'T.Kho';
    this.displayQuanTriTien = ['GD', 'PGD', 'PP', 'TP'].includes(this.currentRole) && ['GD', 'P.GD', 'TC&KT'].includes(this.currentPhong)
  }
  loadThanhVien() {
    this.service.CountTatCaNhanVien().subscribe(res => {
      this.countNV = res
    })
  }
  loadThanhVienTheoPB(index: any) {
    this.service.CountNhanVienTheoPB(index).subscribe(res => {
      this.countNV = res
    })
  }
  loadCountAllNhanVienTheoChucVu() {
    this.service.CountAllNhanVienTheoChucVu().subscribe((res: any) => {
      res.forEach((item: any) => {
        this.data.push(item.value);
        this.name.push(item.key);
      });
      this.loadQuanTri(this.data, this.name)
    });
  }
  LoadCountAllChucVu() {
    this.service.CountAllChucVu().subscribe(res => {
      this.countCV = res
    })
  }
  // quản trị của thủ kho
  countVTSD: any
  vTName: any = []
  vTValue: any = []

  LoadCoutTongVatTu() {
    this.service.CoutTongVatTu().subscribe(res => {
      this.countVT = res
    })
  }
  LoadCountTongVatTuSuDung() {
    this.service.CountTongVatTuSuDung().subscribe(res => {
      this.countVTSD = res
    })
  }
  CountAllVatTuSuDungTheoPhong() {
    this.service.CountAllVatTuSuDungTheoPhong().subscribe((res: any) => {
      console.log("danh sách vạt tư pong ban", res)
      res.forEach((item: any) => {
        this.vTName.push(item.key)
        this.vTValue.push(item.value)
      });
      console.log("danh sách sau khi push", this.vTName, this.vTValue)
      this.loadChartThuKho(this.vTValue, this.vTName)
    })
  }
  formatCurrency(value: number): string {
    if (!value) return '0đ';
    const formattedValue = value.toLocaleString('vi-VN');
    return `${formattedValue}đ`;
  }
  loadChartThuKho(data: any, label: any) {
    const dataPoints = data;
    const name = label

    const backgroundColors = dataPoints.map(() => this.getRandomColor());
    new Chart('myChartThuKho', {
      type: 'bar',
      data: {
        labels: name,
        datasets: [{
          label: 'Số vật tư',
          data: dataPoints,
          backgroundColor: backgroundColors,
          borderWidth: 1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: false
          }
        }
      }
    });
  }

  thang: any = []
  tien: any = []
  tongTien: any
  TongTienTheoThang() {
    this.service.TienTheoThang().subscribe((res: any) => {
      console.log("danh sách vạt tư pong ban", res)
      res.forEach((item: any) => {
        this.thang.push(item.value)
        this.tien.push(item.key)
      });
      this.LoadTien(this.thang, this.tien)
    })
  }
  LoadTien(data: any, label: any) {
    const dataPoints = data;
    const name = label

    const backgroundColors = dataPoints.map(() => this.getRandomColor());
    new Chart('myChartTien', {
      type: 'bar',
      data: {
        labels: name,
        datasets: [{
          label: 'TongTien',
          data: dataPoints,
          backgroundColor: backgroundColors,
          borderWidth: 1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: false
          }
        }
      }
    });
  }
  TongTien() {
    this.service.TongTienTrongNam().subscribe((res: any) => {
      console.log(res)
      this.formatCurrency(res);
      this.tongTien = res
    })
  }

}
