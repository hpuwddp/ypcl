using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xz_ypcl_unit.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : System.Attribute
    {
        public string tableName { get; set; }
        public TableAttribute(string tableName) { this.tableName = tableName; }
    }
}