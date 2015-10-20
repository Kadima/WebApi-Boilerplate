using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace WmsWS.ServiceModel.Wms
{
    [Route("/wms/action/list/login", "Post")]
    public class List_Login : IReturn<CommonResponse>
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
    public class List_Login_Logic
    {
        private class Saus1
        {
            public string UserId { get; set; }
            public string Password { get; set; }
        }
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public int LoginCheck(List_Login request) 
        {
            int Result = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Scalar<int>(
                        db.From<Saus1>()
                        .Select(Sql.Count("*"))
                        .Where(s1 => s1.UserId == request.UserId && s1.Password == request.Password)
                    );
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
