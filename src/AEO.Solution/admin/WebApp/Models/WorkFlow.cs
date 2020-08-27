using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //审批记录表
  public partial class WorkFlow:Entity
  {

    [Display(Name = "关联业务单号", Description = "关联业务单号")]
    [MaxLength(128)]
    public string RefKey { get; set; }
    [Display(Name = "审批级别", Description = "审批级别")]
    [MaxLength(30)]
    public string Level { get; set; }
    [Display(Name = "提交人", Description = "提交人")]
    [MaxLength(20)]
    public string Initiator { get; set; }
    [Display(Name = "待审人", Description = "待审人")]
    [MaxLength(20)]
    public string PendingUser { get; set; }
    [Display(Name = "审批人", Description = "审批人")]
    [MaxLength(20)]
    public string ConfirmUser { get; set; }
    [Display(Name = "审批结果", Description = "审批结果")]
    [MaxLength(50)]
    public string Result { get; set; }
    [Display(Name = "审批意见", Description = "审批意见")]
    public string RatifyText { get; set; }
    [Display(Name = "审批说明", Description = "审批说明")]
    public string Description { get; set; }
    [Display(Name = "申请时间", Description = "申请时间")]
    public DateTime InitDate { get; set; }

    [Display(Name = "审批时间", Description = "审批时间")]
    public DateTime? ConfirmDate { get; set; }
  }
}