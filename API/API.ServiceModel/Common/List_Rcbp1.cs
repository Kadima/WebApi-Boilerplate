using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.OrmLite;
using WebApi.ServiceModel.Tables;

namespace WebApi.ServiceModel.Common
{
    [Route("/wms/action/list/rcbp1", "Get")]
    [Route("/wms/action/list/rcbp1/{BusinessPartyName}", "Get")]
    [Route("/freight/rcbp1", "Get")]
    [Route("/freight/rcbp1/{BusinessPartyName}", "Get")]
    [Route("/freight/rcbp1/TrxNo/{TrxNo}", "Get")]
    public class List_Rcbp1 : IReturn<CommonResponse>
    {
        public string TrxNo { get; set; }
        public string BusinessPartyName { get; set; }
    }
    public class List_Rcbp1_Logic
    {
        
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<Rcbp1> GetList(List_Rcbp1 request)
        {
            List<Rcbp1> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (!string.IsNullOrEmpty(request.BusinessPartyName))
                    {
                        Result = db.SelectParam<Rcbp1>(
                            q=> q.StatusCode != null && q.StatusCode !="DEL" && q.BusinessPartyName.StartsWith(request.BusinessPartyName)
                        );
                    }
                    else if (!string.IsNullOrEmpty(request.TrxNo))
                    {
                        Result = db.SelectParam<Rcbp1>(
                            q=> q.StatusCode != null && q.StatusCode !="DEL" && q.TrxNo == int.Parse(request.TrxNo)                           
                        );
                    }
                    else
                    {
                        Result = db.SelectParam<Rcbp1>(
                            q=> q.StatusCode != null && q.StatusCode !="DEL"
                        ).OrderBy(q => q.BusinessPartyCode).Take(20).ToList<Rcbp1>();
                    }
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
