using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //自测题库模板
  public partial class AeoQuestion:Entity
  {
    [Display(Name = "模板名", Description = "模板名")]
    [MaxLength(128)]
    [Required]
    public string Tpl { get; set; }

    [Display(Name = "AEO认证类别", Description = "AEO认证类别")]
    [MaxLength(128)]
    public string AuthType { get; set; }
    [Display(Name = "类别", Description = "类别")]
    [MaxLength(128)]
    [Required]
    public string Category { get; set; }
    [Display(Name = "说明", Description = "说明")]
    [MaxLength(128)]
    public string Description { get; set; }
    [Display(Name = "代码", Description = "代码")]
    [MaxLength(12)]
    public string Code { get; set; }
    [Display(Name = "项目", Description = "项目")]
    [MaxLength(128)]
    public string Title { get; set; }
    [Display(Name = "简称", Description = "简称")]
    [MaxLength(128)]
    public string Short { get; set; }
    [Display(Name = "标准说明", Description = "标准说明")]
    [MaxLength(256)]
    public string StdDescription { get; set; }
    [Display(Name = "注意", Description = "注意")]
    [MaxLength(128)]
    public string Notes { get; set; }
    [Display(Name = "分数", Description = "分数")]
    public int StdScore { get; set; }
    [Display(Name = "测试分数", Description = "测试分数")]
    public int Score { get; set; }
    [Display(Name = "评分说明", Description = "评分说明")]
    [MaxLength(256)]
    public string ScoreDescription { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(128)]
    public string Remark { get; set; }
    [Display(Name = "测试人", Description = "测试人")]
    [MaxLength(28)]
    public string Tester { get; set; }
    [Display(Name = "测试时间", Description = "测试时间")]
    public DateTime? TestDateTime { get; set; }
    [Display(Name = "测试编号", Description = "测试编号")]
    [MaxLength(20)]
    public string TestNo { get; set; }
    [Display(Name = "AEO自认证测评", Description = "AEO自认证测评")]
    public int AeoAuthTestId { get; set; }
    [Display(Name = "AEO自认证测评", Description = "AEO自认证测评")]
    [ForeignKey("AeoAuthTestId")]
    public virtual AeoAuthTest AeoAuthTest { get; set; }
  }
}