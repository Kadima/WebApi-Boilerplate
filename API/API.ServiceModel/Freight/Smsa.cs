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
				[Route("/freight/smsa1/count", "Get")]
				[Route("/freight/smsa1/sps", "Get")]
				[Route("/freight/smsa2/create/{TrxNo}", "Post")]
				[Route("/freight/smsa2/update/{TrxNo}", "Post")]
				[Route("/freight/smsa2/read/{TrxNo}", "Get")]
				[Route("/freight/smsa2/delete/{TrxNo}", "Get")]
				public class Smsa : IReturn<CommonResponse>
    {
								public string SalesmanName { get; set; }
								public string RecordCount { get; set; }
								public string TrxNo { get; set; }
    }
				public class Smsa_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
								public int GetCount(Smsa request)
								{
												int Result = -1;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				if (!string.IsNullOrEmpty(request.SalesmanName))
																				{
																								Result = db.Scalar<int>(
																												"Select count(*) From Smsa1 Where (Select Top 1 SalesmanName From Rcsm1 Where SalesmanCode=Smsa1.SalesmanCode) Like '" + request.SalesmanName + "%'"
																								);
																				}
																}
												}
												catch { throw; }
												return Result;
								}
								public List<_Smsa1> GetSpsList(Smsa request)
								{
												List<_Smsa1> Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				int count = int.Parse(request.RecordCount);
																				string strWhere = "";
																				if (!string.IsNullOrEmpty(request.SalesmanName))
																				{
																								strWhere = " Where (Select Top 1 SalesmanName From Rcsm1 Where SalesmanCode=Smsa1.SalesmanCode) LIKE '" + request.SalesmanName + "%'";
																				}
																				string strSelect = "SELECT " +
																				"s1.*, (Select Top 1 SalesmanName From Rcsm1 Where SalesmanCode=s1.SalesmanCode) AS SalesmanName" +
																				" FROM Smsa1 s1," +
																				"(SELECT TOP " + (count + 20) + " row_number() OVER (ORDER BY TrxNo ASC) n, TrxNo FROM Smsa1 " + strWhere + ") s2" +
																				" WHERE s1.TrxNo = s2.TrxNo AND s2.n > " + count;
																				string strOrderBy = " ORDER BY s2.n ASC";
																				string strSQL = strSelect + strOrderBy;
																				Result = db.Select<_Smsa1>(strSQL);
																}
												}
												catch { throw; }
												return Result;
								}
								public List<_Smsa2> Read_Smsa2(Smsa request)
								{
												List<_Smsa2> Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				Result = db.Select<_Smsa2>("Select * From Smsa2 Where TrxNo=" + int.Parse(request.TrxNo));
																}
												}
												catch { throw; }
												return Result;
								}
    }
}
