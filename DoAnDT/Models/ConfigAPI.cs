namespace DoAnDT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConfigAPI")]
    public partial class ConfigAPI
    {
        public int ID { get; set; }

        [StringLength(5)]
        public string MaNCC { get; set; }

        [Required]
        [StringLength(100)]
        public string LinkRequesrToken { get; set; }

        [Required]
        [StringLength(100)]
        public string LinkAccessToken { get; set; }

        [StringLength(100)]
        public string LinkKiemTraLuongTon { get; set; }

        [StringLength(100)]
        public string LinkXacNhanGiaoHang { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
