using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using xz_ypcl_bll;
using xz_ypcl_model;

namespace xz_ypcl.Controllers
{
    public class SysController : Controller
    {
        //
        // GET: /Sys/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SysUser()
        {
            StringBuilder strB = new StringBuilder();
            strB.Append("<tr><td><input type=\"checkbox\" name=\"IDCheck\" value=\"14458579642011\" class=\"acb\" /></td>");
            strB.Append("<td>城中区</td> <td>瑞景河畔16号楼1-111</td><td>65.97㎡</td><td>65.97㎡</td><td>一室一厅一卫</td><td>混凝土</td>");
            strB.Append("<td>公租房</td><td>建成待租</td><td><a href=\"house_edit.html?fyID=14458579642011\" class=\"edit\">编辑</a>");
            strB.Append("<a href=\"javascript:del('14458579642011');\">删除</a></td></tr>");

            strB.Append("<tr><td><input type=\"checkbox\" name=\"IDCheck\" value=\"14458579642012\" class=\"acb\" /></td>");
            strB.Append("<td>城中区</td> <td>瑞景河畔16号楼1-112</td><td>65.97㎡</td><td>65.97㎡</td><td>一室一厅一卫</td><td>混凝土</td>");
            strB.Append("<td>公租房</td><td>建成待租</td><td><a href=\"house_edit.html?fyID=14458579642012\" class=\"edit\">编辑</a>");
            strB.Append("<a href=\"javascript:del('14458579642012');\">删除</a></td></tr>");

            strB.Append("<tr><td><input type=\"checkbox\" name=\"IDCheck\" value=\"14458579642013\" class=\"acb\" /></td>");
            strB.Append("<td>城中区</td> <td>瑞景河畔16号楼1-113</td><td>65.97㎡</td><td>65.97㎡</td><td>一室一厅一卫</td><td>混凝土</td>");
            strB.Append("<td>公租房</td><td>建成待租</td><td><a href=\"house_edit.html?fyID=14458579642013\" class=\"edit\">编辑</a>");
            strB.Append("<a href=\"javascript:del('14458579642011');\">删除</a></td></tr>");

            strB.Append("<tr><td><input type=\"checkbox\" name=\"IDCheck\" value=\"14458579642011\" class=\"acb\" /></td>");
            strB.Append("<td>城中区</td> <td>瑞景河畔16号楼1-114</td><td>65.97㎡</td><td>65.97㎡</td><td>一室一厅一卫</td><td>混凝土</td>");
            strB.Append("<td>公租房</td><td>建成待租</td><td><a href=\"house_edit.html?fyID=14458579642011\" class=\"edit\">编辑</a>");
            strB.Append("<a href=\"javascript:del('14458579642011');\">删除</a></td></tr>");

            strB.Append("<tr><td><input type=\"checkbox\" name=\"IDCheck\" value=\"14458579642011\" class=\"acb\" /></td>");
            strB.Append("<td>城中区</td> <td>瑞景河畔16号楼1-115</td><td>65.97㎡</td><td>65.97㎡</td><td>一室一厅一卫</td><td>混凝土</td>");
            strB.Append("<td>公租房</td><td>建成待租</td><td><a href=\"house_edit.html?fyID=14458579642011\" class=\"edit\">编辑</a>");
            strB.Append("<a href=\"javascript:del('14458579642011');\">删除</a></td></tr>");
            ViewBag.SysUserList = strB.ToString();
            return View();
        }

        #region 分页
        [HttpPost]
        public JsonResult GetSysUserList(int? pageIndex, int? pageSize, string UserName)
        {
            SysUserBll bll = new SysUserBll();
            int recordCounts = 0;
            var result = bll.GetSysUserList(pageIndex, pageSize, UserName, ref recordCounts);

            return Json(new { messCode = 200, recordCount = recordCounts, data = result });

        }

        #endregion

