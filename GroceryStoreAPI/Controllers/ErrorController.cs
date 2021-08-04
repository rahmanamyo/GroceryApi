using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStoreAPI.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet, Route("/error")]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var statusCode = exception.Error.GetType().Name switch
            {
                "ArgumentException" => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.ServiceUnavailable
            };
            
            return Problem(detail: exception.Error.Message, statusCode: (int) statusCode);
        }
    }
}