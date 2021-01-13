using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MaxWell.Server.Pages;
using Microsoft.AspNetCore.Mvc;
using Ooui.AspNetCore;
using Xamarin.Forms;

namespace MaxWell.Server.Controllers
{
    public class VMobileController : Controller
    {
        public IActionResult Index()
        {
            var page = new VMobilePage();
            var element = page.GetOouiElement();
            return new ElementResult(element, "Hello from XAML!");
        
        }
       
    }
}