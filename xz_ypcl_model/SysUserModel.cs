using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_unit.Attribute;

namespace xz_ypcl_model
{
    [Serializable]
    [Table("S_User")]
    public class SysUserModel
    {
         [Column("RowID", "int", true)]
        public int? RowID { get; set; }
        [Column("UserName", "nvarchar")]
        public string UserName { get; set; }
        [Column("PassWord", "nvarchar")]
        public string PassWord { get; set; }
        
    }
}
