Create Database QL_ThamMyVien_V1
go
use QL_ThamMyVien_V1


CREATE TABLE ChiNhanh (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenChiNhanh NVARCHAR(255),
    DiaChi NVARCHAR(255),
    SDT NVARCHAR(20),
    Email NVARCHAR(255)
);
CREATE TABLE LoaiNhanVien (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiNhanVien NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE TrangThaiNhanVien (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenTrangThai NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE NhanVien (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CCCD NVARCHAR(20),
    HoTen NVARCHAR(255),
    NgaySinh NVARCHAR(255),
    GioiTinh NVARCHAR(20),
    QueQuan NVARCHAR(255),
    SDT NVARCHAR(20),
    Email NVARCHAR(255),
    NganHang NVARCHAR(20),
    DiaChiHienTai NVARCHAR(255),
    BHYT NVARCHAR(20),
    BHTN NVARCHAR(20),
    Luong INT,
    IDLoaiNhanVien INT,
    IDTrangThaiNhanVien INT,
    SoLanDeCu INT,
    IDChiNhanh INT,
    Khac NVARCHAR(MAX),
    FOREIGN KEY (IDLoaiNhanVien) REFERENCES LoaiNhanVien(ID),
    FOREIGN KEY (IDTrangThaiNhanVien) REFERENCES TrangThaiNhanVien(ID),
    FOREIGN KEY (IDChiNhanh) REFERENCES ChiNhanh(ID)
);
CREATE TABLE LoaiTaiKhoan (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiTaiKhoan NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE TaiKhoanNguoiDung (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TaiKhoan NVARCHAR(255),
    MatKhau NVARCHAR(255),
    Email NVARCHAR(255),
    IDTrangThaiNhanVien INT,
    IDNhanVien INT,
    IDLoaiTaiKhoan INT,
    FOREIGN KEY (IDTrangThaiNhanVien) REFERENCES TrangThaiNhanVien(ID),
    FOREIGN KEY (IDNhanVien) REFERENCES NhanVien(ID),
    FOREIGN KEY (IDLoaiTaiKhoan) REFERENCES LoaiTaiKhoan(ID)
);
CREATE TABLE LoaiThaoTac (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai NVARCHAR(255)
);
CREATE TABLE ThaoTac (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ThoiGian NVARCHAR(20),
    NoiDungThayDoi NVARCHAR(MAX),
    IDLoaiThaoTac INT,
	IDNguoiThaoTac INT,
    FOREIGN KEY (IDLoaiThaoTac) REFERENCES LoaiThaoTac(ID),
	FOREIGN KEY (IDNguoiThaoTac) REFERENCES TaiKhoanNguoiDung(ID)
);
CREATE TABLE LoaiKhachHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiKhachHang NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE TrangThaiKhachHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenTrangThai NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE KhachHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(255),
    NgaySinh NVARCHAR(20),
    SoDienThoai NVARCHAR(255),
    CCCD NVARCHAR(255),
    LinkFB NVARCHAR(255),
    LinkZalo NVARCHAR(255),
    Email NVARCHAR(255),
    GioiTinh NVARCHAR(20),
    DiaChi NVARCHAR(255),
    DichVuQuanTam NVARCHAR(MAX),
    IDTrangThai INT,
    IDLoaiKhachHang INT,
    Khac NVARCHAR(MAX),
    FOREIGN KEY (IDTrangThai) REFERENCES TrangThaiKhachHang(ID),
    FOREIGN KEY (IDLoaiKhachHang) REFERENCES LoaiKhachHang(ID)
);
CREATE TABLE LoaiDichVu (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiDichVu NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE KhuyenMai (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenKhuyenMai NVARCHAR(255),
    GiaTriKhuyenMai NVARCHAR(255),
    ThoiGianKhuyenMai NVARCHAR(20),
    Khac NVARCHAR(MAX)
);
CREATE TABLE DichVu (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenDichVu NVARCHAR(255),
    IDLoaiDichVu INT,
    ChiTietDichVu NVARCHAR(MAX),
    Gia INT,
    Khac NVARCHAR(MAX),
    SoNgayTheoDoi INT,
    IDKhuyenMaiApDung INT,
    FOREIGN KEY (IDLoaiDichVu) REFERENCES LoaiDichVu(ID),
    FOREIGN KEY (IDKhuyenMaiApDung) REFERENCES KhuyenMai(ID)
);
CREATE TABLE LoaiSanPham (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiSanPham NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE SanPham (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenSanPham NVARCHAR(255),
    GioiThieu NVARCHAR(MAX),
    CongDung NVARCHAR(MAX),
    XuatXu NVARCHAR(255),
    GiaBan INT,
    GiaNhap INT,
    IDKhuyenMai INT,
	IDLoaiSanPham INT,
    Khac NVARCHAR(MAX),
    FOREIGN KEY (IDKhuyenMai) REFERENCES KhuyenMai(ID),
	FOREIGN KEY (IDLoaiSanPham) REFERENCES LoaiSanPham(ID)
);
CREATE TABLE SanPham_ChiNhanh (
    IDSanPham INT,
    IDChiNhanh INT,
    SoLuong INT,
    PRIMARY KEY (IDSanPham, IDChiNhanh),
    FOREIGN KEY (IDSanPham) REFERENCES SanPham(ID),
    FOREIGN KEY (IDChiNhanh) REFERENCES ChiNhanh(ID)
);
CREATE TABLE LoaiPhieuBaoHanh (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiPhieuBaoHanh NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE PhieuBaoHanh (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NgayBaoHanh NVARCHAR(20),
    IDNhanVienBaoHanh INT,
    IDNhanVienTheoDoi INT,
    NgayHetBaoHanh NVARCHAR(20),
    IDKhachHang INT,
    IDDichVuBaoHanh INT,
    NoiDungBaoHanh NVARCHAR(MAX),
    IDChiNhanhBaoHanh INT,
	IDLoaiPhieuBaoHanh INT,
    Khac NVARCHAR(MAX),
    FOREIGN KEY (IDNhanVienBaoHanh) REFERENCES NhanVien(ID),
    FOREIGN KEY (IDNhanVienTheoDoi) REFERENCES NhanVien(ID),
    FOREIGN KEY (IDKhachHang) REFERENCES KhachHang(ID),
    FOREIGN KEY (IDDichVuBaoHanh) REFERENCES DichVu(ID),
	FOREIGN KEY (IDLoaiPhieuBaoHanh) REFERENCES LoaiPhieuBaoHanh(ID),
    FOREIGN KEY (IDChiNhanhBaoHanh) REFERENCES ChiNhanh(ID)
);
CREATE TABLE TrangThaiPhieuTheoDoi (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenTrangThai NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE PhieuTheoDoiKhachHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDKhachHang INT,
    IDNhanVien INT,
    IDTrangThaiTheoDoi INT,
    IDChiNhanhTheoDoi INT,
    FOREIGN KEY (IDKhachHang) REFERENCES KhachHang(ID),
    FOREIGN KEY (IDNhanVien) REFERENCES NhanVien(ID),
    FOREIGN KEY (IDTrangThaiTheoDoi) REFERENCES TrangThaiPhieuTheoDoi(ID),
    FOREIGN KEY (IDChiNhanhTheoDoi) REFERENCES ChiNhanh(ID)
);
CREATE TABLE NoiDungPhieuTheoDoi (
    STT INT PRIMARY KEY,
    IDPhieuTheoDoi INT,
    NoiDung NVARCHAR(MAX),
    NgayTheoDoi NVARCHAR(20),
    FOREIGN KEY (IDPhieuTheoDoi) REFERENCES PhieuTheoDoiKhachHang(ID)
);
CREATE TABLE LoaiLichHen (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiLichHen NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE TrangThaiLichHen (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenTrangThai NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE LichHen (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDNhanVienTaoLich INT,
    IDNhanVienTiepNhan INT,
    NgayTaoLich NVARCHAR(20),
    NgayThucHien NVARCHAR(20),
    IDKhachHang INT,
    IDDichVuThucHien INT,
    IDChiNhanh INT,
	IDTrangThaiLichHen INT,
	IDLoaiLichHen INT,
    Khac NVARCHAR(255),
    FOREIGN KEY (IDNhanVienTaoLich) REFERENCES NhanVien(ID),
    FOREIGN KEY (IDNhanVienTiepNhan) REFERENCES NhanVien(ID),
    FOREIGN KEY (IDKhachHang) REFERENCES KhachHang(ID),
    FOREIGN KEY (IDDichVuThucHien) REFERENCES DichVu(ID),
    FOREIGN KEY (IDChiNhanh) REFERENCES ChiNhanh(ID),
	FOREIGN KEY (IDLoaiLichHen) REFERENCES LoaiLichHen(ID),
	FOREIGN KEY (IDTrangThaiLichHen) REFERENCES TrangThaiLichHen(ID)
);
CREATE TABLE GioHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDTaiKhoan INT,
    FOREIGN KEY (IDTaiKhoan) REFERENCES TaiKhoanNguoiDung(ID)
);
CREATE TABLE NoiDungGioHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDGioHang INT,
    IDSanPham INT,
    IDDichVu INT,
    SoLuong INT,
    GiaTien INT,
    FOREIGN KEY (IDGioHang) REFERENCES GioHang(ID),
    FOREIGN KEY (IDSanPham) REFERENCES SanPham(ID),
    FOREIGN KEY (IDDichVu) REFERENCES DichVu(ID)
);
CREATE TABLE LoaiBaiDang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiBaiDang NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE BaiDang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TieuDe NVARCHAR(255),
    NgayDang NVARCHAR(20),
    LuotXem INT,
	IDLoaiBaiDang INT,
	IDNguoiDang INT,
	FOREIGN KEY (IDLoaiBaiDang) REFERENCES LoaiBaiDang(ID),
	FOREIGN KEY (IDNguoiDang) REFERENCES TaiKhoanNguoiDung(ID)
);
CREATE TABLE NoiDungBaiDang (
    STT INT,
    IDBaiDang INT,
    NoiDung NVARCHAR(MAX),
    TieuDe NVARCHAR(255),
    Link NVARCHAR(255),
	PRIMARY KEY (STT, IDBaiDang),
    FOREIGN KEY (IDBaiDang) REFERENCES BaiDang(ID)
);
CREATE TABLE HinhAnhBaiDang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDSTTNoiDungBaiDang INT,
	IDBaiDang INT,
    HinhAnh NVARCHAR(MAX),
    FOREIGN KEY (IDSTTNoiDungBaiDang,IDBaiDang) REFERENCES NoiDungBaiDang(STT,IDBaiDang)
);
CREATE TABLE HinhAnhSanPham (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDSanPham INT,
    HinhAnh NVARCHAR(MAX),
    FOREIGN KEY (IDSanPham) REFERENCES SanPham(ID)
);
CREATE TABLE HinhAnhKhachHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDKhachHang INT,
    HinhAnh NVARCHAR(MAX),
    FOREIGN KEY (IDKhachHang) REFERENCES KhachHang(ID)
);
CREATE TABLE TrangThaiDonDatHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenTrangThai NVARCHAR(255)
);
CREATE TABLE DonDatHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDTrangThai INT,
    NgayDatHang NVARCHAR(20),
    IDChiNhanh INT,
    IDKhachHang INT,
    TongTien INT,
    DiaChiGiaoHang NVARCHAR(255),
    SDTNguoiNhan NVARCHAR(20),
    Khac NVARCHAR(MAX),
    FOREIGN KEY (IDTrangThai) REFERENCES TrangThaiDonDatHang(ID),
    FOREIGN KEY (IDChiNhanh) REFERENCES ChiNhanh(ID),
    FOREIGN KEY (IDKhachHang) REFERENCES KhachHang(ID)
);
CREATE TABLE NoiDungDonDatHang (
    STT INT PRIMARY KEY,
    IDDonDatHang INT,
    IDSanPham INT,
    SoLuong INT,
    IDKhuyenMai INT,
    ThanhTien INT,
    FOREIGN KEY (IDDonDatHang) REFERENCES DonDatHang(ID),
    FOREIGN KEY (IDSanPham) REFERENCES SanPham(ID),
    FOREIGN KEY (IDKhuyenMai) REFERENCES KhuyenMai(ID)
);
CREATE TABLE LoaiHoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiHoaDon NVARCHAR(255),
    Khac NVARCHAR(MAX)
);
CREATE TABLE HoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDLoaiHoaDon INT,
    NgayTaoHoaDon NVARCHAR(20),
    IDNhanVienTaoHoaDon INT,
    IDKhachHang INT,
    HinhThucThanhToan NVARCHAR(255),
    TongTien INT,
    IDChiNhanh INT,
    Khac NVARCHAR(MAX),
    FOREIGN KEY (IDLoaiHoaDon) REFERENCES LoaiHoaDon(ID),
    FOREIGN KEY (IDNhanVienTaoHoaDon) REFERENCES NhanVien(ID),
    FOREIGN KEY (IDKhachHang) REFERENCES KhachHang(ID),
    FOREIGN KEY (IDChiNhanh) REFERENCES ChiNhanh(ID)
);
CREATE TABLE NoiDungHoaDon (
    STT INT PRIMARY KEY,
    IDHoaDon INT,
    NoiDung NVARCHAR(255),
    SoLuong INT,
    GiamGia INT,
    ThanhTien INT,
    FOREIGN KEY (IDHoaDon) REFERENCES HoaDon(ID),
    FOREIGN KEY (GiamGia) REFERENCES KhuyenMai(ID)
);
CREATE TABLE TrangThaiThanhToan (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenTrangThai NVARCHAR(255)
);
CREATE TABLE ThanhToanOnline (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDHoaDon INT,
    NoiDungThanhToan NVARCHAR(255),
    IDTrangThaiThanhToan INT,
    NgayThanhToan NVARCHAR(20),
    SoTienThanhToan INT,
    IDChiNhanh INT,
    FOREIGN KEY (IDHoaDon) REFERENCES HoaDon(ID),
    FOREIGN KEY (IDTrangThaiThanhToan) REFERENCES TrangThaiThanhToan(ID),
    FOREIGN KEY (IDChiNhanh) REFERENCES ChiNhanh(ID)
);
CREATE TABLE LichSuKhachHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NgaySuDung NVARCHAR(20),
    IDDichVuSuDung INT,
    TongTien INT,
    IDBaoHanh INT,
    IDHoaDon INT,
    IDNhanVienTuVan INT,
    IDNhanVienChamSoc INT,
    IDNhanVienKyThuat INT,
    IDChiNhanhThucHien INT,
    Khac NVARCHAR(MAX),
    FOREIGN KEY (IDDichVuSuDung) REFERENCES DichVu(ID),
    FOREIGN KEY (IDBaoHanh) REFERENCES PhieuBaoHanh(ID),
    FOREIGN KEY (IDHoaDon) REFERENCES HoaDon(ID),
    FOREIGN KEY (IDNhanVienTuVan) REFERENCES NhanVien(ID),
    FOREIGN KEY (IDNhanVienChamSoc) REFERENCES NhanVien(ID),
    FOREIGN KEY (IDNhanVienKyThuat) REFERENCES NhanVien(ID),
    FOREIGN KEY (IDChiNhanhThucHien) REFERENCES ChiNhanh(ID)
);

