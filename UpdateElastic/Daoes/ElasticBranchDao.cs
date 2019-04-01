

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UpdateElastic.Models;
using UpdateElastic.Utilities;

namespace UpdateElastic.Daoes
{
    public class ElasticBranchDao : BaseDao<ElasticBranch>
    {
        private static string updateSql;
        private static string deleteSql;
        private ElasticHelper ehelper;
        private DatabaseHelper helper;
        private DataLogger logger;
        private DatabaseLogger dbLogger;
        public ElasticBranchDao() : this(null,null,null,null)
        {
            
        }
        public ElasticBranchDao(ElasticHelper ehelper,DatabaseHelper helper,DatabaseLogger dbLogger,DataLogger logger)
        {
            if(ehelper == null)
            {
                ehelper = ElasticHelper.getDefaultInstance();
            }
            if(helper == null)
            {
                helper = DatabaseHelper.getDefaultInstance();
            }
            if(dbLogger == null)
            {
                dbLogger = DatabaseLogger.getDefaultInstance();
            }
            if(logger == null)
            {
                logger = DataLogger.getInstance();
            }
            this.ehelper = ehelper;
            this.helper = helper;
            this.dbLogger = dbLogger;
            this.logger = logger;
        }
        public DatabaseHelper getDatabaseHelper()
        {
            return helper;
        }
        public override bool delete(string id)
        {
            return ehelper.delete(id).Success;
        }

        public override ElasticBranch get(string id)
        {
            StringResponse response = ehelper.get(id);
            if (!response.Success) throw Exception("Lấy dữ liệu từ id = <" + id + "> không thành công,vui lòng thử lại!");
            var result = JObject.Parse(response.Body);
            if (!JsonHelper.getBoolValue(result, "found",false)) throw Exception("Không tìm được bản ghi nào có id = <" + id + ">,vui lòng thử lại!");
            return result["_source"].ToObject<ElasticBranch>();                     
        }

        private Exception Exception(string v)
        {
            logger.writeToFile(DateTime.Now.ToString() + " : " + v);
            throw new NotImplementedException();
        }

        public override bool insert(string id, ElasticBranch value)
        {
            return ehelper.updateOrCreate(id, value).Success;
        }
        public bool insertOrUpdate(string id, DbDataReader reader)
        {
            return insert(id, new ElasticBranch()
                        .setAnhId(helper.getIntValue(reader, "anhid", 0))
                        .setArticleId(helper.getIntValue(reader, "articleid", 0))
                        .setChatLuong(helper.getDoubleValue(reader, "chatluong", 0))
                        .setChietKhau(helper.getIntValue(reader, "chietkhau", 0))
                        .setChiNhanhDoiTacId(helper.getStringValue(reader, "chinhanhdoitacid", ""))
                        .setChuyenMon(helper.getStringValue(reader, "chuyenmon", ""))
                        .setDanhGia(helper.getDoubleValue(reader, "danhgia", 0))
                        .setDiaChi(helper.getStringValue(reader, "diachi", ""))
                        .setDoiTacKhuyenMaiId(helper.getStringValue(reader, "doitackhuyenmaiid", ""))
                        .setFilterIds(helper.getStringValue(reader, "filteritemids", "[]"))
                        .setGiaTrungBinh(helper.getStringValue(reader, "giatrungbinh", ""))
                        .setHieuLuc(helper.getBoolValue(reader, "hieuluc", false))
                        .setId(helper.getStringValue(reader, "id", ""))
                        .setLoaiHopDong(helper.getIntValue(reader, "loaihopdong", 0))
                        .setLogo(helper.getStringValue(reader, "logo", ""))
                        .setMaDoiTac(helper.getStringValue(reader, "madoitac", ""))
                        .setMTenChiNhanh(helper.getStringValue(reader, "mtenchinhanh", ""))
                        .setNhomCNDoiTacId(helper.getStringValue(reader, "nhomcndoitacid", ""))
                        .setNhomKhuyenMaiId(helper.getIntValue(reader, "nhomkhuyenmaiid", 3))
                        .setOrder(helper.getIntValue(reader, "order", -1))
                        .setPheDuyet(helper.getBoolValue(reader, "pheduyet", false))
                        .setPriority(helper.getIntValue(reader, "priority", 0))
                        .setQuanHuyenId(helper.getIntValue(reader, "quanhuyenid", 0))
                        .setTaiTro(helper.getStringValue(reader, "taitro", ""))
                        .setTenChiNhanh(helper.getStringValue(reader, "tenchinhanh", ""))
                        .setTenNhomKhuyenMai(helper.getStringValue(reader, "tennhomkhuyenmai", ""))
                        .setTenThuongHieu(helper.getStringValue(reader, "tenthuonghieu", ""))
                        .setTieuDe(helper.getStringValue(reader, "tieude", ""))
                        .setTinhId(helper.getIntValue(reader, "tinhid", 1))
                        .setToaDo(helper.getDoubleValue(reader, "lat", 0), helper.getDoubleValue(reader, "lon", 0))
                        .setTongDaiPasgo(helper.getStringValue(reader, "tongdaipasgo", ""))
                        .setTrangThai(helper.getIntValue(reader, "trangthai", 0))
                        .setVersion(helper.getIntValue(reader, "version", 0)));
        }
        public override bool update(string id, ElasticBranch value)
        {
            return ehelper.updateOrCreate(id,value).Success;
        }
        public object execute()
        {
            int sum = dbLogger.count("1", "ElasticSyncLog",null);
            logger.writeToFile("Have " + sum + " to edit");

