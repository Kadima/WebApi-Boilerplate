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
    [Route("/freight/plcp1", "Post")]
    public class Update_Plcp1 : IReturn<CommonResponse>
    {
        public Plcp1 plcp1 { get; set; }
    }
    public class Update_Plcp1_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public int UpdateResult(Update_Plcp1 request)
        {
            int Result = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Update<Plcp1>(
                        new
                        {
                            StatusCode = request.plcp1.StatusCode
                        },
                        p => p.TrxNo == request.plcp1.TrxNo
                    );
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
