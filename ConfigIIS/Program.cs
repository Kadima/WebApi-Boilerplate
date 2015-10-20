using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigIIS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string folderPath = "C:\\inetpub\\wwwroot\\TmsWS";
                string applicationPath = "/TmsWS";
                string applicationPoolName = "TmsWebService";
                if (!FolderSecurityHelper.ExistFolderRights(folderPath))
                {
                    FolderSecurityHelper.SetFolderRights(folderPath);
                }
                if (!IISControlHelper.ExistApplicationPool(applicationPoolName))
                {
                    IISControlHelper.CreateApplicationPool(applicationPoolName);
                }
                if (!IISControlHelper.ExistApplication(applicationPath))
                {
                    IISControlHelper.CreateApplication(applicationPath, folderPath, applicationPoolName);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
