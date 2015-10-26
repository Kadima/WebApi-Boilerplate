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
    [Route("/wms/action/list/rcbp1", "Get")]
    [Route("/wms/action/list/rcbp1/{BusinessPartyName}", "Get")]
    [Route("/wms/action/list/rcbp1/TrxNo/{TrxNo}", "Get")]
    public class List_Rcbp1 : IReturn<CommonResponse>
    {
        public string TrxNo { get; set; }
        public string BusinessPartyName { get; set; }
    }
    public class List_Rcbp1_Response
    {
        public int TrxNo { get; set; }
        public string BusinessPartyCode { get; set; }
        public string BusinessPartyName { get; set; }
        public string ContactName1 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CountryCode { get; set; }
        public string Fax { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
    }
    public class List_Rcbp1_Logic
    {
        private class Rcbp1
        {
            public int TrxNo { get; set; }
            public string BusinessPartyCode { get; set; }
            public string BusinessPartyName { get; set; }
            public string ContactName1 { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string Address4 { get; set; }
            public string CityCode { get; set; }
            public string CityName { get; set; }
            public string CountryCode { get; set; }
            public string Fax { get; set; }
            public string Telephone { get; set; }
            public string Email { get; set; }
            public string WebSite { get; set; }
            public string StatusCode { get; set; }
        }
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<List_Rcbp1_Response> GetList(List_Rcbp1 request)
        {
            List<List_Rcbp1_Response> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    if (!request.BusinessPartyName.IsNullOrEmpty())
                    {
                        Result = db.Select<List_Rcbp1_Response>(
                            db.From<Rcbp1>()
                            .Where(r1 => r1.StatusCode != null && r1.StatusCode != "DEL" && r1.BusinessPartyName.StartsWith(request.BusinessPartyName))
                            .OrderBy(r1 => r1.BusinessPartyCode)
                            .Take(10)
                        );
                    }
                    else if (!request.TrxNo.IsNullOrEmpty())
                    {
                        Result = db.Select<List_Rcbp1_Response>(
                            db.From<Rcbp1>()
                            .Where(r1 => r1.StatusCode != null && r1.StatusCode != "DEL" && r1.TrxNo == request.TrxNo.ToInt())
                        );
                    }
                    else
                    {
                        Result = db.Select<List_Rcbp1_Response>(
                            db.From<Rcbp1>()
                            .Where(r1 => r1.StatusCode != null && r1.StatusCode != "DEL")
                            .OrderBy(r1 => r1.BusinessPartyCode)
                            .Take(20)
                        );
                    }
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
