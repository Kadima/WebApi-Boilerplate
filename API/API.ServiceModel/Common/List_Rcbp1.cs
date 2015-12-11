using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.OrmLite;
using WebApi.ServiceModel.Tables;

namespace WebApi.ServiceModel.Common
{
    [Route("/wms/action/list/rcbp1", "Get")]
    [Route("/wms/action/list/rcbp1/{BusinessPartyName}", "Get")]
    [Route("/freight/rcbp1", "Get")]
	[Route("/freight/rcbp1/{BusinessPartyName}", "Get")]
	[Route("/freight/rcbp1/sps/{RecordCount}", "Get")]
	[Route("/freight/rcbp1/sps/{RecordCount}/{BusinessPartyName}", "Get")]
    [Route("/freight/rcbp1/TrxNo/{TrxNo}", "Get")]
    public class List_Rcbp1 : IReturn<CommonResponse>
    {
        public string TrxNo { get; set; }
        public string BusinessPartyName { get; set; }
		public string RecordCount { get; set; }
    }
    public class List_Rcbp1_Logic
    {
        
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<Rcbp1> GetList(List_Rcbp1 request)
        {
            List<Rcbp1> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (!string.IsNullOrEmpty(request.BusinessPartyName))
                    {
                        //Result = db.SelectParam<Rcbp1>(
                        //    q=> q.StatusCode != null && q.StatusCode !="DEL" && q.BusinessPartyName.StartsWith(request.BusinessPartyName)
                        //);
						string strSQL = "Select *,(Select Top 1 CountryName From Rccy1 Where CountryCode=Rcbp1.CountryCode) AS CountryName From Rcbp1 Where IsNUll(StatusCode,'')<>'DEL' And BusinessPartyName LIKE '" + request.BusinessPartyName + "%' Order By BusinessPartyCode Asc";
						Result = db.Select<Rcbp1>(strSQL);
                    }
                    else if (!string.IsNullOrEmpty(request.TrxNo))
                    {
                        //Result = db.SelectParam<Rcbp1>(
                        //    q=> q.StatusCode != null && q.StatusCode !="DEL" && q.TrxNo == int.Parse(request.TrxNo)                           
                        //);
						string strSQL = "Select *,(Select Top 1 CountryName From Rccy1 Where CountryCode=Rcbp1.CountryCode) AS CountryName From Rcbp1 Where IsNUll(StatusCode,'')<>'DEL' And TrxNo=" + int.Parse(request.TrxNo);
						Result = db.Select<Rcbp1>(strSQL);
                    }
                    else
                    {
                        //Result = db.SelectParam<Rcbp1>(
                        //    q=> q.StatusCode != null && q.StatusCode !="DEL"
                        //).OrderBy(q => q.BusinessPartyCode).Take(20).ToList<Rcbp1>();
						string strSQL = "Select Top 20 *,(Select Top 1 CountryName From Rccy1 Where CountryCode=Rcbp1.CountryCode) AS CountryName From Rcbp1 Where IsNUll(StatusCode,'')<>'DEL' Order By BusinessPartyName Asc";
						Result = db.Select<Rcbp1>(strSQL);
                    }
                }
            }
            catch { throw; }
            return Result;
        }
		public List<Rcbp1> GetSpsList(List_Rcbp1 request)
		{
			List<Rcbp1> Result = null;
			try
			{
				using (var db = DbConnectionFactory.OpenDbConnection())
				{
					int count = int.Parse(request.RecordCount);
					string strWhere = "";
					if (!string.IsNullOrEmpty(request.BusinessPartyName))
					{
						strWhere = " Where BusinessPartyName LIKE '" + request.BusinessPartyName + "%'";
					}				
					string strSelect= "SELECT " +
								"r1.*, (Select Top 1 CountryName From Rccy1 Where CountryCode=r1.CountryCode) AS CountryName " +
								"FROM Rcbp1 r1," +
								"(SELECT TOP " + (count + 20) + " row_number() OVER (ORDER BY BusinessPartyName ASC) n, TrxNo FROM Rcbp1 " + strWhere + ") r2 " +
								"WHERE r1.TrxNo = r2.TrxNo AND r2.n > " + count;
					string strOrderBy = " ORDER BY r2.n ASC";
					string strSQL = strSelect + strOrderBy;
					Result = db.Select<Rcbp1>(strSQL);
				}
			}
			catch { throw; }
			return Result;
		}
    }
}
