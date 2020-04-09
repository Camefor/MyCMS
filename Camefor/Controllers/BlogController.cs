using Camefor.IServices;
using Camefor.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Camefor.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    //[Intercept(typeof(CallLogger))]
    public class BlogController : ControllerBase {

        readonly IBlogArticleServices _blogArticleServices;

        public BlogController(IBlogArticleServices blogArticleServices) {
            _blogArticleServices = blogArticleServices;
        }
        // GET: api/Blog/5
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id">参数id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetBlog")]

        public async Task<List<BlogArticle>> Get(int id) {
            return await _blogArticleServices.Query(d => d.bID == id);
        }

        [HttpGet]
        public int DoSomething(string id) {
            var _data = _blogArticleServices.DoSomething((System.Convert.ToInt32(id)));
            return _data;
        }
    }
}