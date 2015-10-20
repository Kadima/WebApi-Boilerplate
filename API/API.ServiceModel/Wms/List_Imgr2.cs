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
    [Route("/wms/action/list/imgr2/{GoodsReceiptNoteNo}", "Get")]
    public class List_Imgr2 : IReturn<CommonResponse>
    {
        public string GoodsReceiptNoteNo { get; set; }
    }
    public class List_Imgr2_Response
    {
        public int TrxNo { get; set; }
        public int LineItemNo { get; set; }
        public int ProductTrxNo { get; set; }
        public string ProductCode { get; set; }
        public string DimensionFlag { get; set; }
        public int PackingQty { get; set; }
        public int WholeQty { get; set; }
        public int LooseQty { get; set; }
    }
    public class List_Imgr2_Logic
    {
        private class Imgr1
        {
            public int TrxNo { get; set; }
            public string GoodsReceiptNoteNo { get; set; }
        }
        private class Imgr2
        {
            public int TrxNo { get; set; }
            public int LineItemNo { get; set; }
            public int ProductTrxNo { get; set; }
            public string ProductCode { get; set; }
            public string DimensionFlag { get; set; }
            public int PackingQty { get; set; }
            public int WholeQty { get; set; }
            public int LooseQty { get; set; }
        }
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<List_Imgr2_Response> GetList(List_Imgr2 request)
        {
            List<List_Imgr2_Response> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Select<List_Imgr2_Response>(
                        db.From<Imgr2>()
                        .LeftJoin<Imgr2,Imgr1>((i2, i1) => i2.TrxNo == i1.TrxNo)
                        .Where<Imgr1>(i1 => i1.GoodsReceiptNoteNo == request.GoodsReceiptNoteNo)
                    );
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
