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
  //商机阶段
  public partial class OpportunityStage:Entity
  {
    [Display(Name = "阶段", Description = "阶段")]
    [MaxLength(128)]
    public string Stage { get; set; }
    [Display(Name = "成功率", Description = "成功率")]
    public decimal SuccessRate { get; set; }
    [Display(Name = "成功率", Description = "成功率")]
    [MaxLength(20)]
    public string Success { get; set; }
    [Display(Name = "确认时间", Description = "确认时间")]
    [DefaultValue(null)]
    public DateTime ConfirmDate { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(128)]
    public string Remark { get; set; }
    [Display(Name = "商机", Description = "商机")]
    public int BusinessOpportunityId { get; set; }
    
    [Display(Name = "商机", Description = "商机")]
    [ForeignKey("BusinessOpportunityId")]
    public BusinessOpportunity BusinessOpportunity { get; set; }
  }
}