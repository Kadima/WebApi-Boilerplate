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
    [Route("/wms/action/list/imsn1/{GoodsIssueNoteNo}", "Get")]
    public class List_Imsn1 : IReturn<CommonResponse>
    {
        public string GoodsIssueNoteNo { get; set; }
    }
    public class List_Imsn1_Response
    {
        public string IssueNoteNo { get; set; }
        public int IssueLineItemNo { get; set; }
        public string SerialNo { get; set; }
    }
    public class List_Imsn1_Logic
    {
        private class Imgi1
        {
            public int TrxNo { get; set; }
            public string GoodsIssueNoteNo { get; set; }
        }
        private class Imgi2
        {
            public int TrxNo { get; set; }
            public int LineItemNo { get; set; }
        }
        private class Imsn1
        {
            public string IssueNoteNo { get; set; }
            public int IssueLineItemNo { get; set; }
            public string SerialNo { get; set; }
        }
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<List_Imsn1_Response> GetList(List_Imsn1 request)
        {
            List<List_Imsn1_Response> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Select<List_Imsn1_Response>(
                        db.From<Imsn1>()
                        .LeftJoin<Imsn1, Imgi1>((s1, i1) => s1.IssueNoteNo == i1.GoodsIssueNoteNo)
                        .LeftJoin<Imgi1, Imgi2>((i1, i2) => i1.TrxNo == i2.TrxNo)
                        .Where<Imgi1>(i1 => i1.GoodsIssueNoteNo == request.GoodsIssueNoteNo)
                        .And<Imsn1, Imgi2>((s1, i1) => s1.IssueLineItemNo == i1.LineItemNo)
                    );
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
