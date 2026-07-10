/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     1/26/2026 7:43:45 PM                         */
/*==============================================================*/



/*==============================================================*/
/* Table: CHITIETHOADON                                         */
/*==============================================================*/

SET FOREIGN_KEY_CHECKS = 0;
create table CHITIETHOADON
(
   MaHD                 varchar(15) not null  comment '',
   MaChiTietHD          varchar(20) not null  comment '',
   MaSP                 varchar(15) not null  comment '',
   SoLuongBan           int  comment '',
   DonGiaBan            decimal(18,2)  comment '',
   ThanhTien            decimal(18,2)  comment '',
   GiamGia              decimal(5,2)  comment '',
   primary key (MaHD, MaChiTietHD)
);

/*==============================================================*/
/* Table: HOADON                                                */
/*==============================================================*/
create table HOADON
(
   MaHD                 varchar(15) not null  comment '',
   MaNV                 varchar(15) not null  comment '',
   MaKhachHang          varchar(15) not null  comment '',
   NgayLapHD            date  comment '',
   NgayGiaoHang         date  comment '',
   TrangThaiGiaoHang    national varchar(50)  comment '',
   primary key (MaHD)
);

/*==============================================================*/
/* Table: KHACHHANG                                             */
/*==============================================================*/
create table KHACHHANG
(
   MaKhachHang          varchar(15) not null  comment '',
   TenKhachHang         national varchar(100)  comment '',
   SDTKhachHang         national varchar(15)  comment '',
   DiaChiKhachHang      national varchar(255)  comment '',
   primary key (MaKhachHang)
);

/*==============================================================*/
/* Table: MUCDICHSUDUNG                                         */
/*==============================================================*/
create table MUCDICHSUDUNG
(
   MaMD                 varchar(15) not null  comment '',
   MaSP                 varchar(15) not null  comment '',
   TenMD                national varchar(50)  comment '',
   MoTaMD               national varchar(500)  comment '',
   primary key (MaMD)
);

/*==============================================================*/
/* Table: NHACUNGCAP                                            */
/*==============================================================*/
create table NHACUNGCAP
(
   MaNcc                national varchar(255) not null  comment '',
   TenNcc               national varchar(100)  comment '',
   MoTaNcc              national varchar(500)  comment '',
   primary key (MaNcc)
);

/*==============================================================*/
/* Table: NHANVIEN                                              */
/*==============================================================*/
create table NHANVIEN
(
   MaNV                 varchar(15) not null  comment '',
   TenNV                national varchar(50)  comment '',
   NgaySinh             date  comment '',
   GioiTinh             national varchar(10)  comment '',
   SoDT                 varchar(15)  comment '',
   DiaChiNV             national varchar(255)  comment '',
   VaiTroKhuVucPhuTrach national varchar(100)  comment '',
   TrangThaiLamViec     national varchar(150)  comment '',
   primary key (MaNV)
);

/*==============================================================*/
/* Table: NHOMSANPHAM                                           */
/*==============================================================*/
create table NHOMSANPHAM
(
   MaNhomSP             varchar(15) not null  comment '',
   TenNhomSP            national varchar(100)  comment '',
   primary key (MaNhomSP)
);

/*==============================================================*/
/* Table: SANPHAM                                               */
/*==============================================================*/
create table SANPHAM
(
   MaSP                 varchar(15) not null  comment '',
   MaVL                 varchar(15) not null  comment '',
   MaNcc                national varchar(255) not null  comment '',
   MaNhomSP             varchar(15) not null  comment '',
   TenSP                national varchar(100)  comment '',
   DonViTinh            national varchar(50)  comment '',
   SoLuongTon           int  comment '',
   GiaBan               decimal(18,2)  comment '',
   MoTa                 national varchar(500)  comment '',
   primary key (MaSP)
);

/*==============================================================*/
/* Table: VATLIEU                                               */
/*==============================================================*/
create table VATLIEU
(
   MaVL                 varchar(15) not null  comment '',
   TenVL                national varchar(100)  comment '',
   primary key (MaVL)
);

alter table CHITIETHOADON add constraint FK_CHITIETH_THUOC_HOADON foreign key (MaHD)
      references HOADON (MaHD);

alter table CHITIETHOADON add constraint FK_CHITIETH_XUAT_HIEN_SANPHAM foreign key (MaSP)
      references SANPHAM (MaSP);

alter table HOADON add constraint FK_HOADON_DUOC_CAP_KHACHHAN foreign key (MaKhachHang)
      references KHACHHANG (MaKhachHang);

alter table HOADON add constraint FK_HOADON_LAP_NHANVIEN foreign key (MaNV)
      references NHANVIEN (MaNV);

alter table MUCDICHSUDUNG add constraint FK_MUCDICHS_CO_SANPHAM foreign key (MaSP)
      references SANPHAM (MaSP);

alter table SANPHAM add constraint FK_SANPHAM_CHUA_NHOMSANP foreign key (MaNhomSP)
      references NHOMSANPHAM (MaNhomSP);

alter table SANPHAM add constraint FK_SANPHAM_CUNG_CAP_NHACUNGC foreign key (MaNcc)
      references NHACUNGCAP (MaNcc);

alter table SANPHAM add constraint FK_SANPHAM_LAM_NEN_VATLIEU foreign key (MaVL)
      references VATLIEU (MaVL);

