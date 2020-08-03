using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //操作日志
  public partial class ActionLog:Entity
  {
    [Display(Name ="关联ID", Description = "关联ID")]
    public int RefId { get; set; }
    [Display(Name = "关联单号", Description = "关联单号")]
    [MaxLength(128)]
    public string RekKey { get; set; }
    [Display(Name = "操作时间", Description = "操作时间")]
    public DateTime ActionDateTime { get; set; }
    [Display(Name = "操作人员", Description = "操作人员")]
    [MaxLength(20)]
    public string User { get; set; }
    [Display(Name = "操作类型", Description = "操作类型")]
    [MaxLength(20)]
    public string Action { get; set; }
    [Display(Name = "操作内容", Description = "操作内容")]
    [MaxLength(128)]
    public string Content { get; set; }
    [Display(Name = "同步标志", Description = "同步标志")]
    public bool Flag { get; set; }
  }
}