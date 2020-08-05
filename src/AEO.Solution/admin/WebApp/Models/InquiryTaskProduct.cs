using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //询价任务产品明细
  public partial class InquiryTaskProduct:Entity
  {

    [Display(Name = "产品编号", Description = "产品编号(自动生成,可手工修改)")]
    [MaxLength(50)]
    public string ProductNo { get; set; }
    [Display(Name = "中文品名", Description = "中文品名")]
    [MaxLength(200)]
    public string ProductName { get; set; }
    [Display(Name = "产品类别", Description = "产品类别")]
    [MaxLength(50)]
    public string CategoryName { get; set; }
    [Display(Name = "英文品名", Description = "英文品名")]
    [MaxLength(200)]
    public string ProductEnName { get; set; }
    [Display(Name = "中文描述", Description = "中文描述")]
    public string CnDescription { get; set; }
    [Display(Name = "英文描述", Description = "英文描述")]
    public string EnDescription { get; set; }
    [Display(Name = "客户货号", Description = "客户货号")]
    [MaxLength(128)]
    public string ThirdProductNo { get; set; }
    [Display(Name = "询价数量", Description = "询价数量")]
    public decimal Qty { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    public string Unit { get; set; }
    [Display(Name = "执行人", Description = "执行人")]
    [MaxLength(20)]
    public string Executor { get; set; }
    [Display(Name = "供应商", Description = "供应商")]
    [MaxLength(20)]
    public string SupplierCode { get; set; }
    [Display(Name = "供应商名称", Description = "供应商名称")]
    [MaxLength(128)]
    public string SupplierName { get; set; }

    [Display(Name = "产品图片", Description = "产品备注")]
    public string Logo { get; set; }

    [Display(Name = "任务单号", Description = "任务单号")]
    [MaxLength(20)]
    [Required]
    public string TaskNo { get; set; }
    [Display(Name = "询价任务", Description = "询价任务")]
    public int InquiryTaskId { get; set; }
    [Display(Name = "询价任务", Description = "询价任务")]
    [ForeignKey("InquiryTaskId")]
    public InquiryTask InquiryTask { get; set; }
  }
}