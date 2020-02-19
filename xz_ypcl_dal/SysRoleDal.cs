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
   public class SysRoleDal
    {
        /// <summary>
        /// PC端系统角色集合查询
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public int GetCount(string RoleName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(RoleName))
            {
                sql = string.Format(@"SELECT COUNT(A.RowID) AS TotalCount FROM S_Role A   WHERE A.RoleName LIKE '%{0}%' ", RoleName);
            }
            else
                sql = @"SELECT COUNT(A.RowID) AS TotalCount FROM S_Role A ";

            DataBase dbase = new DataBase();
            var dt = dbase.GetDataTable(sql, null);
            return dt == null ? 0 : Convert.ToInt32(dt.Rows[0][0]);
        }

        #region PC端系统角色集合分页查询 请勿删除
        public List<SysRoleModel> GetSysRoleList(int? pageIndex, int? pageSize, string RoleName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(RoleName))
            {
                sql = string.Format(@"SELECT * FROM (
                          SELECT ROW_NUMBER() OVER(ORDER BY A.RowID ASC) AS ROWNUMBER,* FROM S_Role A 
                           WHERE A.RoleName LIKE '%{0}%'
                          ) B WHERE B.ROWNUMBER BETWEEN {1} AND {2} ", RoleName, ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            }
            else
                sql = string.Format(@"SELECT * FROM (
                          SELECT ROW_NUMBER() OVER(ORDER BY A.RowID ASC) AS ROWNUMBER,* FROM S_Role A) B WHERE B.ROWNUMBER BETWEEN {0} AND {1} ", ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            DataBase dbase = new DataBase();
            List<SysRoleModel> SysRoleList = dbase.GetDataSql<SysRoleModel>(sql, null);
            return SysRoleList;
        }

        #endregion

        /// <summary>
        /// 插入新纪录
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public bool InsertData(SysRoleModel newModel)
        {
            string sql = @"INSERT INTO S_Role(RoleName) VALUES(@RoleName) ";
            SqlParameter[] parmes ={
                                  new SqlParameter("@RoleName",newModel.RoleName)
                                  };
            DataBase dbase = new DataBase();
            return dbase.updateData(sql, parmes) > 0 ? true : false;
        }

        public bool UpData(SysRoleModel newModel)
        {
            string sql = @"UPDATE S_Role SET RoleName=@RoleName WHERE RowID=@RowID ";
            SqlParameter[] parmes ={
                                  new SqlParameter("@RoleName",newModel.RoleName),
                                          new SqlParameter("@RowID",newModel.RowID),
                                  };
            DataBase dbase = new DataBase();
            return dbase.updateData(sql, parmes) > 0 ? true : false;
        }



        public int DelData(int rowid)
        {
            string sql = SqlHelper.DataDelSqlConvert<SysRoleModel>();
            SqlParameter[] parmes ={
                                 new SqlParameter("@RowID",rowid)
                                  };
            DataBase dbase = new DataBase();
            return dbase.updateData(sql, parmes);
        }
    }
}
