using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        public string Phone()
        {
            return "+1-555-555-5555";
        }

        public string Country()
        {
            return "USA";
        }
    }
}
