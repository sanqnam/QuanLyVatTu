import {  CUSTOM_ELEMENTS_SCHEMA,NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ToastrModule } from 'ngx-toastr';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthGuard } from './guard/auth.guard';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgImageSliderModule } from 'ng-image-slider';

import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { UserComponent } from './quantri/user/user/user.component';
import { SidebarComponent } from './home/sidebar/sidebar.component';
import { HeaderComponent } from './home/header/header.component';
import { ProfileComponent } from './quantri/user/profile/profile.component';
import { ImageUploadComponent } from './image-upload/image-upload.component';
import { EditComponent } from './quantri/user/user/edit/edit.component';
import { PagepaginationComponent } from './pagepagination/pagepagination.component';
import { AddComponent } from './quantri/user/user/add/add.component';
import { ErrorComponent } from './home/error/error/error.component';
import { VattuComponent } from './home/content/vattu/vattu/vattu.component';
import { VtquantamComponent } from './home/content/vattu/vattu/vtquantam/vtquantam.component';
import { AddPhieuDNComponent } from './home/content/vattu/vattu/vtquantam/add-phieu-dn/add-phieu-dn.component';
import { ChucVuComponent } from './quantri/chuc-vu/chuc-vu.component';
import { AdChucVuComponent } from './quantri/chuc-vu/ad-chuc-vu/ad-chuc-vu.component';
import { EditchucvuComponent } from './quantri/chuc-vu/editchucvu/editchucvu.component';
import { PhongbanComponent } from './quantri/phongban/phongban.component';
import { EditPhongBanComponent } from './quantri/phongban/edit-phong-ban/edit-phong-ban.component';
import { AddPhongBanComponent } from './quantri/phongban/add-phong-ban/add-phong-ban.component';
import { PhieuduyetComponent } from './cap-lanh-dao/phieuduyet/phieuduyet.component';
import { ChitietphieuComponent } from './cap-lanh-dao/phieuduyet/chitietphieu/chitietphieu.component';
import { PhieuDeNghiComponent } from './home/content/phieu-de-nghi/phieu-de-nghi.component';
import { ChitietComponent } from './home/content/phieu-de-nghi/chitiet/chitiet.component';
import { ThukhoComponent } from './thukho/thukho.component';
import { PhieuCapVatTuComponent } from './thukho/phieu-cap-vat-tu/phieu-cap-vat-tu.component';
import { AddVatTuComponent } from './thukho/add-vat-tu/add-vat-tu.component';
import { VatTuSuDungComponent } from './home/content/vattu/vat-tu-su-dung/vat-tu-su-dung.component';
import { YeuCauMuaComponent } from './thukho/phieu-cap-vat-tu/yeu-cau-mua/yeu-cau-mua.component';
import { PhieuChoDuyetComponent } from './home/content/phieu-de-nghi/phieu-cho-duyet/phieu-cho-duyet.component';
import { PhieuHoanThanhComponent } from './home/content/phieu-de-nghi/phieu-hoan-thanh/phieu-hoan-thanh.component';
import { VatTuDangYeuCauComponent } from './home/content/vattu/vat-tu-dang-yeu-cau/vat-tu-dang-yeu-cau.component';
import { VatTuCanMuaComponent } from './nguoimua/vat-tu-can-mua/vat-tu-can-mua.component';
import { TaoPhieuMuaComponent } from './nguoimua/vat-tu-can-mua/tao-phieu-mua/tao-phieu-mua.component';
import { PhieuMuaComponent } from './cap-lanh-dao/phieu-mua/phieu-mua.component';
import { MoreImaUploadComponent } from './image-upload/more-ima-upload/more-ima-upload.component';
import { PhieuSuaComponent } from './home/content/phieu-sua/phieu-sua.component';
import { AddPhieuSuaComponent } from './home/content/phieu-sua/add-phieu-sua/add-phieu-sua.component';
import { PhieuDeNghiSuaComponent } from './cap-lanh-dao/phieu-de-nghi-sua/phieu-de-nghi-sua.component';
import { ChiTietPhieuSuaComponent } from './cap-lanh-dao/phieu-de-nghi-sua/chi-tiet-phieu-sua/chi-tiet-phieu-sua.component';
import { ViewHinhAnhComponent } from './home/content/view-hinh-anh/view-hinh-anh.component';
import { PhieuByPbPhuTrachComponent } from './cap-lanh-dao/phieu-de-nghi-sua/phieu-by-pb-phu-trach/phieu-by-pb-phu-trach.component';
import { SetNvSuaComponent } from './cap-lanh-dao/phieu-de-nghi-sua/phieu-by-pb-phu-trach/set-nv-sua/set-nv-sua.component';
import { AllPhieuDnByPbComponent } from './home/content/phieu-de-nghi/all-phieu-dn-by-pb/all-phieu-dn-by-pb.component';
import { NvPhuTrachComponent } from './home/content/phieu-sua/nv-phu-trach/nv-phu-trach.component';
import { AllPhieuSuaComponent } from './cap-lanh-dao/phieu-de-nghi-sua/all-phieu-sua/all-phieu-sua.component';
import { PupupDuyetPhieuComponent } from './home/content/pupup/pupup-duyet-phieu/pupup-duyet-phieu.component';
import { DashboardComponent } from './home/dashboard/dashboard.component';
import { DuyetPhieuMuaComponent } from './cap-lanh-dao/phieu-mua/duyet-phieu-mua/duyet-phieu-mua.component';
import { PhieuMuaBiTraComponent } from './nguoimua/phieu-mua-bi-tra/phieu-mua-bi-tra.component';
import { SuaPhieuMuaComponent } from './nguoimua/phieu-mua-bi-tra/sua-phieu-mua/sua-phieu-mua.component';
import { NhapKhoComponent } from './thukho/nhap-kho/nhap-kho.component';
import { ChiTietNhapComponent } from './thukho/nhap-kho/chi-tiet-nhap/chi-tiet-nhap.component';
import { PhieuMuaHoanThanhComponent } from './nguoimua/phieu-mua-hoan-thanh/phieu-mua-hoan-thanh.component';
import { PhieuMuaHoanThanhChiTietComponent } from './nguoimua/phieu-mua-hoan-thanh/phieu-mua-hoan-thanh-chi-tiet/phieu-mua-hoan-thanh-chi-tiet.component';
import { DeNghiNhapKhoComponent } from './nguoimua/de-nghi-nhap-kho/de-nghi-nhap-kho.component';
import { PhieuDangXuLyComponent } from './home/content/phieu-sua/phieu-dang-xu-ly/phieu-dang-xu-ly.component';
import { PhieuDaHoanThanhComponent } from './home/content/phieu-sua/phieu-da-hoan-thanh/phieu-da-hoan-thanh.component';
import { ChiTietSuaComponent } from './home/content/phieu-sua/chi-tiet-sua/chi-tiet-sua.component';




