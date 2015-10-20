using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.Data;
using System.Data;

namespace API.ServiceModel.Wms
{
    [Route("/api/table/{TableName}/{FieldName}", "Get")]
    [Route("/api/table/{TableName}/{FieldName}/{Filters}", "Get")]
    public class TableField : IReturn<TableFieldResponse>
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public string Filters { get; set; }
    }
    public class TableFieldResponse
    {
        public string strResult { get; set; }
    }
    public class TableFieldLogic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public IConnectString ConnectString { get; set; }
        public TableFieldResponse[] ShowResponse(TableField request)
        {
            TableFieldResponse[] tfr;
            string strSQLCommand = "SELECT " + request.FieldName + " FROM " + request.TableName + "";
            if (!string.IsNullOrEmpty(request.Filters))
            {
                strSQLCommand = strSQLCommand + " Where " + request.Filters;
            }
            DataTable dtRec = GetSQLCommandReturnDT(strSQLCommand);
            if (dtRec != null && dtRec.Rows.Count > 0)
            {
                tfr = new TableFieldResponse[dtRec.Rows.Count];
                for (int intI = 0; intI <= dtRec.Rows.Count - 1; intI++)
                {
                    tfr[intI] = new TableFieldResponse();
                    tfr[intI].strResult = dtRec.Rows[intI][0].ToString();
                }
            }
            else
            {
                tfr = new TableFieldResponse[0];
                tfr[0] = new TableFieldResponse();
            }
            return tfr;
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
