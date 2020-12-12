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
    [Route("[controller]")]
    public class UrlServiceController : Controller
    {
        private IAction<UrlServiceModel> _action;

        public UrlServiceController(IAction<UrlServiceModel> action)
        {
            _action = action;
        }

        [HttpGet]
        public IActionResult Status()
        {
            return Ok();
        }

        [HttpPost]
        public string ConvertToDeeplink(string input)
        {
            return _action.Execute(input);
        }
    }
}
