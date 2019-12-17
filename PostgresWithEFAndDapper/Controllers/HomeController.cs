using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PostgresWithEFAndDapper.Blog;
using PostgresWithEFAndDapper.Infrastructure;
using PostgresWithEFAndDapper.Models;

namespace PostgresWithEFAndDapper.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogContext _blogContext;
        private readonly IDbConnectionProvider _connectionProvider;

        public HomeController(BlogContext blogContext, IDbConnectionProvider connectionProvider)
        {
            _blogContext = blogContext;
            _connectionProvider = connectionProvider;
        }

        public IActionResult Index()
        {
            using var connection = _connectionProvider.Connection();
            var blogsFromDapper = connection.Query<Blog.Domain.Blog>("SELECT * FROM blog").ToList();
            var blogs = _blogContext.Blogs.AsNoTracking().ToList();
            return View(blogsFromDapper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string domainName)
        {
            _blogContext.Blogs.Add(new Blog.Domain.Blog() {Url = $"https://www.{domainName}"});
            _blogContext.SaveChanges();
            return View("Index", _blogContext.Blogs.AsNoTracking().ToList());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}