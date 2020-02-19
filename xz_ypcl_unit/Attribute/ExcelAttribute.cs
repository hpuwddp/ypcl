using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xz_ypcl_unit.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelAttribute: System.Attribute
    {
        public string Title { get; set; }

        public ExcelAttribute(string Title) { this.Title = Title; }
    }
}