export function tokenGetter() { 
  return localStorage.getItem("token"); 
}
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    UserComponent,
    SidebarComponent,
    HeaderComponent,
    ProfileComponent,
    ImageUploadComponent,
    EditComponent,
    PagepaginationComponent,
    AddComponent,
    ErrorComponent,
    VattuComponent, 
    VtquantamComponent,
    AddPhieuDNComponent,
    ChucVuComponent,
    AdChucVuComponent,
    EditchucvuComponent,
    PhongbanComponent,
    EditPhongBanComponent,
    AddPhongBanComponent,
    PhieuduyetComponent,
    ChitietphieuComponent,
    PhieuDeNghiComponent,
    ChitietComponent,
    ThukhoComponent,
    PhieuCapVatTuComponent,
    AddVatTuComponent,
    VatTuSuDungComponent,
    YeuCauMuaComponent,
    PhieuChoDuyetComponent,
    PhieuHoanThanhComponent,
    VatTuDangYeuCauComponent,
    VatTuCanMuaComponent,
    TaoPhieuMuaComponent,
    PhieuMuaComponent,
    MoreImaUploadComponent,
    PhieuSuaComponent,
    AddPhieuSuaComponent,
    PhieuDeNghiSuaComponent,
    ChiTietPhieuSuaComponent,
    ViewHinhAnhComponent,
    PhieuByPbPhuTrachComponent,
    SetNvSuaComponent,
    AllPhieuDnByPbComponent,
    NvPhuTrachComponent,
    AllPhieuSuaComponent,
    PupupDuyetPhieuComponent,
    DashboardComponent,
    DuyetPhieuMuaComponent,
    PhieuMuaBiTraComponent,
    SuaPhieuMuaComponent,
    NhapKhoComponent,
    ChiTietNhapComponent,
    PhieuMuaHoanThanhComponent,
    PhieuMuaHoanThanhChiTietComponent,
    DeNghiNhapKhoComponent,
    PhieuDangXuLyComponent,
    PhieuDaHoanThanhComponent,
    ChiTietSuaComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NgxPaginationModule,
    NgxSpinnerModule.forRoot(),
    HttpClientModule,
    NgImageSliderModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7006"],
        disallowedRoutes: []
      }
    })
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  exports:[NgxSpinnerModule],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
