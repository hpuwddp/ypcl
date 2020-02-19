using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xz_ypcl_unit.Model
{
    public class CommandInfo
    {
        public string CommandText { get; set; }
        public SqlParameter[] Parameters { get; set; }

        public CommandInfo(string sql, SqlParameter[] parms)
        {
            this.CommandText = sql;
            this.Parameters = parms;
        }

        public CommandInfo() { }
    }
}
