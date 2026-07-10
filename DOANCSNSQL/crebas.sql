/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2017                    */
/* Created on:     12/6/2025 6:17:57 PM                         */
/*==============================================================*/
CREATE DATABASE QLCHBNT
GO
USE QLCHBNT

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETHOADON') and o.name = 'FK_CHITIETH_THUOC_HOADON')
alter table CHITIETHOADON
   drop constraint FK_CHITIETH_THUOC_HOADON
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETHOADON') and o.name = 'FK_CHITIETH_XUAT HIEN_SANPHAM')
alter table CHITIETHOADON
   drop constraint "FK_CHITIETH_XUAT HIEN_SANPHAM"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON') and o.name = 'FK_HOADON_DUOC CAP_KHACHHAN')
alter table HOADON
   drop constraint "FK_HOADON_DUOC CAP_KHACHHAN"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON') and o.name = 'FK_HOADON_LAP_NHANVIEN')
alter table HOADON
   drop constraint FK_HOADON_LAP_NHANVIEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MUCDICHSUDUNG') and o.name = 'FK_MUCDICHS_CO_SANPHAM')
alter table MUCDICHSUDUNG
   drop constraint FK_MUCDICHS_CO_SANPHAM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SANPHAM') and o.name = 'FK_SANPHAM_CHUA_NHOMSANP')
alter table SANPHAM
   drop constraint FK_SANPHAM_CHUA_NHOMSANP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SANPHAM') and o.name = 'FK_SANPHAM_CUNG CAP_NHACUNGC')
alter table SANPHAM
   drop constraint "FK_SANPHAM_CUNG CAP_NHACUNGC"
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SANPHAM') and o.name = 'FK_SANPHAM_LAM NEN_VATLIEU')
alter table SANPHAM
   drop constraint "FK_SANPHAM_LAM NEN_VATLIEU"
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETHOADON')
            and   name  = 'xuat hien trong_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETHOADON."xuat hien trong_FK"
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIETHOADON')
            and   type = 'U')
   drop table CHITIETHOADON
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON')
            and   name  = 'Duoc cap_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON."Duoc cap_FK"
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON')
            and   name  = 'lap_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON.lap_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOADON')
            and   type = 'U')
   drop table HOADON
go

if exists (select 1
            from  sysobjects
           where  id = object_id('KHACHHANG')
            and   type = 'U')
   drop table KHACHHANG
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MUCDICHSUDUNG')
            and   name  = 'co_FK'
            and   indid > 0
            and   indid < 255)
   drop index MUCDICHSUDUNG.co_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MUCDICHSUDUNG')
            and   type = 'U')
   drop table MUCDICHSUDUNG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NHACUNGCAP')
            and   type = 'U')
   drop table NHACUNGCAP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NHANVIEN')
            and   type = 'U')
   drop table NHANVIEN
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NHOMSANPHAM')
            and   type = 'U')
   drop table NHOMSANPHAM
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('SANPHAM')
            and   name  = 'lam nen_FK'
            and   indid > 0
            and   indid < 255)
   drop index SANPHAM."lam nen_FK"
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('SANPHAM')
            and   name  = 'cung cap_FK'
            and   indid > 0
            and   indid < 255)
   drop index SANPHAM."cung cap_FK"
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('SANPHAM')
            and   name  = 'chua_FK'
            and   indid > 0
            and   indid < 255)
   drop index SANPHAM.chua_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SANPHAM')
            and   type = 'U')
   drop table SANPHAM
go

if exists (select 1
            from  sysobjects
           where  id = object_id('VATLIEU')
            and   type = 'U')
   drop table VATLIEU
go

/*==============================================================*/
/* Table: CHITIETHOADON                                         */
/*==============================================================*/
create table CHITIETHOADON (
   MaHD                 varchar(15)          not null,
   MaSP                 varchar(15)          not null,
   SoLuongBan           int                  null,
   DonGiaBan            decimal(18,2)        null,
   ThanhTien            decimal(18,2)        null,
   GiamGia              decimal(5,2)         null,
   constraint PK_CHITIETHOADON primary key (MaHD)
)
go

/*==============================================================*/
/* Index: "xuat hien trong_FK"                                  */
/*==============================================================*/




create nonclustered index "xuat hien trong_FK" on CHITIETHOADON (MaSP ASC)
go

/*==============================================================*/
/* Table: HOADON                                                */
/*==============================================================*/
create table HOADON (
   MaHD                 varchar(15)          not null,
   MaNV                 varchar(15)          not null,
   MaKhachHang          varchar(15)          not null,
   NgayLapHD            datetime             null,
   DiaChiGiaoHang       nvarchar(100)        null,
   NgayGiaoHang         datetime             null,
   TrangThaiGiaoHang    nvarchar(50)         null,
   constraint PK_HOADON primary key (MaHD)
)
go

/*==============================================================*/
/* Index: lap_FK                                                */
/*==============================================================*/




create nonclustered index lap_FK on HOADON (MaNV ASC)
go

/*==============================================================*/
/* Index: "Duoc cap_FK"                                         */
/*==============================================================*/




create nonclustered index "Duoc cap_FK" on HOADON (MaKhachHang ASC)
go

