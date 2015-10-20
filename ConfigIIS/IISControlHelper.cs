using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Web.Administration;

namespace ConfigIIS
{
    /// <summary>
    /// IIS 操作方法集合 (IIS7 or higher)
    /// </summary>
    public class IISControlHelper
    {
        public static bool ExistApplication(string applicationName)
        {
            ServerManager iisManager = new ServerManager();
            foreach (Application a in iisManager.Sites[0].Applications)
            {
                if (a.Path.Equals(applicationName))
                {
                    return true;
                }
            }
            return false;
        }

        public static void CreateApplication(string applicationPath, string folderPath, string applicationPoolName)
        {
            ServerManager iisManager = new ServerManager();
            iisManager.Sites[0].Applications.Add(applicationPath, folderPath);
            iisManager.Sites[0].Applications[applicationPath].ApplicationPoolName = applicationPoolName;
            iisManager.CommitChanges();
        }

        public static void DeleteApplication(string applicationPath)
        {
            ServerManager iisManager = new ServerManager();
            iisManager.Sites[0].Applications.Remove(iisManager.Sites[0].Applications[applicationPath]);
            iisManager.CommitChanges();
        }

        public static bool ExistApplicationPool(string appPoolName)
        {
            ServerManager iisManager = new ServerManager();
            foreach (ApplicationPool ap in iisManager.ApplicationPools)
            {
                if (ap.Name.Equals(appPoolName))
                {
                    return true;
                }
            }
            return false;
        }

        public static void CreateApplicationPool(string appPoolName)
        {
            ServerManager iisManager = new ServerManager();
            ApplicationPool appPool = iisManager.ApplicationPools.Add(appPoolName);
            appPool.AutoStart = true;
            appPool.ManagedPipelineMode = ManagedPipelineMode.Integrated;
            appPool.ManagedRuntimeVersion = "v4.0";
            appPool.ProcessModel.IdentityType = ProcessModelIdentityType.ApplicationPoolIdentity;
            iisManager.CommitChanges();            
        }

        public static void DeleteApplicationPool(string poolName)
        {
            ServerManager iisManager = new ServerManager();
            ApplicationPool appPool = iisManager.ApplicationPools[poolName];
            iisManager.ApplicationPools.Remove(appPool);
            iisManager.CommitChanges();
        }
    }
}
