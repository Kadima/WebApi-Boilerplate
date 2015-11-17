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
    [Route("/wms/action/list/imgi1/{CustomerCode}", "Get")]
    [Route("/wms/action/list/imgi1/gin/", "Get")]
    [Route("/wms/action/list/imgi1/gin/{GoodsIssueNoteNo}", "Get")]
    public class List_Imgi1 : IReturn<CommonResponse>
    {
        public string CustomerCode { get; set; }
        public string GoodsIssueNoteNo { get; set; }
    }
    public class List_Imgi1_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<Imgi1> GetList(List_Imgi1 request)
        {
            List<Imgi1> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (!string.IsNullOrEmpty(request.CustomerCode))
                    {
                        Result = db.Select<Imgi1>(
                            "IsNull(CustomerCode,'')<>'' And IsNull(GoodsIssueNoteNo,'')<>'' And IsNull(StatusCode,'')<>'DEL' And IsNull(StatusCode,'')<>'EXE' And IsNull(StatusCode,'')<>'CMP' And CustomerCode={0} OrderBy IssueDateTime Desc",
                            request.CustomerCode
                        );
                    }
                    else if (!string.IsNullOrEmpty(request.GoodsIssueNoteNo))
                    {
                        Result = db.Select<Imgi1>(
                            "IsNull(CustomerCode,'')<>'' And IsNull(GoodsIssueNoteNo,'')<>'' And IsNull(StatusCode,'')<>'DEL' And IsNull(StatusCode,'')<>'EXE' And IsNull(StatusCode,'')<>'CMP' And GoodsIssueNoteNo like '{0}%' OrderBy IssueDateTime Desc",
                            request.GoodsIssueNoteNo
                        );
                    }                  
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
