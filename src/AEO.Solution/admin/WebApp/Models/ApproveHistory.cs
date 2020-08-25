using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //审批历史记录
  public partial class ApproveHistory:Entity
  {
    [Display(Name = "关联ID", Description = "关联ID")]
    [DefaultValue(null)]
    public int? RefId { get; set; }
    [Display(Name = "关联单号", Description = "关联单号")]
    [MaxLength(128)]
    [DefaultValue(null)]
    public string RefKey { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(32)]
    [DefaultValue("待审")]
    public string Status { get; set; }
    [Display(Name = "发起人", Description = "发起人")]
    [MaxLength(32)]
    [DefaultValue("user")]
    public string Initiator { get; set; }
    [Display(Name = "提交时间", Description = "提交时间")]
    [DefaultValue(null)]
    public DateTime? SubmitDate { get; set; }

    [Display(Name = "待审人", Description = "待审人")]
    [MaxLength(32)]
    public string ToAuditor { get; set; }

    [Display(Name = "审批人", Description = "审批人")]
    [MaxLength(32)]
    public string Approver { get; set; }
    [Display(Name = "审批时间", Description = "审批时间")]
    [DefaultValue(null)]
    public DateTime? ApprovedDate { get; set; }
    [Display(Name = "审批意见", Description = "审批意见")]
    [MaxLength(512)]
    public string Result { get; set; }

    [Display(Name = "审批说明", Description = "审批说明")]
    [MaxLength(512)]
    public string Comment { get; set; }
    [Display(Name = "审批备注", Description = "审批备注")]
    [MaxLength(512)]
    public string Remark { get; set; }

  }
}