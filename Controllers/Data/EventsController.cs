using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DivisionWebGlobal.DAL;
using DivisionWebGlobal.Models.Data;
using DivisionWebGlobal.Models.Dto;
using DivisionWebGlobal.Services;
using PagedList;

namespace DivisionWebGlobal.Controllers.Data
{
    [Authorize]
    public class EventsController : Controller
    {
        private MainDbContext db = new MainDbContext();
        private DeviceTablesService tableService = new DeviceTablesService();
        private StateService stateService = new StateService();

        /// <summary>
        /// ЛИЧНЫЙ КАБИНЕТ Вернуть страницу со списком тревожных сообщений
        /// Возращает список всех тревожных сообщений (event)
        /// Информацию о конфигурации (config)
        /// Информацию о текущем состоянии устройства (сделать автообновление) (head_state)
        /// Информацию о конфигурационной таблице устройств (head_table)
        /// Информацию об адресе из конфигурации (address)
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int? page, int? idHead, string type = "all")
        {
            // !!! запрос происходит не с ObjectCardIndex а с GetEventData // check // временно передавать в модель тип текущих сообщений
            int _idHead = idHead == null ? 1 : (int)idHead;
            var dvHeadEvents = db.DvHeadEvents.Include(e => e.DvHead).Where(e => e.DvHead.Id.Equals(_idHead)).ToList();
            var pageNumber = (page ?? 1);
            var pageSize = 6;

            // получить таблицы подключенных устройств
            TablesDto tablesDto = tableService.RequestDeviceTables(_idHead);
            if (tablesDto.Failure)
            {
                return HttpNotFound();
            };

            // узнать состояние DV-HEAD OMEGA
            HeadState headState = stateService.RequestLastStateById(_idHead);

            // TODO Config и Address в дальнейшем можно хранить в классе конфигурация приложения

            // в зависимости от типа запрашиваемых данных требуется возвращать обработанные или нет сообщения
            // var events = null; - check
            EventViewModel eventViewModel = new EventViewModel();

            switch (type)
            {
                case "all":
                    eventViewModel.Events = dvHeadEvents.OrderByDescending(o => o.Time).ToPagedList(pageNumber, pageSize);
                    eventViewModel.Type = "all";
                    break;
                case "crud":
                    eventViewModel.Events = dvHeadEvents.Where(o => o.Alarm > 0).OrderByDescending(o => o.Time).ToPagedList(pageNumber, pageSize);
                    eventViewModel.Type = "crud";
                    break;
                default:
                    eventViewModel.Events = dvHeadEvents.OrderByDescending(o => o.Time).ToPagedList(pageNumber, pageSize);
                    eventViewModel.Type = "all";
                    break;
            }

            eventViewModel.ExternalTable = tablesDto.ExternalTable;
            eventViewModel.ConfigurationTable = tablesDto.ConfigurationTable;
            eventViewModel.DvHead = tablesDto.DvHead;
            eventViewModel.DvHeadState = headState;
            eventViewModel.Address = tablesDto.Address;

            return View(eventViewModel);
        }

        // вернуть карточку объекта
        public ActionResult ObjectCardIndex(int? page, int? idHead, string type = "all", int? badVariable = 0)
        {
            int _idHead = idHead == null ? 1 : (int)idHead;
            var dvHeadEvents = db.DvHeadEvents.Include(e => e.DvHead).Where(e => e.DvHead.Id.Equals(_idHead)).ToList();
            var pageNumber = (page ?? 1);
            var pageSize = 6;

            EventViewModel eventViewModel = new EventViewModel();
            switch (type)
            {
                case "all":
                    eventViewModel.Events = dvHeadEvents.OrderByDescending(o => o.Time).ToPagedList(pageNumber, pageSize);
                    eventViewModel.Type = "all";
                    break;
                case "crud":
                    eventViewModel.Events = dvHeadEvents.Where(o => o.Alarm > 0).OrderByDescending(o => o.Time).ToPagedList(pageNumber, pageSize);
                    eventViewModel.Type = "crud";
                    var fuck = eventViewModel.Events.ToList();
                    break;
                default:
                    eventViewModel.Events = dvHeadEvents.OrderByDescending(o => o.Time).ToPagedList(pageNumber, pageSize);
                    eventViewModel.Type = "all";
                    break;
            }
            eventViewModel.DvHead = eventViewModel.Events.FirstOrDefault().DvHead;
            if (badVariable == 3)
            {
                return View(eventViewModel);
            }
            //this.HttpContext.Response.AddHeader("refresh", "2; url=" + Url.Action("ObjectCardIndex"));

            return Request.IsAjaxRequest() 
                ? (ActionResult)PartialView("GetEventData", eventViewModel) 
                : View(eventViewModel);
        }

