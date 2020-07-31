using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using LazyCache;
using NLog;
using WebApp.App_Helpers.third_party.api;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
  //[Authorize]
  [RoutePrefix("Home")]
  public class HomeController : Controller
  {
    private readonly IAppCache cache;
    private readonly IMapper mapper;
    private readonly NLog.ILogger logger;
    private readonly SqlSugar.ISqlSugarClient db;


    public HomeController(
       NLog.ILogger logger,
      SqlSugar.ISqlSugarClient db,
      IAppCache cache, IMapper mapper) {
      this.db = db;
      this.cache = cache;
      this.mapper = mapper;
      this.logger = logger;

    }

    public async Task<ActionResult> Index()
    {
      //var list = this.companyService.Queryable().ToList();
     //var key= KeyGenerator.GetProductKey();


      ViewBag.P1 = 0;
      ViewBag.P2 = 0;
      ViewBag.P3 =0;
      ViewBag.P4 =0;
      return this.View();
    }

    public ActionResult About()
    {
      this.ViewBag.Message = "Your application description page.";

      return this.View();
    }

    public ActionResult GetTime() =>
        //ViewBag.Message = "Your application description page.";

        this.View();
    public ActionResult BlankPage() => this.View();
    public ActionResult AgileBoard() => this.View();


    public ActionResult Contact()
    {
      this.ViewBag.Message = "Your contact page.";

      return this.View();
    }
    public ActionResult Chat() => this.View();




  }
}