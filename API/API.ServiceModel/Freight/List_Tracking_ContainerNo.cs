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
    [Route("/freight/tracking/ContainerNo/{ContainerNo}", "Get")]
    [Route("/freight/tracking/ContainerNo/count/{ContainerNoToCount}", "Get")]
    [Route("/freight/tracking/ContainerNo/module/{JobNo}", "Get")]
    public class List_Tracking_ContainerNo : IReturn<CommonResponse>
    {
        public string ContainerNo { get; set; }
        public string ContainerNoToCount { get; set; }
        public string JobNo { get; set; }
    }
    public class List_Tracking_ContainerNo_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<Jmjm1> GetList(List_Tracking_ContainerNo request)
        {
            List<Jmjm1> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (!string.IsNullOrEmpty(request.ContainerNoToCount))
                    {
                        Result = db.Select<Jmjm1>(
                           "Select Jmjm1.JobNo,Jmjm1.JobType,Jmjm1.ModuleCode from Jmjm1 left Join Jmjt1 on Jmjm1.JobType=Jmjt1.JobType where charindex('" + request.ContainerNoToCount + ",',isnull(ContainerNo,'')+',')>0"
                       );
                    }
                    else if (!string.IsNullOrEmpty(request.ContainerNo))
                    {
                        Result = db.Select<Jmjm1>(
                            "Select * from Jmjm1 Where ContainerNo LIKE '%" + request.ContainerNo + "%' Order by Jmjm1.JobDate Desc"
                        );
                    }
                    else if (!string.IsNullOrEmpty(request.JobNo))
                    {
                        Result = db.Select<Jmjm1>(
                            "select * from Jmjm1 Where JobNo='{0}'",request.ContainerNo
                        );
                    }                    
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
