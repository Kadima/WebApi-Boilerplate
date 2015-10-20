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
    [Route("/wms/action/list/imgi2/{GoodsIssueNoteNo}", "Get")]
    public class List_Imgi2 : IReturn<CommonResponse>
    {
        public string GoodsIssueNoteNo { get; set; }
    }
    public class List_Imgi2_Response
    {
        public int TrxNo { get; set; }
        public int LineItemNo { get; set; }
        public string StoreNo { get; set; }
        public int ProductTrxNo { get; set; }
        public string ProductCode { get; set; }
        public string DimensionFlag { get; set; }
        public int PackingQty { get; set; }
        public int WholeQty { get; set; }
        public int LooseQty { get; set; }
        public string ProductName { get; set; }
        public string SerialNoFlag { get; set; }
        public string UserDefine01 { get; set; }
    }
    public class List_Imgi2_Logic
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
            public int ProductTrxNo { get; set; }
            public string ProductCode { get; set; }
            public string DimensionFlag { get; set; }
            public string StoreNo { get; set; }
            public int PackingQty { get; set; }
            public int WholeQty { get; set; }
            public int LooseQty { get; set; }
        }
        private class Impr1
        {
            public int TrxNo { get; set; }
            public string ProductName { get; set; }
            public string SerialNoFlag { get; set; }
            public string UserDefine01 { get; set; }
        }
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<List_Imgi2_Response> GetList(List_Imgi2 request)
        {
            List<List_Imgi2_Response> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Select<List_Imgi2_Response>(
                        db.From<Imgi2>()
                        .LeftJoin<Imgi2, Imgi1>((i2, i1) => i2.TrxNo == i1.TrxNo)
                        .LeftJoin<Imgi2, Impr1>((i2, i1) => i2.ProductTrxNo == i1.TrxNo)
                        .Where<Imgi1>(i1 => i1.GoodsIssueNoteNo == request.GoodsIssueNoteNo)
                    );
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
