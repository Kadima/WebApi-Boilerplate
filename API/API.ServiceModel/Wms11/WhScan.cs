using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Data;
using ServiceStack.Data;

namespace API.ServiceModel.Wms
{
    //Ver GRN
    [Route("/api/scan/{UserID}/{ConfrimType}/{ClearData}/{AsnNo}/{BarCode}/{Qty}", "Put")]
    //putaway
    [Route("/api/scan/{UserID}/{ConfrimType}/{ClearData}/{AsnNo}/{CustomerCode}/{WarehouseCode}/{StoreNo}/{BarCode}/{Qty}/{SerialNo}", "Put")]
    //GoodsTransfer
    [Route("/api/scan/{UserID}/{ConfrimType}/{ClearData}/{BarCode}/{OldWarehouseCode}/{OldStoreNo}/{WarehouseCode}/{StoreNo}/{Qty}", "Put")]
    //PickingGIN
    [Route("/api/scan/{UserID}/{ConfrimType}/{ClearData}/{GoodsIssueNoteNo}/{CustomerCode}/{IssueNoteLineItemNo}/{WarehouseCode}/{StoreNo}/{BarCode}/{Qty}/{SerialNo}", "Put")]
    //VerifyGIN
    [Route("/api/scan/{UserID}/{ConfrimType}/{ClearData}/{GoodsIssueNoteNo}/{CustomerCode}/{BarCode}/{Qty}", "Put")]
   
    public class WhScan : IReturn<WhScanResponse>
    {
        public string UserID { get; set; }
        public string ClearData { get; set; }
        public string AsnNo { get; set; }
        public string BarCode { get; set; }
        public string CustomerCode { get; set; }
        public string Description { get; set; }
        public string GoodsIssueNoteNo { get; set; }
        public int IssueNoteLineItemNo { get; set; }
        public string ConfrimType { get; set; }
        public string OldStoreNo { get; set; }
        public string ProductCode { get; set; }
        public int Qty { get; set; }
        public string StoreNo { get; set; }
        public string SerialNo { get; set; }
        public string WarehouseCode { get; set; }
        public string OldWarehouseCode { get; set; }
    }
    public class WhScanResponse
    {
        public int intResult { get; set; }
    }
    public class WhScanLogic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }

        public int InsertResult(WhScan request)
        {
            int Result = -1;
            try
            {
                using (var db = DbConnectionFactory.OpenDbConnection())
                {
                    string strSQLCommand = "";
                    if (request.ClearData == "Y")
                    {
                        strSQLCommand = "Delete WhScan Where ComfirmBy = " + Modfunction.SQLSafeValue(request.UserID) + " AND ConfrimType = " + Modfunction.SQLSafeValue(request.ConfrimType);
                        Result = db.Scalar<int>(strSQLCommand);
                    }
                    strSQLCommand = "insert into whscan(AsnNo,BarCode,ConfirmBy,CustomerCode,GoodsIssueNoteNo,ConfrimType,OldStoreNo,ProductCode,Qty,StoreNo,SerialNo,WarehouseCode,OldWarehouseCode,IssueNoteLineItemNo) Values(" + Modfunction.SQLSafeValue(request.AsnNo) + "," + Modfunction.SQLSafeValue(request.BarCode) + "," + Modfunction.SQLSafeValue(request.UserID) + "," + Modfunction.SQLSafeValue(request.CustomerCode) + "," + Modfunction.SQLSafeValue(request.GoodsIssueNoteNo) + "," + Modfunction.SQLSafeValue(request.ConfrimType) + "," + Modfunction.SQLSafeValue(request.OldStoreNo) + "," + Modfunction.SQLSafeValue(request.ProductCode) + "," + Modfunction.SQLSafeValue(request.Qty) + "," + Modfunction.SQLSafeValue(request.StoreNo) + "," + Modfunction.SQLSafeValue(request.SerialNo) + "," + Modfunction.SQLSafeValue(request.WarehouseCode) + "," + Modfunction.SQLSafeValue(request.OldWarehouseCode) + "," + Modfunction.SQLSafeValue(request.IssueNoteLineItemNo) + ")";
                    Result = db.Scalar<int>(strSQLCommand);
                }                
            }
            catch
            {
                Result = -1;
            }  
            return Result;
        }
    }

}
