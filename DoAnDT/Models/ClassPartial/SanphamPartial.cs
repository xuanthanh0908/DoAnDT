using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnDT.Models
{
    [MetadataTypeAttribute(typeof(SanphamPartial.Metadata))]
    public partial class SanphamPartial
    {
        internal sealed class Metadata
        {
            [AllowHtml]
            public string MoTa { get; set; }
        }
    }

    public partial class DanhsachdangkisanphamNCC
    {
       
    }
    [MetadataTypeAttribute(typeof(HopDongNCC.Metadata))]
    public partial class HopDongNCC
    {
        internal sealed class Metadata
        {
            [Range(1,999999999,ErrorMessage="Mời bạn nhập trong khoảng 1 -> 999999999")]
            public Nullable<int> ThoiHanHD { get; set; }
            [Range(1, 999999, ErrorMessage = "Mời bạn nhập trong khoảng 1 -> 999999")]
            public Nullable<int> SLToiThieu { get; set; }
            [Range(1, 999999, ErrorMessage = "Mời bạn nhập trong khoảng 1 -> 999999")]
            public Nullable<int> SLCungCap { get; set; }
        }
    }
}