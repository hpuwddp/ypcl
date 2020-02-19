using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xz_ypcl_model.Dto
{
    [Serializable]
    public class HyLogDto
    {
        public int RowID { get; set; }
        public int HyID { get; set; }
        public string HyNickName { get; set; }
        public DateTime LogTime { get; set; }
    }
}
