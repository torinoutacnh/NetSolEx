using Core.Constant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Repository.Contract.Infrastructure;
using Repository.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSol.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<User> _userRepo;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IServiceProvider serviceProvider)
        {
            _userRepo = serviceProvider.GetRequiredService<IRepository<User>>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        [HttpGet]
        [Route(Endpoints.UserEndpoint.BaseEndpoint)]
        public List<User> Users()
        {
            return _userRepo.Get().ToList();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
