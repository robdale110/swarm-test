using App.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace App.Web.Controllers
{
    [Route("api/[controller]")]
    public class ConfigController : Controller
    {
        private IConfiguration _configuration;
 
        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ConfigModel Get()
        {
            return new ConfigModel
            {
                ApiUrl = _configuration["PasswordApi:Url"]
            };
        }
    }
}