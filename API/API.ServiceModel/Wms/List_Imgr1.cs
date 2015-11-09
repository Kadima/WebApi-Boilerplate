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
    [Route("/wms/action/list/imgr1/{CustomerCode}", "Get")]
    [Route("/wms/action/list/imgr1/grn/", "Get")]
    [Route("/wms/action/list/imgr1/grn/{GoodsReceiptNoteNo}", "Get")]
    public class List_Imgr1 : IReturn<CommonResponse>
    {
        public string CustomerCode { get; set; }
        public string GoodsReceiptNoteNo { get; set; }
    }
    public class List_Imgr1_Response
    {
        public int Index { get; set; }
        public int TrxNo { get; set; }
        public string CustomerCode { get; set; }
        public string GoodsReceiptNoteNo { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string RefNo { get; set; }
    }
    public class List_Imgr1_Logic
    {
        private class Imgr1
        {
            public int TrxNo { get; set; }
            public string CustomerCode { get; set; }
            public string GoodsReceiptNoteNo { get; set; }
            public DateTime ReceiptDate { get; set; }
            public string RefNo { get; set; }
            public string StatusCode { get; set; }
        }
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<List_Imgr1_Response> GetList(List_Imgr1 request)
        {
            List<List_Imgr1_Response> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (!request.CustomerCode.IsNullOrEmpty())
                    {
                        Result = db.Select<List_Imgr1_Response>(
                            db.From<Imgr1>()
                            .Where(r1 => r1.CustomerCode != null && r1.CustomerCode != "" && r1.GoodsReceiptNoteNo != null && r1.GoodsReceiptNoteNo != "" && r1.StatusCode != null && r1.StatusCode != "DEL" && r1.StatusCode != "EXE" && r1.StatusCode != "CMP" && r1.CustomerCode == request.CustomerCode)
                            .OrderByDescending(r1 => r1.ReceiptDate)
                        );
                    }
                    else if (!request.GoodsReceiptNoteNo.IsNullOrEmpty())
                    {
                        Result = db.Select<List_Imgr1_Response>(
                            db.From<Imgr1>()
                            .Where(r1 => r1.CustomerCode != null && r1.CustomerCode != "" && r1.GoodsReceiptNoteNo != null && r1.GoodsReceiptNoteNo != "" && r1.StatusCode != null && r1.StatusCode != "DEL" && r1.StatusCode != "EXE" && r1.StatusCode != "CMP" && r1.GoodsReceiptNoteNo.StartsWith(request.GoodsReceiptNoteNo))
                            .OrderByDescending(r1 => r1.ReceiptDate)
                        );
                    }                  
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
