using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmsWS.ServiceModel;
using WmsWS.ServiceModel.Wms;

namespace WmsWS.ServiceInterface.Wms
{
    public class LoginService
    {
        public void initial(Auth auth, List_Login request, List_Login_Logic eventloginLogic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {
                if (eventloginLogic.LoginCheck(request) > 0)
                {
                    ecr.meta.code = 200;
                    ecr.meta.message = "OK";
                    ecr.data.results = request.UserId;
                }
                else
                {
                    ecr.meta.code = 612;
                    ecr.meta.message = "Invalid User";
                }
            }
            else
            {
                ecr.meta.code = 401;
                ecr.meta.message = "Unauthorized";
            }
        }
    }
}
