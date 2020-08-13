using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //市场活动
  public partial class MarketAct:Entity
  {

    [Display(Name = "市场活动名称", Description = "市场活动名称")]
    [MaxLength(128)]
    [Required]
    public string Name { get; set; }
    [Display(Name = "负责人", Description = "负责人")]
    [MaxLength(20)]
    [Required]
    public string Owner { get; set; }
    [Display(Name = "活动状态", Description = "活动状态")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("计划")]
    public string Status { get; set; }
    [Display(Name = "活动类型", Description = "活动类型")]
    [MaxLength(56)]
    [Required]
    public string ActType { get; set; }
    [Display(Name = "计划开始日期", Description = "计划开始日期")]
    [DefaultValue(null)]
    public DateTime? PlanStartDate { get; set; }
    [Display(Name = "计划完成日期", Description = "计划完成日期")]
    [DefaultValue(null)]
    public DateTime? PlanFinishDate { get; set; }
    [Display(Name = "费用预算", Description = "费用预算")]
    [DefaultValue(null)]
    public decimal? BudgetExpense { get; set; }
    [Display(Name = "币种", Description = "币种")]
    [DefaultValue(null)]
    [MaxLength(56)]
    public string Cur { get; set; }
    [Display(Name = "活动地点", Description = "活动地点")]
    [MaxLength(128)]
    [DefaultValue(null)]
    public string Address { get; set; }
    [Display(Name = "活动计划", Description = "活动计划")]
    [DefaultValue(null)]
    [MaxLength(512)]
    public string PlanDesc { get; set; }
    [Display(Name = "实际开始日期", Description = "实际开始日期")]
    [DefaultValue(null)]
    public DateTime? ActualStartDate { get; set; }
    [Display(Name = "实际完成日期", Description = "实际完成日期")]
    [DefaultValue(null)]
    public DateTime? ActualFinishDate { get; set; }
    [Display(Name = "实际投入", Description = "实际投入")]
    [DefaultValue(null)]
    public decimal? ActExpense { get; set; }
    [Display(Name = "预计收入", Description = "预计收入")]
    [DefaultValue(null)]
    public decimal? Income { get; set; }
    [Display(Name = "执行情况", Description = "执行情况")]
    [DefaultValue(null)]
    [MaxLength(512)]
    public string ExecDesc { get; set; }
    [Display(Name = "活动总结", Description = "活动总结")]
    [DefaultValue(null)]
    [MaxLength(512)]
    public string SumaryDesc { get; set; }
    [Display(Name = "评估效果", Description = "评估效果")]
    [DefaultValue(null)]
    [MaxLength(512)]
    public string EffectDesc { get; set; }
    

  }

}