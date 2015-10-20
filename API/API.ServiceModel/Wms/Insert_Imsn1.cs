using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace WmsWS.ServiceModel.Wms
{
    [Route("/wms/action/insert/imsn1", "Post")]
    public class Insert_Imsn1 : IReturn<CommonResponse>
    {
        public string IssueNoteNo { get; set; }
        public string IssueLineItemNo { get; set; }
        public string ReceiptNoteNo { get; set; }
        public string ReceiptLineItemNo { get; set; }
        public string SerialNo { get; set; }
    }
    public class Insert_Imsn1_Logic
    {
        private class Imsn1
        {
            public string IssueNoteNo { get; set; }
            public string IssueLineItemNo { get; set; }
            public string ReceiptNoteNo { get; set; }
            public string ReceiptLineItemNo { get; set; }
            public string SerialNo { get; set; }
        }
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public long UpdateImsn1(Insert_Imsn1 request)
        {
            long Result = -1;
            int intResult = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (request.IssueNoteNo.Length > 0)
                    {
                        intResult = db.Scalar<int>(
                          db.From<Imsn1>().Select(Sql.Count("*")).Where(i1 => i1.IssueNoteNo == request.IssueNoteNo && i1.IssueLineItemNo == request.IssueLineItemNo && i1.SerialNo == request.SerialNo)
                      );
                        if (intResult < 1)
                        {
                            Result = db.Insert(new Imsn1 { IssueNoteNo = request.IssueNoteNo, IssueLineItemNo = request.IssueLineItemNo, SerialNo = request.SerialNo });
                        }
                    }
                    else
                    {
                        intResult = db.Scalar<int>(
                            db.From<Imsn1>().Select(Sql.Count("*")).Where(i1 => i1.ReceiptNoteNo == request.ReceiptNoteNo && i1.ReceiptLineItemNo == request.ReceiptLineItemNo && i1.SerialNo == request.SerialNo)
                        );
                        if (intResult < 1)
                        {
                            Result = db.Insert(new Imsn1 { ReceiptNoteNo = request.ReceiptNoteNo, ReceiptLineItemNo = request.ReceiptLineItemNo, SerialNo = request.SerialNo });
                        }
                    }
                    
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
