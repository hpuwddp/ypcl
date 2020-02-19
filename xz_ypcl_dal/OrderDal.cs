using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_model.Dto;
using xz_ypcl_unit.DataBase;

namespace xz_ypcl_dal
{
    public class OrderDal
    {
        /// <summary>
        /// PC端订单集合查询
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public int GetCount(string HyName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(HyName))
            {
                sql = string.Format(@"SELECT COUNT(A.RowID) AS TotalCount FROM 
(
                          SELECT TH.NickName  FROM T_Order TOR LEFT JOIN T_Hy TH ON TOR.HyID=TH.RowID
                          
                          WHERE TH.NickName LIKE '%0%') A
WHERE A.NickName LIKE '%{0}%' ", HyName);
            }
            else
                sql = @"SELECT COUNT(A.RowID) AS TotalCount FROM T_Order A ";

            DataBase dbase = new DataBase();
            var dt = dbase.GetDataTable(sql, null);
            return dt == null ? 0 : Convert.ToInt32(dt.Rows[0][0]);
        }

        #region PC端订单集合分页查询 请勿删除
        public List<OrderDto> GetOrderDtoList(int? pageIndex, int? pageSize, string HyName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(HyName))
            {
                sql = string.Format(@" SELECT * FROM ( 
                          SELECT ROW_NUMBER() OVER(ORDER BY A.CreateTime Desc) AS ROWNUMBER,
                          * FROM(
                          SELECT TOR.*,TH.NickName,TP.ProductName FROM T_Order TOR LEFT JOIN T_Hy TH ON TOR.HyID=TH.RowID
                          LEFT JOIN T_Product TP ON TOR.ProductID=TP.RowID
                          WHERE TH.NickName LIKE '%0%') A) B WHERE B.ROWNUMBER BETWEEN {1} AND {2}
                          ", HyName, ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            }
            else
                sql = string.Format(@"SELECT * FROM ( 
                          SELECT ROW_NUMBER() OVER(ORDER BY A.CreateTime Desc) AS ROWNUMBER,
                          * FROM(
                          SELECT TOR.*,TH.NickName,TP.ProductName FROM T_Order TOR LEFT JOIN T_Hy TH ON TOR.HyID=TH.RowID
                          LEFT JOIN T_Product TP ON TOR.ProductID=TP.RowID
                          ) A) B WHERE B.ROWNUMBER BETWEEN {0} AND {1} ", ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            DataBase dbase = new DataBase();
            List<OrderDto> OrderList = dbase.GetDataSql<OrderDto>(sql, null);
            return OrderList;
        }

        #endregion

    }
}
