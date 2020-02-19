using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xz_ypcl_bll;

namespace xz_ypcl.Controllers
{
    public class HyController : Controller
    {
        //
        // GET: /Hy/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HyList() { return View(); }

        #region 分页
        [HttpPost]
        public JsonResult GetHyList(int? pageIndex, int? pageSize, string HyName)
        {
            HyBll bll = new HyBll();
            int recordCounts = 0;
            var result = bll.GetHyDtoList(pageIndex, pageSize, HyName, ref recordCounts);

            return Json(new { messCode = 200, recordCount = recordCounts, data = result });

        }

        #endregion


        public ActionResult HyLog() { return View(); }

        #region 分页
        [HttpPost]
        public JsonResult GetHyLogList(int? pageIndex, int? pageSize, string HyName)
        {
            HyBll bll = new HyBll();
            int recordCounts = 0;
            var result = bll.GetHyLogDtoList(pageIndex, pageSize, HyName, ref recordCounts);

            return Json(new { messCode = 200, recordCount = recordCounts, data = result });

        }

        #endregion
	}
}