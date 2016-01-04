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
				[Route("/freight/tracking/sps/{FilterName}/{RecordCount}/{FilterValue}", "Get")]
				[Route("/freight/tracking/{FilterName}/{FilterValue}", "Get")]
				[Route("/freight/tracking/{FilterName}/{ModuleCode}/{FilterValue}", "Get")]

    public class List_Tracking : IReturn<CommonResponse>
    {
        public string FilterName { get; set; }
								public string FilterValue { get; set; }
								public string ModuleCode { get; set; }
								public string RecordCount { get; set; }
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
                            "Select Jmjm1.JobNo,Jmjm1.JobType,Jmjm1.ModuleCode From Jmjm1 Left Join Jmjt1 on Jmjm1.JobType=Jmjt1.JobType Where charindex('" + request.FilterValue + ",',ISNULL(ContainerNo,'')+',')>0"
                        );
                        Result = ResultJmjm1.Count;
                    }
																				else if (request.FilterName.ToUpper() == "OrderNo".ToUpper())
																				{
																								List<Omtx1> ResultOmtx1 = db.Select<Omtx1>(
																												"Select Top 1 TrxNo From Omtx1 Where OrderNo='" + request.FilterValue + "'"
																								);
																								Result = ResultOmtx1.Count;
																				}
																				else
																				{
																								List<Jmjm1> ResultJmjm1 = db.Select<Jmjm1>(
																												"Select Jmjm1.JobNo,Jmjm1.JobType,Jmjm1.ModuleCode From Jmjm1 Left Join Jmjt1 on Jmjm1.JobType=Jmjt1.JobType Where " + request.FilterName + "='" + request.FilterValue + "'"
																								);
																								Result = ResultJmjm1.Count;
																				}
                }
            }
            catch { throw; }
            return Result;
        }
								public List<Jmjm1> GetSpsList(List_Tracking request)
								{
												List<Jmjm1> Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				if (!string.IsNullOrEmpty(request.FilterValue))
																				{
																								int count = int.Parse(request.RecordCount);
																								string strWhere = "";
																								string strSelect = "";
																								string strOrderBy = "";
																								string strSQL = "";
																								if (request.FilterName == "ContainerNo")
																								{
																												strWhere = " Where ContainerNo LIKE '%" + request.FilterValue + "%'";
																												strSelect = "SELECT " +
																																"j1.*,(Select Top 1 UomDescription From Rcum1 Where UomCode=j1.UomCode) AS UomDescription " +
																																"FROM Jmjm1 j1, " +
																																"(SELECT TOP " + (count + 10) + " row_number() OVER (ORDER BY JobNo ASC, JobDate DESC) n, JobNo FROM Jmjm1 " + strWhere + ") j2 " +
																																"WHERE j1.JobNo = j2.JobNo AND j2.n > " + count;
																												strOrderBy = " ORDER BY j2.n ASC";
																												strSQL = strSelect + strOrderBy;
																												Result = db.Select<Jmjm1>(strSQL);
																								}
																								else if (request.FilterName == "OrderNo")
																								{

																								}
																								else
																								{
																												strWhere = " Where " + request.FilterName + "='" + request.FilterValue + "'";
																												strSelect = "SELECT " +
																																"j1.*,(Select Top 1 UomDescription From Rcum1 Where UomCode=j1.UomCode) AS UomDescription " +
																																"FROM Jmjm1 j1, " +
																																"(SELECT TOP " + (count + 10) + " row_number() OVER (ORDER BY JobNo ASC, JobDate DESC) n, JobNo FROM Jmjm1 " + strWhere + ") j2 " +
																																"WHERE j1.JobNo = j2.JobNo AND j2.n > " + count;
																												strOrderBy = " ORDER BY j2.n ASC";
																												strSQL = strSelect + strOrderBy;
																												Result = db.Select<Jmjm1>(strSQL);
																								}
																				}
																}
												}
												catch { throw; }
												return Result;
								}
								public List<Tracking_OrderNo> GetOmtx1List(List_Tracking request)
								{
												List<Tracking_OrderNo> Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				List<Omtx1> ResultOmtx1 = db.Select<Omtx1>(
																												"Select Top 1 TrxNo From Omtx1 Where OrderNo='" + request.FilterValue + "'"
																								);
																				if (ResultOmtx1.Count > 0)
																				{
																								int TrxNo = ResultOmtx1[0].TrxNo;
																								Result = db.Select<Tracking_OrderNo>(
																												"Select TrxNo," +
																												"(Select PortName From Rcsp1 Where Rcsp1.PortCode=a.PortOfLoadingCode) AS PortOfLoadingName," +
																												"(Select PortName From Rcsp1 Where Rcsp1.PortCode=a.PortOfDischargeCode) AS PortOfDischargeName," +
																												"(Select DeliveryTypeName From Rcdl1 Where Rcdl1.DeliveryType=a.DeliveryType) AS DeliveryTypeName," +
																												"(Select CityName From Rcct1 Where Rcct1.CityCode=a.DestCode) AS DestName," +
																												"(Select CityName From Rcct1 Where Rcct1.CityCode=a.OriginCode) AS OriginName," +
																												"(Select Top 1 BookingNo From Sebk1 Where Sebk1.CustomerRefNo=a.OrderNo and OrderNo!='') AS BookingNo ," +
																												"(Select Top 1 JobNo From  Sebk1 Where Sebk1.CustomerRefNo=a.OrderNo and OrderNo!='') AS JobNo ," +
																												"(Select  AirportName From Rcap1 Where Rcap1.AirportCode =a.airportDeptCode) AS AirportDeptName," +
																												"(Select  AirportName From Rcap1 Where Rcap1.AirportCode =a.airportDestCode) AS AirportDestName," +
																												"CommodityDescription " +
																												"From Omtx1 a " +
																												"Where TrxNo=" + TrxNo
																								);
																				}																				
																}
												}
												catch { throw; }
												return Result;
								}
								public List<Omtx3> GetOmtx3List(List_Tracking request)
								{
												List<Omtx3> Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{

																				if (request.FilterName.ToUpper() == "OrderNo".ToUpper())
																				{
																								Result = db.Select<Omtx3>(
																												"Select TrxNo, LineItemNo, Pcs, GrossWeight, Length, Height, Width, Pcs*Length*Width*Height AS Volume " +
																												"From Omtx3 Where TrxNo=" + request.FilterValue
																								);
																				}
																}
												}
												catch { throw; }
												return Result;
								}
								public List<Jmjm1> GetList(List_Tracking request)
								{
												List<Jmjm1> Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				if (!string.IsNullOrEmpty(request.ModuleCode))
																				{
																								Result = db.Select<Jmjm1>(
																												"Select *,(Select Top 1 UomDescription From Rcum1 Where UomCode=Jmjm1.UomCode) AS UomDescription From Jmjm1 Where ModuleCode='" + request.ModuleCode + "' And JobNo='" + request.FilterValue + "' Order By Jmjm1.JobNo Asc,Jmjm1.JobDate Desc"
																								);
																				}
																}
												}
												catch { throw; }
												return Result;
								}
								public List<Tracking_ContainerNo_AE> GetAEList(List_Tracking request)
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
																								"a.ModuleCode,a.JobNo,a.JobType, a.CustomerRefNo as ReferenceNo,a.AwbBlNo,a.MawbOBLNo,a.OriginCode,a.DestCode,a.OriginName,a.DestName," +
																							"a.Pcs,a.GrossWeight,a.Volume,a.CommodityDescription as Commodity, (Select Top 1 UomDescription From Rcum1 Where UomCode=a.UomCode) AS UomDescription " +
																							"From Jmjm1 a Left Join Aeaw1 c on c.AwbNo=a.AwbBlNo " +
																								"Where a.ModuleCode='AE' and a.JobNo='" + request.FilterValue + "'"
																				);
																}
												}
												catch { throw; }
												return Result;
								}
								public List<Tracking_ContainerNo_AI> GetAIList(List_Tracking request)
								{
												List<Tracking_ContainerNo_AI> Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				Result = db.Select<Tracking_ContainerNo_AI>(
																								"Select a.ModuleCode,a.JobNo,a.JobType,a.CustomerRefNo as ReferenceNo,a.AwbBlNo,a.MawbOBLNo,a.OriginCode,a.DestCode,a.OriginName,a.DestName," +
																								"a.Pcs,a.GrossWeight,a.Volume,a.CommodityDescription as Commodity, (Select Top 1 UomDescription From Rcum1 Where UomCode=a.UomCode) AS UomDescription " +
																								"From Jmjm1 a Left Join Aiaw1 c on c.AwbNo=a.AwbBlNo " +
																								"Where a.ModuleCode='AI' and a.JobNo='" + request.FilterValue + "'"
																				);
																}
												}
												catch { throw; }
												return Result;
								}
								public List<Tracking_ContainerNo_SE> GetSEList(List_Tracking request)
								{
												List<Tracking_ContainerNo_SE> Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				Result = db.Select<Tracking_ContainerNo_SE>(
																								"Select c.VesselName, c.VoyageNo, c.FeederVesselName, c.FeederVoyage, a.ModuleCode," +
																								"a.JobNo, a.JobType, a.CustomerRefNo as ReferenceNo, a.AwbBlNo, a.MawbOBLNo, a.OriginCode, a.DestCode," +
																								"a.Pcs, a.GrossWeight, a.Volume, a.CommodityDescription as Commodity, a.ETD, a.ETA," +
																								"a.PortOfLoadingName, a.PortOfDischargeName, a.NoOf20ftContainer, a.NoOf40ftContainer, a.NoOf45ftContainer, a.ContainerNo," +
																								"(SELECT TOP 1 CityCode From Saco1) AS CityCode, c.AtaDate AS ATA, (Select Top 1 UomDescription From Rcum1 Where UomCode=a.UomCode) AS UomDescription " +
																								"From Jmjm1 a Left Join Sebl1 c on c.BlNo=a.AwbBlNo " +
																								"Where a.ModuleCode='SE' and a.JobNo='" + request.FilterValue + "'"
																				);
																}
												}
												catch { throw; }
												return Result;
								}
								public List<Tracking_ContainerNo_SI> GetSIList(List_Tracking request)
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
																								"(Select Top 1 UomDescription From Rcum1 Where UomCode=a.UomCode) AS UomDescription " +
																								"From Jmjm1 a Left Join Sebl1 c on c.BlNo=a.AwbBlNo " +
																								"Where a.ModuleCode='SI'  and a.JobNo='" + request.FilterValue + "'"
																				);
																}
												}
												catch { throw; }
												return Result;
								}
    }
}
