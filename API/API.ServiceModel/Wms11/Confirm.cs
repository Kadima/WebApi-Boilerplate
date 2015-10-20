using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.Auth;

namespace API.ServiceModel.Wms
{
    [Route("/api/action/confirm", "Post Get")]  
  
    public class Confirm : IReturn<ConfirmResponse>
    {
        public string UserID { get; set; }
        public string AsnNo { get; set; }
        public string ConfirmType { get; set; }       
    }
    public class ConfirmResponse
    {
        public string strResult { get; set; }
    }
    public class ConfirmLogic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public IConnectString ConnectString { get; set; }
        public string GoodsTransfer(string strAsn, string strConfirmBy)
        {
            string Result = "";
            string strBarCode = GetBarCode("UserDefine01");
            DataTable dtWhScan = GetSQLCommandReturnDT("Select *,Impr1.TrxNo AS PrductTrxNo,Impr1.CustomerCode AS ProductCustomerCode from WhScan join Impr1 On Impr1." + strBarCode + " = WhScan.BarCode Where AsnNo = " + Modfunction.SQLSafeValue(strAsn) + " AND ComfirmBy = " + Modfunction.SQLSafeValue(strConfirmBy));
            Boolean blnSameCustomer = true;
            if (dtWhScan != null && dtWhScan.Rows.Count > 0)
            {
                string strCustomer = Modfunction.CheckNull(dtWhScan.Rows[0]["ProductCustomerCode"]);
                for (int i = 1; i < dtWhScan.Rows.Count; i++)
                {
                    if (strCustomer != Modfunction.CheckNull(dtWhScan.Rows[0]["ProductCustomerCode"]))
                    {
                        blnSameCustomer = false;
                        break;
                    }
                }
                Result = SaveImit(dtWhScan, blnSameCustomer);
            }
            return Result;
        }
        public string VerifyGIN(string strAsnNo, string strConfirmBy)
        {
            string Result = "";
            string strBarCode = GetBarCode("UserDefine01");
            DataTable dtWhScan = GetSQLCommandReturnDT("Select *,(Select TrxNo from Impr1 Where Impr1." + strBarCode + " = WhScan.BarCode) AS ProductTrxNo,'' AS ComputeFlag from WhScan Where GoodsIssueNoteNo = " + Modfunction.SQLSafeValue(strAsnNo) + " AND ComfirmBy = " + Modfunction.SQLSafeValue(strConfirmBy));
            if (dtWhScan != null & dtWhScan.Rows.Count > 0)
            {
                DataTable dtImgi = GetSQLCommandReturnDT("Select TrxNo from Imgi1 Where GoodsIssueNoteNo = " + Modfunction.SQLSafeValue(dtWhScan.Rows[0]["GoodsIssueNoteNo"]) + "AND CustomerCode = " + Modfunction.SQLSafeValue(dtWhScan.Rows[0]["CustomerCode"]));
                if (dtImgi != null && dtImgi.Rows.Count > 0)
                {
                    dtImgi = GetSQLCommandReturnDT("select ProductTrxNo,SUM(case DimensionFlag when '1' then PackingQty when '2' then WholeQty else LooseQty end) DimQty,'' AS ComputeFlag from imgi2 Where TrxNo = " + Modfunction.SQLSafeValue(dtImgi.Rows[0]["TrxNo"]) + " Group by ProductTrxNo,DimensionFlag");
                    if (dtImgi != null && dtImgi.Rows.Count > 0)
                    {
                        int intScanQty, intImgiQty;
                        for (int intScanIndex = 0; intScanIndex < dtWhScan.Rows.Count; intScanIndex++)
                        {
                            if (dtWhScan.Rows[intScanIndex]["CompateFlag"].ToString() == "")
                            {
                                int intScanNewIndex = intScanIndex;
                                intScanQty = 0;
                                string strProductTrxNo = dtWhScan.Rows[intScanIndex]["ProductTrxNo"].ToString();
                                for (intScanNewIndex = intScanIndex; intScanNewIndex < dtWhScan.Rows.Count - 1; intScanNewIndex++)
                                {
                                    if (strProductTrxNo == dtWhScan.Rows[intScanNewIndex]["ProductTrxNo"].ToString() && dtWhScan.Rows[intScanNewIndex]["CompateFlag"].ToString() == "")
                                    {
                                        intScanQty = intScanQty + (int)dtWhScan.Rows[intScanNewIndex]["Qty"];
                                        dtWhScan.Rows[intScanNewIndex]["CompateFlag"] = "Y";
                                    }
                                }
                                intImgiQty = 0;
                                for (int intImgiIndex = 0; intImgiIndex < dtImgi.Rows.Count - 1; intImgiIndex++)
                                {
                                    if (strProductTrxNo == dtImgi.Rows[intImgiIndex]["ProductTrxNo"].ToString() && dtImgi.Rows[intImgiIndex]["CompateFlag"].ToString() == "")
                                    {
                                        dtImgi.Rows[intImgiIndex]["CompateFlag"] = "Y";
                                        intImgiQty = (int)dtImgi.Rows[intImgiIndex]["DimQty"];
                                    }
                                }
                                if (intScanQty != intImgiQty)
                                { return "The quantity of goods is wrong."; }
                            }
                        }
                        for (int intImgiIndex = 0; intImgiIndex < dtImgi.Rows.Count - 1; intImgiIndex++)
                        {
                            if (dtImgi.Rows[intImgiIndex]["CompateFlag"].ToString() == "")
                            {
                                return "Some goods for ASN not scan.";
                            }
                        }
                    }
                    else
                    { return "The Issue Note and Customer not mapping."; }
                }
                else
                { return "The Issue Note and Customer not mapping."; }
            }
            else
            { return "Not data for Scan."; }
            //VerifyGIN
            //  [Route("{GoodsIssueNoteNo}/{CustomerCode}/{BarCode}/{Qty}", "Get")]
            return Result;
        }
        public string PickingGIN(string strAsnNo, string strConfirmBy)
        {
            string Result = "";
            string strBarCode = GetBarCode("UserDefine01");
            DataTable dtWhScan = GetSQLCommandReturnDT("Select *,(Select TrxNo from Impr1 Where Impr1." + strBarCode + " = WhScan.BarCode) AS ProductTrxNo,'' AS ComputeFlag from WhScan Where GoodsIssueNoteNo = " + Modfunction.SQLSafeValue(strAsnNo) + " AND ComfirmBy = " + Modfunction.SQLSafeValue(strConfirmBy));
            if (dtWhScan != null & dtWhScan.Rows.Count > 0)
            {
                DataTable dtImgi = GetSQLCommandReturnDT("Select TrxNo from Imgi1 Where GoodsIssueNoteNo = " + Modfunction.SQLSafeValue(dtWhScan.Rows[0]["GoodsIssueNoteNo"]) + "AND CustomerCode = " + Modfunction.SQLSafeValue(dtWhScan.Rows[0]["CustomerCode"]));
                if (dtImgi != null && dtImgi.Rows.Count > 0)
                {
                    dtImgi = GetSQLCommandReturnDT("select ProductTrxNo,SUM(case DimensionFlag when '1' then PackingQty when '2' then WholeQty else LooseQty end) DimQty,'' AS ComputeFlag from imgi2 Where TrxNo = " + Modfunction.SQLSafeValue(dtImgi.Rows[0]["TrxNo"]) + " Group by ProductTrxNo,DimensionFlag");
                    if (dtImgi != null && dtImgi.Rows.Count > 0)
                    {
                        int intScanQty, intImgiQty;
                        for (int intScanIndex = 0; intScanIndex < dtWhScan.Rows.Count; intScanIndex++)
                        {
                            if (dtWhScan.Rows[intScanIndex]["CompateFlag"].ToString() == "")
                            {
                                int intScanNewIndex = intScanIndex;
                                intScanQty = 0;
                                string strProductTrxNo = dtWhScan.Rows[intScanIndex]["ProductTrxNo"].ToString();
                                for (intScanNewIndex = intScanIndex; intScanNewIndex < dtWhScan.Rows.Count - 1; intScanNewIndex++)
                                {
                                    if (strProductTrxNo == dtWhScan.Rows[intScanNewIndex]["ProductTrxNo"].ToString() && dtWhScan.Rows[intScanNewIndex]["CompateFlag"].ToString() == "")
                                    {
                                        intScanQty = intScanQty + (int)dtWhScan.Rows[intScanNewIndex]["Qty"];
                                        dtWhScan.Rows[intScanNewIndex]["CompateFlag"] = "Y";
                                    }
                                }
                                intImgiQty = 0;
                                for (int intImgiIndex = 0; intImgiIndex < dtImgi.Rows.Count - 1; intImgiIndex++)
                                {
                                    if (strProductTrxNo == dtImgi.Rows[intImgiIndex]["ProductTrxNo"].ToString() && dtImgi.Rows[intImgiIndex]["CompateFlag"].ToString() == "")
                                    {
                                        dtImgi.Rows[intImgiIndex]["CompateFlag"] = "Y";
                                        intImgiQty = (int)dtImgi.Rows[intImgiIndex]["DimQty"];
                                    }
                                }
                                if (intScanQty != intImgiQty)
                                { return "The quantity of goods is wrong."; }
                            }
                        }
                        for (int intImgiIndex = 0; intImgiIndex < dtImgi.Rows.Count - 1; intImgiIndex++)
                        {
                            if (dtImgi.Rows[intImgiIndex]["CompateFlag"].ToString() == "")
                            {
                                return "Some goods for ASN not scan.";
                            }
                        }
                        for (int intScanIndex = 0; intScanIndex < dtWhScan.Rows.Count - 1; intScanIndex++)
                        {
                            saveImsn1SerialNo(dtWhScan.Rows[intScanIndex]["GoodsIssueNoteNo"].ToString(), Convert.ToInt32(dtWhScan.Rows[intScanIndex]["IssueNoteLineItemNo"]), dtWhScan.Rows[intScanIndex]["SerialNo"].ToString(), false);
                        }
                    }
                    else
                    { return "The Issue Note and Customer not mapping."; }
                }
                else
                { return "The Issue Note and Customer not mapping."; }
            }
            else
            { return "Not data for Scan."; }
            return Result;
            //PickingGIN
            //[Route("{GoodsIssueNoteNo}/{CustomerCode}/{IssueNoteLineItemNo}/{WarehouseCode}/{StoreNo}/{BarCode}/{Qty}/{SerialNo}", "Get")]

        }
        public string Putway(string strAsn, string strConfirmBy)
        {
            string Result = "";
            DataTable dtOmtx2, dtWhScan;
            string strBarCodeColumn = GetBarCode("UserDefine01");
            dtWhScan = GetSQLCommandReturnDT("Select WhScan.*,(Select TrxNo from Impr1 Where impr1. " + strBarCodeColumn + " = Whscan.BarCode) AS ProductCodeTrxNo,(Select WarehouseName from whwh1 where WarehouseCode = WhScan.WareHouse ) AS WarehouseName,rcbp1.BusinessPartyName,rcbp1.Address1,rcbp1.Address2,rcbp1.Address3,rcbp1.Address4  from WhScan Join Rcbp1 on WhScan.CustomerCode=Rcbp1.BusinessPartyCode Where AsnNo = " + Modfunction.SQLSafeValue(strAsn) + " AND ComfirmBy = " + Modfunction.SQLSafeValue(strConfirmBy));
            if (dtWhScan != null && dtWhScan.Rows.Count > 0)
            {
                dtOmtx2 = GetSQLCommandReturnDT("select BalanceLooseQty,BalancePackingQty,BalanceWholeQty,DimensionFlag,ProductCode,TrxNo,LineItemNo from Omtx2 Where TrxNo = (Select Top 1 TrxNo from Omtx1 Where AsnNo = " + Modfunction.SQLSafeValue(strAsn) + ")");
                if (dtOmtx2 != null && dtOmtx2.Rows.Count > 0)
                {
                    SaveImgr(dtWhScan, dtOmtx2);
                }
            }
            return Result;
        }
        private string SaveImit(DataTable dtWhScan, Boolean blnSameCustomer)
        {
            string Result = "";
            DataTable dtRec;
            DataTable dtImit = GetSQLCommandReturnDT("Select Top 0 from imit1");
            if (dtImit != null)
            {
                dtImit.Rows.Add(dtImit.NewRow());
                if (blnSameCustomer)
                {
                    dtImit.Rows[0]["CustomerCode"] = dtWhScan.Rows[0]["ProductCustomerCode"];
                }
                dtImit.Rows[0]["TransferDateTime"] = DateTime.Today;
                dtImit.Rows[0]["CreateBy"] = dtWhScan.Rows[0]["ConfirmBy"];
                dtImit.Rows[0]["UpdateBy"] = dtWhScan.Rows[0]["ConfirmBy"];
                dtImit.Rows[0]["WorkStation"] = "PDADriver_" + dtWhScan.Rows[0]["ConfirmBy"].ToString();
                dtImit.Rows[0]["GoodsTransferNoteNo"] = CreateGoodsReceiptNo(dtImit, "Imit");
                if (dtImit.Rows[0]["GoodsTransferNoteNo"] == null || dtImit.Rows[0]["GoodsTransferNoteNo"].ToString() == "")
                {
                    dtRec = GetSQLCommandReturnDT("Select NextGoodsTransferNo From Impa1");
                    dtImit.Rows[0]["GoodsTransferNoteNo"] = Modfunction.CheckNull(dtRec.Rows[0][0]);
                    string strNewNextGoodsTransferNo = CheckUpdateFieldLength(Modfunction.CheckNull(dtRec.Rows[0][0])).ToString();
                    GetSQLCommandReturnInt("Update Impa1 set NextGoodsTransferNo = '" + Modfunction.SQLSafe(strNewNextGoodsTransferNo) + "'");
                }
                int intSaveResult = InsertTableRecordByDatatable("Imit1", dtImit);
                if (intSaveResult == -1)
                    return "Confirm unsuccess.";
                dtRec = GetSQLCommandReturnDT("Select Max(TrxNo) from Imit1 Where WorkStation = " + Modfunction.SQLSafeValue(dtImit.Rows[0]["WorkStation"]) + " AND CreateBy = " + Modfunction.SQLSafeValue(dtImit.Rows[0]["CreateBy"]));
                if (dtRec != null && dtRec.Rows.Count > 0)
                {
                    int intTrxNo = Convert.ToInt32(dtRec.Rows[0][0]);
                    DataTable dtImit2 = GetSQLCommandReturnDT("Select Top 0 from Imit2 ");
                    dtImit2.Rows.Clear();
                    for (int intIndex = 0; intIndex < dtWhScan.Rows.Count; intIndex++)
                    {
                        dtRec = GetSQLCommandReturnDT("Select * from Impr1 Where TrxNo = " + Convert.ToInt32(dtWhScan.Rows[intIndex]["ProductTrxNo"]).ToString());
                        if (dtRec != null && dtRec.Rows.Count > 0)
                        {
                            dtImit2.Rows.Add(dtImit2.NewRow());
                            dtImit2.Rows[dtImit2.Rows.Count - 1]["TrxNo"] = intTrxNo;
                            dtImit2.Rows[dtImit2.Rows.Count - 1]["LineItemNo"] = dtImit2.Rows.Count;
                            dtImit2.Rows[dtImit2.Rows.Count - 1]["ProductTrxNo"] = dtRec.Rows[0]["TrxNo"];
                            dtImit2.Rows[dtImit2.Rows.Count - 1]["Weight"] = Convert.ToDecimal(dtRec.Rows[0]["UnitWt"]) * Convert.ToInt32(dtWhScan.Rows[intIndex]["Qty"]);
                            dtImit2.Rows[dtImit2.Rows.Count - 1]["Volume"] = Convert.ToDecimal(dtRec.Rows[0]["UnitVol"]) * Convert.ToInt32(dtWhScan.Rows[intIndex]["Qty"]);
                            if (dtRec.Rows[0]["DimensionFlag"] != null)
                            {
                                if (dtRec.Rows[0]["DimensionFlag"].ToString() == "1")
                                {
                                    dtImit2.Rows[dtImit2.Rows.Count - 1]["PackingQty"] = dtWhScan.Rows[intIndex]["Qty"];
                                    dtImit2.Rows[dtImit2.Rows.Count - 1]["WholeQty"] = Convert.ToInt32(dtWhScan.Rows[intIndex]["Qty"]) * Convert.ToInt32(dtRec.Rows[0]["PackingPackageSize"]);
                                    dtImit2.Rows[dtImit2.Rows.Count - 1]["LooseQty"] = Convert.ToInt32(dtImit2.Rows[dtImit2.Rows.Count - 1]["WholeQty"]) * Convert.ToInt32(dtRec.Rows[0]["WholePackageSize"]);
                                }
                                if (dtRec.Rows[0]["DimensionFlag"].ToString() == "2")
                                {
                                    dtImit2.Rows[dtImit2.Rows.Count - 1]["WholeQty"] = dtWhScan.Rows[intIndex]["Qty"];
                                    dtImit2.Rows[dtImit2.Rows.Count - 1]["LooseQty"] = Convert.ToInt32(dtImit2.Rows[dtImit2.Rows.Count - 1]["WholeQty"]) * Convert.ToInt32(dtRec.Rows[0]["WholePackageSize"]);
                                }
                                if (dtRec.Rows[0]["DimensionFlag"].ToString() == "3")
                                {
                                    dtImit2.Rows[dtImit2.Rows.Count - 1]["LooseQty"] = dtWhScan.Rows[intIndex]["Qty"];
                                }
                            }
                            dtImit2.Rows[dtImit2.Rows.Count - 1]["NewStoreNo"] = dtWhScan.Rows[intIndex]["StoreNo"];
                            dtImit2.Rows[dtImit2.Rows.Count - 1]["NewWarehouseCode"] = dtWhScan.Rows[intIndex]["WarehouseCode"];
                            dtImit2.Rows[dtImit2.Rows.Count - 1]["StoreNo"] = dtWhScan.Rows[intIndex]["OldStoreNo"];
                            dtImit2.Rows[dtImit2.Rows.Count - 1]["WarehouseCode"] = dtWhScan.Rows[intIndex]["OldWarehouseCode"];
                        }
                    }
                    if (dtImit2 != null && dtImit2.Rows.Count > 0)
                    {
                        intSaveResult = InsertTableRecordByDatatable("Imit2", dtImit2);
                        if (intSaveResult == -1)
                            return "Confirm unsuccess.";
                    }
                }
                else
                {
                    Result = "Confirm Unsuccess.";
                }
                return Result;
            }
            return Result;
        }
        private string SaveImgr(DataTable dtWhScan, DataTable dtOmtx)
        {
            DataTable dtRec = null;
            string Result = "";
            int intSaveResult = -1;
            DataTable dtImgr1 = GetSQLCommandReturnDT("Select Top 0 * from Imgr1");
            dtImgr1.Columns.Remove(dtImgr1.Columns["TrxNo"]);
            dtImgr1.Columns.Remove(dtImgr1.Columns["CreateDateTime"]);
            dtImgr1.Columns.Remove(dtImgr1.Columns["UpdateDateTime"]);
            dtImgr1.Rows.Add(dtImgr1.NewRow());
            dtImgr1.Rows[0]["CustomerCode"] = dtWhScan.Rows[0]["CustomerCode"];
            dtImgr1.Rows[0]["WarehouseCode"] = dtWhScan.Rows[0]["WarehouseCode"];
            dtImgr1.Rows[0]["WarehouseName"] = dtWhScan.Rows[0]["WarehouseName"];
            dtImgr1.Rows[0]["CustomerName"] = dtWhScan.Rows[0]["BusinessPartyName"];
            dtImgr1.Rows[0]["CustomerAddress1"] = dtWhScan.Rows[0]["Address1"];
            dtImgr1.Rows[0]["CustomerAddress2"] = dtWhScan.Rows[0]["Address2"];
            dtImgr1.Rows[0]["CustomerAddress3"] = dtWhScan.Rows[0]["Address3"];
            dtImgr1.Rows[0]["CustomerAddress4"] = dtWhScan.Rows[0]["Address4"];
            dtImgr1.Rows[0]["ReceiptDate"] = DateTime.Today;
            dtImgr1.Rows[0]["CreateBy"] = dtWhScan.Rows[0]["ConfirmBy"];
            dtImgr1.Rows[0]["UpdateBy"] = dtWhScan.Rows[0]["ConfirmBy"];
            dtImgr1.Rows[0]["WorkStation"] = "PDADriver_" + dtWhScan.Rows[0]["ConfirmBy"].ToString();
            dtImgr1.Rows[0]["GoodsReceiptNoteNo"] = CreateGoodsReceiptNo(dtImgr1, "Imgr");
            if (dtImgr1.Rows[0]["GoodsReceiptNoteNo"] == null || dtImgr1.Rows[0]["GoodsReceiptNoteNo"].ToString() == "")
            {
                dtRec = GetSQLCommandReturnDT("Select NextGoodsReceiptNo From Impa1");
                dtImgr1.Rows[0]["GoodsReceiptNoteNo"] = Modfunction.CheckNull(dtRec.Rows[0][0]);
                string strNewNextGoodsReceiptNo = CheckUpdateFieldLength(Modfunction.CheckNull(dtRec.Rows[0][0])).ToString();
                GetSQLCommandReturnInt("Update Impa1 set NextGoodsReceiptNo = '" + Modfunction.SQLSafe(strNewNextGoodsReceiptNo) + "'");
            }
            intSaveResult = InsertTableRecordByDatatable("Imgr1", dtImgr1);
            if (intSaveResult == -1)
                return "Confirm unsuccess.";
            dtRec = GetSQLCommandReturnDT("Select Max(TrxNo) from Imgr1 Where WorkStation = " + Modfunction.SQLSafeValue(dtImgr1.Rows[0]["WorkStation"]) + " AND CreateBy = " + Modfunction.SQLSafeValue(dtImgr1.Rows[0]["CreateBy"]) + " AND CustomerCode = " + Modfunction.SQLSafeValue(dtImgr1.Rows[0]["CustomerCode"]));
            if (dtRec != null && dtRec.Rows.Count > 0)
            {
                int intTrxNo = Convert.ToInt32(dtRec.Rows[0][0]);
                DataTable dtImgr2 = GetSQLCommandReturnDT("Select Top 0 from Imgr2 ");
                dtImgr2.Rows.Clear();
                for (int intIndex = 0; intIndex < dtWhScan.Rows.Count; intIndex++)
                {
                    dtRec = GetSQLCommandReturnDT("Select * from Impr1 Where TrxNo = " + Convert.ToInt32(dtWhScan.Rows[intIndex]["ProductTrxNo"]).ToString());
                    if (dtRec != null && dtRec.Rows.Count > 0)
                    {
                        dtImgr2.Rows.Add(dtImgr2.NewRow());
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["TrxNo"] = intTrxNo;
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["LineItemNo"] = dtImgr2.Rows.Count;
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["DimensionFlag"] = dtRec.Rows[0]["DimensionFlag"];
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["UnitVol"] = dtRec.Rows[0]["UnitVol"];
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["UnitWt"] = dtRec.Rows[0]["UnitWt"];
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["UnitVolFlag"] = dtRec.Rows[0]["UnitVolFlag"];
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["ProductTrxNo"] = dtRec.Rows[0]["TrxNo"];
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["ProductDescription"] = dtRec.Rows[0]["ProductName"];
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["Weight"] = Convert.ToDecimal(dtRec.Rows[0]["UnitWt"]) * Convert.ToInt32(dtWhScan.Rows[intIndex]["Qty"]);
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["Volume"] = Convert.ToDecimal(dtRec.Rows[0]["UnitVol"]) * Convert.ToInt32(dtWhScan.Rows[intIndex]["Qty"]);
                        if (dtRec.Rows[0]["DimensionFlag"] != null)
                        {
                            if (dtRec.Rows[0]["DimensionFlag"].ToString() == "1")
                            {
                                dtImgr2.Rows[dtImgr2.Rows.Count - 1]["PackingQty"] = dtWhScan.Rows[intIndex]["Qty"];
                                dtImgr2.Rows[dtImgr2.Rows.Count - 1]["WholeQty"] = Convert.ToInt32(dtWhScan.Rows[intIndex]["Qty"]) * Convert.ToInt32(dtRec.Rows[0]["PackingPackageSize"]);
                                dtImgr2.Rows[dtImgr2.Rows.Count - 1]["LooseQty"] = Convert.ToInt32(dtImgr2.Rows[dtImgr2.Rows.Count - 1]["WholeQty"]) * Convert.ToInt32(dtRec.Rows[0]["WholePackageSize"]);
                            }
                            if (dtRec.Rows[0]["DimensionFlag"].ToString() == "2")
                            {
                                dtImgr2.Rows[dtImgr2.Rows.Count - 1]["WholeQty"] = dtWhScan.Rows[intIndex]["Qty"];
                                dtImgr2.Rows[dtImgr2.Rows.Count - 1]["LooseQty"] = Convert.ToInt32(dtImgr2.Rows[dtImgr2.Rows.Count - 1]["WholeQty"]) * Convert.ToInt32(dtRec.Rows[0]["WholePackageSize"]);
                            }
                            if (dtRec.Rows[0]["DimensionFlag"].ToString() == "3")
                            {
                                dtImgr2.Rows[dtImgr2.Rows.Count - 1]["LooseQty"] = dtWhScan.Rows[intIndex]["Qty"];
                            }
                        }
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["DimensionFlag"] = dtRec.Rows[0]["DimensionFlag"];
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["StoreNo"] = dtWhScan.Rows[intIndex]["StoreNo"];
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["CustomerCode"] = dtWhScan.Rows[intIndex]["CustomerCode"];
                        dtImgr2.Rows[dtImgr2.Rows.Count - 1]["WarehouseCode"] = dtWhScan.Rows[intIndex]["WarehouseCode"];
                        saveImsn1SerialNo(Modfunction.CheckNull(dtImgr1.Rows[0]["GoodsReceiptNoteNo"]), Convert.ToInt32(dtImgr2.Rows[dtImgr2.Rows.Count - 1]["LineItemNo"]), Modfunction.CheckNull(dtWhScan.Rows[intIndex]["SerialNo"]), true);
                    }
                }
                if (dtImgr2 != null && dtImgr2.Rows.Count > 0)
                {
                    intSaveResult = InsertTableRecordByDatatable("Imgr2", dtImgr2);
                    if (intSaveResult == -1)
                        return "Confirm unsuccess.";
                }
            }
            else
            {
                Result = "Confirm Unsuccess.";
            }
            return Result;
        }
        private void saveImsn1SerialNo(string strNoteNo, int intLineItemNo, string strSerialNo, Boolean blnRecript)
        {
            if (strSerialNo == "") return;
            string[] strSerialList = System.Text.RegularExpressions.Regex.Split(strSerialNo, ",");
            if (strSerialList != null)
            {
                for (int i = 0; i < strSerialList.Length; i++)
                {
                    if (blnRecript == true)
                    {
                        GetSQLCommandReturnInt("Insert into imsn1(ReceiptNoteNo,ReceiptLineItemNo,SerialNo) Values(" + Modfunction.SQLSafeValue(strNoteNo) + "," + intLineItemNo.ToString() + "," + Modfunction.SQLSafeValue(strSerialList[i]) + ")");
                    }
                    else
                    {
                        GetSQLCommandReturnInt("Insert into imsn1(IssueNoteNo,IssueLineItemNo,SerialNo) Values(" + Modfunction.SQLSafeValue(strNoteNo) + "," + intLineItemNo.ToString() + "," + Modfunction.SQLSafeValue(strSerialList[i]) + ")");
                    }
                }
            }
        }
        public string VerifyGRN(DataTable dtWhScan, DataTable dtOmtx2)
        {
            string Result = "";
            if (dtWhScan != null && dtWhScan.Rows.Count > 0)
            {
                if (dtOmtx2 != null && dtOmtx2.Rows.Count > 0)
                {
                    //CompateFlag'
                    int intScanQty, intOmtxQty;
                    for (int intScanIndex = 0; intScanIndex < dtWhScan.Rows.Count; intScanIndex++)
                    {
                        if (dtWhScan.Rows[intScanIndex]["CompateFlag"].ToString() == "")
                        {
                            int intScanNewIndex = intScanIndex;
                            intScanQty = 0;
                            string strProductCode = dtWhScan.Rows[intScanIndex]["ProductCode"].ToString();
                            for (intScanNewIndex = intScanIndex; intScanNewIndex < dtWhScan.Rows.Count - 1; intScanNewIndex++)
                            {
                                if (strProductCode == dtWhScan.Rows[intScanNewIndex]["ProductCode"].ToString() && dtWhScan.Rows[intScanNewIndex]["CompateFlag"].ToString() == "")
                                {
                                    intScanQty = intScanQty + (int)dtWhScan.Rows[intScanNewIndex]["Qty"];
                                    dtWhScan.Rows[intScanNewIndex]["CompateFlag"] = "Y";
                                }
                            }
                            intOmtxQty = 0;
                            for (int intOmtxIndex = 0; intOmtxIndex < dtOmtx2.Rows.Count - 1; intOmtxIndex++)
                            {
                                if (strProductCode == dtOmtx2.Rows[intOmtxIndex]["ProductCode"].ToString() && dtOmtx2.Rows[intOmtxIndex]["CompateFlag"].ToString() == "")
                                {
                                    dtOmtx2.Rows[intOmtxIndex]["CompateFlag"] = "Y";
                                    intOmtxQty = (int)dtOmtx2.Rows[intOmtxIndex]["DimQty"];

                                }
                            }
                            if (intScanQty != intOmtxQty)
                            { return "The quantity of goods is wrong."; }
                        }
                    }
                    for (int intOmtxIndex = 0; intOmtxIndex < dtOmtx2.Rows.Count - 1; intOmtxIndex++)
                    {
                        if (dtOmtx2.Rows[intOmtxIndex]["CompateFlag"].ToString() == "")
                        {
                            return "Some goods for ASN not scan.";
                        }
                    }
                }
            }
            return Result;
        }
        private int InsertTableRecordByDatatable(string strtableName, DataTable dt)
        {
            int result = -1;
            for (int intI = 0; intI <= dt.Rows.Count - 1; intI++)
            {
                string strFieldList = "";
                string strValueList = "";
                for (int intCol = 0; intCol <= dt.Columns.Count - 1; intCol++)
                {
                    strFieldList = strFieldList + (string.IsNullOrEmpty(strFieldList.Trim()) ? "" : ",") + dt.Columns[intCol].ColumnName;
                    if (GetDataType(dt.Columns[intCol].DataType.Name) == 2 && dt.Rows[intI][intCol] != null)
                    {
                        if (Convert.ToInt32(((DateTime)dt.Rows[intI][intCol]).ToString("HHmm")) > 0)
                        {
                            strValueList = strValueList + (string.IsNullOrEmpty(strValueList.Trim()) ? "" : ",") + "'" + ((DateTime)dt.Rows[intI][intCol]).ToString("yyyy-MM-dd HH:mm") + "'";
                        }
                        else
                        {
                            strValueList = strValueList + (string.IsNullOrEmpty(strValueList.Trim()) ? "" : ",") + "'" + ((DateTime)dt.Rows[intI][intCol]).ToString("yyyy-MM-dd") + "'";
                        }
                    }
                    else
                    {
                        strValueList = strValueList + (string.IsNullOrEmpty(strValueList.Trim()) ? "" : ",") + Modfunction.SQLSafeValue(dt.Rows[intI][intCol]);
                    }
                }
                result = GetSQLCommandReturnInt("Insert into  " + strtableName + " (" + strFieldList + ") Values(" + strValueList + ")");
            }
            return result;
        }
        public int GetDataType(string strDataType)
        {
            switch (strDataType.ToLower())
            {
                case "datetime":
                    return 2;
                case "decimal":
                    return 1;
                case "int":
                    return 1;
                case "smallint":
                    return 1;
                case "tinyint":
                    return 1;
                case "System.Int32":
                    return 1;
                case "varchar":
                    return 0;
                case "nvarchar":
                    return 0;
                case "char":
                    return 0;
            }
            return 0;
        }
        private string CreateGoodsReceiptNo(DataTable dt, string strNumberType)
        {
            string functionReturnValue = null;
            int intI = 0;
            string m_strJobSeqNo = "";
            string m_strPullFrom = "";
            string m_strUpdateNextField = "";
            int intMonth = 0;
            string strGoodsReceiptNo = null;
            int intYear = 0;
            string[] strArr = null;
            DataTable dtRec = null;
            DataTable dtRec2 = null;
            string m_SQlCommandText = null;
            functionReturnValue = "";
            m_SQlCommandText = "Select * From Sanm1 Where NumberType = '" + strNumberType + "' Order By JobType Desc";
            dtRec = GetSQLCommandReturnDT(m_SQlCommandText);
            if ((dtRec != null))
            {
                if (dtRec.Rows.Count > 0)
                {
                    if (dtRec.Rows[0]["Cycle"] == null)
                    {
                        return functionReturnValue;
                    }
                    //Cycle = Continuous                                                                                                                                                                                                                                                                                                                                                                                       												
                    if (dtRec.Rows[0]["Cycle"].ToString() == "C")
                    {
                        m_strPullFrom = "NextNo";
                        m_strJobSeqNo = Modfunction.CheckNull(dtRec.Rows[0]["NextNo"]);
                        //Cycle = Month                                                                                                                                                                                                                                                                                                                                                                                       												
                    }
                    else if (Modfunction.CheckNull(dtRec.Rows[0]["Cycle"]) == "M")
                    {
                        intMonth = DateTime.Now.Month;
                        intYear = DateTime.Now.Year;
                        m_SQlCommandText = "Select * From Sanm2 Where TrxNo = " + Modfunction.CheckNull(dtRec.Rows[0]["TrxNo"]) + " And Year = '" + intYear + "'";
                        dtRec2 = GetSQLCommandReturnDT(m_SQlCommandText);
                        if ((dtRec2 != null))
                        {
                            if (dtRec2.Rows.Count > 0)
                            {
                                switch (intMonth)
                                {
                                    case 1:
                                        m_strPullFrom = "Mth01NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth01NextNo"].ToString();
                                        break;
                                    case 2:
                                        m_strPullFrom = "Mth02NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth02NextNo"].ToString();
                                        break;
                                    case 3:
                                        m_strPullFrom = "Mth03NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth03NextNo"].ToString();
                                        break;
                                    case 4:
                                        m_strPullFrom = "Mth04NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth04NextNo"].ToString();
                                        break;
                                    case 5:
                                        m_strPullFrom = "Mth05NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth05NextNo"].ToString();
                                        break;
                                    case 6:
                                        m_strPullFrom = "Mth06NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth06NextNo"].ToString();
                                        break;
                                    case 7:
                                        m_strPullFrom = "Mth07NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth07NextNo"].ToString();
                                        break;
                                    case 8:
                                        m_strPullFrom = "Mth08NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth08NextNo"].ToString();
                                        break;
                                    case 9:
                                        m_strPullFrom = "Mth09NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth09NextNo"].ToString();
                                        break;
                                    case 10:
                                        m_strPullFrom = "Mth10NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth10NextNo"].ToString();
                                        break;
                                    case 11:
                                        m_strPullFrom = "Mth11NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth11NextNo"].ToString();
                                        break;
                                    case 12:
                                        m_strPullFrom = "Mth12NextNo";
                                        m_strJobSeqNo = dtRec2.Rows[0]["Mth12NextNo"].ToString();
                                        break;
                                }
                            }
                            else
                            {
                                return functionReturnValue;
                            }
                        }
                        //No flag = Year                                                                                                                                                                                                                                                                                                                                                                                       												
                    }
                    else if (Modfunction.CheckNull(dtRec.Rows[0]["Cycle"]) == "Y")
                    {
                        intYear = DateTime.Now.Year;
                        m_SQlCommandText = "Select YearNextNo From Sanm2 Where TrxNo = " + Modfunction.CheckNull(dtRec.Rows[0]["TrxNo"]) + " And Year = '" + intYear + "'";
                        dtRec2 = GetSQLCommandReturnDT(m_SQlCommandText);
                        if ((dtRec2 != null))
                        {
                            if (dtRec2.Rows.Count > 0)
                            {
                                m_strPullFrom = "YearNextNo";
                                m_strJobSeqNo = Modfunction.CheckNull(dtRec2.Rows[0]["YearNextNo"]);
                            }
                            else
                            {
                                return functionReturnValue;
                            }
                        }
                    }
                    strGoodsReceiptNo = "";
                    if (Modfunction.CheckNull(dtRec.Rows[0]["Prefix"]) != "")
                    {
                        strArr = System.Text.RegularExpressions.Regex.Split(dtRec.Rows[0]["Prefix"].ToString(), ",");
                        for (intI = 0; intI <= strArr.Length; intI++)
                        {
                            strGoodsReceiptNo = strGoodsReceiptNo + ReturnPrefixSuffix(strArr[intI], dt);
                        }
                    }
                    strGoodsReceiptNo = strGoodsReceiptNo + m_strJobSeqNo;
                    // & "00"                                                                                                                                                                                                                                                                                                                                                                                       												
                    if (Modfunction.CheckNull(dtRec.Rows[0]["Suffix"]).Trim().Length > 0)
                    {
                        strArr = null;
                        strArr = System.Text.RegularExpressions.Regex.Split(dtRec.Rows[0]["Suffix"].ToString(), ",");
                        for (intI = 0; intI <= strArr.Length; intI++)
                        {
                            strGoodsReceiptNo = strGoodsReceiptNo + ReturnPrefixSuffix(strArr[intI], dt);
                        }
                    }
                    functionReturnValue = strGoodsReceiptNo;
                    //120629DC NET3983
                    if (functionReturnValue.Length > 20)
                    {
                        functionReturnValue = functionReturnValue.Substring(functionReturnValue.Length - 20, 20);
                    }
                    m_strUpdateNextField = CheckUpdateFieldLength(m_strJobSeqNo);
                    //Cycle = Continuous                                                                                                                                                                                                                                                                                                                                                                                       												
                    if (Modfunction.CheckNull(dtRec.Rows[0]["Cycle"]) == "C")
                    {
                        m_SQlCommandText = "Update Sanm1 Set " + m_strPullFrom + " = '" + Modfunction.SQLSafe(m_strUpdateNextField) + "' Where TrxNo = " + Modfunction.CheckNull(dtRec.Rows[0]["TrxNo"]);
                        //Add 1 For Next Job No                                                                                                                                                                                                                                                                                                                                                                                       												
                    }
                    else
                    {
                        m_SQlCommandText = "Update Sanm2 Set " + m_strPullFrom + " = '" + Modfunction.SQLSafe(m_strUpdateNextField) + "' Where TrxNo = " + Modfunction.CheckNull(dtRec.Rows[0]["TrxNo"]) + " And Year = '" + intYear + "'";
                    }
                    GetSQLCommandReturnInt(m_SQlCommandText);
                }
            }
            return functionReturnValue;
        }
        private string ReturnPrefixSuffix(string strPrefixSuffix, DataTable dtImgr)
        {
            string functionReturnValue = null;
            functionReturnValue = "";
            // ERROR: Not supported in C#: OnErrorStatement

            int intMth = 0;
            switch (strPrefixSuffix)
            {
                case "MM":
                    functionReturnValue = DateTime.Now.Month.ToString();
                    break;
                case "M":
                    intMth = DateTime.Now.Month;
                    if (intMth == 10)
                    {
                        functionReturnValue = "O";
                    }
                    else if (intMth == 11)
                    {
                        functionReturnValue = "N";
                    }
                    else if (intMth == 12)
                    {
                        functionReturnValue = "D";
                    }
                    else
                    {
                        functionReturnValue = intMth.ToString();
                    }
                    break;
                case "YY":
                    functionReturnValue = DateTime.Now.ToString("yy");
                    break;
                case "Y":
                    functionReturnValue = DateTime.Now.ToString("yy").Substring(1, 1);
                    break;
                case "NN":
                    functionReturnValue = "00";
                    break;
                case "N":
                    functionReturnValue = "0";
                    break;
                case "CUST":
                    //120629DC NET3983
                    functionReturnValue = Modfunction.CheckNull(dtImgr.Rows[0]["CustomerCode"]);
                    break;
                default:
                    if (strPrefixSuffix.Substring(0, 1) == "F")
                    {
                        functionReturnValue = strPrefixSuffix.Substring(1);
                    }
                    break;
            }
            return functionReturnValue;
        }
        private string CheckUpdateFieldLength(string strField)
        {
            string functionReturnValue = null;
            int intI = 0;
            int intStartLen = 0;
            functionReturnValue = Convert.ToString(Convert.ToInt32(strField) + 1);
            intStartLen = functionReturnValue.Length;
            if (intStartLen != strField.Length)
            {
                for (intI = intStartLen; intI <= strField.Length - 1; intI++)
                {
                    functionReturnValue = "0" + functionReturnValue;
                }
            }
            return functionReturnValue;
        }
        private string GetBarCode(string strBarCode)
        {
            DataTable dtRec = GetSQLCommandReturnDT("Select BarCodeField from Impa1");
            if (dtRec != null && dtRec.Rows.Count > 0)
            {
                if (dtRec.Rows[0][0] != null && dtRec.Rows[0][0].ToString() != "")
                { return dtRec.Rows[0][0].ToString(); }
            }
            return strBarCode;
        }
        public string VerifyGRNNew(string strAsn, string strConfirmBy)
        {
            string Result = "";
            DataTable dtOmtx2, dtWhScan;
            string strBarCodeColumn = GetBarCode("UserDefine01");
            dtWhScan = GetSQLCommandReturnDT("Select *,(Select ProductCode from Impr1 Where impr1. " + strBarCodeColumn + " = Whscan.BarCode) AS ProductCode,'' AS CompateFlag from WhScan Where AsnNo = " + Modfunction.SQLSafeValue(strAsn) + " AND ComfirmBy = " + Modfunction.SQLSafeValue(strConfirmBy));
            if (dtWhScan != null && dtWhScan.Rows.Count > 0)
            {
                dtOmtx2 = GetSQLCommandReturnDT("select SUM(case DimensionFlag when '1' then BalancePackingQty when '2' then BalanceWholeQty else BalanceLooseQty end) DimQty,ProductCode,'' AS ComputeFlag from Omtx2 Where TrxNo = (Select Top 1 TrxNo from Omtx1 Where AsnNo = " + Modfunction.SQLSafeValue(strAsn) + ") Group BY ProductCode");
                if (dtOmtx2 != null && dtOmtx2.Rows.Count > 0)
                {
                    Result = VerifyGRN(dtWhScan, dtOmtx2);
                }
            }
            return Result;
        }
        private int GetSQLCommandReturnInt(string strSql)
        {
            return SqlHelper.ExecuteNonQuery(ConnectString.strValue, CommandType.Text, strSql);
        }
        private DataTable GetSQLCommandReturnDT(string strSql)
        {
            return SqlHelper.ExecuteDataTable(ConnectString.strValue, CommandType.Text, strSql);
        }
    }
}
