/*==============================================================*/
/* DBMS name:      SAP SQL Anywhere 17                          */
/* Created on:     12/18/2025 8:50:19 PM                        */
/*==============================================================*/


if exists(select 1 from sys.sysforeignkey where role='FK_CHITIETH_THUOC_HOADON') then
    alter table CHITIETHOADON
       delete foreign key FK_CHITIETH_THUOC_HOADON
end if;

if exists(select 1 from sys.sysforeignkey where role='FK_CHITIETH_XUAT_HIEN_SANPHAM') then
    alter table CHITIETHOADON
       delete foreign key FK_CHITIETH_XUAT_HIEN_SANPHAM
end if;

if exists(select 1 from sys.sysforeignkey where role='FK_HOADON_DUOC_CAP_KHACHHAN') then
    alter table HOADON
       delete foreign key FK_HOADON_DUOC_CAP_KHACHHAN
end if;

if exists(select 1 from sys.sysforeignkey where role='FK_HOADON_LAP_NHANVIEN') then
    alter table HOADON
       delete foreign key FK_HOADON_LAP_NHANVIEN
end if;

if exists(select 1 from sys.sysforeignkey where role='FK_MUCDICHS_CO_SANPHAM') then
    alter table MUCDICHSUDUNG
       delete foreign key FK_MUCDICHS_CO_SANPHAM
end if;

if exists(select 1 from sys.sysforeignkey where role='FK_SANPHAM_CHUA_NHOMSANP') then
    alter table SANPHAM
       delete foreign key FK_SANPHAM_CHUA_NHOMSANP
end if;

if exists(select 1 from sys.sysforeignkey where role='FK_SANPHAM_CUNG_CAP_NHACUNGC') then
    alter table SANPHAM
       delete foreign key FK_SANPHAM_CUNG_CAP_NHACUNGC
end if;

if exists(select 1 from sys.sysforeignkey where role='FK_SANPHAM_LAM_NEN_VATLIEU') then
    alter table SANPHAM
       delete foreign key FK_SANPHAM_LAM_NEN_VATLIEU
end if;

drop index if exists CHITIETHOADON.xuat_hien_trong_FK;

drop index if exists CHITIETHOADON.thuoc_FK;

drop index if exists CHITIETHOADON.CHITIETHOADON_PK;

drop table if exists CHITIETHOADON;

drop index if exists HOADON.Duoc_cap_FK;

drop index if exists HOADON.lap_FK;

drop index if exists HOADON.HOADON_PK;

drop table if exists HOADON;

drop index if exists KHACHHANG.KHACHHANG_PK;

drop table if exists KHACHHANG;

drop index if exists MUCDICHSUDUNG.co_FK;

drop index if exists MUCDICHSUDUNG.MUCDICHSUDUNG_PK;

drop table if exists MUCDICHSUDUNG;

drop index if exists NHACUNGCAP.NHACUNGCAP_PK;

drop table if exists NHACUNGCAP;

drop index if exists NHANVIEN.NHANVIEN_PK;

drop table if exists NHANVIEN;

drop index if exists NHOMSANPHAM.NHOMSANPHAM_PK;

drop table if exists NHOMSANPHAM;

drop index if exists SANPHAM.lam_nen_FK;

drop index if exists SANPHAM.cung_cap_FK;

drop index if exists SANPHAM.chua_FK;

drop index if exists SANPHAM.SANPHAM_PK;

drop table if exists SANPHAM;

drop index if exists VATLIEU.VATLIEU_PK;

drop table if exists VATLIEU;

/*==============================================================*/
/* Table: CHITIETHOADON                                         */
/*==============================================================*/
create or replace table CHITIETHOADON 
(
   MaHD                 varchar(15)                    not null,
   MaChiTietHD          varchar(20)                    not null,
   MaSP                 varchar(15)                    not null,
   SoLuongBan           integer                        null,
   DonGiaBan            decimal(18,2)                  null,
   ThanhTien            decimal(18,2)                  null,
   GiamGia              decimal(5,2)                   null,
   constraint PK_CHITIETHOADON primary key clustered (MaHD, MaChiTietHD)
);

