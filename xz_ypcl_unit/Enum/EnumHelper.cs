using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xz_ypcl_unit.Enum
{
    public class EnumHelper
    {
        public class SeItem
        {
            public string value { get; set; }
            public string text { get; set; }
        }

        /// <summary>
        /// 性别信息
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public static string GetSexList(string currentSexValue)
        {
            List<SeItem> list = new List<SeItem>();
            list.Add(new SeItem { value = "男", text = "男" });
            list.Add(new SeItem { value = "女", text = "女" });
            list.Add(new SeItem { value = "保密", text = "保密" });
            StringBuilder sb = new StringBuilder();
            string CusTemplate = "<div class=\"radio-box\"><input name=\"Sex\" type=\"radio\" value=\"{0}\" {1}> <label>{2}</label></div>";
            if (list != null && list.Count > 0)
            {
                foreach (SeItem cus in list)
                {
                    sb.AppendFormat(CusTemplate, cus.value, cus.value == currentSexValue ? " checked " : string.Empty, cus.text);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 产品类别信息
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public static string GetProductTypeList(string currentSexValue)
        {
            List<SeItem> list = new List<SeItem>();
            list.Add(new SeItem { value = "一般化学品", text = "一般化学品" });
            list.Add(new SeItem { value = "危险化学品", text = "危险化学品" });
            StringBuilder sb = new StringBuilder();
            string CusTemplate = "<div class=\"radio-box\"><input name=\"ProductType\" type=\"radio\" value=\"{0}\" {1}> <label>{2}</label></div>";
            if (list != null && list.Count > 0)
            {
                foreach (SeItem cus in list)
                {
                    sb.AppendFormat(CusTemplate, cus.value, cus.value == currentSexValue ? " checked " : string.Empty, cus.text);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 产品等级信息
        /// 请勿删除
        /// </summary>
        /// <param name="currentSexValue"></param>
        /// <returns></returns>
        public static string GetProductLevelList(string currentSexValue)
        {
            List<SeItem> list = new List<SeItem>();
            list.Add(new SeItem { value = "一级", text = "一级" });
            list.Add(new SeItem { value = "二级", text = "二级" });
            list.Add(new SeItem { value = "三级", text = "三级" });
            list.Add(new SeItem { value = "四级", text = "四级" });
            StringBuilder sb = new StringBuilder();
            string CusTemplate = "<div class=\"radio-box\"><input name=\"ProductLevel\" type=\"radio\" value=\"{0}\" {1}> <label>{2}</label></div>";
            if (list != null && list.Count > 0)
            {
                foreach (SeItem cus in list)
                {
                    sb.AppendFormat(CusTemplate, cus.value, cus.value == currentSexValue ? " checked " : string.Empty, cus.text);
                }
            }
            return sb.ToString();
        }
    }
}
