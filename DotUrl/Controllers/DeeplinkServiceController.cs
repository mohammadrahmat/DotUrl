using DotUrl.Interfaces;
using DotUrl.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotUrl.Controllers
{
    [ApiController]
    [Route("controller")]
    public class DeeplinkServiceController : Controller
    {
        private IAction<DeeplinkServiceModel> _action;

        public DeeplinkServiceController(IAction<DeeplinkServiceModel> action)
        {
            _action = action;
        }

        [HttpGet]
        public IActionResult Status()
        {
            return Ok();
        }

        [HttpPost]
        public string ConvertToUrl(string input)
        {
            return _action.Execute(input);
        }
    }
}
