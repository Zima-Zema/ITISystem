using ITISystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using ITISystem.ViewModel;
using System.Net;

namespace ITISystem.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext _context;
        public QuestionsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Questions
        public ActionResult Index()
        {
            var qlist = _context.Questions.Include(q => q.Courses);
            return View(qlist);
        }
        public ActionResult New()
        {
            var viewModel = new QuestionViewModel()
            {
                CourseList = _context.Courses.ToList(),
                QuestionObj = new Question()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(QuestionViewModel Qvm)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new QuestionViewModel()
                {
                    QuestionObj = Qvm.QuestionObj,
                    CourseList = _context.Courses.ToList()
                };
                return View("New", viewModel);
            }

            if (Qvm.QuestionObj != null)
            {
                if (Qvm.QuestionObj.Answer_Model == "A")
                {
                    Qvm.QuestionObj.Answer_Model = Qvm.QuestionObj.Answer_A;
                }
                if (Qvm.QuestionObj.Answer_Model == "B")
                {
                    Qvm.QuestionObj.Answer_Model = Qvm.QuestionObj.Answer_B;
                }
                if (Qvm.QuestionObj.Answer_Model == "C")
                {
                    Qvm.QuestionObj.Answer_Model = Qvm.QuestionObj.Answer_C;
                }
                if (Qvm.QuestionObj.Answer_Model == "D")
                {
                    Qvm.QuestionObj.Answer_Model = Qvm.QuestionObj.Answer_D;
                }
            }

            if (Qvm.QuestionObj.Question_id == 0)
            {
                _context.Questions.Add(Qvm.QuestionObj);
            }
            else
            {
                var OldQu = _context.Questions.SingleOrDefault(qq => qq.Question_id == Qvm.QuestionObj.Question_id);
                OldQu.Header = Qvm.QuestionObj.Header;
                OldQu.Body = Qvm.QuestionObj.Body;
                OldQu.Grade = Qvm.QuestionObj.Grade;
                OldQu.Answer_A = Qvm.QuestionObj.Answer_A;
                OldQu.Answer_B = Qvm.QuestionObj.Answer_B;
                OldQu.Answer_C = Qvm.QuestionObj.Answer_C;
                OldQu.Answer_D = Qvm.QuestionObj.Answer_D;
                OldQu.Answer_Model = Qvm.QuestionObj.Answer_Model;
                OldQu.Course_key = Qvm.QuestionObj.Course_key;

            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Questions");
        }

        public ActionResult Edit(int id)
        {
            var qObj = _context.Questions.SingleOrDefault(qq => qq.Question_id == id);
            if (qObj == null)
            {
                return HttpNotFound();
            }
            var viewModel = new QuestionViewModel()
            {
                QuestionObj = qObj,
                CourseList = _context.Courses.ToList()
            };
            return View("New", viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = _context.Questions.Include(c => c.Courses).SingleOrDefault(cu => cu.Question_id == id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);

        }
    }

}