import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { UserComponent } from './quantri/user/user/user.component';
import { AuthGuard } from './guard/auth.guard';
import { ProfileComponent } from './quantri/user/profile/profile.component';
import { ErrorComponent } from './home/error/error/error.component';
import { HeaderComponent } from './home/header/header.component';
import { VattuComponent } from './home/content/vattu/vattu/vattu.component';
import { VtquantamComponent } from './home/content/vattu/vattu/vtquantam/vtquantam.component';
import { AddPhieuDNComponent } from './home/content/vattu/vattu/vtquantam/add-phieu-dn/add-phieu-dn.component';
import { ChucVuComponent } from './quantri/chuc-vu/chuc-vu.component';
import { PhongbanComponent } from './quantri/phongban/phongban.component';
import { PhieuduyetComponent } from './cap-lanh-dao/phieuduyet/phieuduyet.component';
import { PhieuDeNghiComponent } from './home/content/phieu-de-nghi/phieu-de-nghi.component';
import { ThukhoComponent } from './thukho/thukho.component';
import { PhieuCapVatTuComponent } from './thukho/phieu-cap-vat-tu/phieu-cap-vat-tu.component';
import { VatTuSuDungComponent } from './home/content/vattu/vat-tu-su-dung/vat-tu-su-dung.component';
import { PhieuChoDuyetComponent } from './home/content/phieu-de-nghi/phieu-cho-duyet/phieu-cho-duyet.component';
import { PhieuHoanThanhComponent } from './home/content/phieu-de-nghi/phieu-hoan-thanh/phieu-hoan-thanh.component';
import { VatTuDangYeuCauComponent } from './home/content/vattu/vat-tu-dang-yeu-cau/vat-tu-dang-yeu-cau.component';
import { VatTuCanMuaComponent } from './nguoimua/vat-tu-can-mua/vat-tu-can-mua.component';
import { PhieuMuaComponent } from './cap-lanh-dao/phieu-mua/phieu-mua.component';
import { PhieuDeNghiSuaComponent } from './cap-lanh-dao/phieu-de-nghi-sua/phieu-de-nghi-sua.component';
import { PhieuByPbPhuTrachComponent } from './cap-lanh-dao/phieu-de-nghi-sua/phieu-by-pb-phu-trach/phieu-by-pb-phu-trach.component';
import { AllPhieuDnByPbComponent } from './home/content/phieu-de-nghi/all-phieu-dn-by-pb/all-phieu-dn-by-pb.component';
import { NvPhuTrachComponent } from './home/content/phieu-sua/nv-phu-trach/nv-phu-trach.component';
import { AllPhieuSuaComponent } from './cap-lanh-dao/phieu-de-nghi-sua/all-phieu-sua/all-phieu-sua.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { PhieuMuaBiTraComponent } from './nguoimua/phieu-mua-bi-tra/phieu-mua-bi-tra.component';
import { NhapKhoComponent } from './thukho/nhap-kho/nhap-kho.component';
import { PhieuMuaHoanThanhComponent } from './nguoimua/phieu-mua-hoan-thanh/phieu-mua-hoan-thanh.component';
import { DeNghiNhapKhoComponent } from './nguoimua/de-nghi-nhap-kho/de-nghi-nhap-kho.component';
import { PhieuDangXuLyComponent } from './home/content/phieu-sua/phieu-dang-xu-ly/phieu-dang-xu-ly.component';
import { PhieuDaHoanThanhComponent } from './home/content/phieu-sua/phieu-da-hoan-thanh/phieu-da-hoan-thanh.component';


const routes: Routes = [
  {path: 'login', component:LoginComponent, pathMatch:'full'},
  {path:'', component:HomeComponent, canActivate:[AuthGuard], 
    children:[
      {path:'header', component:HeaderComponent},
      {path:'user', component: UserComponent,canActivate:[AuthGuard]},
      {path:'profile', component: ProfileComponent,canActivate:[AuthGuard]},
      {path:'error', component: ErrorComponent,canActivate:[AuthGuard]},
      {path:'vattu', component:VattuComponent,canActivate:[AuthGuard]},
      {path:'quantam', component:VtquantamComponent,canActivate:[AuthGuard]},
      {path:'chucvu', component:ChucVuComponent, canActivate:[AuthGuard]},
      {path:'phongban',component:PhongbanComponent, canActivate:[AuthGuard]},
      {path: 'phieuduyet',component:PhieuduyetComponent,canActivate:[AuthGuard]},
      {path:'phieuDeNghiVatTu',component:PhieuDeNghiComponent,canActivate:[AuthGuard]},
      {path:'dsvattu',component:ThukhoComponent,canActivate:[AuthGuard]},
      {path:'phieucapvattu',component:PhieuCapVatTuComponent,canActivate:[AuthGuard]},
      {path:'vattusudung', component:VatTuSuDungComponent,canActivate:[AuthGuard]},
      {path:'choduyet',component:PhieuChoDuyetComponent,canActivate:[AuthGuard]},
      {path:'phieuhoanthanh', component:PhieuHoanThanhComponent, canActivate:[AuthGuard]},
      {path:'vattuyeucau', component:VatTuDangYeuCauComponent, canActivate:[AuthGuard]},
      {path:'vattucanmua', component:VatTuCanMuaComponent,canActivate:[AuthGuard]},
      {path:'phieuduyetmuavattu', component:PhieuMuaComponent, canActivate:[AuthGuard]},
      {path:'phieuduyetsua',component: PhieuDeNghiSuaComponent, canActivate :[AuthGuard]},
      {path: 'phieusuatiepnhan', component:PhieuByPbPhuTrachComponent, canActivate:[AuthGuard]},
      {path: 'phieudenghibyphongban', component:AllPhieuDnByPbComponent, canActivate:[AuthGuard]},
      {path: 'vattucansua', component:NvPhuTrachComponent, canActivate:[AuthGuard]},
      {path: 'allphieusua', component:AllPhieuSuaComponent, canActivate:[AuthGuard]},
      {path: 'dashboard', component:DashboardComponent, canActivate:[AuthGuard]},
      {path: 'phieumuatra', component:PhieuMuaBiTraComponent, canActivate:[AuthGuard]},
      {path: 'nhapkho', component:NhapKhoComponent, canActivate:[AuthGuard]},
      {path: 'phieumuahoanthanh', component:PhieuMuaHoanThanhComponent, canActivate:[AuthGuard]},
      {path: 'denghinhapkho', component:DeNghiNhapKhoComponent, canActivate:[AuthGuard]},
      {path: 'phieusuadangxuly', component:PhieuDangXuLyComponent, canActivate:[AuthGuard]},
      {path: 'phieusuadahoanthanh', component:PhieuDaHoanThanhComponent, canActivate:[AuthGuard]}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
