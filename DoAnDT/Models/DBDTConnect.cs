using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoAnDT.Models
{
    public partial class DBDTConnect : DbContext
    {
        public DBDTConnect()
            : base("name=DBDTConnect")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<ConfigAPI> ConfigAPIs { get; set; }
        public virtual DbSet<DanhsachdangkisanphamNCC> DanhsachdangkisanphamNCCs { get; set; }
        public virtual DbSet<DonHangKH> DonHangKHs { get; set; }
        public virtual DbSet<GiaoDien> GiaoDiens { get; set; }
        public virtual DbSet<HangSanXuat> HangSanXuats { get; set; }
        public virtual DbSet<HopDongNCC> HopDongNCCs { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<Oauth> Oauths { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Sanphamcanmua> Sanphamcanmuas { get; set; }
        public virtual DbSet<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ThongSoKyThuat> ThongSoKyThuats { get; set; }
        public virtual DbSet<Trackingaction> Trackingactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.MaNV)
                .IsFixedLength();

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.CMND)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.BinhLuans)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.MaKH)
                .WillCascadeOnDelete();

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.DonHangKHs)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.MaKH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.NhaCungCaps)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.Net_user)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BinhLuan>()
                .Property(e => e.MaSP)
                .IsFixedLength();

            modelBuilder.Entity<BinhLuan>()
                .Property(e => e.DaTraLoi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.MaDH)
                .IsFixedLength();

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.MaSP)
                .IsFixedLength();

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ConfigAPI>()
                .Property(e => e.MaNCC)
                .IsFixedLength();

            modelBuilder.Entity<ConfigAPI>()
                .Property(e => e.LinkRequesrToken)
                .IsUnicode(false);

            modelBuilder.Entity<ConfigAPI>()
                .Property(e => e.LinkAccessToken)
                .IsUnicode(false);

            modelBuilder.Entity<ConfigAPI>()
                .Property(e => e.LinkKiemTraLuongTon)
                .IsUnicode(false);

            modelBuilder.Entity<ConfigAPI>()
                .Property(e => e.LinkXacNhanGiaoHang)
                .IsUnicode(false);

            modelBuilder.Entity<DanhsachdangkisanphamNCC>()
                .Property(e => e.MaNCC)
                .IsFixedLength();

            modelBuilder.Entity<DanhsachdangkisanphamNCC>()
                .Property(e => e.Ghichu)
                .IsUnicode(false);

            modelBuilder.Entity<DonHangKH>()
                .Property(e => e.MaDH)
                .IsFixedLength();

            modelBuilder.Entity<DonHangKH>()
                .Property(e => e.PhiVanChuyen)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHangKH>()
                .Property(e => e.Ghichu)
                .IsUnicode(false);

            modelBuilder.Entity<DonHangKH>()
                .Property(e => e.Dienthoai)
                .IsFixedLength();

            modelBuilder.Entity<HangSanXuat>()
                .Property(e => e.HangSX)
                .IsFixedLength();

            modelBuilder.Entity<HangSanXuat>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.HangSanXuat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HopDongNCC>()
                .Property(e => e.MaHD)
                .IsFixedLength();

            modelBuilder.Entity<HopDongNCC>()
                .Property(e => e.MaNCC)
                .IsFixedLength();

            modelBuilder.Entity<HopDongNCC>()
                .Property(e => e.MaSP)
                .IsFixedLength();

            modelBuilder.Entity<HopDongNCC>()
                .Property(e => e.DonGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<KhuyenMai>()
                .Property(e => e.MaKM)
                .IsFixedLength();

            modelBuilder.Entity<KhuyenMai>()
                .Property(e => e.AnhCT)
                .IsUnicode(false);

            modelBuilder.Entity<Link>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Link>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiSP>()
                .Property(e => e.MaLoai)
                .IsFixedLength();

            modelBuilder.Entity<LoaiSP>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.LoaiSP1)
                .HasForeignKey(e => e.LoaiSP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.MaNCC)
                .IsFixedLength();

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.SDT_NCC)
                .IsFixedLength();

            modelBuilder.Entity<NhaCungCap>()
                .Property(e => e.Website)
                .IsUnicode(false);

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.HopDongNCCs)
                .WithOptional(e => e.NhaCungCap)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Oauth>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Oauth>()
                .Property(e => e.Consumer_key)
                .IsUnicode(false);

            modelBuilder.Entity<Oauth>()
                .Property(e => e.Callback)
                .IsUnicode(false);

            modelBuilder.Entity<Oauth>()
                .Property(e => e.Request_token)
                .IsUnicode(false);

            modelBuilder.Entity<Oauth>()
                .Property(e => e.Verifier_token)
                .IsUnicode(false);

            modelBuilder.Entity<Oauth>()
                .Property(e => e.MaNCC)
                .IsFixedLength();

            modelBuilder.Entity<Oauth>()
                .Property(e => e.Token)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaSP)
                .IsFixedLength();

            modelBuilder.Entity<SanPham>()
                .Property(e => e.LoaiSP)
                .IsFixedLength();

            modelBuilder.Entity<SanPham>()
                .Property(e => e.HangSX)
                .IsFixedLength();

            modelBuilder.Entity<SanPham>()
                .Property(e => e.GiaGoc)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.GiaTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.BinhLuans)
                .WithOptional(e => e.SanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.HopDongNCCs)
                .WithOptional(e => e.SanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.Sanphamcanmuas)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sanphamcanmua>()
                .Property(e => e.MaSP)
                .IsFixedLength();

            modelBuilder.Entity<Sanphamcanmua>()
                .HasMany(e => e.DanhsachdangkisanphamNCCs)
                .WithOptional(e => e.Sanphamcanmua)
                .HasForeignKey(e => e.MaSPCanMua)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPhamKhuyenMai>()
                .Property(e => e.MaKM)
                .IsFixedLength();

            modelBuilder.Entity<SanPhamKhuyenMai>()
                .Property(e => e.MaSP)
                .IsFixedLength();

            modelBuilder.Entity<ThongSoKyThuat>()
                .Property(e => e.MaSP)
                .IsFixedLength();

            modelBuilder.Entity<Trackingaction>()
                .Property(e => e.MaSP)
                .IsFixedLength();
        }
    }
}
