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
				[Route("/freight/tracking/ContainerNo/module/{ModuleCode}/{JobNo}", "Get")]
				[Route("/freight/tracking/ContainerNo/sps/{RecordCount}/{ContainerNo}", "Get")]
    public class List_Tracking_ContainerNo : IReturn<CommonResponse>
    {
        public string ContainerNo { get; set; }
        public string ContainerNoToCount { get; set; }
								public string JobNo { get; set; }
								public string ModuleCode { get; set; }
								public string RecordCount { get; set; }
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
                    if (!string.IsNullOrEmpty(request.ContainerNo))
                    {
																								Result = db.Select<Jmjm1>(
																												"Select *,(Select Top 1 UomDescription From Rcum1 Where UomCode=Jmjm1.UomCode) AS UomDescription From Jmjm1 Where ContainerNo LIKE '%" + request.ContainerNo + "%' Order by Jmjm1.JobNo Asc,Jmjm1.JobDate Desc"
																								);
                    }
																				else if (!string.IsNullOrEmpty(request.ModuleCode))
                    {
                        Result = db.Select<Jmjm1>(
																												"Select *,(Select Top 1 UomDescription From Rcum1 Where UomCode=Jmjm1.UomCode) AS UomDescription From Jmjm1 Where ModuleCode='" + request.ModuleCode + "' And JobNo='" + request.JobNo + "' Order By Jmjm1.JobNo Asc,Jmjm1.JobDate Desc"
                        );
                    }                    
                }
            }
            catch { throw; }
            return Result;
        }
								public List<Jmjm1_Type> GetCountList(List_Tracking_ContainerNo request)
								{
												List<Jmjm1_Type> Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				if (!string.IsNullOrEmpty(request.ContainerNoToCount))
																				{
																								Result = db.Select<Jmjm1_Type>(
																											"Select Jmjm1.JobNo,Jmjm1.JobType,Jmjm1.ModuleCode From Jmjm1 Left Join Jmjt1 On Jmjm1.JobType=Jmjt1.JobType Where charindex('" + request.ContainerNoToCount + ",',ISNULL(ContainerNo,'')+',')>0"
																							);
																				}
																}
												}
												catch { throw; }
												return Result;
								}
								public List<Jmjm1> GetSpsList(List_Tracking_ContainerNo request)
								{
												List<Jmjm1> Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																if (!string.IsNullOrEmpty(request.ContainerNo))
																{
																				int count = int.Parse(request.RecordCount);
																				string strWhere = "";
																				if (!string.IsNullOrEmpty(request.ContainerNo))
																				{
																								strWhere = " Where ContainerNo LIKE '%" + request.ContainerNo + "%'";
																				}
																				string strSelect = "SELECT " +
																								"j1.*,(Select Top 1 UomDescription From Rcum1 Where UomCode=j1.UomCode) AS UomDescription " +
																								"FROM Jmjm1 j1, " +
																								"(SELECT TOP " + (count + 10) + " row_number() OVER (ORDER BY JobNo ASC, JobDate DESC) n, JobNo FROM Jmjm1 " + strWhere + ") j2 " +
																								"WHERE j1.JobNo = j2.JobNo AND j2.n > " + count;
																				string strOrderBy = " ORDER BY j2.n ASC";
																				string strSQL = strSelect + strOrderBy;
																				Result = db.Select<Jmjm1>(strSQL);
																}
																}
												}
												catch { throw; }
												return Result;
								}
    }
}
