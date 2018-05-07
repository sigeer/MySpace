using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Controllers
{
    public class BlogManagementController : Controller
    {
        public IActionResult BlogManagement()
        {
            return View();
        }
        public IActionResult WriteBlog()
        {
            return View();
        }
        public IActionResult CommentManagement()
        {
            return View();
        }
    }
}