using System.Configuration;

namespace UpdateElastic.Utilities
{
    class DataConfigs
    {
        public static string DATABASE_HOST = "dbhost";
        public static string DATABASE_NAME = "dbname";
        public static string DATABASE_USER = "dbuser";
        public static string DATABASE_PASSWORD = "dbpasword";
        public static string ELASTIC_LINK = "eldb";
        public static string INDEX = "index";
        public static string TYPE = "type";
        public static int INTERVAL = 5 * 1000;
        public static int ELASTIC_TIMEOUT_MINUTES = 2;
        public static string DELETE_SQL = "SELECT * FROM ElasticSyncLog WHERE Action = 'delete'";
        public static string UPDATE_SQL = @"SELECT
		 T1.Id as 'chinhanhdoitacid'
		 ,T3.ArticleId as 'articleid'
		 ,T5.Id as 'id'
		 ,T2.Id as 'nhomcndoitacid'
		,T4.Id as 'nhomkhuyenmaiid'
		,T3.Id as 'doitackhuyenmaiid'
		,T5.Ten AS 'tenthuonghieu'
		,T5.Ma AS 'madoitac'
		,T1.Ten AS 'tenchinhanh'
		,T1.QuanHuyenId as 'quanhuyenid'
		,CASE 
					WHEN T1.PheDuyet = 1 THEN '<b>' + T1.Ten + '</b>'
					ELSE T1.Ten
				END  AS 'mtenchinhanh'
		,T1.DiaChi as 'diachi'
		,ISNULL(T3.NDChietKhau,0) as 'chietkhau'
		,T1.TinhId
		,dbo.GetSDTCenterWithTinh(T1.TinhId) as 'tongdaipasgo'
		,dbo.GetLogoURLForDestination_API_V2(T3.Id) as 'logo'
		,T3.NDTieuDe as 'tieude'
		,ISNULL(T4.Ten,'') as 'tennhomkhuyenmai'
		,ISNULL(T3.NDTaiTro,'') as 'taitro'
		,ISNULL(T3.ChuyenMon,'') as 'chuyenmon'
		,ISNULL(T3.DanhGia,0) AS 'danhgia'
		,ISNULL(T3.NDChatLuongDichVu,0) AS 'chatluong'
		,ISNULL(T3.NDGiaTrungBinh, '') as 'giatrungbinh'
		,T1.LoaiHopDong as 'loaihopdong'
		,T1.ToaDo.Lat as 'lat'
		,T1.ToaDo.Long as 'lon'
		,T9.AnhId as 'anhid'
		,T1.TrangThai as 'trangthai'
		,CAST(T9.[Version] as int) as 'version'
		,T4.[Order] as 'order'
		,T7.Action as 'action'
        ,T3.HieuLuc as 'hieuluc'
		,T1.PheDuyet as 'pheduyet'
		,T5.[Priority] as 'priority'
		,ISNULL('['+STUFF(
			(SELECT DISTINCT CONCAT(',',FilterCategoryItemId)
			FROM FilterCategoryItem a
			INNER JOIN CNDoiTacFilterCategoryItem b ON a.Id = b.FilterCategoryItemId
			WHERE T1.Id = b.CNDoiTacId
			FOR XML PATH('')),1,1,'')+']','[]') as 'filteritemids'
	FROM ChiNhanhDoiTac T1 
	INNER JOIN NhomCNDoiTac T2 ON T1.Id = T2.ChiNhanhDoiTacId
	INNER JOIN DoiTacKhuyenMai T3 ON T2.DoiTacKhuyenMaiId = T3.Id
	INNER JOIN NhomKhuyenMai T4 ON T2.NhomKhuyenMaiId = T4.Id
	INNER JOIN DoiTac T5 ON T5.Id = T1.DoiTacId
	INNER JOIN NhomKhuyenMai T6 ON T6.Id = T2.NhomKhuyenMaiId
	INNER JOIN ElasticSyncLog T7 ON T7.DataId = T3.Id
	LEFT JOIN AnhDoiTacKhuyenMai T9 ON T3.Id = T9.DoiTacKhuyenMaiId AND T9.Width = T9.Height
	WHERE T7.Action != 'delete'";
        public static string getValue(string key)
        {
            return ConfigurationSettings.AppSettings[key] ?? "No key was found";
        }
    }
}
