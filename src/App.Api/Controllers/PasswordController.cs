using App.Api.Documents;
using App.Api.Indexer;
using App.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace App.Api.Controllers
{
    [Route("[controller]")]
    public class PasswordController : Controller
    {
        private static Random _Rand = new Random();
        private IIndex _index;

        public PasswordController(IIndex index)
        {
            _index = index;
        }

        [HttpGet]
        public PasswordResponse Get()
        {
            //v1
            /*
            return new PasswordResponse 
            {
                Password = Guid.NewGuid().ToString().Substring(0,6),
                ApiVersion = "1.0",
                ApiServer = Environment.MachineName
            };
            */

            //v3 & 4
            var response = new PasswordResponse
            {
                Password = GenerateRandomPassword(),
                ApiVersion = "4.0",
                ApiServer = Environment.MachineName
            };
            
            _index.Add(new PasswordDetails(response));

            return response;
        }

        private static string GenerateRandomPassword()
        {

            string[] randomChars = new[] {
        "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
        "abcdefghijkmnopqrstuvwxyz",    // lowercase
        "0123456789",                   // digits
        "!@$?_-"                        // non-alphanumeric
    };
            
            List<char> chars = new List<char>();

            chars.Insert(_Rand.Next(0, chars.Count),
                randomChars[0][_Rand.Next(0, randomChars[0].Length)]);

            chars.Insert(_Rand.Next(0, chars.Count),
                randomChars[1][_Rand.Next(0, randomChars[1].Length)]);

            chars.Insert(_Rand.Next(0, chars.Count),
                randomChars[2][_Rand.Next(0, randomChars[2].Length)]);

            var passwordLength = _Rand.Next(8, 14);
            for (int i = chars.Count; i < passwordLength; i++)
            {
                string rcs = randomChars[_Rand.Next(0, randomChars.Length)];
                chars.Insert(_Rand.Next(0, chars.Count),
                    rcs[_Rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}