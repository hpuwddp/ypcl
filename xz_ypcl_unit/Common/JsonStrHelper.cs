using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace xz_ypcl_unit.Common
{
    public class JsonStrHelper
    {
        /// <summary>
        /// 返回List集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JsonStr"></param>
        /// <returns></returns>
        public static List<T> JSONStringToList<T>(string JsonStr)
        {

            JavaScriptSerializer Serializer = new JavaScriptSerializer();

            List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
            return objs;

        }


        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {

            T obj = Activator.CreateInstance<T>();

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());

                return (T)serializer.ReadObject(ms);

            }

        }
    }
}
