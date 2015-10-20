using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Collections;
using API.ServiceModel;
using API.ServiceModel.Wms;

namespace API.ServiceInterface
{
    public class WmsServices : Service
    {
        public Auth auth { get; set; }
        public LoginLogic loginLogic { get; set; }
        public WhScanLogic whScanLogic { get; set; }
        public ConfirmLogic ConfirmLogic { get; set; }
        public TableFieldLogic tableFieldLogic { get; set; }
        public SQLCommandLogic sqlCommandLogic { get; set; }
        public object Any(WmsLogin request)
        {
            var signature = this.Request.Headers.GetValues("Signature");
            if (signature != null)
            {
                if (auth.AuthResult(signature[0].ToString(), this.Request.RawUrl))
                {
                    var Result = loginLogic.LoginCheck(request).ToString();
                    return new LoginResponse { strResult = Result };
                }
            }
            return new AuthFaildResponse { strResult = "No Authentication." };
        }
        public object Any(WhScan request)
        {
            var signature = this.Request.Headers.GetValues("Signature");
            if (signature != null)
            {
                if (auth.AuthResult(signature[0].ToString(), this.Request.RawUrl))
                {
                    var Result = whScanLogic.InsertResult(request);
                    return new WhScanResponse { intResult = Result };
                }
            }
            return new AuthFaildResponse { strResult = "No Authentication." };
        }
        public object Any(Confirm request)
        {
            var signature = this.Request.Headers.GetValues("Signature");
            if (signature != null)
            {
                if (auth.AuthResult(signature[0].ToString(), this.Request.RawUrl))
                {
                    string Result = "";
                    // I = Verify GIN,P=Putway,T=Goods Transfer,V=Verify GRN,K=Picking GIN
                    if (request.ConfirmType == "I")
                    { Result = ConfirmLogic.VerifyGIN(request.AsnNo, request.UserID); }
                    if (request.ConfirmType == "P")
                    { Result = ConfirmLogic.Putway(request.AsnNo, request.UserID); }
                    if (request.ConfirmType == "T")
                    { Result = ConfirmLogic.GoodsTransfer(request.AsnNo, request.UserID); }
                    if (request.ConfirmType == "V")
                    { Result = ConfirmLogic.VerifyGRNNew(request.AsnNo, request.UserID); }
                    if (request.ConfirmType == "K")
                    { Result = ConfirmLogic.PickingGIN(request.AsnNo, request.UserID); }
                    return new ConfirmResponse { strResult = Result };
                }
            }
            return new AuthFaildResponse { strResult = "No Authentication." };
        }
        public object Any(TableField request)
        {
            var signature = this.Request.Headers.GetValues("Signature");
            if (signature != null)
            {
                if (auth.AuthResult(signature[0].ToString(), this.Request.RawUrl))
                {
                    TableFieldResponse[] tfr = tableFieldLogic.ShowResponse(request);
                    return tfr;
                }
            }
            return new AuthFaildResponse { strResult = "No Authentication." };
        }
        public object Any(SQLCommand request)
        {
            if (auth.AuthResult(this.Request.Headers.GetValues("Signature"), this.Request.RawUrl))
            {
                ArrayList ht = sqlCommandLogic.ShowResponse(request);
                return new SQLCommandResponse { objResult = ht };
            }
            return new AuthFaildResponse { strResult = "No Authentication." };
        }            
    }
}