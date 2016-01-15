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
    [Route("/freight/rcbp3", "Post")]
				public class Insert_Rcbp3 : IReturn<CommonResponse>
    {
        public Rcbp3 rcbp3 { get; set; }
    }
				public class Insert_Rcbp3_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
								public int InsertResult(Insert_Rcbp3 request)
        {
            int Result = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    db.Insert(
																								new Rcbp3 
                        {
																												BusinessPartyCode = request.rcbp3.BusinessPartyCode,
																												LineItemNo = request.rcbp3.LineItemNo,
                            Birthday = null,
                            ContactName = request.rcbp3.ContactName,
                            Department = request.rcbp3.Department,
                            Dislike = request.rcbp3.Dislike,
                            Email = request.rcbp3.Email,
                            Facebook = request.rcbp3.Facebook,
                            Fax = request.rcbp3.Fax,
                            Handphone = request.rcbp3.Handphone,
                            Like = request.rcbp3.Like,
                            MSN = request.rcbp3.MSN,
                            NameCard = null,
                            Others = request.rcbp3.Others,
                            QQ = request.rcbp3.QQ,
                            Skype = request.rcbp3.Skype,
                            Telephone = request.rcbp3.Telephone,
                            Title = request.rcbp3.Title,
                            Twitter = request.rcbp3.Twitter
                        }
                    );
																				Result = 1;
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
