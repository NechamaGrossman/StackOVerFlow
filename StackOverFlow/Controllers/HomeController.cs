using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackOverFlow.Data;
using StackOverFlow.Models;

namespace StackOverFlow.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            QuestionRepository qr = new QuestionRepository(_connectionString);
            List<Question> q = qr.GetQuestions();
            return View(q);
        }

        public IActionResult Question(int Id)
        {
            QuestionRepository qr = new QuestionRepository(_connectionString);
            QuestionTagViewModel qvm = new QuestionTagViewModel();
            qvm.Question = qr.GetQuestionById(Id);
            qvm.Tags = qr.GetTagsForQuestionId(Id);
            qvm.AlreadyLiked = qr.IsQuestionLikedByUser(Id,qr.GetUserByEmail(User.Identity.Name).Id);
            qvm.LoggedOn=(User.Identity.IsAuthenticated);
            return View(qvm);
        }
        public IActionResult GetLikes(int Id)
        {
            QuestionRepository qr = new QuestionRepository(_connectionString);
            LikeCount lk = new LikeCount();
            lk.Number = qr.GetLikesCountForId(Id);
            return Json(lk);
        }
        public IActionResult AddLikes(int QuestionId)
        {
            Likes l = new Likes();
            l.QuestionId = QuestionId;
            QuestionRepository qr = new QuestionRepository(_connectionString);
            l.UserId = qr.GetUserByEmail(User.Identity.Name).Id;
            l.Like = true;
            qr.AddLikes(l);
            int LikesCount = qr.GetLikesCountForId(QuestionId);
            return Json(new { Likes = LikesCount });
        }
        
        public IActionResult AddAnswer(Answers answers)
        {
            QuestionRepository qr = new QuestionRepository(_connectionString);
            answers.UserId = qr.GetUserByEmail(User.Identity.Name).Id;
            qr.AddAnswer(answers);
            return Redirect($"/Home/Question?Id={answers.QuestionId}");

        }
        [HttpPost]
        public IActionResult AskAQuestion(Question question, IEnumerable<string> tags )
        {
            QuestionRepository qr = new QuestionRepository(_connectionString);
            qr.AskQuestion(question, tags.ToList());
            return View();

        }
        public IActionResult AskAQuestion()
        {
            return View();

        }
        public class LikeCount
        {
            public int Number { get; set; }
        }

    }
}
