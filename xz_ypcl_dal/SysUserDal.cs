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
    public class SysUserDal
    {
        /// <summary>
        /// PC端系统用户集合查询
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public int GetCount(string UserName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(UserName))
            {
                sql = string.Format(@"SELECT COUNT(A.RowID) AS TotalCount FROM S_User A   WHERE A.UserName LIKE '%{0}%' ", UserName);
            }
            else
                sql = @"SELECT COUNT(A.RowID) AS TotalCount FROM S_User A ";

            DataBase dbase = new DataBase();
            var dt = dbase.GetDataTable(sql, null);
            return dt == null ? 0 : Convert.ToInt32(dt.Rows[0][0]);
        }

        #region PC端系统用户集合分页查询 请勿删除
        public List<SysUserModel> GetSysUserList(int? pageIndex, int? pageSize, string UserName)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(UserName))
            {
                sql = string.Format(@"SELECT * FROM (
                          SELECT ROW_NUMBER() OVER(ORDER BY A.RowID ASC) AS ROWNUMBER,* FROM S_User A 
                           WHERE A.UserName LIKE '%{0}%'
                          ) B WHERE B.ROWNUMBER BETWEEN {1} AND {2} ", UserName, ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            }
            else
                sql = string.Format(@"SELECT * FROM (
                          SELECT ROW_NUMBER() OVER(ORDER BY A.RowID ASC) AS ROWNUMBER,* FROM S_User A) B WHERE B.ROWNUMBER BETWEEN {0} AND {1} ", ((pageSize * pageIndex) - (pageSize - 1)), (pageIndex * pageSize));
            DataBase dbase = new DataBase();
            List<SysUserModel> SysUserList = dbase.GetDataSql<SysUserModel>(sql, null);
            return SysUserList;
        }

        #endregion

        /// <summary>
        /// 插入新纪录
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public bool InsertData(SysUserModel newModel, string roleids)
        {
            string sql = "INSERT INTO S_User(UserName,PassWord) VALUES('" + newModel.UserName + "','" + newModel.PassWord + "')";
            if (!string.IsNullOrEmpty(roleids))
            {
                string[] roles = roleids.TrimEnd(',').Split(',');
                foreach (string roleid in roles)
                {
                    sql += "insert S_User_Role(UserID,RoleID) values(IDENT_CURRENT('S_User')," + roleid + ");";
                }
            }
            DataBase dbase = new DataBase();
            return dbase.update(sql) > 0 ? true : false;
            //string sql = @"INSERT INTO S_User(UserName,PassWord) VALUES(@UserName,@PassWord) ";
            //SqlParameter[] parmes ={
            //                      new SqlParameter("@UserName",newModel.UserName),
            //                       new SqlParameter("@PassWord",newModel.PassWord)
            //                      };
            //
            //return dbase.updateData(sql, parmes) > 0 ? true : false;
        }

        public bool UpData(SysUserModel newModel, string roleids)
        {

            string sql = @"UPDATE S_User SET UserName='" + newModel.UserName + "',PassWord='" + newModel.PassWord + "' WHERE RowID= " + newModel.RowID + ";DELETE FROM S_User_Role WHERE UserID=" + newModel.RowID + ";";

            if (!string.IsNullOrEmpty(roleids))
            {
                string[] roles = roleids.TrimEnd(',').Split(',');
                foreach (string roleid in roles)
                {
                    sql += "insert S_User_Role(UserID,RoleID) values(" + newModel.RowID + "," + roleid + ");";
                }
            }
            DataBase dbase = new DataBase();
            return dbase.update(sql) > 0 ? true : false;

            //SqlParameter[] parmes ={
            //                      new SqlParameter("@UserName",newModel.UserName),
            //                         new SqlParameter("@PassWord",newModel.PassWord),
            //                              new SqlParameter("@RowID",newModel.RowID),
            //                      };
            //DataBase dbase = new DataBase();
            //return dbase.updateData(sql, parmes) > 0 ? true : false;
        }



        public int DelData(int rowid)
        {
            string sql = "DELETE FROM S_User WHERE RowID=" + rowid + ";DELETE FROM S_User_Role WHERE UserID=" + rowid + ";";
            //string sql = SqlHelper.DataDelSqlConvert<SysUserModel>();
            //SqlParameter[] parmes ={
            //                     new SqlParameter("@RowID",rowid)
            //                      };
            DataBase dbase = new DataBase();
            //return dbase.updateData(sql, parmes);
            return dbase.update(sql);
        }
    }
}
