using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xz_ypcl_bll;

namespace xz_ypcl.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderList() { return View(); }

        #region 分页
        [HttpPost]
        public JsonResult GetOrderList(int? pageIndex, int? pageSize, string HyName)
        {
            OrderBll bll = new OrderBll();
            int recordCounts = 0;
            var result = bll.GetOrderDtoList(pageIndex, pageSize, HyName, ref recordCounts);

            return Json(new { messCode = 200, recordCount = recordCounts, data = result });

        }

        #endregion

         

        #region 删除信息
        //[HttpPost]
        //public ActionResult DelProduct(string rowid)
        //{
        //    if (!string.IsNullOrEmpty(rowid))
        //    {
        //        var list = rowid.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //        OrderBll bll = new OrderBll();
        //        foreach (string t in list)
        //        {
        //            try
        //            {
        //                bll.DelData(Convert.ToInt32(t));
        //            }
        //            catch (Exception ex)
        //            {

        //            }

        //        }
        //        return Json(new { msgCode = 200 });
        //    }
        //    else
        //        return Json(new { msgCode = 500 });

        //}
        #endregion


	}
}