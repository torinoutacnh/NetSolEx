using Core.Constant;
using Core.JWT.LoadSetting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Repository.Contract.Infrastructure;
using Repository.Contract.Models;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSol.Controllers
{
    [AuthorizeJWT]
    public class HomeController : Controller
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Post> _postRepo;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IServiceProvider serviceProvider)
        {
            _userRepo = serviceProvider.GetRequiredService<IRepository<User>>();
            _postRepo = serviceProvider.GetRequiredService<IRepository<Post>>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        [HttpGet]
        [Route(Endpoints.UserEndpoint.BaseEndpoint)]
        public List<User> Users()
        {
            return _userRepo.Get().ToList();
        }

        [HttpGet]
        [Route(Endpoints.UserEndpoint.PostEndpoint)]
        public List<Post> Posts()
        {
            return _postRepo.Get().ToList();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
