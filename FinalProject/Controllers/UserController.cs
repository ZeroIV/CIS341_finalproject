using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Controllers
{
    [Route("/[action]")]
    public class UserController : Controller
    {
        private readonly FinalProjectContext _context;
        private readonly SignInManager<UserAccount> _signInManager;

        public UserController(FinalProjectContext context, SignInManager<UserAccount> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return LocalRedirect("~/Identity/Account/Login");
        }
    }
}
