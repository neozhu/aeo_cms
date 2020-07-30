using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Repository.Pattern.Infrastructure;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
  [Authorize]
  [RoutePrefix("AccountManage")]
  public class AccountManageController : Controller
  {
    private readonly NLog.ILogger logger;
    private ApplicationUserManager _userManager;

    private  ApplicationDbContext dbContext => HttpContext.GetOwinContext().Get<ApplicationDbContext>();
    public AccountManageController(
                                NLog.ILogger logger
                               ) {
      this.logger = logger;
    }
    public ApplicationUserManager UserManager
    {
      get => this._userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      private set => this._userManager = value;
    }
    private ApplicationRoleManager roleManager
    {
      get => this.HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
    }
    private ApplicationSignInManager _signInManager;
     
    public ApplicationSignInManager SignInManager
    {
      get => this._signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
      private set => this._signInManager = value;
    }
    [Route("Index", Name = "系统账号管理", Order = 1)]
    public async Task<ActionResult> Index() {
      var data = await this.dbContext.Tenants
        .Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() })
        .ToListAsync();
      ViewBag.TenantId = data;
      return View();
      }


    //获取租户数据
    public async Task<JsonResult> GetTenantData()
    {
      var data = await this.dbContext.Tenants.ToListAsync();
      return Json(data, JsonRequestBehavior.AllowGet);
    }

    //解锁，加锁账号
    public async Task<JsonResult> SetLockout(string[] userid)
    {
      foreach (var id in userid)
      {
        await this.UserManager.SetLockoutEndDateAsync(id, new DateTimeOffset(DateTime.Now.AddYears(1)));
      }
      return Json(new { success = true }, JsonRequestBehavior.AllowGet);
    }
    //注册新用户
    public async Task<JsonResult> ReregisterUser(AccountRegistrationModel model) {
      if (this.ModelState.IsValid)
      {
        var tenant =await this.dbContext.Tenants.FindAsync(model.TenantId);
        var user = new ApplicationUser
        {
          UserName = model.Username,
          FullName = model.FullName,
          TenantDb = tenant.ConnectionStrings,
          TenantName = tenant.Name,
          TenantId = model.TenantId,
          Gender = 0,
          Email = model.Email,
          PhoneNumber = model.PhoneNumber,
          AccountType = 0,
          Avatars = "ng.jpg",
          AvatarsX120 = "ng.jpg",

        };
        var result = await this.UserManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          this.logger.Info($"注册成功【{user.UserName}】");
          await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim("http://schemas.microsoft.com/identity/claims/tenantid", user.TenantId.ToString()));
          await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.UserName));
          await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.GivenName,  user.FullName??""));
          await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim("http://schemas.microsoft.com/identity/claims/tenantname",   user.TenantName??""));
          await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email));
          await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim("http://schemas.microsoft.com/identity/claims/avatars",  user.Avatars ?? ""));
          await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.MobilePhone,   user.PhoneNumber??""));
          await this.UserManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Country, "zh-cn"));
          var role = "users";
          var any = await this.roleManager.FindByNameAsync(role);
          if (any != null)
          {
            await this.UserManager.AddToRoleAsync(user.Id, role);
          }
          return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        else
        {
          return Json(new { success = false, err = string.Join(",", result.Errors) }, JsonRequestBehavior.AllowGet);
        }

      }
      else
      {
        var modelStateErrors = string.Join(",", ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(n => n.ErrorMessage)));
        return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
      }
    }
    public async Task<JsonResult> SetUnLockout(string[] userid)
    {
      foreach (var id in userid)
      {
        await this.UserManager.SetLockoutEndDateAsync(id, DateTime.Now);
      }
      return Json(new { success = true }, JsonRequestBehavior.AllowGet);
    }
    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<JsonResult> ResetPassword(string id, string newPassword)
    {
      var code =await this.UserManager.GeneratePasswordResetTokenAsync(id);
      var result =await this.UserManager.ResetPasswordAsync(id, code, newPassword);
      if (result.Succeeded)
      {
        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      }
      else
      {
        return Json(new { success = false, err = string.Join(",", result.Errors) }, JsonRequestBehavior.AllowGet);
      }

    }
    
    [HttpGet]
    public  JsonResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "desc", string filterRules = "")
    {
      var filters = PredicateBuilder.From<ApplicationUser>(filterRules);
      var totalCount = 0;

      var users = this.UserManager.Users.Where(filters).OrderByName(sort, order);
     
      totalCount = users.Count();
      var datalist = users.Skip(( page - 1 ) * rows).Take(rows);
      var datarows = datalist.Select(n => new
      {
        Id = n.Id,
        UserName = n.UserName,
        FullName = n.FullName,
        Gender = n.Gender,
        TenantDb = n.TenantName,
        TenantName = n.TenantName,
        AccountType = n.AccountType,
        Email = n.Email,
        TenantId = n.TenantId,
        PhoneNumber = n.PhoneNumber,
        AvatarsX50 = n.Avatars,
        AvatarsX120 = n.AvatarsX120,
        AccessFailedCount = n.AccessFailedCount,
        LockoutEnabled = n.LockoutEnabled,
        LockoutEndDateUtc = n.LockoutEndDateUtc,
        IsOnline = n.IsOnline,
        EnabledChat = n.EnabledChat
      }).ToList();
      var pagelist = new { total = totalCount, rows = datarows };
      return this.Json(pagelist, JsonRequestBehavior.AllowGet);
    }

   
    [HttpGet]
    public JsonResult GetAvatarsX50()
    {
      var list = new List<dynamic>();
      for (var i = 1; i <= 8; i++)
      {
        list.Add(new { name = "femal" + i.ToString() });
        list.Add(new { name = "male" + i.ToString() });
      }
      return this.Json(list.ToArray(), JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public async Task<JsonResult> DeleteCheckedAsync(string[] id)
    {
      foreach (var key in id)
      {
        var user = await this.UserManager.FindByIdAsync(key);
        await this.UserManager.DeleteAsync(user);
      }
      return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
    }
    

    [HttpGet]
    public ActionResult Create() => this.View();

    [HttpPost]
    public async Task<ActionResult> Create(RegisterViewModel model)
    {
      if (this.ModelState.IsValid)
      {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await this.UserManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

          // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
          // Send an email with this link
          // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
          // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
          // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

          return this.RedirectToAction("Index", "AccountManager");
        }
        this.AddErrors(result);
      }

      // If we got this far, something failed, redisplay form
      return this.View(model);
    }
    [HttpGet]
    public async Task<ActionResult> Edit(string id)
    {

      var user = await this.UserManager.FindByIdAsync(id);
      if (user == null)
      {
        return this.View("Error");
      }

      return this.View(user);

    }

    [HttpPost]
    public async Task<ActionResult> Edit(ApplicationUser user)
    {
      if (this.ModelState.IsValid)
      {
        var item = await this.UserManager.FindByIdAsync(user.Id);
        item.UserName = user.UserName;
        item.PhoneNumber = user.PhoneNumber;
        item.Email = user.Email;
        var result = await this.UserManager.UpdateAsync(item);
        if (result.Succeeded)
        {
          return this.RedirectToAction("Index", "AccountManager");
        }
        this.AddErrors(result);
      }
      return this.View(user);

    }

    [HttpPost]
    public async Task<ActionResult> Delete(string id)
    {
      if (this.ModelState.IsValid)
      {
        var user = await this.UserManager.FindByIdAsync(id);
        var result = await this.UserManager.DeleteAsync(user);
        if (result.Succeeded)
        {
          if (this.Request.IsAjaxRequest())
          {
            return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
          }
          return this.RedirectToAction("Index", "AccountManager");
        }
        this.AddErrors(result);
      }
      return this.View();
    }

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        this.ModelState.AddModelError("", error);
      }
    }

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (this.Url.IsLocalUrl(returnUrl))
      {
        return this.Redirect(returnUrl);
      }
      return this.RedirectToAction("Index", "Home");
    }
  }
}