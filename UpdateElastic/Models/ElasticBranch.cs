using Newtonsoft.Json;
using UpdateElastic.Utilities;

namespace UpdateElastic.Models
{
    public class ElasticBranch
    {

        public string id { get; private set; }
        public ElasticBranch setId(string value)
        {
           
            id = value;
            return this;
        }
        public int[] filterids { get; private set; }
        public ElasticBranch setFilterIds(string value)
        {
            
            filterids = JsonConvert.DeserializeObject<int[]>(value);
            return this;
        }
        public int chietkhau { get; private set; }
        public ElasticBranch setChietKhau(int value)
        {
            chietkhau = value;
            return this;
        }
        public int order { get; private set; }
        public ElasticBranch setOrder(int value)
        {
            
            order = value;
            return this;
        }
        public string chuyenmon { get; private set; }
        public ElasticBranch setChuyenMon(string value)
        {
           
            chuyenmon = value;
            return this;
        }
        public int tinhid { get; private set; }
        public ElasticBranch setTinhId(int value)
        {
           
            tinhid = value;
            return this;
        }
        public bool hieuluc { get; private set; }
        public ElasticBranch setHieuLuc(bool value)
        {
            
            hieuluc = value;
            return this;
        }
        public int nhomkhuyenmaiid { get; private set; }
        public ElasticBranch setNhomKhuyenMaiId(int value)
        {
            
            nhomkhuyenmaiid = value;
            return this;
        }
        public string logo { get; private set; }
        public ElasticBranch setLogo(string value)
        {
            
            logo = value;
            return this;
        }
        public bool pheduyet { get; private set; }
        public ElasticBranch setPheDuyet(bool value)
        {
            
            pheduyet = value;
            return this;
        }
        public string tenchinhanh { get; private set; }
        public ElasticBranch setTenChiNhanh(string value)
        {
            
            tenchinhanh = value;
            return this;
        }
        public string giatrungbinh { get; private set; }
        public ElasticBranch setGiaTrungBinh(string value)
        {
            
            giatrungbinh = value;
            return this;
        }
        public string tongdaipasgo { get; private set; }
        public ElasticBranch setTongDaiPasgo(string value)
        {
            
            tongdaipasgo = value;
            return this;
        }
        public int anhid { get; private set; }
        public ElasticBranch setAnhId(int value)
        {
            
            anhid = value;
            return this;
        }
        public int version { get; private set; }
        public ElasticBranch setVersion(int value)
        {
           
            version = value;
            return this;
        }
        public string nhomcndoitacid { get; private set; }
        public ElasticBranch setNhomCNDoiTacId(string value)
        {
           
            nhomcndoitacid = value;
            return this;
        }
        public string mtenchinhanh { get; private set; }
        public ElasticBranch setMTenChiNhanh(string value)
        {
           
            mtenchinhanh = value;
            return this;
        }
        public Location toado { get; private set; }
        public ElasticBranch setToaDo(double lat, double lon)
        {
            
            toado = new Location(lat, lon);
            return this;
        }
        public string tenthuonghieu { get; private set; }
        public ElasticBranch setTenThuongHieu(string value)
        {
           
            tenthuonghieu = value;
            return this;
        }
        public string tieude { get; private set; }
        public ElasticBranch setTieuDe(string value)
        {
            
            tieude = value;
            return this;
        }
        public int quanhuyenid { get; private set; }
        public ElasticBranch setQuanHuyenId(int value)
        {
            
            quanhuyenid = value;
            return this;
        }
        public int trangthai { get; private set; }
        public ElasticBranch setTrangThai(int value)
        {
            
            trangthai = value;
            return this;
        }
        public int priority { get; private set; }
        public ElasticBranch setPriority(int value)
        {
           
            priority = value;
            return this;
        }
        public string chinhanhdoitacid { get; private set; }
        public ElasticBranch setChiNhanhDoiTacId(string value)
        {
            
            chinhanhdoitacid = value;
            return this;
        }
        public string diachi { get; private set; }
        public ElasticBranch setDiaChi(string value)
        {
            
            diachi = value;
            return this;
        }
        public int loaihopdong { get; private set; }
        public ElasticBranch setLoaiHopDong(int value)
        {
            
            loaihopdong = value;
            return this;
        }
        public string doitackhuyenmaiid { get; private set; }
        public ElasticBranch setDoiTacKhuyenMaiId(string value)
        {
            
            doitackhuyenmaiid = value;
            return this;
        }
        public double chatluong { get; private set; }
        public ElasticBranch setChatLuong(double value)
        {
           
            chatluong = value;
            return this;
        }
        public double danhgia { get; private set; }
        public ElasticBranch setDanhGia(double value)
        {
           
            danhgia = value;
            return this;
        }
        public int articleid { get; private set; }
        public ElasticBranch setArticleId(int value)
        {

            articleid = value;
            return this;
        }
        public string madoitac { get; private set; }
        public ElasticBranch setMaDoiTac(string value)
        {
 
            madoitac = value;
            return this;
        }
        public string taitro { get; private set; }
        public ElasticBranch setTaiTro(string value)
        {
     
            taitro = value;
            return this;
        }
        public string tennhomkhuyenmai { get; private set; }
        public ElasticBranch setTenNhomKhuyenMai(string value)
        {
 
            tennhomkhuyenmai = value;
            return this;
        }

    }
    public class Location
    {
        public double lat { get; private set; }
        public double lon { get; private set; }
        public Location(double lat, double lon)
        {
            this.lat = lat;
            this.lon = lon;
        }
    }
}
