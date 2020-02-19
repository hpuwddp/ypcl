using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using xz_ypcl_unit.Pay.WxPay;

namespace xz_ypcl.Aspx.Pay
{
    public partial class WxPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NativePay nativePay = new NativePay();
 
            //生成扫码支付模式二url
            string url2 = nativePay.GetPayUrl("123456789");

             
            Image2.ImageUrl = "MakeQRCode.aspx?data=" + HttpUtility.UrlEncode(url2);
        }
    }
}