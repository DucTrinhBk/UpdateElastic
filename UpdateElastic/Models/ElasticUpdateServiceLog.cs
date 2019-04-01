using System;

namespace UpdateElastic.Models
{
    public class ElasticUpdateServiceLog
    {
       public string SessionId { get; set; }
       public string TrangThai { get; set; }
       public DateTime ThoiGian { get; set; }
       public int SoLuong { get; set; }
       public int SoLuongThanhCong { get; set; }
       public string GhiChu { get; set; }
    }
}
