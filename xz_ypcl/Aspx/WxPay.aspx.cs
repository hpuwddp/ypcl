using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using xz_ypcl_unit.Pay.WxPay;

namespace xz_ypcl.Aspx
{
    public partial class WxPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NativePay nativePay = new NativePay();
            LogHelper.WriteLog("进入页面开始操作");
            //生成扫码支付模式二url
            string url2 = nativePay.GetPayUrl("123456789");
            LogHelper.WriteLog("url2" + url2);

            Image2.ImageUrl = "MakeQRCode.aspx?data=" + HttpUtility.UrlEncode(url2);
        }
    }
}