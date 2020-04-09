using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Camefor.IServices;
using Camefor.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers {
    public class IndexController : Controller {
        public async Task<IActionResult> Index() {
            IArticlesServices _articlesServices = new ArticlesServices();
            var data = await _articlesServices.Query();
            return View(data);
        }

        public IActionResult Post() {
            return View();
        }

    }
}