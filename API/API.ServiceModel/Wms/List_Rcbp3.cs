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
    [Route("/wms/action/list/rcbp3/{BusinessPartyCode}", "Get")]
    public class List_Rcbp3 : IReturn<CommonResponse>
    {
        public string BusinessPartyCode { get; set; }
    }
    public class List_Rcbp3_Response
    {
        public string Title { get; set; }
        public string ContactName { get; set; }
        public string Department { get; set; }
        public string Handphone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public byte[] NameCard { get; set; }
        public string Birthday { get; set; }
        public string Like { get; set; }
        public string Dislike { get; set; }
        public string Facebook { get; set; }
        public string MSN { get; set; }
        public string Skype { get; set; }
        public string Twitter { get; set; }
        public string QQ { get; set; }
        public string Others { get; set; }
    }
    public class List_Rcbp3_Logic
    {
        private class Rcbp3
        {
            public string BusinessPartyCode { get; set; }
            public int LineItemNo { get; set; }
            public DateTime Birthday { get; set; }
            public string ContactName { get; set; }
            public string Department { get; set; }
            public string Title { get; set; }
            public string Telephone { get; set; }
            public string Handphone { get; set; }
            public string Email { get; set; }
            public string NameCard { get; set; }
            public string Fax { get; set; }
            public string Like { get; set; }
            public string Dislike { get; set; }
            public string Facebook { get; set; }
            public string MSN { get; set; }
            public string Skype { get; set; }
            public string Twitter { get; set; }
            public string QQ { get; set; }
            public string Others { get; set; }
        }
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public List<List_Rcbp3_Response> GetList(List_Rcbp3 request)
        {
            List<List_Rcbp3_Response> Result = null;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    Result = db.Select<List_Rcbp3_Response>(
                            db.From<Rcbp3>()
                            .Where(r1 => r1.BusinessPartyCode == request.BusinessPartyCode)
                        );
                }
            }
            catch { throw; }
            return Result;
        }
    }
}
