using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eClinic.Web.Areas.TestArea.Controllers
{
    [Area("TestArea")]
    public class TestOneController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}