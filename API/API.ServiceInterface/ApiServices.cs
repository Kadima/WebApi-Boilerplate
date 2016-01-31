using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceInterface;
using WebApi.ServiceModel;
using WebApi.ServiceModel.Wms;
using WebApi.ServiceModel.Tms;
using WebApi.ServiceModel.Freight;
using WebApi.ServiceModel.Common;
using WebApi.ServiceInterface.Wms;
using WebApi.ServiceInterface.Tms;
using System.IO;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface.Cors;

namespace WebApi.ServiceInterface
{
    //[EnableCors(allowedOrigins: "*", allowedMethods: "GET, POST, PUT, DELETE, OPTIONS", allowedHeaders: "Content-Type, Signature")]
    public class ApiServices : Service
    {        
        public Auth auth { get; set; }
								#region WMS
								public ServiceModel.Wms.Wms_Login_Logic wms_Login_Logic { get; set; }
								public object Any(ServiceModel.Wms.Wms_Login request)
								{
												CommonResponse ecr = new CommonResponse();
												ecr.initial();
												try
												{
																ServiceInterface.Wms.LoginService ls = new ServiceInterface.Wms.LoginService();
																ls.initial(auth, request, wms_Login_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
												}
												catch (Exception ex) { cr(ecr, ex); }
												return ecr;
								}
								public ServiceModel.Wms.List_Imgr1_Logic list_Imgr1_Logic { get; set; }
								public object Get(ServiceModel.Wms.List_Imgr1 request)
								{
												CommonResponse ecr = new CommonResponse();
												ecr.initial();
												try
												{
																ServiceInterface.Wms.ListService ls = new ServiceInterface.Wms.ListService();
																ls.List_Imgr1(auth, request, list_Imgr1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
												}
												catch (Exception ex) { cr(ecr, ex); }
												return ecr;
								}
								public ServiceModel.Wms.List_Impr1_Logic list_Impr1_Logic { get; set; }
								public object Get(ServiceModel.Wms.List_Impr1 request)
								{
												CommonResponse ecr = new CommonResponse();
												ecr.initial();
												try
												{
																ServiceInterface.Wms.ListService ls = new ServiceInterface.Wms.ListService();
																ls.List_Impr1(auth, request, list_Impr1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
												}
												catch (Exception ex) { cr(ecr, ex); }
												return ecr;
								}
								public ServiceModel.Wms.List_Imgr2_Logic list_Imgr2_Logic { get; set; }
								public object Get(ServiceModel.Wms.List_Imgr2 request)
								{
												CommonResponse ecr = new CommonResponse();
												ecr.initial();
												try
												{
																ServiceInterface.Wms.ListService ls = new ServiceInterface.Wms.ListService();
																ls.List_Imgr2(auth, request, list_Imgr2_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
												}
												catch (Exception ex) { cr(ecr, ex); }
												return ecr;
								}
								public ServiceModel.Wms.Confirm_Imgr1_Logic confirm_Imgr1_Logic { get; set; }
								public object Post(ServiceModel.Wms.Confirm_Imgr1 request)
								{
												CommonResponse ecr = new CommonResponse();
												ecr.initial();
												try
												{
																ServiceInterface.Wms.ConfirmService cs = new ServiceInterface.Wms.ConfirmService();
																cs.ConfirmImgr1(auth, request, confirm_Imgr1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
												}
												catch (Exception ex) { cr(ecr, ex); }
												return ecr;
								}
								public ServiceModel.Wms.List_Imgi1_Logic list_Imgi1_Logic { get; set; }
								public object Get(ServiceModel.Wms.List_Imgi1 request)
								{
												CommonResponse ecr = new CommonResponse();
												ecr.initial();
												try
												{
																ServiceInterface.Wms.ListService ls = new ServiceInterface.Wms.ListService();
																ls.List_Imgi1(auth, request, list_Imgi1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
												}
												catch (Exception ex) { cr(ecr, ex); }
												return ecr;
								}
								public ServiceModel.Wms.List_Imgi2_Logic list_Imgi2_Logic { get; set; }
								public object Get(ServiceModel.Wms.List_Imgi2 request)
								{
												CommonResponse ecr = new CommonResponse();
												ecr.initial();
												try
												{
																ServiceInterface.Wms.ListService ls = new ServiceInterface.Wms.ListService();
																ls.List_Imgi2(auth, request, list_Imgi2_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
												}
												catch (Exception ex) { cr(ecr, ex); }
												return ecr;
								}
								public ServiceModel.Wms.List_Imsn1_Logic list_Imsn1_Logic { get; set; }
								public object Get(ServiceModel.Wms.List_Imsn1 request)
								{
												CommonResponse ecr = new CommonResponse();
												ecr.initial();
												try
												{
																ServiceInterface.Wms.ListService ls = new ServiceInterface.Wms.ListService();
																ls.List_Imsn1(auth, request, list_Imsn1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
												}
												catch (Exception ex) { cr(ecr, ex); }
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
																ecr.meta.errors.code = ex.GetHashCode();
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
																ecr.meta.errors.code = ex.GetHashCode();
																ecr.meta.errors.field = ex.HelpLink;
																ecr.meta.errors.message = ex.Message.ToString();
												}
												return ecr;
								}
								*/
								#endregion
								#region Tms
        public ServiceModel.Tms.Tms_Login_Logic tms_Login_Logic { get; set; }
        public object Any(ServiceModel.Tms.Tms_Login request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ServiceInterface.Tms.LoginService ls = new ServiceInterface.Tms.LoginService();
                ls.initial(auth, request, tms_Login_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex) { cr(ecr, ex); }
            return ecr;
        }
        public ServiceModel.Tms.List_JobNo_Logic list_JobNo_Logic { get; set; }
        public object Get(ServiceModel.Tms.List_JobNo request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ServiceInterface.Tms.ListService ls = new ServiceInterface.Tms.ListService();
                ls.ListJobNo(auth, request, list_JobNo_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex) { cr(ecr, ex); }
            return ecr;
        }
        public ServiceModel.Tms.List_Container_Logic list_Container_Logic { get; set; }
        public object Get(ServiceModel.Tms.List_Container request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ServiceInterface.Tms.ListService ls = new ServiceInterface.Tms.ListService();
                ls.ListContainer(auth, request, list_Container_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex) { cr(ecr, ex); }
            return ecr;
        }
        public ServiceModel.Tms.List_Jmjm6_Logic list_Jmjm6_Logic { get; set; }
        public object Get(ServiceModel.Tms.List_Jmjm6 request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ServiceInterface.Tms.ListService ls = new ServiceInterface.Tms.ListService();
                ls.ListJmjm6(auth, request, list_Jmjm6_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex) { cr(ecr, ex); }
            return ecr;
        }
        public ServiceModel.Tms.Update_Done_Logic update_Done_Logic { get; set; }
        public object Post(ServiceModel.Tms.Update_Done request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ServiceInterface.Tms.DoneService ds = new ServiceInterface.Tms.DoneService();
                ds.initial(auth, request, update_Done_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex) { cr(ecr, ex); }
            return ecr;
        }
								#endregion
								#region Freight
        public ServiceModel.Freight.Freight_Login_Logic freight_Login_Logic { get; set; }
        public object Any(ServiceModel.Freight.Freight_Login request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ServiceInterface.Freight.LoginService ls = new ServiceInterface.Freight.LoginService();
                ls.initial(auth, request, freight_Login_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex) { cr(ecr, ex); }
            return ecr;
        }
								public ServiceModel.Freight.Saus_Logic saus_Logic { get; set; }
								public object Any(ServiceModel.Freight.Saus request)
								{
												CommonResponse ecr = new CommonResponse();
												ecr.initial();
												try
												{
																ServiceInterface.Freight.TableService ls = new ServiceInterface.Freight.TableService();
																ls.TS_Saus(auth, request, saus_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
												}
												catch (Exception ex) { cr(ecr, ex); }
												return ecr;
								}
								public ServiceModel.Freight.Smsa_Logic smsa_Logic { get; set; }
								public object Any(ServiceModel.Freight.Smsa request)
								{
												CommonResponse ecr = new CommonResponse();
												ecr.initial();
												try
												{
																ServiceInterface.Freight.TableService ts = new ServiceInterface.Freight.TableService();
																ts.TS_Smsa(auth, request, smsa_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
												}
												catch (Exception ex) { cr(ecr, ex); }
												return ecr;
								}
								public ServiceModel.Freight.Plvi_Logic plvi_Logic { get; set; }
								public object Any(ServiceModel.Freight.Plvi request)
								{
												CommonResponse ecr = new CommonResponse();
												ecr.initial();
												try
												{
																ServiceInterface.Freight.TableService ts = new ServiceInterface.Freight.TableService();
																ts.TS_Plvi(auth, request, plvi_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
												}
												catch (Exception ex) { cr(ecr, ex); }
												return ecr;
								}
        public ServiceModel.Freight.Rcbp_Logic rcbp_Logic { get; set; }
        public object Any(ServiceModel.Freight.Rcbp request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ServiceInterface.Freight.TableService ls = new ServiceInterface.Freight.TableService();
                ls.TS_Rcbp(auth, request, rcbp_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex) { cr(ecr, ex); }
            return ecr;
        }
        public ServiceModel.Freight.Rcvy_Logic list_Rcvy1_Logic { get; set; }
        public object Get(ServiceModel.Freight.Rcvy request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ServiceInterface.Freight.TableService ls = new ServiceInterface.Freight.TableService();
                ls.TS_Rcvy(auth, request, list_Rcvy1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex) { cr(ecr, ex); }
            return ecr;
        }
        public ServiceModel.Freight.Tracking_Logic list_Tracking_Logic { get; set; }
        public object Get(ServiceModel.Freight.Tracking request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ServiceInterface.Freight.TableService ls = new ServiceInterface.Freight.TableService();
                ls.TS_Tracking(auth, request, list_Tracking_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex) { cr(ecr, ex); }
            return ecr;
        }
								#endregion
								#region Common
        public ServiceModel.Common.List_Rcbp1_Logic list_Rcbp1_Logic { get; set; }
        public object Get(ServiceModel.Common.List_Rcbp1 request)
        {
            CommonResponse ecr = new CommonResponse();
            ecr.initial();
            try
            {
                ServiceInterface.Common.ListService ls = new ServiceInterface.Common.ListService();
                ls.List_Rcbp1(auth, request, list_Rcbp1_Logic, ecr, this.Request.Headers.GetValues("Signature"), this.Request.RawUrl);
            }
            catch (Exception ex) { cr(ecr, ex); }
            return ecr;
        }
								#endregion
        private CommonResponse cr(CommonResponse ecr, Exception ex)
        {
            ecr.meta.code = 599;
            ecr.meta.message = "The server handle exceptions, the operation fails.";
            ecr.meta.errors.code = ex.GetHashCode();
            ecr.meta.errors.field = ex.HelpLink;
            ecr.meta.errors.message = ex.Message.ToString();
            return ecr;
        }
    }
}
