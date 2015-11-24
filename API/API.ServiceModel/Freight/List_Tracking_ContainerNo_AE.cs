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
    [Route("/freight/tracking/ContainerNo/AE/{JobNo}", "Get")]
    public class List_Tracking_ContainerNo_AE : IReturn<CommonResponse>
    {
        public string JobNo { get; set; }
    }
    public class List_Tracking_ContainerNo_AE_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<Tracking_ContainerNo_AE> GetList(List_Tracking_ContainerNo_AE request)
        {
            List<Tracking_ContainerNo_AE> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Select<Tracking_ContainerNo_AE>(
                        "select c.FirstToDestCode,c.FirstByAirlineID,c.FirstFlightNo,c.FirstFlightDate," +
                        "c.SecondToDestCode,c.SecondByAirlineID,c.SecondFlightNo,c.SecondFlightDate," +
	                      "c.ThirdToDestCode,c.ThirdByAirlineID,c.ThirdFlightNo,c.ThirdFlightDate," +
                        "a.ModuleCode,a.JobNo,a.JobType, a.CustomerRefNo as ReferenceNo,a.AwbBlNo,a.MawbOBLNo,a.OriginCode,a.DestCode," +
	                      "a.Pcs,a.GrossWeight,a.Volume,a.CommodityDescription as Commodity " +
	                      "From Jmjm1 a Left Join Aeaw1 c on c.AwbNo=a.AwbBlNo " +
                        "Where a.ModuleCode='AE' and a.JobNo='{0}'",request.JobNo
                    );
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
