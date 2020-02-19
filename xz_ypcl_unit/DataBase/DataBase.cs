using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_unit.Model;

namespace xz_ypcl_unit.DataBase
{
    public class DataBase : IDisposable
    {
        //数据库连接
        public SqlConnection Connection;
        //数据库连接串
        protected String ConnectionString;

        //构造函数
        public DataBase()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        }

        //释放非托管资源
        ~DataBase()
        {
            try
            {
                if (Connection != null)
                    Connection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            try
            {
                Dispose();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //打开数据库连接
        protected void Open()
        {
            if (Connection == null)
            {
                Connection = new SqlConnection(ConnectionString);
            }
            if (Connection.State.Equals(ConnectionState.Closed))
            {
                Connection.Open();
            }
        }

        //关闭数据库连接
        public void Close()
        {
            if (Connection != null)
                Connection.Close();
            Dispose();
        }

        public SqlConnection GetSqlConnection()
        {
            if (Connection == null)
            {
                Connection = new SqlConnection(ConnectionString);
            }
            if (Connection.State.Equals(ConnectionState.Closed))
            {
                Connection.Open();
            }
            return Connection;
        }

        //释放资源
        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
                Connection = null;
            }
        }

        #region 获取数据，返回一个SqlDateRedear，三个重载
        //获取数据，返回一个SqlDataReader（调用后注意调用SqlDataReader.Close()）
        public SqlDataReader GetDataReader(String SqlString)
        {
            SqlCommand cmd = null;
            try
            {
                Open();
                cmd = new SqlCommand(SqlString, Connection);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
                //ex.ToString();
                return null;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                Dispose();
                Close();
            }

        }

        //带一个Sqlparameter的GetDataReader方法的重载
        public SqlDataReader GetDataReader(String SqlString, SqlParameter para)
        {
            SqlCommand cmd = null;
            try
            {
                Open();
                cmd = new SqlCommand(SqlString, Connection);
                if (para != null)
                {
                    cmd.Parameters.Add(para);
                }
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
                //ex.ToString();
                return null;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                Dispose();
                Close();
            }

        }

        //带Sqlparameter[]数组的GetDataReader方法的重载

        public SqlDataReader GetDataReader(String SqlString, SqlParameter[] paras)
        {
            SqlCommand cmd = null;
            try
            {
                Open();
                cmd = new SqlCommand(SqlString, Connection);
                if (paras != null)
                {
                    cmd.Parameters.AddRange(paras);
                }
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
                //ex.ToString();
                return null;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                Dispose();
                Close();
            }

        }

        #endregion 获取数据，返回一个SqlDataReader，三个重载

        #region 获取数据，返回一个DataSet，三个重载
        //获取数据，返回一个DataSet
        public DataSet GetDataSet(String sqlString)
        {
            return GetDataSet(sqlString, null);
        }

        //带Sqlparameter[]数组的GetDataSet方法的重载
        public DataSet GetDataSet(String sqlString, Dictionary<string, object> paras)
        {
            SqlDataAdapter adapter = null;
            try
            {
                Open();
                adapter = new SqlDataAdapter(sqlString, Connection);
                if (paras != null)
                {
                    foreach (string key in paras.Keys)
                    {
                        adapter.SelectCommand.Parameters.Add("@" + key, paras[key]);
                    }
                }
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset;
            }
            catch (Exception ex)
            {
                throw ex;
                //ex.ToString();
                return null;
            }
            finally
            {
                if (adapter != null)
                {
                    adapter.Dispose();
                }
                Dispose();
                Close();
            }
        }

        public List<T> GetDataSet<T>(String SqlString, Dictionary<string, object> paras)
        {
            SqlCommand cmd = null;
            List<T> list = new List<T>();
            Type type = typeof(T);
            var properties = type.GetProperties();
            try
            {
                Open();
                cmd = new SqlCommand(SqlString, Connection);
                if (paras != null && paras.Keys.Count > 0)
                {
                    SqlParameter[] sqlParas = null;
                    sqlParas = new SqlParameter[paras.Keys.Count];
                    int i = 0;
                    foreach (string key in paras.Keys)
                    {
                        sqlParas[i++] = new SqlParameter("@" + key, paras[key]);
                    }
                    cmd.Parameters.AddRange(sqlParas);
                }
                SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (sdr.Read())
                {

                    // T o = new T();

                    ConstructorInfo[] constructors = type.GetConstructors(); //使用这个方法获取构造函数列表    
                    object o = constructors[0].Invoke(null); //实例化一个这个构造函数有两个参数的类型对象,如果参数为空，则为null

                    int count = sdr.FieldCount;
                    for (int i = 0; i < count; i++)
                    {
                        string name = sdr.GetName(i);
                        var value = sdr.GetValue(i);


                        //反射字典类型
                        if (type == typeof(Dictionary<string, object>))
                        {
                            MethodInfo methodAdd = type.GetMethod("Add");
                            methodAdd.Invoke(o, new object[] { name, value });
                        }
                        else
                        {
                            name = name.Replace("_", "");
                            var pro = properties.Where(p => p.Name == name).FirstOrDefault();
                            if (pro != null)
                            {
                                pro.SetValue(o, value, null);
                            }
                        }

                    }
                    list.Add((T)o);
                };
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                Dispose();
                Close();
            }

        }
        #endregion 获取数据，返回一个DataSet，三个重载

        #region 获取数据，返回一个ModelList，三个重载


        //获取记录总数
        public int getRecordCount(String sqlString)
        {
            Dictionary<string, object> paras = null;
            return getRecordCount(sqlString, paras);
        }

        //带Sqlparameter[]数组的获取记录总数方法的重载
        public int getRecordCount(String sqlString, Dictionary<string, object> paras)
        {
            SqlCommand comm = null;
            int toatalCountRecord = 0;
            try
            {
                Open();
                String countSql = "select count(*) as RECORD_COUNT from (" + sqlString + ") t";
                comm = new SqlCommand(countSql, Connection);
                if (paras != null)
                {
                    foreach (string key in paras.Keys)
                    {
                        comm.Parameters.Add("@" + key, paras[key]);
                    }
                }
                toatalCountRecord = Convert.ToInt32(comm.ExecuteScalar());
                Console.WriteLine(toatalCountRecord);
            }
            catch (Exception ex)
            {
                throw ex;
                //ex.ToString();
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }
                Dispose();
                Close();
            }
            return toatalCountRecord;


        }


