using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //客户跟进情况
  public partial class CustomerFollow:Entity
  {

    [Display(Name = "客户联系人", Description = "客户联系人")]
    [MaxLength(80)]
    [Required]
    public string ContactName { get; set; }
    [Display(Name = "跟进方式", Description = "跟进方式")]
    [MaxLength(20)]
    public string FollowType { get; set; }
    [Display(Name = "跟进状态", Description = "跟进状态")]
    [MaxLength(20)]
    public string Status { get; set; }
    [Display(Name = "跟进人", Description = "跟进人")]
    [MaxLength(20)]
    public string Owner { get; set; }
    [Display(Name = "跟进时间", Description = "跟进时间")]
    [DefaultValue("now")]
    public DateTime FollowDate { get; set; }
    [Display(Name = "跟进内容", Description = "跟进内容")]
    public string Content { get; set; }

    #region 需要设置提醒
    [Display(Name = "设置提醒时间", Description = "设置提醒时间")]
    public DateTime? ReminderTime { get; set; }
    [Display(Name = "提醒内容", Description = "提醒内容")]
    public string ReminderContent { get; set; }
    [Display(Name = "提醒人员", Description = "提醒人员")]
    [MaxLength(200)]
    public string ReminderTo { get; set; }
    #endregion


    [Display(Name = "客户编号", Description = "客户编号")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("customer.CustomerCode")]
    public string CustomerCode { get; set; }
    [Display(Name = "客户名称", Description = "客户名称")]
    [MaxLength(80)]
    [Required]
    [DefaultValue("customer.CustomerName")]
    public string CustomerName { get; set; }

    [Display(Name = "所属客户", Description = "所属客户")]
    [DefaultValue("customer.Id")]
    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    [Display(Name = "所属客户", Description = "所属客户")]
    public Customer Customer { get; set; }
  }
}