using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_unit.Attribute;

namespace xz_ypcl_unit.DataBase
{
    public class SqlHelper
    {
        /// <summary>
        /// 单表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string DataQuerySqlConvert<T>() where T : new()
        {
            try
            {
                Type type = typeof(T); string tableName = string.Empty;
                var tableAttribute = type.GetCustomAttributes(typeof(TableAttribute), false);
                if (tableAttribute != null)
                    tableName = ((TableAttribute)tableAttribute[0]).tableName;
                PropertyInfo[] propers = type.GetProperties();
                object[] objDataFieldAttribute = null;
                string sqlText = "SELECT "; bool columnLength = false;
                foreach (PropertyInfo info in propers)
                {
                    objDataFieldAttribute = info.GetCustomAttributes(typeof(ColumnAttribute), false);
                    if (objDataFieldAttribute != null && objDataFieldAttribute.Count()>0)
                    {
                        columnLength = true;
                        sqlText += ((ColumnAttribute)objDataFieldAttribute[0]).columnName + ",";
                    }
                }
                if (columnLength)
                {
                    sqlText = sqlText.TrimEnd(',') + " FROM " + tableName;
                }
                return sqlText;
            }
            catch (Exception ex)
            {
                throw new Exception("转换sql查询语句失败");
            }
        }


        /// <summary>
        /// 单表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string DataQuerySqlConvert<T>(string strWhere) where T : new()
        {
            try
            {
                Type type = typeof(T); string tableName = string.Empty;
                var tableAttribute = type.GetCustomAttributes(typeof(TableAttribute), false);
                if (tableAttribute != null)
                    tableName = ((TableAttribute)tableAttribute[0]).tableName;
                PropertyInfo[] propers = type.GetProperties();
                object[] objDataFieldAttribute = null;
                string sqlText = "SELECT "; bool columnLength = false;
                foreach (PropertyInfo info in propers)
                {
                    objDataFieldAttribute = info.GetCustomAttributes(typeof(ColumnAttribute), false);
                    if (objDataFieldAttribute != null && objDataFieldAttribute.Count()>0)
                    {
                        columnLength = true;
                        sqlText += ((ColumnAttribute)objDataFieldAttribute[0]).columnName + ",";
                    }
                }
                if (columnLength)
                {
                    sqlText = sqlText.TrimEnd(',') + " FROM " + tableName;
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        sqlText += strWhere;
                    }
                }

                return sqlText;
            }
            catch (Exception ex)
            {
                throw new Exception("转换sql查询语句失败");
            }
        }


        /// <summary>
        /// 单表插数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string DataInsertSqlConvert<T>()
        {
            try
            {
                Type type = typeof(T); string tableName = string.Empty;
                var tableAttribute = type.GetCustomAttributes(typeof(TableAttribute), false);
                if (tableAttribute != null)
                    tableName = ((TableAttribute)tableAttribute[0]).tableName;
                PropertyInfo[] propers = type.GetProperties();
                object[] objDataFieldAttribute = null;
                string sqlText = " INSERT INTO " + tableName + " (@column) VALUES(@value) ";
                string sqlC = string.Empty; string sqlV = string.Empty;

                bool columnLength = false;
                foreach (PropertyInfo info in propers)
                {
                    objDataFieldAttribute = info.GetCustomAttributes(typeof(ColumnAttribute), false);
                    if (objDataFieldAttribute != null && objDataFieldAttribute.Count() > 0)
                    {
                        if (((ColumnAttribute)objDataFieldAttribute[0]).isPrimaryKey != true)
                        {
                            columnLength = true;
                            sqlC += ((ColumnAttribute)objDataFieldAttribute[0]).columnName + ",";
                            sqlV += "@" + ((ColumnAttribute)objDataFieldAttribute[0]).columnName + ",";
                        }

                    }
                }
                if (columnLength)
                {
                    sqlC = sqlC.TrimEnd(','); sqlV = sqlV.TrimEnd(',');
                    sqlText = sqlText.Replace("@column", sqlC).Replace("@value", sqlV);
                }
                return sqlText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 单表主键删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string DataDelSqlConvert<T>()
        {
            try
            {
                Type type = typeof(T); string tableName = string.Empty;
                var tableAttribute = type.GetCustomAttributes(typeof(TableAttribute), false);
                if (tableAttribute != null)
                    tableName = ((TableAttribute)tableAttribute[0]).tableName;
                PropertyInfo[] propers = type.GetProperties();
                object[] objDataFieldAttribute = null;
                string sqlText = " DELETE FROM  " + tableName;
                foreach (PropertyInfo info in propers)
                {
                    objDataFieldAttribute = info.GetCustomAttributes(typeof(ColumnAttribute), false);
                    if (objDataFieldAttribute != null)
                    {
                        //主键
                        if (((ColumnAttribute)objDataFieldAttribute[0]).isPrimaryKey == true)
                        {
                            sqlText += " WHERE " + ((ColumnAttribute)objDataFieldAttribute[0]).columnName + "=@" + ((ColumnAttribute)objDataFieldAttribute[0]).columnName;
                            break;
                        }
                    }
                }
                return sqlText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 单表主键修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string DataUpdateSqlConvert<T>()
        {
            try
            {
                Type type = typeof(T); string tableName = string.Empty;
                var tableAttribute = type.GetCustomAttributes(typeof(TableAttribute), false);
                if (tableAttribute != null)
                    tableName = ((TableAttribute)tableAttribute[0]).tableName;
                PropertyInfo[] propers = type.GetProperties();
                object[] objDataFieldAttribute = null;
                string sqlText = " UPDATE " + tableName + " SET ";
                string sqlC = string.Empty;
                string sqlWhere = string.Empty;
                bool columnLength = false;
                foreach (PropertyInfo info in propers)
                {
                    objDataFieldAttribute = info.GetCustomAttributes(typeof(ColumnAttribute), false);
                    if (objDataFieldAttribute != null)
                    {
                        if (((ColumnAttribute)objDataFieldAttribute[0]).isPrimaryKey != true)
                        {
                            columnLength = true;
                            sqlC += ((ColumnAttribute)objDataFieldAttribute[0]).columnName + "=@" + ((ColumnAttribute)objDataFieldAttribute[0]).columnName + ",";
                        }

                        //主键
                        if (((ColumnAttribute)objDataFieldAttribute[0]).isPrimaryKey == true)
                        {
                            sqlWhere = " WHERE " + ((ColumnAttribute)objDataFieldAttribute[0]).columnName + "=@" + ((ColumnAttribute)objDataFieldAttribute[0]).columnName;
                        }
                    }
                }
                if (columnLength)
                {
                    sqlText += sqlC.TrimEnd(',') + sqlWhere;
                }
                return sqlText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