            //Xoa du lieu
            //insert va update dữ liệu
            var deleteObj = (object[])helper.execute(DataConfigs.DELETE_SQL, onExecute, "delete");
            var updateObj = (object[])helper.execute(DataConfigs.UPDATE_SQL, onExecute, "insert or update");
            int successNum = Convert.ToInt32(deleteObj[0]) + Convert.ToInt32(updateObj[0]);
            List<ElasticUpdateLog> logs = new List<ElasticUpdateLog>();
            logs.AddRange((List<ElasticUpdateLog>)deleteObj[1]);
            logs.AddRange((List<ElasticUpdateLog>)updateObj[1]);
            for (int i = 0;i<logs.Count;i++)
            {
                dbLogger.logTo("ElasticUpdateLog", logs[i]);
                if (logs[i].State.Equals("Success"))
                {
                    helper.execute("DELETE FROM ElasticSyncLog WHERE ArticleId = " + logs[i].ArticleId);
                }
            }
            logger.writeToFile("Success: " + successNum + "");
            ElasticUpdateServiceLog log = new ElasticUpdateServiceLog();
            log.SoLuong = sum;
            log.SoLuongThanhCong = successNum;
            log.ThoiGian = DateTime.Now;
            log.TrangThai = "running";
            log.GhiChu = "";
            return log;
        }
        /*
         * 
         * 
         **/
        private object onExecute(DbDataReader reader,string action)
        {

            List<ElasticUpdateLog> logs = new List<ElasticUpdateLog>();
            if (reader.HasRows)
            {
                int successRowNum = 0;
                while (reader.Read())
                {
                    ElasticUpdateLog log = new ElasticUpdateLog();
                    log.ArticleId = helper.getIntValue(reader, "articleid", 0);
                    log.Action = helper.getStringValue(reader, "action", "");
                    log.Time = DateTime.Now;
                    logger.writeToFile(JsonConvert.SerializeObject(log));
                    if (action.Equals("delete"))
                    {
                        var src = JsonConvert.DeserializeObject<ElasticBranch>(JObject.Parse(ehelper.get(log.ArticleId.ToString()).Body)["_source"].ToString());
                        logger.writeToFile("delete: => " + src);
                        bool isSuccess = delete(log.ArticleId.ToString());
                        successRowNum += isSuccess ? 1:0;
                        log.DataId = helper.getStringValue(reader, "dataid", "");
                        log.State = isSuccess ? "Success" : "Failure";
                        ehelper.updateOrCreate("pasgosearchlog", "log", log.DataId, new
                        {
                            infor = log,
                            data = src
                        });
                    }
                    else
                    {
                        bool isSuccess = insertOrUpdate(log.ArticleId.ToString(), reader);
                        successRowNum += isSuccess ? 1:0;
                        log.DataId = helper.getStringValue(reader, "doitackhuyenmaiid", "");
                        log.State = isSuccess ? "Success" : "Failure";
                        var src = JObject.Parse(ehelper.get(log.ArticleId.ToString()).Body)["_source"];
                        ehelper.updateOrCreate("pasgosearchlog", "log", log.DataId, new
                        {
                            infor = log,
                            data = src.ToString()
                        });
                        logger.writeToFile("update: => " + src);
                    }
                    logs.Add(log);
                    
                }
                return new object[]
                {
                    successRowNum,logs
                };
            }
            logger.writeToFile("no object to "+action);
            return new object[]
                {
                    0,logs
                };
        }
    }
}
