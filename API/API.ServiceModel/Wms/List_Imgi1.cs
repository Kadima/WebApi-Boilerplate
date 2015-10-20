using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace WmsWS.ServiceModel.Wms
{
    [Route("/wms/action/list/imgi1/{CustomerCode}", "Get")]
    [Route("/wms/action/list/imgi1/gin/", "Get")]
    [Route("/wms/action/list/imgi1/gin/{GoodsIssueNoteNo}", "Get")]
    public class List_Imgi1 : IReturn<CommonResponse>
    {
        public string CustomerCode { get; set; }
        public string GoodsIssueNoteNo { get; set; }
    }
    public class List_Imgi1_Response
    {
        public int Index { get; set; }
        public int TrxNo { get; set; }
        public string CustomerCode { get; set; }
        public string GoodsIssueNoteNo { get; set; }
        public DateTime IssueDateTime { get; set; }
        public string RefNo { get; set; }
    }
    public class List_Imgi1_Logic
    {
        private class Imgi1
        {
            public int TrxNo { get; set; }
            public string CustomerCode { get; set; }
            public string GoodsIssueNoteNo { get; set; }
            public DateTime IssueDateTime { get; set; }
            public string RefNo { get; set; }
            public string StatusCode { get; set; }
        }
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<List_Imgi1_Response> GetList(List_Imgi1 request)
        {
            List<List_Imgi1_Response> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (!request.CustomerCode.IsNullOrEmpty())
                    {
                        Result = db.Select<List_Imgi1_Response>(
                            db.From<Imgi1>()
                            .Where(r1 => r1.CustomerCode != null && r1.CustomerCode != "" && r1.GoodsIssueNoteNo != null && r1.GoodsIssueNoteNo != "" && r1.StatusCode != null && r1.StatusCode != "DEL" && r1.StatusCode != "EXE" && r1.StatusCode != "CMP" && r1.CustomerCode == request.CustomerCode)
                            .OrderByDescending(r1 => r1.IssueDateTime)
                        );
                    }
                    else if (!request.GoodsIssueNoteNo.IsNullOrEmpty())
                    {
                        Result = db.Select<List_Imgi1_Response>(
                            db.From<Imgi1>()
                            .Where(r1 => r1.CustomerCode != null && r1.CustomerCode != "" && r1.GoodsIssueNoteNo != null && r1.GoodsIssueNoteNo != "" && r1.StatusCode != null && r1.StatusCode != "DEL" && r1.StatusCode != "EXE" && r1.StatusCode != "CMP" && r1.GoodsIssueNoteNo.StartsWith(request.GoodsIssueNoteNo))
                            .OrderByDescending(r1 => r1.IssueDateTime)
                            .Take(10)
                        );
                    }                  
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