/*==============================================================*/
/* Index: CHITIETHOADON_PK                                      */
/*==============================================================*/
create unique clustered index CHITIETHOADON_PK on CHITIETHOADON (
MaHD ASC,
MaChiTietHD ASC
);

/*==============================================================*/
/* Index: thuoc_FK                                              */
/*==============================================================*/
create index thuoc_FK on CHITIETHOADON (
MaHD ASC
);

/*==============================================================*/
/* Index: xuat_hien_trong_FK                                    */
/*==============================================================*/
create index xuat_hien_trong_FK on CHITIETHOADON (
MaSP ASC
);

/*==============================================================*/
/* Table: HOADON                                                */
/*==============================================================*/
create or replace table HOADON 
(
   MaHD                 varchar(15)                    not null,
   MaNV                 varchar(15)                    not null,
   MaKhachHang          varchar(15)                    not null,
   NgayLapHD            date                           null,
   DiaChiGiaoHang       varchar(100)                   null,
   NgayGiaoHang         date                           null,
   TrangThaiGiaoHang    varchar(50)                    null,
   constraint PK_HOADON primary key clustered (MaHD)
);

/*==============================================================*/
/* Index: HOADON_PK                                             */
/*==============================================================*/
create unique clustered index HOADON_PK on HOADON (
MaHD ASC
);

/*==============================================================*/
/* Index: lap_FK                                                */
/*==============================================================*/
create index lap_FK on HOADON (
MaNV ASC
);

/*==============================================================*/
/* Index: Duoc_cap_FK                                           */
/*==============================================================*/
create index Duoc_cap_FK on HOADON (
MaKhachHang ASC
);

/*==============================================================*/
/* Table: KHACHHANG                                             */
/*==============================================================*/
create or replace table KHACHHANG 
(
   MaKhachHang          varchar(15)                    not null,
   TenKhachHang         varchar(100)                   null,
   SDTKhachHang         varchar(15)                    null,
   DiaChiKhachHang      varchar(255)                   null,
   constraint PK_KHACHHANG primary key clustered (MaKhachHang)
);

/*==============================================================*/
/* Index: KHACHHANG_PK                                          */
/*==============================================================*/
create unique clustered index KHACHHANG_PK on KHACHHANG (
MaKhachHang ASC
);

/*==============================================================*/
/* Table: MUCDICHSUDUNG                                         */
/*==============================================================*/
create or replace table MUCDICHSUDUNG 
(
   MaMD                 varchar(15)                    not null,
   MaSP                 varchar(15)                    not null,
   TenMD                varchar(50)                    null,
   MoTaMD               varchar(500)                   null,
   constraint PK_MUCDICHSUDUNG primary key clustered (MaMD)
);

/*==============================================================*/
/* Index: MUCDICHSUDUNG_PK                                      */
/*==============================================================*/
create unique clustered index MUCDICHSUDUNG_PK on MUCDICHSUDUNG (
MaMD ASC
);

/*==============================================================*/
/* Index: co_FK                                                 */
/*==============================================================*/
create index co_FK on MUCDICHSUDUNG (
MaSP ASC
);

/*==============================================================*/
/* Table: NHACUNGCAP                                            */
/*==============================================================*/
create or replace table NHACUNGCAP 
(
   MaNcc                varchar(255)                   not null,
   TenNcc               varchar(100)                   null,
   MoTaNcc              varchar(500)                   null,
   constraint PK_NHACUNGCAP primary key clustered (MaNcc)
);

/*==============================================================*/
/* Index: NHACUNGCAP_PK                                         */
/*==============================================================*/
create unique clustered index NHACUNGCAP_PK on NHACUNGCAP (
MaNcc ASC
);

/*==============================================================*/
/* Table: NHANVIEN                                              */
/*==============================================================*/
create or replace table NHANVIEN 
(
   MaNV                 varchar(15)                    not null,
   TenNV                varchar(50)                    null,
   NgaySinh             date                           null,
   GioiTinh             varchar(10)                    null,
   SoDT                 varchar(15)                    null,
   DiaChiNV             varchar(255)                   null,
   VaiTroKhuVucPhuTrach varchar(100)                   null,
   TrangThaiLamViec     varchar(150)                   null,
   constraint PK_NHANVIEN primary key clustered (MaNV)
);

