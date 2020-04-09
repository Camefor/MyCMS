using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Camefor.IServices;
using Camefor.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class PostController : Controller
    {
        [ActionName("Index")]
        public async Task<IActionResult> Index() {
           
            IArticlesServices _articlesServices = new ArticlesServices();
            var data = await _articlesServices.QueryByID(2);
            return View(data);
        }


        public async Task<IActionResult> v(long id)
        {
            if (id ==0) {
                return NotFound();
            }
            IArticlesServices _articlesServices = new ArticlesServices();
            var data = await _articlesServices.QueryByID(id);
            return View(data);
        }
    }
}