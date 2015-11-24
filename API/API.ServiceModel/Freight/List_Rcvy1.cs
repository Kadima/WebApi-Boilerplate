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
    [Route("/freight/rcvy1", "Get")]
    [Route("/freight/rcvy1/{PortOfDischargeName}", "Get")]
    [Route("/freight/rcvy1/sps/{PortOfDischargeName}", "Get")]
    public class List_Rcvy1 : IReturn<CommonResponse>
    {
        public string PortOfDischargeName { get; set; }
    }
    public class List_Rcvy1_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public HashSet<string> GetList(List_Rcvy1 request)
        {
            HashSet<string> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (!string.IsNullOrEmpty(request.PortOfDischargeName))
                    {
                        Result = db.HashSet<string>(
                            "Select PortOfDischargeName from rcvy1 where PortOfDischargeName is not null and PortOfDischargeName <> '' and PortOfDischargeName LIKE '" + request.PortOfDischargeName + "%'"
                        );
                    }
                    else
                    {
                        Result = db.HashSet<string>(
                            "Select PortOfDischargeName from rcvy1 where PortOfDischargeName is not null and PortOfDischargeName<>''"
                        );
                    }
                }
            }
            catch { throw; }
            return Result;
        }
        public List<Rcvy1_sps> GetSpsList(List_Rcvy1 request)
        {
            List<Rcvy1_sps> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    string strSQL = "EXEC sps_Track_Rcvy1_List @intUserId=0,@PageSize=0,@PageCount=0,@RecordCount=0,@strWhere='And PortofDischargeName=''" + request.PortOfDischargeName + "'''";
                    Result = db.SqlList<Rcvy1_sps>(strSQL);
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