        // Выбор оператора для демонстрации
        //[Route("ObjectCardIndex/Events/SaveOperatorChoise")]
        //[HttpPost("ObjectCardIndex/Events/SaveOperatorChoise/{operChoiseDto:OperatorChoiseDto}")]
        [Route("ObjectCardIndex/SaveOperatorChoise")]
        [HttpPost]
        public virtual ActionResult SaveOperatorChoise(OperatorChoiseDto operChoiseDto)
        {
            // 01.09.2017 добавлена проверка на наличие тревожных сообщений на данном объекте
            // получить данные от клиента
            if (operChoiseDto == null)
            {
                return Json(new { Success = false, Message = "НЕ УДАЛОСЬ ОБРАБОТАТЬ ЗАПРОС" });
            }

            // если данные корректны, то изменить статус сообщения на обработанное и сохранить выбор оператора
            // 1) изменить запись в таблице Event 
            // 2) TODO: добавить ссылку на информацию [новая таблица], когда произошло обновление записи
            if (ModelState.IsValid)
            {
                // занести действия пользователя в БД
                var ev = db.DvHeadEvents.Find(operChoiseDto.EventId);

                // установить оператора
                // ev. = operChoiseDto.OperatorEmail; TODO Добавить оператора

                // установить выбор оператора
                ev.ContinueActivity = operChoiseDto.Choise;

                // объект перестал быть тревожным
                ev.Alarm = 0;

                // установить статус обработки события
                ev.IsProcess = true;

                // TODO инициировать дальнейшие действия - позвонить в ЧОП, записать в БД
                // TODO обновить интерфейс оператора - страницу на стороне оператора
                db.Entry(ev).State = EntityState.Modified;
                db.SaveChanges(); // TODO проверки на соединение, и наличие записи

                // TODO проверить наличие тревожных сообщений на центральном контроллере DV-HEAD OMEGA по id 
                // var tsCount = db.DvHeads.Find(operChoiseDto.HeadId).// Where(e => e.)
                // var tsCount = db.DvHeads.Where(head => head.Id == operChoiseDto.HeadId )
                var tsCount = db.DvHeadEvents.Where(eve => eve.Idhead == operChoiseDto.HeadId && eve.Alarm > 0).Count();
                // объект перестал быть тревожным
                if (tsCount == 0)
                {
                    // нашли необходимый DV-HEAD OMEGA
                    var dvHead = db.DvHeads.Find(operChoiseDto.HeadId);
                    // сделали нетревожным
                    dvHead.Alarm = 0;
                    db.Entry(dvHead).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json(new { Success = true, Message = "Тревожное сообщение обработано" });
        }

        public PartialViewResult GetEventData(int? page, int? idHead)
        {
            int _idHead = idHead == null ? 1 : (int)idHead;
            var dvHeadEvents = db.DvHeadEvents.Include(e => e.DvHead).Where(e => e.DvHead.Id.Equals(_idHead)).ToList();
            var pageNumber = (page ?? 1);
            var pageSize = 6;

            EventViewModel eventViewModel = new EventViewModel()
            {
                Events = dvHeadEvents.OrderByDescending(o => o.Time).ToPagedList(pageNumber, pageSize)
            };
            eventViewModel.DvHead = eventViewModel.Events.FirstOrDefault().DvHead;
            return PartialView(eventViewModel);
        }

        /// <summary>
        /// Вернуть страницу с информацией о событии по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.DvHeadEvents.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress");
            return View();
        }

        // POST: Events/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Time,Idhead,EventType,EventOriginal,EventDecrypt")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.DvHeadEvents.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress", @event.Idhead);
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.DvHeadEvents.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress", @event.Idhead);
            return View(@event);
        }

        // POST: Events/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Time,Idhead,EventType,EventOriginal,EventDecrypt")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idhead = new SelectList(db.DvHeads, "Id", "Ipaddress", @event.Idhead);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.DvHeadEvents.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.DvHeadEvents.Find(id);
            db.DvHeadEvents.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
