using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_unit.Attribute;

namespace xz_ypcl_model
{
    [Serializable]
    [Table("T_Product")]
    public class ProductModel
    {
        [Column("RowID", "int", true)]
        public int? RowID { get; set; }
        [Column("ProductName", "nvarchar")]
        public string ProductName { get; set; }
        [Column("Days", "int")]
        public int Days { get; set; }

        [Column("Price", "decimal")]
        public decimal Price { get; set; }
    }
}
