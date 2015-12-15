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
    [Route("/freight/tracking/ContainerNo/SI/{JobNo}", "Get")]
    public class List_Tracking_ContainerNo_SI : IReturn<CommonResponse>
    {
        public string JobNo { get; set; }
    }
    public class List_Tracking_ContainerNo_SI_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<Tracking_ContainerNo_SI> GetList(List_Tracking_ContainerNo_SI request)
        {
            List<Tracking_ContainerNo_SI> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Select<Tracking_ContainerNo_SI>(
                        "Select c.VesselName,c.VoyageNo,c.FeederVesselName,c.FeederVoyage,a.ModuleCode," +
                        "a.JobNo,a.JobType,a.CustomerRefNo as ReferenceNo,a.AwbBlNo,a.MawbOBLNo,a.OriginCode,a.DestCode," +
                        "a.Pcs,a.GrossWeight,a.Volume,a.CommodityDescription as Commodity,a.ETD,a.ETA," +
																								"a.PortofLoadingName,a.PortofDischargeName,a.Noof20FtContainer,a.Noof40FtContainer,a.Noof45FtContainer,a.ContainerNo," +
																								"a.OriginName, a.DestName " +
                        "From Jmjm1 a Left Join Sebl1 c on c.BlNo=a.AwbBlNo " +
                        "Where a.ModuleCode='SI'  and a.JobNo='" + request.JobNo + "'"
                    );
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
