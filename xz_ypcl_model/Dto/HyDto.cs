using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xz_ypcl_model.Dto
{
    [Serializable]
    public class HyDto
    {
        public int RowID { get; set; }
        public string NickName { get; set; }
        public int IsVip { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
