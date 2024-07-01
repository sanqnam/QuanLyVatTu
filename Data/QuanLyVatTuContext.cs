using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLVT_BE.Data;

public partial class QuanLyVatTuContext : DbContext
{
    public QuanLyVatTuContext()
    {
    }

    public QuanLyVatTuContext(DbContextOptions<QuanLyVatTuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietPhieu> ChiTietPhieus { get; set; }

    public virtual DbSet<ChiTietPhieuSuaChua> ChiTietPhieuSuaChuas { get; set; }

    public virtual DbSet<ChiTietVatTu> ChiTietVatTus { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<DeNghiNhapKho> DeNghiNhapKhos { get; set; }

    public virtual DbSet<HinhAnhSuaChua> HinhAnhSuaChuas { get; set; }

    public virtual DbSet<HinhAnhVatTu> HinhAnhVatTus { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<Kho> Khos { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PhieuDeNghiSuaChua> PhieuDeNghiSuaChuas { get; set; }

    public virtual DbSet<PhieuDeNghiVatTu> PhieuDeNghiVatTus { get; set; }

    public virtual DbSet<PhieuTrinhMua> PhieuTrinhMuas { get; set; }

    public virtual DbSet<PhongBan> PhongBans { get; set; }

    public virtual DbSet<ThongBao> ThongBaos { get; set; }

    public virtual DbSet<TinhTrangPhieu> TinhTrangPhieus { get; set; }

    public virtual DbSet<TinhTrangVatTu> TinhTrangVatTus { get; set; }

    public virtual DbSet<TinhTrangXuLy> TinhTrangXuLies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VatTu> VatTus { get; set; }

    public virtual DbSet<VatTuQuanTam> VatTuQuanTams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SOJU\\SOJU;Initial Catalog=QuanLyVatTu;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietPhieu>(entity =>
        {
            entity.HasKey(e => e.IdChiTietPhieu);

            entity.ToTable("ChiTietPhieu");

            entity.Property(e => e.DonViCungCap).HasMaxLength(50);
            entity.Property(e => e.DonViTinhDeNghi).HasMaxLength(50);
            entity.Property(e => e.DonViTinhThayDoi).HasMaxLength(50);
            entity.Property(e => e.GhiChuDuyetPhieu).HasColumnType("ntext");
            entity.Property(e => e.GhiChuNguoiDung).HasMaxLength(300);
            entity.Property(e => e.GhiChuSuaPhieuMua).HasColumnType("ntext");
            entity.Property(e => e.GhiChuSuaTheoHoaDon).HasColumnType("ntext");
            entity.Property(e => e.GiChuMuaThem).HasColumnType("ntext");
            entity.Property(e => e.GiChuThuKho).HasColumnType("ntext");
            entity.Property(e => e.GiChuXuatVatTu).HasColumnType("ntext");
            entity.Property(e => e.MaVatTu).HasMaxLength(100);
            entity.Property(e => e.TenVatTu).HasMaxLength(300);
            entity.Property(e => e.Vat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VAT");

            entity.HasOne(d => d.IdHoaDonNavigation).WithMany(p => p.ChiTietPhieus)
                .HasForeignKey(d => d.IdHoaDon)
                .HasConstraintName("FK_ChiTietPhieu_HoaDon");

            entity.HasOne(d => d.IdPhieuDeNghiNavigation).WithMany(p => p.ChiTietPhieus)
                .HasForeignKey(d => d.IdPhieuDeNghi)
                .HasConstraintName("FK_ChiTietPhieu_PhieuDeNghiVatTu");

            entity.HasOne(d => d.IdPhieuDeNghiMuaNavigation).WithMany(p => p.ChiTietPhieus)
                .HasForeignKey(d => d.IdPhieuDeNghiMua)
                .HasConstraintName("FK_ChiTietPhieu_PhieuTrinhMua");

            entity.HasOne(d => d.IdTinhTrangXuLyNavigation).WithMany(p => p.ChiTietPhieus)
                .HasForeignKey(d => d.IdTinhTrangXuLy)
                .HasConstraintName("FK_ChiTietPhieu_TinhTrangXuLy");

            entity.HasOne(d => d.IdVatTuNavigation).WithMany(p => p.ChiTietPhieus)
                .HasForeignKey(d => d.IdVatTu)
                .HasConstraintName("FK_ChiTietPhieu_VatTu");
        });

        modelBuilder.Entity<ChiTietPhieuSuaChua>(entity =>
        {
            entity.HasKey(e => e.IdChiTietSuaChua);

            entity.ToTable("ChiTietPhieuSuaChua");

            entity.Property(e => e.GhiChuThucHien).HasMaxLength(250);
            entity.Property(e => e.MoTaTinhTrang).HasMaxLength(250);
            entity.Property(e => e.NgayDuaRaNgoai).HasColumnType("date");
            entity.Property(e => e.NgayNhanLai).HasColumnType("date");
            entity.Property(e => e.TrangThaiXuLy).HasMaxLength(50);

            entity.HasOne(d => d.IdChiTietVatTuNavigation).WithMany(p => p.ChiTietPhieuSuaChuas)
                .HasForeignKey(d => d.IdChiTietVatTu)
                .HasConstraintName("FK_ChiTietPhieuSuaChua_ChiTietVatTu");

            entity.HasOne(d => d.IdPhieuSuaChuaNavigation).WithMany(p => p.ChiTietPhieuSuaChuas)
                .HasForeignKey(d => d.IdPhieuSuaChua)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietPhieuSuaChua_PhieuDeNghiSuaChua1");

            entity.HasOne(d => d.NguoiThucHienNavigation).WithMany(p => p.ChiTietPhieuSuaChuas)
                .HasForeignKey(d => d.NguoiThucHien)
                .HasConstraintName("FK_ChiTietPhieuSuaChua_User");

            entity.HasOne(d => d.PhongBanThucHienNavigation).WithMany(p => p.ChiTietPhieuSuaChuas)
                .HasForeignKey(d => d.PhongBanThucHien)
                .HasConstraintName("FK_ChiTietPhieuSuaChua_PhongBan");
        });

        modelBuilder.Entity<ChiTietVatTu>(entity =>
        {
            entity.HasKey(e => e.IdChiTietVatTu);

            entity.ToTable("ChiTietVatTu");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.LichSu).HasColumnType("ntext");
            entity.Property(e => e.MaVatTu).HasMaxLength(100);

            entity.HasOne(d => d.IdDeNghiNhapKhoNavigation).WithMany(p => p.ChiTietVatTus)
                .HasForeignKey(d => d.IdDeNghiNhapKho)
                .HasConstraintName("FK_ChiTietVatTu_DeNghiNhapKho");

            entity.HasOne(d => d.IdTinhTrangNavigation).WithMany(p => p.ChiTietVatTus)
                .HasForeignKey(d => d.IdTinhTrang)
                .HasConstraintName("FK_ChiTietVatTu_TinhTrangVatTu");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ChiTietVatTus)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_ChiTietVatTu_User");

            entity.HasOne(d => d.IdVatTuNavigation).WithMany(p => p.ChiTietVatTus)
                .HasForeignKey(d => d.IdVatTu)
                .HasConstraintName("FK_ChiTietVatTu_VatTu");
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.IdChucVu);

            entity.ToTable("ChucVu");

            entity.Property(e => e.MaChuVu)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenChucVu).HasMaxLength(50);
        });

        modelBuilder.Entity<DeNghiNhapKho>(entity =>
        {
            entity.HasKey(e => e.IdPhieuNhapKho);

            entity.ToTable("DeNghiNhapKho");

            entity.Property(e => e.IdPhieuNhapKho).ValueGeneratedOnAdd();
            entity.Property(e => e.MaPhieuDenghi).HasMaxLength(50);
            entity.Property(e => e.NgayTaoPhieu).HasColumnType("date");
            entity.Property(e => e.TinhTrang).HasMaxLength(50);

            entity.HasOne(d => d.IdChiTietPhieuNavigation).WithMany(p => p.DeNghiNhapKhos)
                .HasForeignKey(d => d.IdChiTietPhieu)
                .HasConstraintName("FK_DeNghiNhapKho_ChiTietPhieu");

            entity.HasOne(d => d.IdPhieuNhapKhoNavigation).WithOne(p => p.DeNghiNhapKho)
                .HasForeignKey<DeNghiNhapKho>(d => d.IdPhieuNhapKho)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeNghiNhapKho_HinhAnhVatTu");

            entity.HasOne(d => d.IdThuKhoNavigation).WithMany(p => p.DeNghiNhapKhoIdThuKhoNavigations)
                .HasForeignKey(d => d.IdThuKho)
                .HasConstraintName("FK_DeNghiNhapKho_User1");

            entity.HasOne(d => d.NguoiKiemPhieuNavigation).WithMany(p => p.DeNghiNhapKhoNguoiKiemPhieuNavigations)
                .HasForeignKey(d => d.NguoiKiemPhieu)
                .HasConstraintName("FK_DeNghiNhapKho_User2");

            entity.HasOne(d => d.NguoiTaoPhieuNavigation).WithMany(p => p.DeNghiNhapKhoNguoiTaoPhieuNavigations)
                .HasForeignKey(d => d.NguoiTaoPhieu)
                .HasConstraintName("FK_DeNghiNhapKho_User");
        });

        modelBuilder.Entity<HinhAnhSuaChua>(entity =>
        {
            entity.HasKey(e => e.IdHinhSuaChua);

            entity.ToTable("HinhAnhSuaChua");

            entity.Property(e => e.Url).HasMaxLength(300);

            entity.HasOne(d => d.IdChiTietSuaChuaNavigation).WithMany(p => p.HinhAnhSuaChuas)
                .HasForeignKey(d => d.IdChiTietSuaChua)
                .HasConstraintName("FK_HinhAnhSuaChua_ChiTietPhieuSuaChua");
        });

        modelBuilder.Entity<HinhAnhVatTu>(entity =>
        {
            entity.HasKey(e => e.IdHinhAnh);

            entity.ToTable("HinhAnhVatTu");

            entity.Property(e => e.Image)
                .HasMaxLength(300)
                .HasColumnName("image");

            entity.HasOne(d => d.IdChiTietVatTuNavigation).WithMany(p => p.HinhAnhVatTus)
                .HasForeignKey(d => d.IdChiTietVatTu)
                .HasConstraintName("FK_HinhAnhVatTu_ChiTietVatTu");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.HinhAnhVatTus)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_HinhAnhVatTu_User");

            entity.HasOne(d => d.IdVatTuNavigation).WithMany(p => p.HinhAnhVatTus)
                .HasForeignKey(d => d.IdVatTu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HinhAnhVatTu_VatTu");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.IdHoaDon);

            entity.ToTable("HoaDon");

            entity.Property(e => e.DonViCungCap).HasMaxLength(200);
            entity.Property(e => e.HinhThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.NgayHoaDon).HasColumnType("date");
            entity.Property(e => e.NgayNhapHoaDon).HasColumnType("date");
            entity.Property(e => e.SoHoaDon).HasMaxLength(100);

            entity.HasOne(d => d.IdPhieuDeNghiMuaNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.IdPhieuDeNghiMua)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoaDon_PhieuTrinhMua");
        });

        modelBuilder.Entity<Kho>(entity =>
        {
            entity.HasKey(e => e.IdKho);

            entity.ToTable("Kho");

            entity.Property(e => e.IdKho).ValueGeneratedNever();
            entity.Property(e => e.TenKho).HasMaxLength(50);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.IdNoti);

            entity.ToTable("Notification");

            entity.Property(e => e.IdNoti).HasColumnName("idNoti");
            entity.Property(e => e.IdTb).HasColumnName("idTB");
            entity.Property(e => e.Mess).HasMaxLength(300);
            entity.Property(e => e.TimeTao).HasColumnType("datetime");
            entity.Property(e => e.Url).HasMaxLength(200);

            entity.HasOne(d => d.NguoiGuiNavigation).WithMany(p => p.NotificationNguoiGuiNavigations)
                .HasForeignKey(d => d.NguoiGui)
                .HasConstraintName("FK_Notification_User");

            entity.HasOne(d => d.NguoiNhanNavigation).WithMany(p => p.NotificationNguoiNhanNavigations)
                .HasForeignKey(d => d.NguoiNhan)
                .HasConstraintName("FK_Notification_User1");
        });

