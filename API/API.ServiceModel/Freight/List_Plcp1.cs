using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.OrmLite;
using WebApi.ServiceModel.Tables;

namespace WebApi.ServiceModel.Freight
{
    [Route("/freight/plcp1/VoucherNo/{VoucherNo}/{StatusCode}", "Get")]
    [Route("/freight/plcp1/VoucherNo/{VoucherNo}", "Get")]
    [Route("/freight/plcp1/VendorName/{VendorName}/{StatusCode}", "Get")]
    [Route("/freight/plcp1/VendorName/{VendorName}", "Get")]
    [Route("/freight/plcp1/{StatusCode}", "Get")]
    [Route("/freight/plcp1", "Get")]
    public class List_Plcp1 : IReturn<CommonResponse>
    {
        public string VoucherNo { get; set; }
        public string VendorName { get; set; }
        public string StatusCode { get; set; }
    }
    public class List_Plcp1_Logic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<Plcp1> GetList(List_Plcp1 request)
        {
            List<Plcp1> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (!string.IsNullOrEmpty(request.VoucherNo) && !string.IsNullOrEmpty(request.StatusCode))
                    {
                        Result = db.SelectParam<Plcp1>(
                            q => q.StatusCode != null && q.StatusCode == request.StatusCode && q.VoucherNo.StartsWith(request.VoucherNo)
                        );
                    }
                    else if (!string.IsNullOrEmpty(request.VendorName) && !string.IsNullOrEmpty(request.StatusCode))
                    {
                        Result = db.SelectParam<Plcp1>(
                            q => q.StatusCode != null && q.StatusCode == request.StatusCode && q.VendorName.StartsWith(request.VendorName)
                        );
                    }
                    else if (!string.IsNullOrEmpty(request.VoucherNo))
                    {
                        Result = db.SelectParam<Plcp1>(
                            q => q.StatusCode != null && q.StatusCode != "DEL" && q.VoucherNo.StartsWith(request.VoucherNo)
                        );
                    }
                    else if (!string.IsNullOrEmpty(request.VendorName))
                    {
                        Result = db.SelectParam<Plcp1>(
                            q => q.StatusCode != null && q.StatusCode != "DEL" && q.VendorName.StartsWith(request.VendorName)
                        );
                    }
                    else if (!string.IsNullOrEmpty(request.StatusCode))
                    {
                        Result = db.SelectParam<Plcp1>(
                            q => q.StatusCode != null && q.StatusCode == request.StatusCode
                        );
                    }
                    else
                    {
                        Result = db.Where<Plcp1>(r1 => r1.StatusCode != null && r1.StatusCode != "DEL");
                    }
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
