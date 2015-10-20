using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsWS.ServiceModel
{
    public interface ISecretKey
    {
        string strValue { get; set; }
    }
    public class SecretKeyFactory : ISecretKey
    {
        public string strValue { get; set; }
        public SecretKeyFactory(string secretKey)
        {
            strValue = secretKey;
        }
    }
    public interface IConnectString
    {
        string strValue { get; set; }
    }
    public class ConnectStringFactory : IConnectString
    {
        public string strValue { get; set; }
        public ConnectStringFactory(string connectionString)
        {
            strValue = connectionString;
        }
    }
}