        #endregion 获取数据，返回一个ModelList，三个重载

        #region 获取数据，返回DataTable，三个重载
        //获取数据，返回一个DataTable
        public DataTable GetDataTable(String SqlString)
        {
            return GetDataTable(SqlString, null);
        }

        //带Sqlparameter[]数组的GetDataTable方法的重载

        public DataTable GetDataTable(String SqlString, Dictionary<string, object> paras)
        {
            SqlDataAdapter adapter = null;
            try
            {
                Open();
                adapter = new SqlDataAdapter(SqlString, Connection);
                if (paras != null)
                {
                    foreach (string key in paras.Keys)
                    {
                        adapter.SelectCommand.Parameters.Add("@" + key, paras[key]);
                    }
                }
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
                //ex.ToString();
                return null;
            }
            finally
            {
                if (adapter != null)
                {
                    adapter.Dispose();
                }
                Dispose();
                Close();
            }
        }

        #endregion 获取数据，返回一个DataTable，三个重载

        #region 获取数据，返回DataRow，三个重载
        //获取数据，返回一个DataRow
        public DataRow GetDataRow(String SqlString)
        {
            return GetDataRow(SqlString, null);

        }

        //带Sqlparameter[]数组的GetDataRow方法的重载

        public DataRow GetDataRow(String SqlString, Dictionary<string, object> paras)
        {
            DataSet dataset = GetDataSet(SqlString, paras);
            dataset.CaseSensitive = false;
            if (dataset.Tables[0].Rows.Count > 0)
            {
                return dataset.Tables[0].Rows[0];
            }
            else
            {
                return null;
            }
        }

        #endregion 获取数据，返回一个DataRow，三个重载


        #region 更新数据库，返回受影响的行数，两个重载
        //更新数据库
        public int update(String SqlString)
        {
            return update(SqlString, null);
        }

        //带Dictionary<string, object>的update方法的重载

