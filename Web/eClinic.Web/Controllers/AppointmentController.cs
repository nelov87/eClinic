using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eClinic.Web.Controllers
{
    public class AppointmentController : BaseController
    {
        public IActionResult ShowMonth()
        {
            return this.View();
        }
    }
}