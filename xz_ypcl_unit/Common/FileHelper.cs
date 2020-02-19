using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace xz_ypcl_unit.meBase.Common
{
   public class FileHelper
    {
  
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">通过form表达提交的文件</param>
        /// <param name="virpath">文件要保存的虚拟路径</param>
        public static void uploadFile(HttpPostedFile file, string virpath)
        {
            if (file.ContentLength > 1024 * 1024 * 6)
            {
                throw new Exception("文件不能大于6M");
            }
            string imgtype = Path.GetExtension(file.FileName);
            //imgtype对上传的文件进行限制
            //if (imgtype != ".zip" && imgtype != ".mp3")
            //{
            //    throw new Exception("只允许上传zip、rar....文件");
            //}
            string dirFullPath = HttpContext.Current.Server.MapPath(virpath);
            if (!Directory.Exists(dirFullPath))//如果文件夹不存在，则先创建文件夹
            {
                Directory.CreateDirectory(dirFullPath);
            }
            file.SaveAs(dirFullPath + file.FileName);
        }
    }
}
