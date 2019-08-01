﻿using EClinic.Web.ViewModels.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace EClinic.Services
{
    public interface ISiteService
    {
        PageViewModel GetContent(string page);

        void EditPage(string page);
    }
}
