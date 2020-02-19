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
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList() {
            return View();
        }
 
        #region 分页
        [HttpPost]
        public JsonResult GetProductList(int? pageIndex, int? pageSize, string ProductName)
        {
            ProductBll bll = new ProductBll();
            int recordCounts = 0;
            var result = bll.GetProductList(pageIndex, pageSize, ProductName, ref recordCounts);

            return Json(new { messCode = 200, recordCount = recordCounts, data = result });

        }

        #endregion

        [HttpGet]
        public ActionResult ProductEdit(int rowid)
        {
            ViewBag.RowID = rowid;
            StringBuilder strB = new StringBuilder();
            if (rowid == 0)
            {
                //新增视图
                strB.Append("<tr><td class=\"ui_text_rt\">用户名</td><td class=\"ui_text_lt\"><input type=\"text\" id=\"UserName\" name=\"UserName\" value=\"瑞景河畔16号楼1-112\" class=\"ui_input_txt02\" />");
                strB.Append("</td></tr><tr><td class=\"ui_text_rt\">密码</td><td class=\"ui_text_lt\"><input type=\"text\" id=\"PassWord\" name=\"PassWord\" value=\"城中区\" class=\"ui_input_txt02\" />");
                strB.Append("</td></tr>");
            }
            else
            {
                //编辑视图
                strB.Append("<tr><td class=\"ui_text_rt\">用户名</td><td class=\"ui_text_lt\"><input type=\"text\" id=\"UserName\" name=\"UserName\" value=\"修改瑞景河畔16号楼1-112\" class=\"ui_input_txt02\" />");
                strB.Append("</td></tr><tr><td class=\"ui_text_rt\">密码</td><td class=\"ui_text_lt\"><input type=\"text\" id=\"PassWord\" name=\"PassWord\" value=\"修改城中区\" class=\"ui_input_txt02\" />");
                strB.Append("</td></tr>");
            }
            ViewBag.ProductInfo = strB.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult ProductOperation(string data)
        {
            ProductModel productModel = JsonConvert.DeserializeObject<ProductModel>(data);
            ProductBll bll = new ProductBll();
            if (productModel.RowID == null || productModel.RowID == 0)
            {
                //新增操作
                bll.InsertData(productModel);
            }
            else
            {
                //编辑操作
                bll.UpData(productModel);
            }
            return Json(new { msgCode = 200 });
        }

        #region 删除信息
        [HttpPost]
        public ActionResult DelProduct(string rowid)
        {
            if (!string.IsNullOrEmpty(rowid))
            {
                var list = rowid.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                ProductBll bll = new ProductBll();
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