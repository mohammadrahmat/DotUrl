using DotUrl.Interfaces;
using DotUrl.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotUrl.Controllers
{
    [ApiController]
    public class DeeplinkServiceController : Controller
    {
        private readonly IAction<DeeplinkServiceModel> _action;

        public DeeplinkServiceController(IAction<DeeplinkServiceModel> action)
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
        public string ConvertToUrl(string input)
        {
            return _action.Execute(input);
        }
    }
}
