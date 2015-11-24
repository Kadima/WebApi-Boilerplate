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
    [Route("/freight/tracking/count/{FilterName}/{FilterValue}", "Get")]
    public class List_Tracking : IReturn<CommonResponse>
    {
        public string FilterName { get; set; }
        public string FilterValue { get; set; }
    }
    public class List_Tracking_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public int GetCount(List_Tracking request)
        {
            int Result = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (request.FilterName.ToUpper() == "ContainerNo".ToUpper())
                    {
                        List<Jmjm1> ResultJmjm1 = db.Select<Jmjm1>(
                            "Select Jmjm1.JobNo,Jmjm1.JobType,Jmjm1.ModuleCode from Jmjm1 left Join Jmjt1 on Jmjm1.JobType=Jmjt1.JobType where charindex('" + request.FilterValue + ",',isnull(ContainerNo,'')+',')>0"
                        );
                        Result = ResultJmjm1.Count;
                    }
                    else if (request.FilterName.ToUpper() == "JobNo".ToUpper())
                    {

                    }
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