        [HttpGet]
        public ActionResult UserEdit(int rowid)
        {
            ViewBag.RowID = rowid;
            StringBuilder strB = new StringBuilder();
            if (rowid == 0)
            {
                //新增视图
                strB.Append("<tr><td class=\"ui_text_rt\">用户名</td><td class=\"ui_text_lt\"><input type=\"text\" id=\"UserName\" name=\"UserName\" value=\"瑞景河畔16号楼1-112\" class=\"ui_input_txt02\" />");
                strB.Append("</td></tr><tr><td class=\"ui_text_rt\">密码</td><td class=\"ui_text_lt\"><input type=\"text\" id=\"PassWord\" name=\"PassWord\" value=\"城中区\" class=\"ui_input_txt02\" />");
                strB.Append("</td></tr>");
                strB.Append("<tr><td class=\"ui_text_rt\">角色</td><td class=\"ui_text_lt\">");
                strB.Append("<select class=\"demo\" multiple=\"multiple\"><optgroup label=\"Languages\"><option value=\"cp\">C++</option><option value=\"cs\">C#</option>");
                strB.Append("<option value=\"oc\">Object C</option><option value=\"c\">C</option></optgroup></select>");


                strB.Append("</td></tr>");




            }
            else
            {
                //编辑视图
                strB.Append("<tr><td class=\"ui_text_rt\">用户名</td><td class=\"ui_text_lt\"><input type=\"text\" id=\"UserName\" name=\"UserName\" value=\"修改瑞景河畔16号楼1-112\" class=\"ui_input_txt02\" />");
                strB.Append("</td></tr><tr><td class=\"ui_text_rt\">密码</td><td class=\"ui_text_lt\"><input type=\"text\" id=\"PassWord\" name=\"PassWord\" value=\"修改城中区\" class=\"ui_input_txt02\" />");
                strB.Append("</td></tr>");
            }
            ViewBag.UserInfo = strB.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult UserOperation(string data, string roleids)
        {
            SysUserModel sysUser = JsonConvert.DeserializeObject<SysUserModel>(data);
            SysUserBll bll = new SysUserBll();
            if (sysUser.RowID == null || sysUser.RowID == 0)
            {
                //新增操作
                bll.InsertData(sysUser,roleids);
            }
            else
            {
                //编辑操作
                bll.UpData(sysUser,roleids);
            }
            return Json(new { msgCode = 200 });
        }

        #region 删除用户信息
        [HttpPost]
        public ActionResult DelUser(string rowid)
        {
            if (!string.IsNullOrEmpty(rowid))
            {
                var list = rowid.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SysUserBll bll = new SysUserBll();
                foreach (string t in list)
                {
                    try
                    {
                        bll.DelData(Convert.ToInt32(t));
                    }
                    catch (Exception ex)
                    {

                    }

                }
                return Json(new { msgCode = 200 });
            }
            else
                return Json(new { msgCode = 500 });

        }
        #endregion














        public ActionResult SysRole()
        {
            StringBuilder strB = new StringBuilder();
            strB.Append("<tr><td><input type=\"checkbox\" name=\"IDCheck\" value=\"14458579642011\" class=\"acb\" /></td>");
            strB.Append("<td>城中区</td> <td>瑞景河畔16号楼1-111</td><td>65.97㎡</td><td>65.97㎡</td><td>一室一厅一卫</td><td>混凝土</td>");
            strB.Append("<td>公租房</td><td>建成待租</td><td><a href=\"house_edit.html?fyID=14458579642011\" class=\"edit\">编辑</a>");
            strB.Append("<a href=\"javascript:del('14458579642011');\">删除</a></td></tr>");

            strB.Append("<tr><td><input type=\"checkbox\" name=\"IDCheck\" value=\"14458579642012\" class=\"acb\" /></td>");
            strB.Append("<td>城中区</td> <td>瑞景河畔16号楼1-112</td><td>65.97㎡</td><td>65.97㎡</td><td>一室一厅一卫</td><td>混凝土</td>");
            strB.Append("<td>公租房</td><td>建成待租</td><td><a href=\"house_edit.html?fyID=14458579642012\" class=\"edit\">编辑</a>");
            strB.Append("<a href=\"javascript:del('14458579642012');\">删除</a></td></tr>");

            strB.Append("<tr><td><input type=\"checkbox\" name=\"IDCheck\" value=\"14458579642013\" class=\"acb\" /></td>");
            strB.Append("<td>城中区</td> <td>瑞景河畔16号楼1-113</td><td>65.97㎡</td><td>65.97㎡</td><td>一室一厅一卫</td><td>混凝土</td>");
            strB.Append("<td>公租房</td><td>建成待租</td><td><a href=\"house_edit.html?fyID=14458579642013\" class=\"edit\">编辑</a>");
            strB.Append("<a href=\"javascript:del('14458579642011');\">删除</a></td></tr>");

            strB.Append("<tr><td><input type=\"checkbox\" name=\"IDCheck\" value=\"14458579642011\" class=\"acb\" /></td>");
            strB.Append("<td>城中区</td> <td>瑞景河畔16号楼1-114</td><td>65.97㎡</td><td>65.97㎡</td><td>一室一厅一卫</td><td>混凝土</td>");
            strB.Append("<td>公租房</td><td>建成待租</td><td><a href=\"house_edit.html?fyID=14458579642011\" class=\"edit\">编辑</a>");
            strB.Append("<a href=\"javascript:del('14458579642011');\">删除</a></td></tr>");

            strB.Append("<tr><td><input type=\"checkbox\" name=\"IDCheck\" value=\"14458579642011\" class=\"acb\" /></td>");
            strB.Append("<td>城中区</td> <td>瑞景河畔16号楼1-115</td><td>65.97㎡</td><td>65.97㎡</td><td>一室一厅一卫</td><td>混凝土</td>");
            strB.Append("<td>公租房</td><td>建成待租</td><td><a href=\"house_edit.html?fyID=14458579642011\" class=\"edit\">编辑</a>");
            strB.Append("<a href=\"javascript:del('14458579642011');\">删除</a></td></tr>");
            ViewBag.SysUserList = strB.ToString();
            return View();
        }

        #region 分页
        [HttpPost]
        public JsonResult GetSysRoleList(int? pageIndex, int? pageSize, string RoleName)
        {
            SysRoleBll bll = new SysRoleBll();
            int recordCounts = 0;
            var result = bll.GetSysRoleList(pageIndex, pageSize, RoleName, ref recordCounts);

            return Json(new { messCode = 200, recordCount = recordCounts, data = result });

        }

        #endregion

        [HttpGet]
        public ActionResult RoleEdit(int rowid)
        {
            ViewBag.RowID = rowid;
            StringBuilder strB = new StringBuilder();
            if (rowid == 0)
            {
                //新增视图
                strB.Append("<tr><td class=\"ui_text_rt\">角色名称</td><td class=\"ui_text_lt\"><input type=\"text\" id=\"RoleName\" name=\"RoleName\" value=\"瑞景河畔16号楼1-112\" class=\"ui_input_txt02\" />");
                strB.Append("</td></tr>");
            }
            else
            {
                //编辑视图
                strB.Append("<tr><td class=\"ui_text_rt\">角色名称</td><td class=\"ui_text_lt\"><input type=\"text\" id=\"RoleName\" name=\"RoleName\" value=\"修改瑞景河畔16号楼1-112\" class=\"ui_input_txt02\" />");
                strB.Append("</td></tr>");
            }
            ViewBag.RoleInfo = strB.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult RoleOperation(string data)
        {
            SysRoleModel sysRole = JsonConvert.DeserializeObject<SysRoleModel>(data);
            SysRoleBll bll = new SysRoleBll();
            if (sysRole.RowID == null || sysRole.RowID == 0)
            {
                //新增操作
                bll.InsertData(sysRole);
            }
            else
            {
                //编辑操作
                bll.UpData(sysRole);
            }
            return Json(new { msgCode = 200 });
        }

        #region 删除信息
        [HttpPost]
        public ActionResult DelRole(string rowid)
        {
            if (!string.IsNullOrEmpty(rowid))
            {
                var list = rowid.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SysRoleBll bll = new SysRoleBll();
                foreach (string t in list)
                {
                    try
                    {
                        bll.DelData(Convert.ToInt32(t));
                    }
                    catch (Exception ex)
                    {

                    }

                }
                return Json(new { msgCode = 200 });
            }
            else
                return Json(new { msgCode = 500 });

        }
        #endregion
    }
}