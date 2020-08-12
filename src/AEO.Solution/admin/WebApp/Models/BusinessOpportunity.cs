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
  //商机管理
  public partial class BusinessOpportunity : Entity
  {
    [Display(Name = "商机名称", Description = "商机名称")]
    [MaxLength(128)]
    [Required]
    public string Name { get; set; }

    [Display(Name = "负责人", Description = "负责人")]
    [MaxLength(20)]
    [DefaultValue("user")]
    [Required]
    public string Owner { get; set; }

    [Display(Name = "所属客户", Description = "所属客户")]
    public int CustomerId { get; set; }
    [Display(Name = "联系人", Description = "联系人")]
    [MaxLength(80)]
    [Required]
    public string ContactName { get; set; }
    [Display(Name = "发现日期", Description = "发现日期")]
    [DefaultValue("now")]
    public DateTime OpDate{ get; set; }
    [Display(Name = "提供人", Description = "提供人")]
    [MaxLength(80)]
    public string ProvidePeople { get; set; }

    [Display(Name = "机会来源", Description = "机会来源")]
    [MaxLength(50)]
    [Required]
    public string Source { get; set; }
    [Display(Name = "市场活动", Description = "市场活动")]
    [MaxLength(128)]
    public string MarketAction { get; set; }
    [Display(Name = "跟进状态", Description = "跟进状态")]
    [MaxLength(50)]
    public string Status { get; set; }
    [Display(Name = "币种", Description = "币种")]
    [MaxLength(50)]
    public string Curr { get; set; }

    [Display(Name = "预计签单日期", Description = "预计签单日期")]
    [DefaultValue(null)]
    public DateTime? PrDate { get; set; }
    [Display(Name = "预计成交金额", Description = "预计成交金额")]
    [DefaultValue(null)]
    public decimal? Amount { get; set; }
    [Display(Name = "商机内容", Description = "商机内容")]
    [MaxLength(512)]
    public string Content { get; set; }
    [Display(Name = "当前阶段", Description = "当前阶段")]
    [MaxLength(128)]
    public string Stage { get; set; }
    [Display(Name = "更新时间", Description = "更新时间")]
    [DefaultValue(null)]
    public DateTime? StageDate { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(128)]
    public string Remark { get; set; }
    [ForeignKey("CustomerId")]
    [Display(Name = "所属客户", Description = "所属客户")]
    public Customer Customer { get; set; }
    [Display(Name = "客户编号", Description = "客户编号")]
    [MaxLength(20)]
    //[Required]
    public string CustomerCode { get; set; }
    [Display(Name = "客户名称", Description = "客户名称")]
    [MaxLength(80)]
    //[Required]
    public string CustomerName { get; set; }


  }
}