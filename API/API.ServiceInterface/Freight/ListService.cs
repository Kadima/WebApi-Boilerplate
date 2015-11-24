using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.ServiceModel;
using WebApi.ServiceModel.Common;
using WebApi.ServiceModel.Freight;

namespace WebApi.ServiceInterface.Freight
{
    public class ListService
    {
        public void ListRcbp3(Auth auth, List_Rcbp3 request, List_Rcbp3_Logic list_Rcbp3_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {
                ecr.data.results = list_Rcbp3_Logic.GetList(request);
                ecr.meta.code = 200;
                ecr.meta.message = "OK";
            }
            else
            {
                ecr.meta.code = 401;
                ecr.meta.message = "Unauthorized";
            }
        }
        public void ListPlcp1(Auth auth, List_Plcp1 request, List_Plcp1_Logic list_Plcp1_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {
                ecr.data.results = list_Plcp1_Logic.GetList(request);
                ecr.meta.code = 200;
                ecr.meta.message = "OK";
            }
            else
            {
                ecr.meta.code = 401;
                ecr.meta.message = "Unauthorized";
            }
        }
        public void ListRcvy1(Auth auth, List_Rcvy1 request, List_Rcvy1_Logic list_Rcvy1_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {
                if(uri.IndexOf("/sps/") > 0){
                    ecr.data.results = list_Rcvy1_Logic.GetSpsList(request);
                }else{                    
                    ecr.data.results = list_Rcvy1_Logic.GetList(request);
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
