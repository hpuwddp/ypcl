using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_model.Dto;
using xz_ypcl_unit.DataBase;

namespace xz_ypcl_dal
{
   public class HyDal
    {
        /// <summary>
        /// PC端会员集合查询
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public int GetCount(string HyName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(HyName))
            {
                sql = string.Format(@"SELECT COUNT(A.RowID) AS TotalCount FROM T_Hy A WHERE NickName LIKE '%0%", HyName);
            }
            else
                sql = @"SELECT COUNT(A.RowID) AS TotalCount FROM T_Hy A ";

            DataBase dbase = new DataBase();
            var dt = dbase.GetDataTable(sql, null);
            return dt == null ? 0 : Convert.ToInt32(dt.Rows[0][0]);
        }

        #region PC端会员集合分页查询 请勿删除
        public List<HyDto> GetHyDtoList(int? pageIndex, int? pageSize, string HyName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(HyName))
            {
                sql = string.Format(@" SELECT * FROM ( 
                          SELECT ROW_NUMBER() OVER(ORDER BY A.CreateTime Desc) AS ROWNUMBER,
                          * FROM T_Hy WHERE NickName LIKE '%0%' ) B WHERE B.ROWNUMBER BETWEEN {1} AND {2}
                          ", HyName, ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            }
            else
                sql = string.Format(@"SELECT * FROM ( 
                          SELECT ROW_NUMBER() OVER(ORDER BY A.CreateTime Desc) AS ROWNUMBER,
                          * FROM T_Hy ) B WHERE B.ROWNUMBER BETWEEN {0} AND {1} ", ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            DataBase dbase = new DataBase();
            List<HyDto> HyList = dbase.GetDataSql<HyDto>(sql, null);
            return HyList;
        }

        #endregion









        /// <summary>
        /// PC端会员登录集合查询
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public int GetHyLogCount(string HyName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(HyName))
            {
                sql = string.Format(@"SELECT COUNT(A.RowID) AS TotalCount FROM T_Hy_Log A WHERE HyNickName LIKE '%0%", HyName);
            }
            else
                sql = @"SELECT COUNT(A.RowID) AS TotalCount FROM T_Hy_Log A ";

            DataBase dbase = new DataBase();
            var dt = dbase.GetDataTable(sql, null);
            return dt == null ? 0 : Convert.ToInt32(dt.Rows[0][0]);
        }

        #region PC端会员登陆集合分页查询 请勿删除
        public List<HyLogDto> GetHyLogDtoList(int? pageIndex, int? pageSize, string HyName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(HyName))
            {
                sql = string.Format(@" SELECT * FROM ( 
                          SELECT ROW_NUMBER() OVER(ORDER BY A.CreateTime Desc) AS ROWNUMBER,
                          * FROM T_Hy_Log WHERE HyNickName LIKE '%0%' ) B WHERE B.ROWNUMBER BETWEEN {1} AND {2}
                          ", HyName, ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            }
            else
                sql = string.Format(@"SELECT * FROM ( 
                          SELECT ROW_NUMBER() OVER(ORDER BY A.CreateTime Desc) AS ROWNUMBER,
                          * FROM T_Hy_Log ) B WHERE B.ROWNUMBER BETWEEN {0} AND {1} ", ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            DataBase dbase = new DataBase();
            List<HyLogDto> HyLogList = dbase.GetDataSql<HyLogDto>(sql, null);
            return HyLogList;
        }

        #endregion
    }
}
