using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_dal;
using xz_ypcl_model;

namespace xz_ypcl_bll
{
    public class SysUserBll
    {
        #region
        /// <summary>
        /// PC端系统用户集合分页查询
        /// 请勿删除
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<SysUserModel> GetSysUserList(int? pageIndex, int? pageSize, string UserName, ref int recordCount)
        {
            SysUserDal dal = new SysUserDal();
            recordCount = dal.GetCount(UserName);
            return dal.GetSysUserList(pageIndex, pageSize, UserName);
        }
        #endregion


        public bool InsertData(SysUserModel newModel, string roleids)
        {
            SysUserDal dal = new SysUserDal();
            return dal.InsertData(newModel,roleids);
        }
        /// <summary>
        /// 修改记录
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public bool UpData(SysUserModel newModel, string roleids)
        {
            SysUserDal dal = new SysUserDal();
            return dal.UpData(newModel,roleids);
        }



        public int DelData(int rowid)
        {
            SysUserDal dal = new SysUserDal();
            return dal.DelData(rowid);
        }

        
    }
}
