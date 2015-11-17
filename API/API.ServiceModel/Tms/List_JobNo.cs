using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.OrmLite;
using WebApi.ServiceModel.Tables;

namespace WebApi.ServiceModel.Tms
{
    [Route("/event/action/list/jobno/{PhoneNumber}", "Get")]
    public class List_JobNo : IReturn<CommonResponse>
    {
        public string PhoneNumber { get; set; }
    }
    public class List_JobNo_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public HashSet<string> GetList(List_JobNo request)
        {
            HashSet<string> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.HashSet<string>(
                        "Select Distinct JobNo From Jmjm4 Where IsNull(Jmjm4.DoneFlag,'')<>'Y' And Jmjm4.PhoneNumber={0}",request.PhoneNumber
                    );
                }
            }
            catch { throw; }
            return Result;
        }
        public long GetCount(string strPhoneNumber, string strJobNo)
        {
            long Result = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Count<Jmjm4>(j4 => j4.PhoneNumber == strPhoneNumber && j4.JobNo == strJobNo && (j4.DoneFlag != "Y" || j4.DoneFlag == null));
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
