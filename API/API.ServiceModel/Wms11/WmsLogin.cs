using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace API.ServiceModel.Wms
{
    [Route("/api/wms/action/login", "Post")]
    public class WmsLogin : IReturn<LoginResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public string strResult { get; set; }
    }
    public class LoginLogic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public int LoginCheck(WmsLogin request)
        {
            int Result = -1;
            try
            {
                string com = "SELECT count(*) FROM saus1 WHERE UserId =" + Modfunction.SQLSafeValue(request.UserName) + " And Password=" + Modfunction.SQLSafeValue(request.Password) + "";
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Scalar<int>(com);
                }
            }
            catch
            {
                Result = -1;
            }
            return Result;
        }
    } 
}