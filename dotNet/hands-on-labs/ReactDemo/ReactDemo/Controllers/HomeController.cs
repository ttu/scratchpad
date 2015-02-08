using ReactDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReactDemo.Controllers
{
    public class HomeController : Controller
    {
        private static List<CommentModel> _comments = new List<CommentModel>();

        static HomeController()
        {
            _comments = new List<CommentModel>
            {
                new CommentModel
                {
                    Author = "Daniel Lo Nigro",
                    Text = "Hello ReactJS.NET World!"
                },
                new CommentModel
                {
                    Author = "Pete Hunt",
                    Text = "This is one comment"
                },
                new CommentModel
                {
                    Author = "Jordan Walke",
                    Text = "This is *another* comment"
                },
            };
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
            // If use server side rendering, comments are passed to view
            //return View(_comments);
        }

        [Route("comments")]
        public ActionResult Comments()
        { 
            return Json(_comments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("comments/new")]
        public ActionResult AddComment(CommentModel comment)
        {
            _comments.Add(comment);
            return Content("Success :)");
        }
    }
}