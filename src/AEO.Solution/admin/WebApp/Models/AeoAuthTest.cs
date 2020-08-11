using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //AEO自认证测评记录
  public partial class AeoAuthTest:Entity
  {
    [Display(Name = "企业名称", Description = "企业名称")]
    [MaxLength(128)]
    //[Required]
    public string Name { get; set; }
    [Display(Name = "企业十位编码", Description = "企业十位编码")]
    [MaxLength(10)]
    public string TradeCode { get; set; }
    
    [Display(Name = "统一社会信用代码", Description = "统一社会信用代码")]
    [MaxLength(18)]
    //[Required]
    public string CreditCode { get; set; }

    [Display(Name = "企业类型", Description = "企业类型")]
    [MaxLength(128)]
    public string Ctype { get; set; }
    [Display(Name = "测试编号", Description = "测试编号")]
    [MaxLength(20)]
    [Required]
    public string TestNo { get; set; }
    [Display(Name = "AEO认证类别", Description = "AEO认证类别")]
    [MaxLength(128)]
    public string AuthType { get; set; }
    [Display(Name = "主管海关代码", Description = "主管海关代码")]
    [MaxLength(10)]
    public string MasterCustom { get; set; }
    [Display(Name = "海关注册日期", Description = "海关注册日期")]
    public DateTime? RegistDate { get; set; }
    [Display(Name = "是否境外", Description = "是否境外")]
    [MaxLength(50)]
    [DefaultValue("中国境内")]
    public string IsForeign { get; set; }
    [Display(Name = "特殊区域", Description = "特殊区域")]
    [MaxLength(128)]
    [DefaultValue("")]
    public string Zone { get; set; }
    [Display(Name = "海关注册时长", Description = "海关注册时长")]
    public decimal? RegistedTime { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    [DefaultValue("年")]
    public string Unit { get; set; }
    [Display(Name = "自评日期", Description = "自评日期")]
    [DefaultValue("now")]
    public DateTime? AuthDate { get; set; }
    [Display(Name = "测试人", Description = "测试人")]
    [MaxLength(28)]
    public string Tester { get; set; }
    [Display(Name = "年份", Description = "年份")]
    public int? Year { get; set; }

    [Display(Name = "评定开始日期", Description = "评定开始日期")]
    [DefaultValue(null)]
    public DateTime? BeginDate { get; set; }
    [Display(Name = "评定结束日期", Description = "评定结束日期")]
    [DefaultValue(null)]
    public DateTime? EndDate { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(512)]
    public string Remark { get; set; }

    #region 结果
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(12)]
    public string Status { get; set; }
    [Display(Name = "合格分数", Description = "合格分数")]
    public decimal? StdScore { get; set; }
    [Display(Name = "分数", Description = "分数")]
    public decimal? Score { get; set; }
    [Display(Name = "结果", Description = "结果")]
    [MaxLength(128)]
    public string Result { get; set; }
    #endregion

    public AeoAuthTest()
    {
      this.Aeoquestions = new HashSet<AeoQuestion>();
    }
    public virtual ICollection<AeoQuestion> Aeoquestions { get; set; }
  }
}