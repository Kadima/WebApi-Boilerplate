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
    [Route("/freight/rcbp3", "Get")]
				[Route("/freight/rcbp3/{BusinessPartyCode}", "Get")]
				[Route("/freight/rcbp3/delete", "Get")]
				[Route("/freight/rcbp3/delete/{BusinessPartyCode}/{LineItemNo}", "Get")]
    public class List_Rcbp3 : IReturn<CommonResponse>
    {
								public string BusinessPartyCode { get; set; }
								public int LineItemNo { get; set; }
    }
    public class List_Rcbp3_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<Rcbp3> GetList(List_Rcbp3 request)
        {
            List<Rcbp3> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (!string.IsNullOrEmpty(request.BusinessPartyCode))
                    { 
                        Result = db.Where<Rcbp3>(r1 => r1.BusinessPartyCode == request.BusinessPartyCode);
                    }
                }
            }
            catch { throw; }
            return Result;
        }
								public int DeleteItem(List_Rcbp3 request)
								{
												int Result = -1;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				Result = db.Delete<Rcbp3>(r3 => r3.BusinessPartyCode == request.BusinessPartyCode && r3.LineItemNo == request.LineItemNo);
																}
												}
												catch { throw; }
												return Result;
								}
    }
}
