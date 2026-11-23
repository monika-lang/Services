using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Services.RequestLogger;

namespace p48_lessons_asp.Controllers
{
    public class RequestLoggerController : Controller
    {
        public IRequestLogger _requestLogger;

        public RequestLoggerController(IRequestLogger requestLogger)
        {
            _requestLogger = requestLogger;
        }

        public IActionResult Index()
        {
            _requestLogger.LogRequest(HttpContext.Request.Method, HttpContext.Request.GetDisplayUrl(), StatusCodes.Status200OK);
            return View(_requestLogger.Requests);
        }

    }
}
