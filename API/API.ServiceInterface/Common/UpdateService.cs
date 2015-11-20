using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.ServiceModel;
using WebApi.ServiceModel.Common;

namespace WebApi.ServiceInterface.Common
{
    public class UpdateService
    {
        public void UpdateRcbp1(Auth auth, Update_Rcbp1 request, Update_Rcbp1_Logic update_Rcbp1_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {
                if (update_Rcbp1_Logic.UpdateResult(request) > 0)
                {
                    ecr.meta.code = 200;
                    ecr.meta.message = "OK";
                }
                else
                {
                    ecr.meta.code = 612;
                    ecr.meta.message = "The specified resource does not exist";
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
