using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {            
            return View("Error", statusCode);

        }
        [Route("Error")]
        public IActionResult Index()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionDetails != null)
            {
                //_logger.LogError("Unhandle exception.");
                //_logger.LogError(exceptionDetails.Error.StackTrace);
            }
            return View();
        }
    }
}
