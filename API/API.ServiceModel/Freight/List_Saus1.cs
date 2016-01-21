using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.OrmLite;
using WebApi.ServiceModel.Tables;
using System.Windows.Forms;

namespace WebApi.ServiceModel.Freight
{
				[Route("/freight/saus1/memo/{UserID}", "Get")]
				[Route("/freight/saus1/{UserID}", "Get")]
    [Route("/freight/saus1", "Get")]
    public class List_Saus1 : IReturn<CommonResponse>
    {
								public string UserID { get; set; }
    }
				public class List_Saus1_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
								public string GetMemo(List_Saus1 request)
								{
												string Result = null;
												try
												{
																using (var db = DbConnectionFactory.OpenDbConnection())
																{
																				RichTextBox rtb = new RichTextBox();
																				List<Saus1> ls = db.Select<Saus1>("Select Memo From Saus1 Where UserID='" + request.UserID + "'");
																				if (ls.Count > 0)
																				{
																								rtb.Rtf = ls[0].Memo;
																				}
																				Result = rtb.Text;
																}
												}
												catch { throw; }
												return Result;
								}
    }
}
