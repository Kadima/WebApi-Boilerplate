using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.ServiceModel;
using WebApi.ServiceModel.Common;

namespace WebApi.ServiceInterface.Common
{
    public class ListService
    {
        public void List_Rcbp1(Auth auth, List_Rcbp1 request, List_Rcbp1_Logic list_Rcbp1_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {
				if (uri.IndexOf("/sps") > 0)
				{
					ecr.data.results = list_Rcbp1_Logic.GetSpsList(request);
				}
				else
				{
					ecr.data.results = list_Rcbp1_Logic.GetList(request);
				}
                ecr.meta.code = 200;
                ecr.meta.message = "OK";
            }
            else
            {
                ecr.meta.code = 401;
                ecr.meta.message = "Unauthorized";
            }
        }
    }
}
