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
				[Route("/freight/saus1/memo", "Post")]
				[Route("/freight/saus1", "Post")]
    public class Update_Saus1 : IReturn<CommonResponse>
    {
								public Saus1 saus1 { get; set; }
    }
				public class Update_Saus1_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
								public int UpdateMemoResult(Update_Saus1 request)
        {
            int Result = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
																				RichTextBox rtb = new RichTextBox();
																				List<Saus1> ls = db.Select<Saus1>("Select IsNull(Memo,'') From Saus1 Where UserID='" + request.saus1.UserId + "'");
																				if (ls.Count > 0)
																				{
																								rtb.Text = request.saus1.Memo;
																				}
																				Result = db.Update<Saus1>(
																								new
																								{
																												Memo = rtb.Rtf
																								},
																								p => p.UserId == request.saus1.UserId
																				);
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
