using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.ServiceModel;
using WebApi.ServiceModel.Common;
using WebApi.ServiceModel.Freight;

namespace WebApi.ServiceInterface.Freight
{
    public class TableService
				{
								public void TS_Smsa(Auth auth, Smsa request, Smsa_Logic logic, CommonResponse ecr, string[] token, string uri)
								{
												if (auth.AuthResult(token, uri))
												{
																if (uri.IndexOf("/smsa1/count") > 0)
																{
																				ecr.data.results = logic.GetCount(request);
																}
																else if (uri.IndexOf("/smsa1/sps") > 0)
																{
																				ecr.data.results = logic.GetSpsList(request);
																}
																else if (uri.IndexOf("/smsa2/create") > 0)
																{
																}
																else if (uri.IndexOf("/smsa2/update") > 0)
																{
																}
																else if (uri.IndexOf("/smsa2/read") > 0)
																{
																				ecr.data.results = logic.Read_Smsa2(request);
																}
																else if (uri.IndexOf("/smsa2/delete") > 0)
																{
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
																if (uri.IndexOf("/sps") > 0)
																{
																				ecr.data.results = list_Plcp1_Logic.GetSpsList(request);
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
								public void List_Saus1(Auth auth, List_Saus1 request, List_Saus1_Logic list_Saus1_Logic, CommonResponse ecr, string[] token, string uri)
								{
												if (auth.AuthResult(token, uri))
												{
																if (uri.IndexOf("/memo") > 0)
																{
																				ecr.data.results = list_Saus1_Logic.GetMemo(request);
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
