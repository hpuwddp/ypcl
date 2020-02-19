using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xz_ypcl_model.Dto
{
    [Serializable]
    public class OrderDto
    {
        public int RowID { get; set; }
        public string OrderCode { get; set; }
        public int ProductID { get; set; }
        public int HyID { get; set; }
        public string PayType { get; set; }
        public DateTime CreateTime { get; set; }
        public string NickName { get; set; }
        public string ProductName { get; set; }
    }
}
