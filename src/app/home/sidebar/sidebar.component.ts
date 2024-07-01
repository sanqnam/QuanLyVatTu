import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit{
  displayMenu=false;
  displayQuanTri = false;
  displayLanhDao= false;
  currentRole:any;
  displayThuKho:any;
  displayNguoiMua:any;
  currentPhong:any
  displaySua:any
  displayTrinhMua:any
  displayNguoiSua:any
  constructor(private router:Router){
  }
  ngOnInit(): void {
    this.MenuDisplay();
  }
  MenuDisplay(){
    this.currentRole = localStorage.getItem('maChucVu');
    this.currentPhong = localStorage.getItem('maPhongBan')
    this.displayQuanTri=this.currentRole=='QTri';
    this.displayLanhDao = ['PP', 'TP', 'GD', 'PGD'].includes(this.currentRole);
    this.displayThuKho= this.currentRole == 'T.Kho';
    this.displayNguoiMua= this.currentRole == 'M.Hang';
    this.displaySua =  ['KT&AT'].includes(this.currentPhong)
    this.displayTrinhMua =['TC&KT','KH&VT','GD','P.GD'].includes(this.currentPhong)
    this.displayNguoiSua = this.currentRole=='N.Vien' && this.currentPhong =='KT&AT'

  }

}
