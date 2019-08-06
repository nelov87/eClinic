using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EClinic.Web.Areas.Doctor.Controllers
{
    [Authorize(Roles = "Administrator, Doctor")]
    [Area("Doctor")]
    public class DoctorController : Controller
    {
       
    }
}