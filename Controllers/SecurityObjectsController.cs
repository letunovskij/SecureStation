using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DivisionWebGlobal.Models;
using DivisionWebGlobal.Models.Data;
using DivisionWebGlobal.Models.ViewModel;
using DivisionWebGlobal.DAL;
using System.Diagnostics;
using System.Web.Helpers;

namespace DivisionWebGlobal.Controllers
{
    [Authorize]
    public class SecurityObjectsController : Controller
    {
        public MainDbContext dbContext = new MainDbContext();

        public ActionResult Index()
        {
            // получить все Config
            IEnumerable<Config> dvHeads = dbContext.DvHeads.ToList();
            IEnumerable<Event> dvHeadEvents = dbContext.DvHeadEvents.OrderByDescending(o => o.Time).ToList();
            var securityObjectInfoIndexData = new SecurityObjectInfoViewModel()
            {
                Objects = dvHeads,
                Events = dvHeadEvents.OrderByDescending(e => e.Time).Take(100)
            };
            return View(securityObjectInfoIndexData);
            // TODO в дальнейшем сформировать таблицу городов
        }

        // добавить возврат View с запросом DV-HEAD по микрорайону и тревожным сообщениям


        // GET: SecurityObject
        public ActionResult Index2()
        {
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            System.Collections.Generic.SortedList<int, string> st = new System.Collections.Generic.SortedList<int,string>();
            // получить все охраняемые объекты из БД
            IEnumerable<SecurityObject> securityObjects = dbContext.SecurityObjects.ToList();

            // TODO!!! realize statistic service когда появится ясность с реализацией (см. систему администрирования)
            // подсчитывает количество объектов в городе и микрорайоне
            // Для города завести таблицу: Город. Для микрорайона: Микрорайон. Заполнять их на странице администрирования

            var allCount_Rostov = dbContext.DvHeads.Where(e => e.Address.City.Equals("Ростов-на-Дону")).Count();
            var underSecObjs_Rostov = dbContext.DvHeads.Where(e => e.UnderSecurity > 0 && e.Address.City.Equals("Ростов-на-Дону")).Count(); // кол-во охр. объекты
            var alarm_Rostov = dbContext.DvHeads.Where(e => e.Alarm > 0 && e.Address.City.Equals("Ростов-на-Дону")).Count();

            var allCount_Proletar = dbContext.DvHeads.Where(e => e.Address.Mkrn.Equals("Пролетарский район")).Count();
            var underSecObj_Mkrn_Proletar = dbContext.DvHeads.Where(e => e.UnderSecurity > 0 && e.Address.Mkrn.Equals("Пролетарский район")).Count();
            var alarm_Mkrn_Proletar = dbContext.DvHeads.Where(e => e.Alarm > 0 && e.Address.Mkrn.Equals("Пролетарский район")).Count();

            // временно рассчитывается количество объектов в Ростове в Пролетарском районе из таблицы Config. 
            // Имеет смысл приступать к дальнейшей разработки только после решения вопроса с администрированием
            IEnumerable<CityViewModel> cvms = new List<CityViewModel>() {
                new CityViewModel()
                {
                    Name = "Ростов-на-Дону",
                    AllCount = allCount_Rostov,
                    UmbrellasCount = underSecObjs_Rostov,
                    AlarmsCount = alarm_Rostov,
                    CrashsCount = 0,
                    DebtsCount = 0,
                    Mkrns = new List<MkrnViewModel>
                    {
                        new MkrnViewModel { Name = "Октябрьский", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Пролетарский", AllCount = allCount_Proletar, UmbrellasCount = underSecObj_Mkrn_Proletar, AlarmsCount = alarm_Mkrn_Proletar, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Железнодорожный", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Кировский", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Ленинский", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Левенцовский", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Платовский", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Суворовский", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Первомайский", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Советский", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                    }
                },
                new CityViewModel()
                {
                    Name = "Краснодар",
                    AllCount = 0,
                    UmbrellasCount = 0,
                    AlarmsCount = 0,
                    CrashsCount = 0,
                    DebtsCount = 0,
                    Mkrns = new List<MkrnViewModel>
                    {
                        new MkrnViewModel { Name = "Центральный", AllCount = 0,  UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Пашковский", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Покровка", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Дубинка", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Славянский", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Калинино", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "ЗИП", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Школьный", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Черемушки", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "РИП", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "КСК", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "КБК", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Фестивальный", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "РМ3", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "СХИ", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "9 километр", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Гидростроителей", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Авиагородок", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Комсомольский", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "ККБ", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "МХГ", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Юбилейный", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                        new MkrnViewModel { Name = "Городок им. Маршала Жукова", AllCount = 0, UmbrellasCount = 0, AlarmsCount = 0, CrashsCount = 0, DebtsCount = 0 },
                    }
                },
            };
            // получить тревожные сообщения - events
            //
            var dvHeadEvents = dbContext.DvHeadEvents.OrderByDescending(e => e.Time).Take(100)./*Include(e => e.DvHead).*/ToList();

            var securityObjectIndexData = new SecurityObjectsViewModel
            {
                Cities = cvms,
                Events = dvHeadEvents
            };

            return View(securityObjectIndexData);
        }

        // ajax запрос для определения наличия тревог на объекте
        // временно обновлять состояние страницы целиком - вызвать метод Index2 [объект может перестать находиться под охраной]
        public virtual JsonResult/*ActionResult*/ UpdateAlarmEvents()
        {
            // 08/09/2017
            // TODO реализовать 2 очереди сообщений - одна с необработанными сообщениями, другая с обработанными
            // Сообщения накапливаются, по мере накопления отсылаются пользователям, 
            
            // хорошее решение - веб-сокеты
            // временное решение - модифицировать DMG - но плохо поддерживаемый код, все в одном файле
            // Надо раз в 5 минут обновлять список email - ов для рассылки и список разрешений на рассылку email
            // в отдельном потоке синхронизировать обновление из БД
            var alarm_Rostov = dbContext.DvHeads.Where(e => e.Alarm > 0 && e.Address.City.Equals("Ростов-на-Дону")).Count();
            var underSecurity_Rostov = dbContext.DvHeads.Where(e => e.UnderSecurity > 0 && e.Address.City.Equals("Ростов-на-Дону")).Count();
            var all_Rostov = dbContext.DvHeads.Where(e => e.Address.City.Equals("Ростов-на-Дону")).Count();
            // временно для отладки - проверка на наличие постановки раздела под охрану

            // передать на клиент данные по городам - alarm_Rostov, 
            if (alarm_Rostov > 0) {
                return Json(new { Success = true, AlarmRostov = alarm_Rostov, UnderSecurity_Rostov = underSecurity_Rostov, AllRostov = all_Rostov, Message = "Появились тревожные сообщения" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = false, AlarmRostov = alarm_Rostov, underSecurity_Rostov = underSecurity_Rostov, AllRostov = all_Rostov, Message = "Нет тревожных сообщений"}, JsonRequestBehavior.AllowGet);
        }


    }
}