/*==============================================================*/
/* Index: NHANVIEN_PK                                           */
/*==============================================================*/
create unique clustered index NHANVIEN_PK on NHANVIEN (
MaNV ASC
);

/*==============================================================*/
/* Table: NHOMSANPHAM                                           */
/*==============================================================*/
create or replace table NHOMSANPHAM 
(
   MaNhomSP             varchar(15)                    not null,
   TenNhomSP            varchar(100)                   null,
   constraint PK_NHOMSANPHAM primary key clustered (MaNhomSP)
);

/*==============================================================*/
/* Index: NHOMSANPHAM_PK                                        */
/*==============================================================*/
create unique clustered index NHOMSANPHAM_PK on NHOMSANPHAM (
MaNhomSP ASC
);

/*==============================================================*/
/* Table: SANPHAM                                               */
/*==============================================================*/
create or replace table SANPHAM 
(
   MaSP                 varchar(15)                    not null,
   MaVL                 varchar(15)                    not null,
   MaNcc                varchar(255)                   not null,
   MaNhomSP             varchar(15)                    not null,
   TenSP                varchar(100)                   null,
   DonViTinh            varchar(50)                    null,
   SoLuongTon           integer                        null,
   GiaBan               decimal(18,2)                  null,
   MoTa                 varchar(500)                   null,
   constraint PK_SANPHAM primary key clustered (MaSP)
);

/*==============================================================*/
/* Index: SANPHAM_PK                                            */
/*==============================================================*/
create unique clustered index SANPHAM_PK on SANPHAM (
MaSP ASC
);

/*==============================================================*/
/* Index: chua_FK                                               */
/*==============================================================*/
create index chua_FK on SANPHAM (
MaNhomSP ASC
);

/*==============================================================*/
/* Index: cung_cap_FK                                           */
/*==============================================================*/
create index cung_cap_FK on SANPHAM (
MaNcc ASC
);

/*==============================================================*/
/* Index: lam_nen_FK                                            */
/*==============================================================*/
create index lam_nen_FK on SANPHAM (
MaVL ASC
);

/*==============================================================*/
/* Table: VATLIEU                                               */
/*==============================================================*/
create or replace table VATLIEU 
(
   MaVL                 varchar(15)                    not null,
   TenVL                varchar(100)                   null,
   constraint PK_VATLIEU primary key clustered (MaVL)
);

/*==============================================================*/
/* Index: VATLIEU_PK                                            */
/*==============================================================*/
create unique clustered index VATLIEU_PK on VATLIEU (
MaVL ASC
);

alter table CHITIETHOADON
   add constraint FK_CHITIETH_THUOC_HOADON foreign key (MaHD)
      references HOADON (MaHD)
      on update restrict
      on delete restrict;

alter table CHITIETHOADON
   add constraint FK_CHITIETH_XUAT_HIEN_SANPHAM foreign key (MaSP)
      references SANPHAM (MaSP)
      on update restrict
      on delete restrict;

alter table HOADON
   add constraint FK_HOADON_DUOC_CAP_KHACHHAN foreign key (MaKhachHang)
      references KHACHHANG (MaKhachHang)
      on update restrict
      on delete restrict;

alter table HOADON
   add constraint FK_HOADON_LAP_NHANVIEN foreign key (MaNV)
      references NHANVIEN (MaNV)
      on update restrict
      on delete restrict;

alter table MUCDICHSUDUNG
   add constraint FK_MUCDICHS_CO_SANPHAM foreign key (MaSP)
      references SANPHAM (MaSP)
      on update restrict
      on delete restrict;

alter table SANPHAM
   add constraint FK_SANPHAM_CHUA_NHOMSANP foreign key (MaNhomSP)
      references NHOMSANPHAM (MaNhomSP)
      on update restrict
      on delete restrict;

alter table SANPHAM
   add constraint FK_SANPHAM_CUNG_CAP_NHACUNGC foreign key (MaNcc)
      references NHACUNGCAP (MaNcc)
      on update restrict
      on delete restrict;

alter table SANPHAM
   add constraint FK_SANPHAM_LAM_NEN_VATLIEU foreign key (MaVL)
      references VATLIEU (MaVL)
      on update restrict
      on delete restrict;

