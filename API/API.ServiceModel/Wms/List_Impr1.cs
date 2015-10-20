using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace WmsWS.ServiceModel.Wms
{
    [Route("/wms/action/list/impr1/{BarCode}", "Get")]
    public class List_Impr1 : IReturn<CommonResponse>
    {
        public string BarCode { get; set; }
    }
    public class Impr1
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string SerialNoFlag { get; set; }
        public string UserDefine01 { get; set; }
        public string StatusCode { get; set; }
    }
    public class List_Impr1_Logic
    {        
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public Impr1 GetList(List_Impr1 request)
        {
            Impr1 Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Single<Impr1>(i1 => i1.ProductCode != null && i1.ProductCode != "" && i1.StatusCode != null && i1.StatusCode != "DEL" && i1.UserDefine01 == request.BarCode);
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
