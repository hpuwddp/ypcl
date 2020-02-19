using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_unit.Attribute;

namespace xz_ypcl_model
{
    [Serializable]
    [Table("S_Role")]
   public class SysRoleModel
    {
        [Column("RowID", "int", true)]
        public int? RowID { get; set; }
        [Column("RoleName", "nvarchar")]
        public string RoleName { get; set; }
    }
}
