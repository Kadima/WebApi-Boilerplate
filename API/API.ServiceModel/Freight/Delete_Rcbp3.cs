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
				[Route("/freight/rcbp3/delete/{BusinessPartyCode}/{LineItemNo}", "Get")]
				[Route("/freight/rcbp3/delete", "Get")]
				public class Delete_Rcbp3 : IReturn<CommonResponse>
				{
								public string BusinessPartyCode { get; set; }
								public string LineItemNo { get; set; }
    }
				public class Delete_Rcbp3_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
								public int DeleteResult(Delete_Rcbp3 request)
        {
												int Result = -1;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				db.Delete<Rcbp3>(r3 => r3.BusinessPartyCode == request.BusinessPartyCode && r3.LineItemNo == int.Parse(request.LineItemNo));
																				Result = 1;
																}
												}
												catch { throw; }
												return Result;
        }
    }
}
