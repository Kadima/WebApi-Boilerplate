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
        public void List_Tracking(Auth auth, List_Tracking request, List_Tracking_Logic list_Tracking_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {

                ecr.data.results = list_Tracking_Logic.GetCount(request);
                ecr.meta.code = 200;
                ecr.meta.message = "OK";
            }
            else
            {
                ecr.meta.code = 401;
                ecr.meta.message = "Unauthorized";
            }
        }
        public void List_Tracking_ContainerNo(Auth auth, List_Tracking_ContainerNo request, List_Tracking_ContainerNo_Logic list_Tracking_ContainerNo_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {

                ecr.data.results = list_Tracking_ContainerNo_Logic.GetList(request);
                ecr.meta.code = 200;
                ecr.meta.message = "OK";
            }
            else
            {
                ecr.meta.code = 401;
                ecr.meta.message = "Unauthorized";
            }
        }
        public void List_Tracking_ContainerNo_AE(Auth auth, List_Tracking_ContainerNo_AE request, List_Tracking_ContainerNo_AE_Logic list_Tracking_ContainerNo_AE_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {

                ecr.data.results = list_Tracking_ContainerNo_AE_Logic.GetList(request);
                ecr.meta.code = 200;
                ecr.meta.message = "OK";
            }
            else
            {
                ecr.meta.code = 401;
                ecr.meta.message = "Unauthorized";
            }
        }
        public void List_Tracking_ContainerNo_AI(Auth auth, List_Tracking_ContainerNo_AI request, List_Tracking_ContainerNo_AI_Logic list_Tracking_ContainerNo_AI_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {

                ecr.data.results = list_Tracking_ContainerNo_AI_Logic.GetList(request);
                ecr.meta.code = 200;
                ecr.meta.message = "OK";
            }
            else
            {
                ecr.meta.code = 401;
                ecr.meta.message = "Unauthorized";
            }
        }
        public void List_Tracking_ContainerNo_SE(Auth auth, List_Tracking_ContainerNo_SE request, List_Tracking_ContainerNo_SE_Logic list_Tracking_ContainerNo_SE_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {

                ecr.data.results = list_Tracking_ContainerNo_SE_Logic.GetList(request);
                ecr.meta.code = 200;
                ecr.meta.message = "OK";
            }
            else
            {
                ecr.meta.code = 401;
                ecr.meta.message = "Unauthorized";
            }
        }
        public void List_Tracking_ContainerNo_SI(Auth auth, List_Tracking_ContainerNo_SI request, List_Tracking_ContainerNo_SI_Logic list_Tracking_ContainerNo_SI_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {

                ecr.data.results = list_Tracking_ContainerNo_SI_Logic.GetList(request);
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
