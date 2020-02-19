using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_dal;
using xz_ypcl_model.Dto;

namespace xz_ypcl_bll
{
   public class HyBll
    {
        #region
        /// <summary>
        /// PC端会员集合分页查询
        /// 请勿删除
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<HyDto> GetHyDtoList(int? pageIndex, int? pageSize, string HyName, ref int recordCount)
        {
            HyDal dal = new HyDal();
            recordCount = dal.GetCount(HyName);
            return dal.GetHyDtoList(pageIndex, pageSize, HyName);
        }
        #endregion

        #region
        /// <summary>
        /// PC端会员登陆集合分页查询
        /// 请勿删除
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<HyLogDto> GetHyLogDtoList(int? pageIndex, int? pageSize, string HyName, ref int recordCount)
        {
            HyDal dal = new HyDal();
            recordCount = dal.GetHyLogCount(HyName);
            return dal.GetHyLogDtoList(pageIndex, pageSize, HyName);
        }
        #endregion
    }
}
