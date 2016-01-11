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
        public void List_Rcbp3(Auth auth, List_Rcbp3 request, List_Rcbp3_Logic list_Rcbp3_Logic, CommonResponse ecr, string[] token, string uri)
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
        public void List_Plcp1(Auth auth, List_Plcp1 request, List_Plcp1_Logic list_Plcp1_Logic, CommonResponse ecr, string[] token, string uri)
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
        public void List_Rcvy1(Auth auth, List_Rcvy1 request, List_Rcvy1_Logic list_Rcvy1_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {
                if(uri.IndexOf("/sps") > 0){
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
        public void List_Tracking(Auth auth, List_Tracking request, List_Tracking_Logic list_Tracking_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {
																if (uri.IndexOf("/tracking/OrderNo") > 0)
																{
																				ecr.data.results = list_Tracking_Logic.GetOmtx1List(request);
																}
																else if (uri.IndexOf("/tracking/sps") > 0)
																{
																				ecr.data.results = list_Tracking_Logic.GetSpsList(request);
																}
																else if (uri.IndexOf("/tracking/count") > 0)
																{
																				ecr.data.results = list_Tracking_Logic.GetCount(request);
																}
																else if (uri.IndexOf("ModuleCode=AE") > 0)
																{
																				ecr.data.results = list_Tracking_Logic.GetAEList(request);
																}
																else if (uri.IndexOf("ModuleCode=AI") > 0)
																{
																				ecr.data.results = list_Tracking_Logic.GetAIList(request);
																}
																else if (uri.IndexOf("ModuleCode=SE") > 0)
																{
																				ecr.data.results = list_Tracking_Logic.GetSEList(request);
																}
																else if (uri.IndexOf("ModuleCode=SI") > 0)
																{
																				ecr.data.results = list_Tracking_Logic.GetSIList(request);
																}
																else
																{
																				ecr.data.results = list_Tracking_Logic.GetList(request);
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
