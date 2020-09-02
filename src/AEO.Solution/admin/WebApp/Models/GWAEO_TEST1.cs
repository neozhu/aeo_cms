using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
  public class GWAEO_TEST
  {
    [Display(Name = "主键", Description = "主键")]
    [MaxLength(128)]
    public string RID { get; set; }
    [Display(Name = "所属企业", Description = "所属企业")]
    [MaxLength(128)]
    public string CompanyCode { get; set; }
    [Display(Name = "所属企业", Description = "所属企业")]
    [MaxLength(128)]
    public string CompanyName { get; set; }
    [Display(Name = "社会信用代码", Description = "社会信用代码")]
    [MaxLength(50)]
    public string CreditCode { get; set; }
    [Display(Name = "海关编码", Description = "海关编码")]
    [MaxLength(10)]
    public string TradeCode { get; set; }
    [Display(Name = "企业类型", Description = "企业类型")]
    [MaxLength(50)]
    public string CompanyType { get; set; }
    [Display(Name = "AEO认证", Description = "AEO认证")]
    [MaxLength(50)]
    public string Level { get; set; }
    [Display(Name = "主管海关", Description = "主管海关")]
    [MaxLength(50)]
    public string CustomCode { get; set; }
    [Display(Name = "海关注册日期", Description = "海关注册日期")]
    public DateTime? RegDate { get; set; }
    [Display(Name = "是否境外", Description = "是否境外")]
    [MaxLength(10)]
    public string Abroad { get; set; }
    [Display(Name = "特殊区域", Description = "特殊区域")]
    [MaxLength(50)]
    public string Zone { get; set; }
    [Display(Name = "注册时长", Description = "注册时长")]
    public decimal? RegTime { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    public string TimeUnit { get; set; }
    [Display(Name = "自评日期", Description = "自评日期")]
    public DateTime? TestDate { get; set; }
    [Display(Name = "年份", Description = "年份")]
    [MaxLength(20)]
    public string Year { get; set; }
    [Display(Name = "评定开始日期", Description = "评定开始日期")]
    public DateTime? Dt1 { get; set; }
    [Display(Name = "评定结束日期", Description = "评定结束日期")]
    public DateTime? Dt2 { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(512)]
    public string Remark { get; set; }
    [Display(Name = "合格分数", Description = "合格分数")]
    public decimal StdScore { get; set; }
    [Display(Name = "测试总分", Description = "测试总分")]
    public decimal TestScore { get; set; }
    [Display(Name = "测试结果", Description = "测试结果")]
    [MaxLength(50)]
    public string Result { get; set; }
    [Display(Name = "状态1", Description = "状态1")]
    [MaxLength(50)]
    public string Status{ get; set; }
    [Display(Name = "状态1", Description = "状态1")]
    [MaxLength(50)]
    public string Status1 { get; set; }
    [Display(Name = "状态2", Description = "状态2")]
    [MaxLength(50)]
    public string Status2 { get; set; }
    [Display(Name = "状态3", Description = "状态3")]
    [MaxLength(50)]
    public string Status3 { get; set; }
    [Display(Name = "状态4", Description = "状态4")]
    [MaxLength(50)]
    public string Status4 { get; set; }
  }

  public class GWAEO_TEST_DETAIL {
    [Display(Name = "关联主键", Description = "关联主键")]
    [MaxLength(128)]
    public string RID { get; set; }
    [Display(Name = "分类", Description = "分类")]
    [MaxLength(128)]
    public string Category { get; set; }
    [Display(Name = "分组", Description = "分组")]
    [MaxLength(128)]
    public string Group { get; set; }
    [Display(Name = "序号", Description = "序号")]
    public int No { get; set; }
    [Display(Name = "标题", Description = "标题")]
    [MaxLength(256)]
    public string Title { get; set; }
  }
}