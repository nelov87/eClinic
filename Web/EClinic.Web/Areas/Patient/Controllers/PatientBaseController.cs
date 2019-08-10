using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EClinic.Web.Areas.Patient.Controllers
{
    [Authorize(Roles = "Administrator, Doctor, Patient")]
    [Area("Patient")]
    public class PatientBaseController : Controller
    {
        
    }
}