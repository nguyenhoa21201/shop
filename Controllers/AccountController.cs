using Microsoft.AspNetCore.Mvc;
using project.Models;
using System.Diagnostics;
namespace project.Controllers
{
    public class Account : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
