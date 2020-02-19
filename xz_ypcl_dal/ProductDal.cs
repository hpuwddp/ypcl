using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_model;
using xz_ypcl_unit.DataBase;

namespace xz_ypcl_dal
{
   public class ProductDal
    {
        /// <summary>
        /// PC端套餐集合查询
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
       public int GetCount(string ProductName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(ProductName))
            {
                sql = string.Format(@"SELECT COUNT(A.RowID) AS TotalCount FROM T_Product A   WHERE A.ProductName LIKE '%{0}%' ", ProductName);
            }
            else
                sql = @"SELECT COUNT(A.RowID) AS TotalCount FROM T_Product A ";

            DataBase dbase = new DataBase();
            var dt = dbase.GetDataTable(sql, null);
            return dt == null ? 0 : Convert.ToInt32(dt.Rows[0][0]);
        }

        #region PC端套餐集合分页查询 请勿删除
       public List<ProductModel> GetProductList(int? pageIndex, int? pageSize, string ProductName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(ProductName))
            {
                sql = string.Format(@"SELECT * FROM (
                          SELECT ROW_NUMBER() OVER(ORDER BY A.RowID ASC) AS ROWNUMBER,* FROM T_Product A 
                           WHERE A.ProductName LIKE '%{0}%'
                          ) B WHERE B.ROWNUMBER BETWEEN {1} AND {2} ", ProductName, ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            }
            else
                sql = string.Format(@"SELECT * FROM (
                          SELECT ROW_NUMBER() OVER(ORDER BY A.RowID ASC) AS ROWNUMBER,* FROM T_Product A) B WHERE B.ROWNUMBER BETWEEN {0} AND {1} ", ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            DataBase dbase = new DataBase();
            List<ProductModel> ProductList = dbase.GetDataSql<ProductModel>(sql, null);
            return ProductList;
        }

        #endregion

        /// <summary>
        /// 插入新纪录
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
       public bool InsertData(ProductModel newModel)
        {
            string sql = @"INSERT INTO T_Product(ProductName,Days,Price) VALUES(@ProductName,@Days,@Price) ";
            SqlParameter[] parmes ={
                                  new SqlParameter("@ProductName",newModel.ProductName),
                                   new SqlParameter("@Days",newModel.Days),
                                   new SqlParameter("@Price",newModel.Price)
                                  };
            DataBase dbase = new DataBase();
            return dbase.updateData(sql, parmes) > 0 ? true : false;
        }

       public bool UpData(ProductModel newModel)
        {
            string sql = @"UPDATE T_Product SET ProductName=@ProductName,Days=@Days,Price=@Price WHERE RowID=@RowID ";
            SqlParameter[] parmes ={
                                  new SqlParameter("@ProductName",newModel.ProductName),
                                     new SqlParameter("@Days",newModel.Days),
                                     new SqlParameter("@Price",newModel.Price),
                                          new SqlParameter("@RowID",newModel.RowID)
                                  };
            DataBase dbase = new DataBase();
            return dbase.updateData(sql, parmes) > 0 ? true : false;
        }



        public int DelData(int rowid)
        {
            string sql = SqlHelper.DataDelSqlConvert<ProductModel>();
            SqlParameter[] parmes ={
                                 new SqlParameter("@RowID",rowid)
                                  };
            DataBase dbase = new DataBase();
            return dbase.updateData(sql, parmes);
        }
    }
}
