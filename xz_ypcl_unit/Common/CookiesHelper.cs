using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace xz_ypcl_unit.Common
{
    public class CookiesHelper
    {
        #region Cookies
        public static void SetCookieValue(string key, string value)
        {
            HttpCookie cookie = new HttpCookie(key, value);
            cookie.Expires = DateTime.Now.AddDays(30);
            //cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static string GetCookieValue(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] == null)
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Request.Cookies[key].Value;
            }
        }
        /// <summary>
        /// 移出cookie
        /// </summary>
        /// <param name="key"></param>
        public static void DelCookie()
        {
            foreach (string cookiename in HttpContext.Current.Request.Cookies.AllKeys)
            {
                HttpCookie cookies = HttpContext.Current.Request.Cookies[cookiename];
                if (cookies != null)
                {
                    cookies.Expires = DateTime.Today.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(cookies);
                    HttpContext.Current.Request.Cookies.Remove(cookiename);
                }
            }   
        }
        #endregion
    }
}