        modelBuilder.Entity<PhieuDeNghiSuaChua>(entity =>
        {
            entity.HasKey(e => e.IdPhieuSuaChua);

            entity.ToTable("PhieuDeNghiSuaChua");

            entity.Property(e => e.LyDo).HasMaxLength(250);
            entity.Property(e => e.LyDoTraPhieu).HasMaxLength(100);
            entity.Property(e => e.MaPhieuDeNghi).HasMaxLength(50);
            entity.Property(e => e.NgayDuyetPhieu).HasColumnType("datetime");
            entity.Property(e => e.NgayTaoPhieu).HasColumnType("datetime");

            entity.HasOne(d => d.IdPhongNhanNavigation).WithMany(p => p.PhieuDeNghiSuaChuas)
                .HasForeignKey(d => d.IdPhongNhan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhieuDeNghiSuaChua_PhongBan");

            entity.HasOne(d => d.IdTinhTrangPhieuNavigation).WithMany(p => p.PhieuDeNghiSuaChuas)
                .HasForeignKey(d => d.IdTinhTrangPhieu)
                .HasConstraintName("FK_PhieuDeNghiSuaChua_TinhTrangPhieu");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.PhieuDeNghiSuaChuas)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhieuDeNghiSuaChua_User");
        });

        modelBuilder.Entity<PhieuDeNghiVatTu>(entity =>
        {
            entity.HasKey(e => e.IdPhieuDeNghi);

            entity.ToTable("PhieuDeNghiVatTu");

            entity.Property(e => e.IdPhieuDeNghi).ValueGeneratedNever();
            entity.Property(e => e.LyDoLapPhieu).HasMaxLength(250);
            entity.Property(e => e.LyDoTraPhieu).HasColumnType("text");
            entity.Property(e => e.NguoiTraPhieu).HasMaxLength(250);
            entity.Property(e => e.SoPhieu).HasMaxLength(50);
            entity.Property(e => e.TimeDuyetPhieu).HasColumnType("datetime");
            entity.Property(e => e.TimeTaoPhieu).HasColumnType("datetime");

            entity.HasOne(d => d.IdLanhDaoNavigation).WithMany(p => p.PhieuDeNghiVatTuIdLanhDaoNavigations)
                .HasForeignKey(d => d.IdLanhDao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhieuDeNghiVatTu_User2");

            entity.HasOne(d => d.IdPhongBanNavigation).WithMany(p => p.PhieuDeNghiVatTus)
                .HasForeignKey(d => d.IdPhongBan)
                .HasConstraintName("FK_PhieuDeNghiVatTu_PhongBan");

            entity.HasOne(d => d.IdThuTruongNavigation).WithMany(p => p.PhieuDeNghiVatTuIdThuTruongNavigations)
                .HasForeignKey(d => d.IdThuTruong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhieuDeNghiVatTu_User1");

            entity.HasOne(d => d.IdTinhTrangPhieuNavigation).WithMany(p => p.PhieuDeNghiVatTus)
                .HasForeignKey(d => d.IdTinhTrangPhieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhieuDeNghiVatTu_TinhTrangPhieu");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.PhieuDeNghiVatTuIdUserNavigations)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhieuDeNghiVatTu_User");
        });

        modelBuilder.Entity<PhieuTrinhMua>(entity =>
        {
            entity.HasKey(e => e.IdPhieuDeNghiMua);

            entity.ToTable("PhieuTrinhMua");

            entity.Property(e => e.LyDo).HasMaxLength(250);
            entity.Property(e => e.LyDoSuaPhieu).HasMaxLength(250);
            entity.Property(e => e.TimeTaoPhieu).HasColumnType("datetime");

            entity.HasOne(d => d.IdLanhDaoNavigation).WithMany(p => p.PhieuTrinhMuaIdLanhDaoNavigations)
                .HasForeignKey(d => d.IdLanhDao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhieuTrinhMua_User2");

            entity.HasOne(d => d.IdPhongBanNavigation).WithMany(p => p.PhieuTrinhMuas)
                .HasForeignKey(d => d.IdPhongBan)
                .HasConstraintName("FK_PhieuTrinhMua_PhongBan");

            entity.HasOne(d => d.IdThuTruongNavigation).WithMany(p => p.PhieuTrinhMuaIdThuTruongNavigations)
                .HasForeignKey(d => d.IdThuTruong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhieuTrinhMua_User1");

            entity.HasOne(d => d.IdTinhTrangPhieuNavigation).WithMany(p => p.PhieuTrinhMuas)
                .HasForeignKey(d => d.IdTinhTrangPhieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhieuTrinhMua_TinhTrangPhieu");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.PhieuTrinhMuaIdUserNavigations)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhieuTrinhMua_User");
        });

        modelBuilder.Entity<PhongBan>(entity =>
        {
            entity.HasKey(e => e.IdPhongBan);

            entity.ToTable("PhongBan");

            entity.Property(e => e.MaPhongBan)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenPhongBan).HasMaxLength(50);
        });

        modelBuilder.Entity<ThongBao>(entity =>
        {
            entity.HasKey(e => e.IdTb);

            entity.ToTable("ThongBao");

            entity.Property(e => e.IdTb)
                .ValueGeneratedNever()
                .HasColumnName("idTB");
            entity.Property(e => e.ThongBao1)
                .HasMaxLength(200)
                .HasColumnName("ThongBao");
        });

        modelBuilder.Entity<TinhTrangPhieu>(entity =>
        {
            entity.HasKey(e => e.IdTrinhTrangPhieu);

            entity.ToTable("TinhTrangPhieu");

            entity.Property(e => e.TenTinhTrangDuyet).HasMaxLength(50);
        });

        modelBuilder.Entity<TinhTrangVatTu>(entity =>
        {
            entity.HasKey(e => e.IdTinhTrang);

            entity.ToTable("TinhTrangVatTu");

            entity.Property(e => e.TenTinhTrang).HasMaxLength(50);
        });

        modelBuilder.Entity<TinhTrangXuLy>(entity =>
        {
            entity.HasKey(e => e.IdTinhTrangXuLy);

            entity.ToTable("TinhTrangXuLy");

            entity.Property(e => e.TenTinhTrangXuLy).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.Property(e => e.AboutMe).HasColumnType("ntext");
            entity.Property(e => e.ConnectionId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(250);
            entity.Property(e => e.DienThoai)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.HinhDaiDien).HasMaxLength(200);
            entity.Property(e => e.HoTen).HasMaxLength(200);
            entity.Property(e => e.MaBiMat)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.IdChucVuNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdChucVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_ChucVu");

            entity.HasOne(d => d.IdPhongBanNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdPhongBan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_PhongBan");
        });

        modelBuilder.Entity<VatTu>(entity =>
        {
            entity.HasKey(e => e.IdVatTu);

            entity.ToTable("VatTu");

            entity.Property(e => e.IdVatTu).ValueGeneratedNever();
            entity.Property(e => e.DonViTinh).HasMaxLength(50);
            entity.Property(e => e.GhiChu).HasColumnType("text");
            entity.Property(e => e.MaVatTu)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SoLuongTonKho).HasDefaultValueSql("((0))");
            entity.Property(e => e.TenVatTu).HasMaxLength(200);
            entity.Property(e => e.ThongSo).HasColumnType("text");
            entity.Property(e => e.ViTri).HasMaxLength(50);
        });

        modelBuilder.Entity<VatTuQuanTam>(entity =>
        {
            entity.HasKey(e => e.IdQuanTam);

            entity.ToTable("VatTuQuanTam");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.VatTuQuanTams)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_VatTuQuanTam_User");

            entity.HasOne(d => d.IdVatTuNavigation).WithMany(p => p.VatTuQuanTams)
                .HasForeignKey(d => d.IdVatTu)
                .HasConstraintName("FK_VatTuQuanTam_VatTu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
