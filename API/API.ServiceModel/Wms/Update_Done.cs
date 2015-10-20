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
    [Route("/event/action/update/done", "Post")]
    public class Update_Done : IReturn<CommonResponse>
    {
        public string JobNo { get; set; }
        public int JobLineItemNo { get; set; }
        public int LineItemNo { get; set; }
        public string DoneFlag { get; set; }
        public DateTime DoneDateTime { get; set; }
        public string Remark { get; set; }
    }
    public class Update_Done_Logic
    {
        private class Jmjm4
        {
            public DateTime DoneDateTime { get; set; }
            public string DoneFlag { get; set; }
            public string JobNo { get; set; }
            public int JobLineItemNo { get; set; }
            public int LineItemNo { get; set; }
            public string Remark { get; set; }
        }
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public int UpdateDone(Update_Done request) 
        {
            int Result = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Update<Jmjm4>(new { DoneDateTime = request.DoneDateTime, DoneFlag = request.DoneFlag, Remark = request.Remark }, p => p.JobNo == request.JobNo && p.JobLineItemNo == request.JobLineItemNo && p.LineItemNo == request.LineItemNo);
                }
            }
            catch { throw; } 
            return Result;
        }
    }
}
