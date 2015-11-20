using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.OrmLite;
using WebApi.ServiceModel.Tables;

namespace WebApi.ServiceModel.Freight
{
    [Route("/freight/rcbp3/{BusinessPartyCode}/{LineItemNo}", "Post")]
    public class Update_Rcbp3 : IReturn<CommonResponse>
    {
        public string BusinessPartyCode { get; set; }
        public int LineItemNo { get; set; }
    }
    public class Update_Rcbp3_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public int GetList(List_Rcbp3 request)
        {
            int Result = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    //Result = db.Where<Rcbp3>(r1 => r1.BusinessPartyCode == request.BusinessPartyCode);
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
