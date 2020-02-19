using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_dal;
using xz_ypcl_model;

namespace xz_ypcl_bll
{
    public class SysRoleBll
    {
        #region
        /// <summary>
        /// PC端系统角色集合分页查询
        /// 请勿删除
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<SysRoleModel> GetSysRoleList(int? pageIndex, int? pageSize, string RoleName, ref int recordCount)
        {
            SysRoleDal dal = new SysRoleDal();
            recordCount = dal.GetCount(RoleName);
            return dal.GetSysRoleList(pageIndex, pageSize, RoleName);
        }
        #endregion


        public bool InsertData(SysRoleModel newModel)
        {
            SysRoleDal dal = new SysRoleDal();
            return dal.InsertData(newModel);
        }
        /// <summary>
        /// 修改记录
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public bool UpData(SysRoleModel newModel)
        {
            SysRoleDal dal = new SysRoleDal();
            return dal.UpData(newModel);
        }



        public int DelData(int rowid)
        {
            SysRoleDal dal = new SysRoleDal();
            return dal.DelData(rowid);
        }
    }
}
