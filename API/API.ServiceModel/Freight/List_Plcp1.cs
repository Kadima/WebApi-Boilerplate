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
				[Route("/freight/plcp1/sps/{RecordCount}/{StatusCode}/VendorName/{VendorName}", "Get")]
				[Route("/freight/plcp1/sps/{RecordCount}/{StatusCode}/VoucherNo/{VoucherNo}", "Get")]
				[Route("/freight/plcp1/sps/{RecordCount}/{StatusCode}", "Get")]
				[Route("/freight/plcp1/sps", "Get")]
    public class List_Plcp1 : IReturn<CommonResponse>
    {
        public string VoucherNo { get; set; }
        public string VendorName { get; set; }
								public string StatusCode { get; set; }
								public string RecordCount { get; set; }
    }
    public class List_Plcp1_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
								//public List<Plcp1> GetList(List_Plcp1 request)
								//{
								//				List<Plcp1> Result = null;
								//				try
								//				{
								//								using (var db = DbConnectionFactory.OpenDbConnection())
								//								{
								//												if (!string.IsNullOrEmpty(request.VoucherNo) && !string.IsNullOrEmpty(request.StatusCode))
								//												{
								//																Result = db.SelectParam<Plcp1>(
								//																				q => q.StatusCode != null && q.StatusCode == request.StatusCode && q.VoucherNo.StartsWith(request.VoucherNo)
								//																);
								//												}
								//												else if (!string.IsNullOrEmpty(request.VendorName) && !string.IsNullOrEmpty(request.StatusCode))
								//												{
								//																Result = db.SelectParam<Plcp1>(
								//																				q => q.StatusCode != null && q.StatusCode == request.StatusCode && q.VendorName.StartsWith(request.VendorName)
								//																);
								//												}
								//												else if (!string.IsNullOrEmpty(request.VoucherNo))
								//												{
								//																Result = db.SelectParam<Plcp1>(
								//																				q => q.StatusCode != null && q.StatusCode != "DEL" && q.VoucherNo.StartsWith(request.VoucherNo)
								//																);
								//												}
								//												else if (!string.IsNullOrEmpty(request.VendorName))
								//												{
								//																Result = db.SelectParam<Plcp1>(
								//																				q => q.StatusCode != null && q.StatusCode != "DEL" && q.VendorName.StartsWith(request.VendorName)
								//																);
								//												}
								//												else if (!string.IsNullOrEmpty(request.StatusCode))
								//												{
								//																Result = db.SelectParam<Plcp1>(
								//																				q => q.StatusCode != null && q.StatusCode == request.StatusCode
								//																);
								//												}
								//												else
								//												{
								//																Result = db.Where<Plcp1>(r1 => r1.StatusCode != null && r1.StatusCode != "DEL");
								//												}
								//								}
								//				}
								//				catch { throw; }
								//				return Result;
								//}
								public List<Plcp1> GetSpsList(List_Plcp1 request)
								{
												List<Plcp1> Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				int count = int.Parse(request.RecordCount);
																				string strWhere = " Where IsNull(StatusCode,'')='" + request.StatusCode + "'";
																				if (!string.IsNullOrEmpty(request.VoucherNo))
																				{
																								strWhere = strWhere + " And VoucherNo LIKE '" + request.VoucherNo + "%'";
																				}
																				else if (!string.IsNullOrEmpty(request.VendorName))
																				{
																								strWhere = strWhere + " And VendorName LIKE '" + request.VendorName + "%'";
																				}
																				string strSelect = "SELECT " +
																				"p1.* " +
																				"FROM Plcp1 p1," +
																				"(SELECT TOP " + (count + 20) + " row_number() OVER (ORDER BY TrxNo ASC) n, TrxNo FROM Plcp1 " + strWhere + ") p2 " +
																				"WHERE p1.TrxNo = p2.TrxNo AND p2.n > " + count;
																				string strOrderBy = " ORDER BY p2.n ASC";
																				string strSQL = strSelect + strOrderBy;
																				Result = db.Select<Plcp1>(strSQL);
																}
												}
												catch { throw; }
												return Result;
								}
    }
}
