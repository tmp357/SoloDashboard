using SoloDashboard.Entities.Models;
using SoloDashboard.Repository.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SoloDashboard.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork uow;

        public HomeController(IUnitOfWork uow)
        {
            this.uow = uow;
        }       
        public ActionResult Dashboard()
        {
            var jobs = uow.DbContextJobsTestData.GetScheduleJobs();
            ViewBag.Title = "All Printflow Activity";
            return View(jobs);
        }

        public ActionResult DashboardFiltered(string colummParam, string SearchColumn, string reportTitle = "All PrintFlow Schedule")
        {
            var exp = uow.DbContextJobsTestData.GetDynamicQueryWithExpresionTrees(SearchColumn, colummParam);
            var job = uow.DbContextJobsTestData.Find(exp);

            ViewBag.Title = reportTitle;
            return View("Dashboard", job);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}