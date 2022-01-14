namespace DoAnDT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sanphamcanmua")]
    public partial class Sanphamcanmua
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sanphamcanmua()
        {
            DanhsachdangkisanphamNCCs = new HashSet<DanhsachdangkisanphamNCC>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(5)]
        public string MaSP { get; set; }

        public int? Soluong { get; set; }

        public DateTime? Ngayketthuc { get; set; }

        public DateTime? Ngaydang { get; set; }

        [Column(TypeName = "ntext")]
        public string Mota { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhsachdangkisanphamNCC> DanhsachdangkisanphamNCCs { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
