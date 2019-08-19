using App.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Web.Controllers
{
    [Route("api/[controller]")]
    public class PasswordController : Controller
    {
        private IConfiguration _configuration;

        public PasswordController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<PasswordResponse> Get()
        {
            using (var client = new HttpClient())
            {
                var passwordJson = await client.GetStringAsync($"{_configuration["PasswordApi:Url"]}/password");
                return JsonConvert.DeserializeObject<PasswordResponse>(passwordJson);
            }
        }
    }
}