/*==============================================================*/
/* Table: KHACHHANG                                             */
/*==============================================================*/
create table KHACHHANG (
   MaKhachHang          varchar(15)          not null,
   TenKhachHang         nvarchar(100)        null,
   SDTKhachHang         nvarchar(15)         null,
   DiaChiKhachHang      nvarchar(255)        not null,
   constraint PK_KHACHHANG primary key (MaKhachHang)
)
go

/*==============================================================*/
/* Table: MUCDICHSUDUNG                                         */
/*==============================================================*/
create table MUCDICHSUDUNG (
   MaMD                 varchar(15)          not null,
   MaSP                 varchar(15)          not null,
   TenMD                nvarchar(50)         null,
   MoTaMD               nvarchar(500)        null,
   constraint PK_MUCDICHSUDUNG primary key (MaMD)
)
go

/*==============================================================*/
/* Index: co_FK                                                 */
/*==============================================================*/




create nonclustered index co_FK on MUCDICHSUDUNG (MaSP ASC)
go

/*==============================================================*/
/* Table: NHACUNGCAP                                            */
/*==============================================================*/
create table NHACUNGCAP (
   MaNcc                nvarchar(255)        not null,
   TenNcc               nvarchar(100)        null,
   MoTaNcc              nvarchar(500)        null,
   constraint PK_NHACUNGCAP primary key (MaNcc)
)
go

/*==============================================================*/
/* Table: NHANVIEN                                              */
/*==============================================================*/
create table NHANVIEN (
   MaNV                 varchar(15)          not null,
   TenNV                nvarchar(50)         null,
   NgaySinhNV           Date                 not null,
   GioiTinh             nvarchar(10)         null,
   SoDienThoaiNV        nvarchar(15)         not null,
   DiaChiNV             nvarchar(255)        not null,
   VaiTroPhuTrach       nvarchar(50)         not null,
   TrangThaiLamViec     nvarchar(50)         not null
   constraint PK_NHANVIEN primary key (MaNV)
)
go

/*==============================================================*/
/* Table: NHOMSANPHAM                                           */
/*==============================================================*/
create table NHOMSANPHAM (
   MaNhomSP             varchar(15)          not null,
   TenNhomSP            nvarchar(100)        null,
   constraint PK_NHOMSANPHAM primary key (MaNhomSP)
)
go

/*==============================================================*/
/* Table: SANPHAM                                               */
/*==============================================================*/
create table SANPHAM (
   MaSP                 varchar(15)          not null,
   MaVL                 varchar(15)          not null,
   MaNcc                nvarchar(255)        not null,
   MaNhomSP             varchar(15)          not null,
   TenSP                nvarchar(100)        null,
   DonViTinh            nvarchar(50)         null,
   SoLuongTon           int                  null,
   MoTa                 nvarchar(500)        null,
   constraint PK_SANPHAM primary key (MaSP)
)
go

/*==============================================================*/
/* Index: chua_FK                                               */
/*==============================================================*/




create nonclustered index chua_FK on SANPHAM (MaNhomSP ASC)
go

/*==============================================================*/
/* Index: "cung cap_FK"                                         */
/*==============================================================*/




create nonclustered index "cung cap_FK" on SANPHAM (MaNcc ASC)
go

/*==============================================================*/
/* Index: "lam nen_FK"                                          */
/*==============================================================*/




create nonclustered index "lam nen_FK" on SANPHAM (MaVL ASC)
go

/*==============================================================*/
/* Table: VATLIEU                                               */
/*==============================================================*/
create table VATLIEU (
   MaVL                 varchar(15)          not null,
   TenVL                nvarchar(100)        null,
   constraint PK_VATLIEU primary key (MaVL)
)
go

alter table CHITIETHOADON
   add constraint FK_CHITIETH_THUOC_HOADON foreign key (MaHD)
      references HOADON (MaHD)
go

alter table CHITIETHOADON
   add constraint "FK_CHITIETH_XUAT HIEN_SANPHAM" foreign key (MaSP)
      references SANPHAM (MaSP)
go

alter table HOADON
   add constraint "FK_HOADON_DUOC CAP_KHACHHAN" foreign key (MaKhachHang)
      references KHACHHANG (MaKhachHang)
go

alter table HOADON
   add constraint FK_HOADON_LAP_NHANVIEN foreign key (MaNV)
      references NHANVIEN (MaNV)
go

alter table MUCDICHSUDUNG
   add constraint FK_MUCDICHS_CO_SANPHAM foreign key (MaSP)
      references SANPHAM (MaSP)
go

alter table SANPHAM
   add constraint FK_SANPHAM_CHUA_NHOMSANP foreign key (MaNhomSP)
      references NHOMSANPHAM (MaNhomSP)
go

alter table SANPHAM
   add constraint "FK_SANPHAM_CUNG CAP_NHACUNGC" foreign key (MaNcc)
      references NHACUNGCAP (MaNcc)
go

alter table SANPHAM
   add constraint "FK_SANPHAM_LAM NEN_VATLIEU" foreign key (MaVL)
      references VATLIEU (MaVL)
go

CREATE TABLE TAIKHOANDANGNHAP (
    SoTaiKhoan     VARCHAR(20)      NOT NULL,
    TenDangNhap    NVARCHAR(100)    NOT NULL,
    MatKhau        NVARCHAR(100)    NOT NULL,
	Quyen          NVARCHAR(50)     NOT NULL,
    CONSTRAINT PK_TaiKhoanDangNhap PRIMARY KEY (SoTaiKhoan)
);
GO
