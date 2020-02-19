using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_dal;
using xz_ypcl_model.Dto;

namespace xz_ypcl_bll
{
   public class OrderBll
    {
        #region
        /// <summary>
        /// PC端订单集合分页查询
        /// 请勿删除
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<OrderDto> GetOrderDtoList(int? pageIndex, int? pageSize, string HyName, ref int recordCount)
        {
            OrderDal dal = new OrderDal();
            recordCount = dal.GetCount(HyName);
            return dal.GetOrderDtoList(pageIndex, pageSize, HyName);
        }
        #endregion
    }
}
