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
        public List<Plcp1> plcp1s { get; set; }
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
																				foreach (Plcp1 p1 in request.plcp1s)
																				{
																								db.Update<Plcp1>(
																												new
																												{
																																StatusCode = p1.StatusCode
																												},
																												p => p.TrxNo == p1.TrxNo
																								);
																				}
																				Result = 1;
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
