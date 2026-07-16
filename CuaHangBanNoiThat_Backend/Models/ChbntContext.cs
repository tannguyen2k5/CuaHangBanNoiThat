using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DOANCNPM.Models;

public partial class ChbntContext : DbContext
{
    public ChbntContext()
    {
    }

    public ChbntContext(DbContextOptions<ChbntContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitiethoadon> Chitiethoadons { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Mucdichsudung> Mucdichsudungs { get; set; }

    public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Nhomsanpham> Nhomsanphams { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    public virtual DbSet<Vatlieu> Vatlieus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Để trống ở đây vì chúng ta đã cấu hình chuỗi kết nối an toàn trong file Program.cs
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cấu hình cho bảng Chi Tiết Hóa Đơn (Xử lý khóa chính phức hợp gồm MaHD và MaChiTietHD)
        modelBuilder.Entity<Chitiethoadon>(entity =>
        {
            entity.HasKey(e => new { e.MaHd, e.MaChiTietHd }).HasName("PK_CHITIETHOADON");

            entity.ToTable("CHITIETHOADON");

            entity.Property(e => e.MaHd)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MaHD");

            entity.Property(e => e.MaChiTietHd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MaChiTietHD");

            entity.Property(e => e.MaSp)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MaSP");

            entity.Property(e => e.DonGiaBan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GiamGia).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ThanhTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.Chitiethoadons)
                .HasForeignKey(d => d.MaHd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETH_THUOC_HOADON");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.Chitiethoadons)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETH_XUAT_HIEN_SANPHAM");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.MaHd);

            entity.ToTable("HOADON");

            entity.HasIndex(e => e.MaKhachHang, "Duoc_cap_FK");

            entity.HasIndex(e => e.MaNv, "lap_FK");

            entity.Property(e => e.MaHd)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MaHD");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.MaNv)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayGiaoHang).HasColumnType("datetime");
            entity.Property(e => e.NgayLapHd)
                .HasColumnType("datetime")
                .HasColumnName("NgayLapHD");
            entity.Property(e => e.TrangThaiGiaoHang).HasMaxLength(50);

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HOADON_DUOC_CAP_KHACHHAN");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HOADON_LAP_NHANVIEN");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang);

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.DiaChiKhachHang).HasMaxLength(255);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SdtkhachHang)
                .HasMaxLength(15)
                .HasColumnName("SDTKhachHang");
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenKhachHang).HasMaxLength(100);
        });

        modelBuilder.Entity<Mucdichsudung>(entity =>
        {
            entity.HasKey(e => e.MaMd);

            entity.ToTable("MUCDICHSUDUNG");

            entity.Property(e => e.MaMd)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MaMD");
            entity.Property(e => e.MoTaMd)
                .HasMaxLength(500)
                .HasColumnName("MoTaMD");
            entity.Property(e => e.TenMd)
                .HasMaxLength(50)
                .HasColumnName("TenMD");
        });

        modelBuilder.Entity<Nhacungcap>(entity =>
        {
            entity.HasKey(e => e.MaNcc);

            entity.ToTable("NHACUNGCAP");

            entity.Property(e => e.MaNcc).HasMaxLength(255);
            entity.Property(e => e.MoTaNcc).HasMaxLength(500);
            entity.Property(e => e.TenNcc).HasMaxLength(100);

            entity.HasMany(d => d.MaSps).WithMany(p => p.MaNccs)
                .UsingEntity<Dictionary<string, object>>(
                    "CungCap",
                    r => r.HasOne<Sanpham>().WithMany()
                        .HasForeignKey("MaSp")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CUNG_CAP_CUNG_CAP2_SANPHAM"),
                    l => l.HasOne<Nhacungcap>().WithMany()
                        .HasForeignKey("MaNcc")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CUNG_CAP_CUNG_CAP_NHACUNGC"),
                    j =>
                    {
                        j.HasKey("MaNcc", "MaSp").HasName("PK_CUNG_CAP");
                        j.ToTable("cung_cap");
                        j.HasIndex(new[] { "MaSp" }, "cung_cap2_FK");
                        j.HasIndex(new[] { "MaNcc" }, "cung_cap_FK");
                        j.IndexerProperty<string>("MaNcc").HasMaxLength(255);
                        j.IndexerProperty<string>("MaSp")
                            .HasMaxLength(15)
                            .IsUnicode(false)
                            .HasColumnName("MaSP");
                    });
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.MaNv);

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.MaNv)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.DiaChiNv)
                .HasMaxLength(255)
                .HasColumnName("DiaChiNV");
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.SoDt)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SoDT");
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("TenNV");
            entity.Property(e => e.TrangThaiLamViec).HasMaxLength(150);
            entity.Property(e => e.VaiTroKhuVucPhuTrach).HasMaxLength(100);
        });

        modelBuilder.Entity<Nhomsanpham>(entity =>
        {
            entity.HasKey(e => e.MaNhomSp);

            entity.ToTable("NHOMSANPHAM");

            entity.Property(e => e.MaNhomSp)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MaNhomSP");
            entity.Property(e => e.TenNhomSp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TenNhomSP");
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.MaSp);

            entity.ToTable("SANPHAM");

            entity.HasIndex(e => e.MaNhomSp, "chua_FK");

            entity.Property(e => e.MaSp)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MaSP");
            entity.Property(e => e.DonViTinh).HasMaxLength(50);
            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MaMd)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MaMD");
            entity.Property(e => e.MaNhomSp)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MaNhomSP");
            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.TenSp)
                .HasMaxLength(100)
                .HasColumnName("TenSP");

            entity.HasOne(d => d.MaMdNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.MaMd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SANPHAM_CO_MUCDICHS");

            entity.HasOne(d => d.MaNhomSpNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.MaNhomSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SANPHAM_CHUA_NHOMSANP");
        });

        modelBuilder.Entity<Vatlieu>(entity =>
        {
            entity.HasKey(e => e.MaVl);

            entity.ToTable("VATLIEU");

            entity.Property(e => e.MaVl)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MaVL");
            entity.Property(e => e.TenVl)
                .HasMaxLength(100)
                .HasColumnName("TenVL");

            entity.HasMany(d => d.MaSps).WithMany(p => p.MaVls)
                .UsingEntity<Dictionary<string, object>>(
                    "LamNen",
                    r => r.HasOne<Sanpham>().WithMany()
                        .HasForeignKey("MaSp")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LAM_NEN_LAM_NEN2_SANPHAM"),
                    l => l.HasOne<Vatlieu>().WithMany()
                        .HasForeignKey("MaVl")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LAM_NEN_LAM_NEN_VATLIEU"),
                    j =>
                    {
                        j.HasKey("MaVl", "MaSp").HasName("PK_LAM_NEN");
                        j.ToTable("lam_nen");
                        j.HasIndex(new[] { "MaSp" }, "lam_nen2_FK");
                        j.HasIndex(new[] { "MaVl" }, "lam_nen_FK");
                        j.IndexerProperty<string>("MaVl")
                            .HasMaxLength(15)
                            .IsUnicode(false)
                            .HasColumnName("MaVL");
                        j.IndexerProperty<string>("MaSp")
                            .HasMaxLength(15)
                            .IsUnicode(false)
                            .HasColumnName("MaSP");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}