        public int update(String SqlString, Dictionary<string, object> paras)
        {
            SqlCommand cmd = null;
            //SqlTransaction trans = null;
            try
            {
                Open();
                cmd = new SqlCommand(SqlString, Connection);
                //trans = cmd.Connection.BeginTransaction();
                if (paras != null)
                {
                    foreach (string key in paras.Keys)
                    {
                        cmd.Parameters.Add("@" + key, paras[key]);
                    }
                }
                return cmd.ExecuteNonQuery();
                //trans.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
                Console.WriteLine("{0} Exception caught.", ex);
                return -1;
                //trans.Rollback();
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                //trans.Dispose();
                Dispose();
                Close();
            }
        }
        //更新数据库事务
        public bool update(List<string> sqlStringList, List<Dictionary<string, object>> paras)
        {
            SqlCommand cmd = new SqlCommand();
            SqlTransaction trans = null;
            try
            {
                Open();
                cmd.Connection = Connection;
                trans = cmd.Connection.BeginTransaction();
                cmd.Transaction = trans;
                for (int i = 0; i < sqlStringList.Count; i++)
                {
                    cmd.CommandText = sqlStringList[i];
                    if (paras != null && paras[i] != null)
                    {
                        foreach (string key in paras[i].Keys)
                        {
                            cmd.Parameters.Add("@" + key, paras[i][key]);
                        }
                    }
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Console.WriteLine("{0} Exception caught.", ex);
                trans.Rollback();
                throw ex;
                return false;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                trans.Dispose();
                Dispose();
                Close();
            }
        }

        #endregion 更新数据库，返回受影响的行数，两个重载

        /// 设置类中的属性值
        public bool setModelValue(string fieldName, object value, object obj)
        {
            try
            {
                Type Ts = obj.GetType();
                object v = Convert.ChangeType(value, Ts.GetProperty(fieldName).PropertyType);
                Ts.GetProperty(fieldName).SetValue(obj, v, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<T> GetDataSql<T>(string sqlString, SqlParameter[] paras) where T : new()
        {
            SqlCommand cmd = null;
            var list = new List<T>();
            Type type = typeof(T);
            var properties = type.GetProperties();
            try
            {
                Open();
                cmd = new SqlCommand(sqlString, Connection);
                if (paras != null && paras.Length > 0)
                {
                    cmd.Parameters.AddRange(paras);
                }
                SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (sdr.Read())
                {
                    T o = new T();
                    int count = sdr.FieldCount;
                    for (int i = 0; i < count; i++)
                    {
                        string name = sdr.GetName(i);
                        var value = sdr.GetValue(i);
                        var pro = properties.Where(p => p.Name == name).FirstOrDefault();
                        if (pro != null && !DBNull.Value.Equals(value))
                        {
                            pro.SetValue(o, value, null);
                        }
                    }
                    list.Add(o);
                };
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                Dispose();
                Close();
            }
        }

        public List<T> GetDataSql<T>(string sqlString, SqlParameter[] paras, SqlConnection conn, SqlTransaction tran) where T : new()
        {
            SqlCommand cmd = null;
            var list = new List<T>();
            Type type = typeof(T);
            var properties = type.GetProperties();
            try
            {
                cmd = new SqlCommand(sqlString, conn, tran);
                if (paras != null && paras.Length > 0)
                {
                    cmd.Parameters.AddRange(paras);
                }
                SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (sdr.Read())
                {
                    T o = new T();
                    int count = sdr.FieldCount;
                    for (int i = 0; i < count; i++)
                    {
                        string name = sdr.GetName(i);
                        var value = sdr.GetValue(i);
                        var pro = properties.Where(p => p.Name == name).FirstOrDefault();
                        if (pro != null && !DBNull.Value.Equals(value))
                        {
                            pro.SetValue(o, value, null);
                        }
                    }
                    list.Add(o);
                };
                sdr.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
        }


        public int updateData(string SqlString, SqlParameter[] paras)
        {
            SqlConnection conn = GetSqlConnection();
            int result = 0;
            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                SqlCommand cmd = null;
                try
                {
                    cmd = new SqlCommand(SqlString, conn, transaction);
                    if (paras != null && paras.Length > 0)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    result = cmd.ExecuteNonQuery();
                    transaction.Commit();
                    Close();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                    Close();
                }
            };
        }

        public int updateData(string sql, SqlParameter[] paras, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql, conn, tran);
                if (paras != null && paras.Length > 0)
                {
                    cmd.Parameters.AddRange(paras);
                }
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
        }

        public object ExecuteScalar(string sql, SqlParameter[] paras)
        {
            SqlCommand cmd = null;
            try
            {
                Open();
                cmd = new SqlCommand(sql, Connection);
                if (paras != null && paras.Length > 0)
                {
                    cmd.Parameters.AddRange(paras);
                }
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                //trans.Dispose();
                Dispose();
                Close();
            }
        }

        public object ExecuteScalar(string sql, SqlParameter[] paras, SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmd = null;
            try
            {
                //Open();
                cmd = new SqlCommand(sql, conn, tran);
                if (paras != null && paras.Length > 0)
                {
                    cmd.Parameters.AddRange(paras);
                }
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
        }

        public void ExecuteCommand(ArrayList varSqlList)
        {

            SqlConnection MyConnection = new SqlConnection(ConnectionString);
            MyConnection.Open();
            SqlTransaction varTrans = MyConnection.BeginTransaction();
            SqlCommand command = new SqlCommand();
            command.Connection = MyConnection;
            command.Transaction = varTrans;
            try
            {
                foreach (string varcommandText in varSqlList)
                {
                    command.CommandText = varcommandText;
                    command.ExecuteNonQuery();
                }
                varTrans.Commit();
            }
            catch (Exception ex)
            {
                varTrans.Rollback();
                throw ex;
            }
            finally
            {
                MyConnection.Close();
            }
        }


        /// <summary>
        /// 启用事务提交多条带参数的SQL语句
        /// </summary>
        /// <param name="detailSql">明细表SQL语句</param>
        /// <param name="detailParam">明细表对应的参数</param>
        /// <returns>返回事务是否成功</returns>
        public bool UpdateByTran(string mainSql, List<SqlParameter[]> mainParam, string detailSql, List<SqlParameter[]> detailParam, string detailSql2, List<SqlParameter[]> detailParam2)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.Transaction = conn.BeginTransaction();//开启事务
                if (mainParam != null && mainParam.Count > 0 && !string.IsNullOrEmpty(mainSql))
                {
                    foreach (SqlParameter[] param in mainParam)
                    {
                        cmd.CommandText = mainSql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(param);
                        cmd.ExecuteNonQuery();
                    }
                }

                if (detailParam != null && !string.IsNullOrEmpty(detailSql))
                {
                    foreach (SqlParameter[] param in detailParam)
                    {
                        cmd.CommandText = detailSql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(param);
                        cmd.ExecuteNonQuery();
                    }
                }
                if (detailParam2 != null && !string.IsNullOrEmpty(detailSql2))
                {
                    foreach (SqlParameter[] param in detailParam2)
                    {
                        cmd.CommandText = detailSql2;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(param);
                        cmd.ExecuteNonQuery();
                    }
                }

                cmd.Transaction.Commit();//提交事务
                return true;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();//回滚事务
                }
                throw ex;
            }
            finally
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction = null;//清空事务
                }
                conn.Close();
            }
        }



        /// <summary>
        /// SqlBulkCopy批量插入数据
        /// </summary>
        /// <param name="connectionStr">链接字符串</param>
        /// <param name="dataTableName">表名</param>
        /// <param name="sourceDataTable">数据源</param>
        /// <param name="batchSize">一次事务插入的行数</param>
        public void SqlBulkCopyByDataTable(string dataTableName, DataTable sourceDataTable, int batchSize = 100000)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {

                    try
                    {
                        sqlBulkCopy.DestinationTableName = dataTableName;
                        sqlBulkCopy.BatchSize = batchSize;
                        for (int i = 0; i < sourceDataTable.Columns.Count; i++)
                        {
                            sqlBulkCopy.ColumnMappings.Add(sourceDataTable.Columns[i].ColumnName, sourceDataTable.Columns[i].ColumnName);
                        }
                        sqlBulkCopy.WriteToServer(sourceDataTable);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public void ExecuteSqlTranWithIndentity(System.Collections.Generic.List<CommandInfo> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int indentity = 0;
                        //循环
                        foreach (CommandInfo myDE in SQLStringList)
                        {
                            string cmdText = myDE.CommandText;
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">OleDb连接</param>
        /// <param name="trans">OleDb事务</param>
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        /// <param name="cmdText">命令文本,例如:Select * from Products</param>
        /// <param name="cmdParms">执行命令的参数</param>
        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;



            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }



    }
}
