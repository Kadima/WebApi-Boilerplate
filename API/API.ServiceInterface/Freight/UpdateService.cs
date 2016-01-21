using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.ServiceModel;
using WebApi.ServiceModel.Freight;

namespace WebApi.ServiceInterface.Freight
{
    public class UpdateService
    {
        public void UpdateRcbp3(Auth auth, Update_Rcbp3 request, Update_Rcbp3_Logic update_Rcbp3_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {
                if (update_Rcbp3_Logic.UpdateResult(request) > 0)
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
        public void UpdatePlcp1(Auth auth, Update_Plcp1 request, Update_Plcp1_Logic update_Plcp1_Logic, CommonResponse ecr, string[] token, string uri)
        {
            if (auth.AuthResult(token, uri))
            {
                if (update_Plcp1_Logic.UpdateResult(request) > 0)
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
								public void UpdateSaus1(Auth auth, Update_Saus1 request, Update_Saus1_Logic update_Saus1_Logic, CommonResponse ecr, string[] token, string uri)
								{
												if (auth.AuthResult(token, uri))
												{
																if (update_Saus1_Logic.UpdateMemoResult(request) > 0)
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
