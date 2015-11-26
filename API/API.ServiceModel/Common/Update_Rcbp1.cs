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
    [Route("/freight/rcbp1", "Post")]
    public class Update_Rcbp1 : IReturn<CommonResponse>
    {
        public Rcbp1 rcbp1 { get; set; }
    }
    public class Update_Rcbp1_Logic
    {
        
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public int UpdateResult(Update_Rcbp1 request)
        {
            int Result = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Update<Rcbp1>(
                        new {
                            BusinessPartyName = request.rcbp1.BusinessPartyName,
                            Address1 = request.rcbp1.Address1,
                            Address2 = request.rcbp1.Address2,
                            Address3 = request.rcbp1.Address3,
                            Address4 = request.rcbp1.Address4,
                            Email = request.rcbp1.Email,
                            WebSite = request.rcbp1.WebSite
                        },
                        p => p.TrxNo == request.rcbp1.TrxNo
                    );  
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
