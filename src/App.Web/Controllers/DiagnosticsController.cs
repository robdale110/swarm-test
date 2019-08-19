using App.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace App.Web.Controllers
{
    [Route("api/[controller]")]
    public class DiagnosticsController : Controller
    {
        [HttpGet]
        public DiagnosticsModel Get()
        {
            return new DiagnosticsModel
            {
                Version = "4.0",
                Server = Environment.MachineName
            };
        }
    }
}