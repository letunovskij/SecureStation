using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DivisionWebGlobal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // TODO - добавить контроллеры с картой на странице контактов
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Index2", "SecurityObjects");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Интерактивная справка.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Страница контактов.";

            return View();
        }
    }
}