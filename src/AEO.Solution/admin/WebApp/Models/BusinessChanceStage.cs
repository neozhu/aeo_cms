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
  public partial class BusinessChanceStage : Entity
  {

    [Display(Name = "商机阶段", Description = "商机阶段")]
    [MaxLength(20)]
    public string Stage { get; set; }
    [Display(Name = "成功率", Description = "成功率")]
    [DefaultValue(0.4)]
    public decimal Rate { get; set; }
    [Display(Name = "确认时间", Description = "确认时间")]
    public DateTime ConfirmDate { get; set; }
    [Display(Name = "负责人", Description = "负责人")]
    [MaxLength(20)]
    public string Owner { get; set; }
    [Display(Name = "备注", Description = "备注")]
    public string Remark { get; set; }
    [Display(Name = "商机", Description = "商机")]
    public int BusinessChanceId { get; set; }
    [ForeignKey("BusinessChanceId")]
    public BusinessChance BusinessChance { get; set; }
  }
}