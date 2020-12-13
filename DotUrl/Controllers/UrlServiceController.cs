using DotUrl.Interfaces;
using DotUrl.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotUrl.Controllers
{
    [ApiController]
    public class UrlServiceController : Controller
    {
        private readonly IAction<UrlServiceModel> _action;

        public UrlServiceController(IAction<UrlServiceModel> action)
        {
            _action = action;
        }

        [HttpGet]
        [Route("[controller]/status")]
        public IActionResult Status()
        {
            return Ok();
        }

        [HttpPost]
        [Route("[controller]/convert")]
        public string ConvertToDeeplink(string input)
        {
            return _action.Execute(input);
        }
    }
}
