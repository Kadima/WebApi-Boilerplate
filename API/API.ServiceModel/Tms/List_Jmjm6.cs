using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.OrmLite;

namespace WebApi.ServiceModel.Tms
{
    [Route("/event/action/list/jmjm6/{jobno}", "Get")]
    public class List_Jmjm6 : IReturn<CommonResponse>
    {
        public string JobNo { get; set; }
    }
    public class List_Jmjm6_Response
    {
        public string JobNo { get; set; }
        public int LineItemNo { get; set; }
        public string ContainerNo { get; set; }
        public string Remark { get; set; }
        public string JobType { get; set; }
        public string VehicleNo { get; set; }
        public string DriverNo { get; set; }
        public string CargoStatusCode { get; set; }
        public DateTime TruckDateTime { get; set; }
        public DateTime RecevieDateTime { get; set; }
        public DateTime ReadyDateTime { get; set; }
        public DateTime UnLoadDateTime { get; set; }
    }
    public class List_Jmjm6_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<List_Jmjm6_Response> GetList(List_Jmjm6 request)
        {
            List<List_Jmjm6_Response> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Select<List_Jmjm6_Response>(
                        "select * from Jmjm6 Left Join Jmjm1 on Jmjm6.JobNo=Jmjm1.JobNo WHERE Jmjm1.StatusCode<>'DEL' And Jmjm1.JobNo={0}", request.JobNo
                    );
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
