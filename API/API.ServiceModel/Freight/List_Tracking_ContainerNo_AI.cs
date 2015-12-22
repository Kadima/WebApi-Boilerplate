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
    [Route("/freight/tracking/ContainerNo/AI/{JobNo}", "Get")]
    public class List_Tracking_ContainerNo_AI : IReturn<CommonResponse>
    {
        public string JobNo { get; set; }
    }
    public class List_Tracking_ContainerNo_AI_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<Tracking_ContainerNo_AI> GetList(List_Tracking_ContainerNo_AI request)
        {
            List<Tracking_ContainerNo_AI> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Select<Tracking_ContainerNo_AI>(
                        "Select a.ModuleCode,a.JobNo,a.JobType,a.CustomerRefNo as ReferenceNo,a.AwbBlNo,a.MawbOBLNo,a.OriginCode,a.DestCode," +
																								"a.Pcs,a.GrossWeight,a.Volume,a.CommodityDescription as Commodity, (Select Top 1 UomDescription From Rcum1 Where UomCode=a.UomCode) AS UomDescription " +
                        "From Jmjm1 a Left Join Aiaw1 c on c.AwbNo=a.AwbBlNo " +
                        "Where a.ModuleCode='AI' and a.JobNo='" + request.JobNo + "'"
                    );
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
