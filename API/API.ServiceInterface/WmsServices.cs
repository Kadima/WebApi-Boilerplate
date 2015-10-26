using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmsWS.ServiceModel;
using WmsWS.ServiceModel.Wms;
using WmsWS.ServiceInterface.Wms;
using System.IO;

namespace WmsWS.ServiceInterface
{
    public class WmsServices : Service
    {        
        public Auth auth { get; set; }
        public List_Login_Logic list_Login_Logic { get; set; }
        public List_Imgr1_Logic list_Imgr1_Logic { get; set; }
        public List_Impr1_Logic list_Impr1_Logic { get; set; }
        public List_Imgr2_Logic list_Imgr2_Logic { get; set; }
        public Confirm_Imgr1_Logic confirm_Imgr1_Logic { get; set; }
        public List_Imgi1_Logic list_Imgi1_Logic { get; set; }
        public List_Imgi2_Logic list_Imgi2_Logic { get; set; }
        public List_Imsn1_Logic list_Imsn1_Logic { get; set; }
        //public List_JobNo_Logic list_JobNo_Logic { get; set; }
        //public Update_Done_Logic update_Done_Logic { get; set; }
        public List_Rcbp1_Logic list_Rcbp1_Logic { get; set; }
        public List_Rcbp3_Logic list_Rcbp3_Logic { get; set; }

        public object Any(List_Login request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try 
            {
                LoginService ls = new LoginService();
                ls.initial(auth, request, list_Login_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex){
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails.";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }            
            return ecr;
        }
        public object Any(List_Imgr1 request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ListService ls = new ListService();
                ls.ListImgr1(auth, request, list_Imgr1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails.";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }
            return ecr;
        }
        public object Any(List_Impr1 request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ListService ls = new ListService();
                ls.ListImpr1(auth, request, list_Impr1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails.";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }
            return ecr;
        }
        public object Any(List_Imgr2 request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ListService ls = new ListService();
                ls.ListImgr2(auth, request, list_Imgr2_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails.";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }
            return ecr;
        }
        public object Any(Confirm_Imgr1 request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ConfirmService cs = new ConfirmService();
                cs.ConfirmImgr1(auth, request, confirm_Imgr1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails.";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }
            return ecr;
        }
        public object Any(List_Imgi1 request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ListService ls = new ListService();
                ls.ListImgi1(auth, request, list_Imgi1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails.";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }
            return ecr;
        }
        public object Any(List_Imgi2 request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ListService ls = new ListService();
                ls.ListImgi2(auth, request, list_Imgi2_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails.";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }
            return ecr;
        }
        public object Any(List_Imsn1 request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ListService ls = new ListService();
                ls.ListImsn1(auth, request, list_Imsn1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails.";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }
            return ecr;
        }
        /*
        public object Any(List_AsnNo request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ListService ls = new ListService();
                ls.ListJobNo(auth, request, list_JobNo_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }
            return ecr;
        }
        public object Any(Update_Done request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                DoneService ds = new DoneService();
                ds.initial(auth, request, update_Done_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails.";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }
            return ecr;
        }
        */
        public object Any(List_Rcbp1 request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ListService ls = new ListService();
                ls.ListRcbp1(auth, request, list_Rcbp1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails.";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }
            return ecr;
        }
        public object Any(List_Rcbp3 request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ListService ls = new ListService();
                ls.ListRcbp3(auth, request, list_Rcbp3_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex)
            {
                ecr.meta.code = 599;
                ecr.meta.message = "The server handle exceptions, the operation fails.";
                ecr.meta.errors.code = ex.HResult;
                ecr.meta.errors.field = ex.HelpLink;
                ecr.meta.errors.message = ex.Message.ToString();
            }
            return ecr;
        }
    }
}
