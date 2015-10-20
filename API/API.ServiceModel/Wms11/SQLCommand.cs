using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using ServiceStack;
using ServiceStack.Data;
using System.Collections;

namespace API.ServiceModel.Wms
{
    [Route("/api/command/{SQLCommandText}", "Get")]
    public class SQLCommand : IReturn<SQLCommandResponse>
    {
         public string SQLCommandText { get; set; }
    }
    public class SQLCommandResponse
    {
        public object objResult { get; set; }
    }
    public class SQLCommandLogic
    {
        public IDbConnectionFactory DbConnectionFactory { get; set; }
        public IConnectString ConnectString { get; set; }
        public ArrayList ShowResponse(SQLCommand request)
        {
            ArrayList ht = new ArrayList();
            DataTable dtRec = GetSQLCommandReturnDT(request.SQLCommandText.Replace("!=", "<>"));
            if (dtRec != null && dtRec.Rows.Count > 0)
            {
                for (int intI = 0; intI <= dtRec.Rows.Count - 1; intI++)
                {
                    Hashtable htRow = new Hashtable();
                    for (int intJ = 0; intJ <= dtRec.Columns.Count - 1; intJ++)
                    {
                        htRow.Add(dtRec.Columns[intJ].ColumnName, dtRec.Rows[intI][intJ].ToString());
                    }
                    ht.Add(htRow);
                }
            }
            return ht;
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
