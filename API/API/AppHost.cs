using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using WmsWS.ServiceInterface;
using System.Reflection;

namespace WmsWS
{
    public class AppHost : AppHostBase
    {
        private static string ver = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        private static string strSecretKey;
        public AppHost()
            : base("WmsWS WebService v" + ver, typeof(WmsServices).Assembly)
        {
        }
        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                DebugMode = false,
                EnableFeatures = Feature.All.Remove(Feature.Xml | Feature.Jsv | Feature.Csv | Feature.Soap11 | Feature.Soap12 | Feature.Soap),
                HandlerFactoryPath = "api"
            });
            CorsFeature cf = new CorsFeature(allowedOrigins: "*", allowedMethods: "GET, POST, PUT, DELETE, OPTIONS", allowedHeaders: "Content-Type, Signature", allowCredentials: false);
            this.Plugins.Add(cf);
            
            string strConnectionString = GetConnectionString();
            var dbConnectionFactory = new OrmLiteConnectionFactory(strConnectionString, SqlServerDialect.Provider, true)
            {                
                ConnectionFilter =
                    x =>
                    new ProfiledDbConnection(x, Profiler.Current)
            };
            container.Register<IDbConnectionFactory>(dbConnectionFactory);

            var connectString = new WmsWS.ServiceModel.ConnectStringFactory(strConnectionString);
            container.Register<WmsWS.ServiceModel.IConnectString>(connectString);

            var secretKey = new WmsWS.ServiceModel.SecretKeyFactory(strSecretKey);
            container.Register<WmsWS.ServiceModel.ISecretKey>(secretKey);

            container.RegisterAutoWired<WmsWS.ServiceModel.Auth>();
            container.RegisterAutoWired<WmsWS.ServiceModel.Wms.List_Login_Logic>();
            container.RegisterAutoWired<WmsWS.ServiceModel.Wms.List_Imgr1_Logic>();
            container.RegisterAutoWired<WmsWS.ServiceModel.Wms.List_Impr1_Logic>();
            container.RegisterAutoWired<WmsWS.ServiceModel.Wms.List_Imgr2_Logic>();
            container.RegisterAutoWired<WmsWS.ServiceModel.Wms.Confirm_Imgr1_Logic>();
            container.RegisterAutoWired<WmsWS.ServiceModel.Wms.List_Imgi1_Logic>();
            container.RegisterAutoWired<WmsWS.ServiceModel.Wms.List_Imgi2_Logic>();
            container.RegisterAutoWired<WmsWS.ServiceModel.Wms.List_Imsn1_Logic>();
            //container.RegisterAutoWired<WmsWS.ServiceModel.Wms.Update_Done_Logic>();
            //container.RegisterAutoWired<WmsWS.ServiceModel.Wms.List_JobNo_Logic>();
            container.RegisterAutoWired<WmsWS.ServiceModel.Wms.List_Rcbp1_Logic>();
        }

        #region DES
        //private string DESKey = "F322186F";
        //private string DESIV = "F322186F";
        private static string DESEncrypt(string strPlain, string strDESKey, string strDESIV)
        {
            string DESEncrypt = "";
            try
            {
                byte[] bytesDESKey = ASCIIEncoding.ASCII.GetBytes(strDESKey);
                byte[] bytesDESIV = ASCIIEncoding.ASCII.GetBytes(strDESIV);
                byte[] inputByteArray = Encoding.Default.GetBytes(strPlain);
                DESCryptoServiceProvider desEncrypt = new DESCryptoServiceProvider();
                desEncrypt.Key = bytesDESKey;
                desEncrypt.IV = bytesDESIV;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, desEncrypt.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(inputByteArray, 0, inputByteArray.Length);
                        csEncrypt.FlushFinalBlock();
                        StringBuilder str = new StringBuilder();
                        foreach (byte b in msEncrypt.ToArray())
                        {
                            str.AppendFormat("{0:X2}", b);
                        }
                        DESEncrypt = str.ToString();
                    }
                }
            }
            catch
            { }
            return DESEncrypt;
        }
        private static string DesDecrypt(string strValue)
        {
            string DesDecrypt = "";
            if (strValue.IsNullOrEmpty())
            {
                return DesDecrypt;
            }
            try
            {
                byte[] DESKey = new byte[] { 70, 51, 50, 50, 49, 56, 54, 70 };
                byte[] DESIV = new byte[] { 70, 51, 50, 50, 49, 56, 54, 70 };
                DES desprovider = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[strValue.Length / 2];
                int intI;
                for (intI = 0; intI < strValue.Length / 2; intI++)
                {
                    inputByteArray[intI] = (byte)(Convert.ToInt32(strValue.Substring(intI * 2, 2), 16));
                }
                desprovider.Key = DESKey;
                desprovider.IV = DESIV;
                using (MemoryStream ms = new MemoryStream())
                {
                    CryptoStream cs = new CryptoStream(ms, desprovider.CreateDecryptor(), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    DesDecrypt = Encoding.Default.GetString(ms.ToArray());
                }
            }
            catch
            { throw; }
            return DesDecrypt;
        }
        #endregion
        private static string GetConnectionString()
        {
            string IniConnection = "";
            string strAppSetting = "";
            string[] strDataBase = new string[3];
            if (strAppSetting.IsNullOrEmpty())
            {
                strAppSetting = System.Configuration.ConfigurationManager.AppSettings["DataBase"];
                strSecretKey = System.Configuration.ConfigurationManager.AppSettings["SecretKey"];
                strDataBase = strAppSetting.Split(',');
                int intCnt;
                for (intCnt = 0; intCnt <= strDataBase.Length - 1; intCnt++)
                {
                    //if (strDataBase[intCnt].ToLower() == strCatalog.ToLower())
                    //{
                    strAppSetting = System.Configuration.ConfigurationManager.AppSettings[strDataBase[intCnt]];
                    string[] strDatabaseInfo;
                    strDatabaseInfo = strAppSetting.Split(',');
                    if (strDatabaseInfo.Length == 6)
                    {
                        IniConnection = System.Configuration.ConfigurationManager.AppSettings[strDatabaseInfo[5]];
                        string strConnection = "";
                        strConnection = IniConnection.Replace("#DataSource", strDatabaseInfo[0]);
                        strConnection = strConnection.Replace("#Catalog", strDatabaseInfo[1]);
                        strConnection = strConnection.Replace("#UserName", strDatabaseInfo[2]);
                        strConnection = strConnection.Replace("#Password", DesDecrypt(strDatabaseInfo[3]));
                        return strConnection;
                    }
                    //}
                }
            }
            return "";
        }
    }
}