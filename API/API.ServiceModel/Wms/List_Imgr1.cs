using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.OrmLite;
using WebApi.ServiceModel.Tables;

namespace WebApi.ServiceModel.Wms
{
    [Route("/wms/action/list/imgr1/{CustomerCode}", "Get")]
    [Route("/wms/action/list/imgr1/grn/", "Get")]
    [Route("/wms/action/list/imgr1/grn/{GoodsReceiptNoteNo}", "Get")]
    public class List_Imgr1 : IReturn<CommonResponse>
    {
        public string CustomerCode { get; set; }
        public string GoodsReceiptNoteNo { get; set; }
    }
    public class List_Imgr1_Logic
    {        
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<Imgr1> GetList(List_Imgr1 request)
        {
            List<Imgr1> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (!string.IsNullOrEmpty(request.CustomerCode))
                    {
                        Result = db.Select<Imgr1>(
                            "IsNull(CustomerCode,'')<>'' And IsNull(GoodsReceiptNoteNo,'')<>'' And IsNull(StatusCode,'')<>'DEL' And IsNull(StatusCode,'')<>'EXE' And IsNull(StatusCode,'')<>'CMP' And CustomerCode={0} OrderBy ReceiptDate Desc",
                            request.CustomerCode
                        );
                    }
                    else if (!string.IsNullOrEmpty(request.GoodsReceiptNoteNo))
                    {
                        Result = db.Select<Imgr1>(
                            "IsNull(CustomerCode,'')<>'' And IsNull(GoodsReceiptNoteNo,'')<>'' And IsNull(StatusCode,'')<>'DEL' And IsNull(StatusCode,'')<>'EXE' And IsNull(StatusCode,'')<>'CMP' And GoodsReceiptNoteNo like '{0}%'",
                            request.GoodsReceiptNoteNo
                        );
                    }                  
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
