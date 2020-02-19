using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xz_ypcl_dal;
using xz_ypcl_model;

namespace xz_ypcl_bll
{
    public class ProductBll
    {
        #region
        /// <summary>
        /// PC端套餐集合分页查询
        /// 请勿删除
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<ProductModel> GetProductList(int? pageIndex, int? pageSize, string ProductName, ref int recordCount)
        {
            ProductDal dal = new ProductDal();
            recordCount = dal.GetCount(ProductName);
            return dal.GetProductList(pageIndex, pageSize, ProductName);
        }
        #endregion


        public bool InsertData(ProductModel newModel)
        {
            ProductDal dal = new ProductDal();
            return dal.InsertData(newModel);
        }
        /// <summary>
        /// 修改记录
        /// 请勿删除
        /// </summary>
        /// <returns></returns>
        public bool UpData(ProductModel newModel)
        {
            ProductDal dal = new ProductDal();
            return dal.UpData(newModel);
        }



        public int DelData(int rowid)
        {
            ProductDal dal = new ProductDal();
            return dal.DelData(rowid);
        }

    }
}
