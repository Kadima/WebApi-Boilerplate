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
				[Route("/freight/rcbp3/{BusinessPartyCode}/{LineItemNo}", "Get")]
				[Route("/freight/rcbp3/{BusinessPartyCode}", "Get")]
    [Route("/freight/rcbp3", "Get")]
    public class List_Rcbp3 : IReturn<CommonResponse>
    {
								public string BusinessPartyCode { get; set; }
								public string LineItemNo { get; set; }
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
																								if (!string.IsNullOrEmpty(request.LineItemNo))
																								{
																												Result = db.Where<Rcbp3>(r1 => r1.BusinessPartyCode == request.BusinessPartyCode && r1.LineItemNo == int.Parse(request.LineItemNo));
																								}
																								else
																								{
																												Result = db.Where<Rcbp3>(r1 => r1.BusinessPartyCode == request.BusinessPartyCode);
																								}
                    }
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
