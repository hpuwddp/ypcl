using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xz_ypcl_unit.Common
{
    public class LogHelper
    {
        /// <summary>
        /// 生成日志
        /// </summary>
        /// <param name="text"></param>
        public static void WriteLog(string text)
        {
            string logPath = "D:\\Log\\Pay.log";
            using (StreamWriter sw = File.AppendText(logPath))
            {
                sw.WriteLine("消息：" + text);
                sw.WriteLine("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.WriteLine("**************************************************");
                sw.WriteLine();
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }
    }
}
