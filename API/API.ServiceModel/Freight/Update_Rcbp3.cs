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
    public class Update_Rcbp3 : IReturn<CommonResponse>
    {
        public Rcbp3 rcbp3 { get; set; }
    }
    public class Update_Rcbp3_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public int UpdateResult(Update_Rcbp3 request)
        {
            int Result = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Update<Rcbp3>(
                        new
                        {
                            Birthday = request.rcbp3.Birthday,
                            ContactName = request.rcbp3.ContactName,
                            Department = request.rcbp3.Department,
                            Dislike = request.rcbp3.Dislike,
                            Email = request.rcbp3.Email,
                            Facebook = request.rcbp3.Facebook,
                            Fax = request.rcbp3.Fax,
                            Handphone = request.rcbp3.Handphone,
                            Like = request.rcbp3.Like,
                            MSN = request.rcbp3.MSN,
                            NameCard = request.rcbp3.NameCard,
                            Others = request.rcbp3.Others,
                            QQ = request.rcbp3.QQ,
                            Skype = request.rcbp3.Skype,
                            Telephone = request.rcbp3.Telephone,
                            Title = request.rcbp3.Title,
                            Twitter = request.rcbp3.Twitter
                        },
                        p => p.BusinessPartyCode == request.rcbp3.BusinessPartyCode && p.LineItemNo == request.rcbp3.LineItemNo
                    );
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
