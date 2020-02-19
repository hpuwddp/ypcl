using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xz_ypcl_unit.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : System.Attribute
    {
        public string columnName { get; set; }
        public string columnType { get; set; }
        public bool isPrimaryKey { get; set; }

        public ColumnAttribute(string columnName, string columnType)
        {
            this.columnName = columnName;
            this.columnType = columnType;
        }

        public ColumnAttribute(string columnName, string columnType, bool isPrimaryKey)
        {
            this.columnName = columnName;
            this.columnType = columnType;
            this.isPrimaryKey = isPrimaryKey;
        }
    }
}
