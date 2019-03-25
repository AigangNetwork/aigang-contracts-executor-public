using System.Net;
using Aigang.Contracts.Executor.API.Utils;
using Aigang.Contracts.Executor.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Aigang.Contracts.Executor.API.Controllers
{
    public class ControllerBase : Controller
    {
        protected ActionResult MakeActionResult(BaseResponse response)
        {
            if (response.Error != null)
            {
                var errorResponse = new ObjectResult(response.Error)
                {
                    StatusCode = (int) HttpStatusResolver.ResolveStatusCodeFromErrors(response.Error)
                };

                return errorResponse;
            }

            if (response.SuccessStatusCode == HttpStatusCode.Accepted)
            {
                return Accepted(response);
            }
    
            return Ok(response);
        }
    }
}