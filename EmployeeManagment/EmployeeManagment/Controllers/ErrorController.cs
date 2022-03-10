using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagment.Controllers
{
	public class ErrorController : Controller
	{

		private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
			this._logger = logger;
        }

		[Route("Error/{statusCode}")]
		public IActionResult HttpStatusCodeHandler(int statusCode)
		{
			var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
			switch(statusCode)
			{
				case 404 :
					ViewBag.ErrorMessage = "Sorry, the page could not be found";
					_logger.LogWarning($"404 Error occured. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
						break;
			}
			return View("NotFound");
		}

		[Route("Error")]
		[AllowAnonymous]
		public IActionResult Error()
        {
			var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

			_logger.LogError($"The path {exceptionDetails.Path} throw an exception {exceptionDetails.Error}");
			
			ViewBag.ExceptionPath = exceptionDetails.Path;
			ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
			ViewBag.StackTrace = exceptionDetails.Error.StackTrace;
			return View("Error");
        }

		public IActionResult Index()
		{
			return View();
		}
	}
}
