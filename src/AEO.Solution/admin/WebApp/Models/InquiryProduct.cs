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
  //询价单产品明细
  public partial class InquiryProduct:Entity
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
    [Display(Name = "厂商代码", Description = "厂商代码")]
    [MaxLength(20)]
    public string SupplierCode { get; set; }
    [Display(Name = "厂商名称", Description = "厂商名称")]
    [MaxLength(128)]
    public string SupplierName { get; set; }

    [Display(Name = "厂商货号", Description = "厂商货号")]
    [MaxLength(128)]
    public string SupplierProductNo { get; set; }

    [Display(Name = "价格类型", Description = "价格类型")]
    [MaxLength(30)]
    public string PriceType { get; set; }
    [Display(Name = "报价金额", Description = "报价金额")]
    [DefaultValue(null)]
    public decimal? Price { get; set; }
    [Display(Name = "币种", Description = "币种")]
    [MaxLength(20)]
    [DefaultValue(null)]
    public string Cur { get; set; }
    [Display(Name = "最小订单量", Description = "最小订单量")]
    [DefaultValue(null)]
    public decimal? MinQty { get; set; }
    [Display(Name = "厂价有效期", Description = "厂价有效期")]
    [DefaultValue(null)]
    public DateTime? PriceDate { get; set; }
    [Display(Name = "反馈说明", Description = "反馈说明")]
    public string Feedback { get; set; }
    [Display(Name = "推荐采纳", Description = "推荐采纳")]
    [DefaultValue(false)]
    public bool Recommended { get; set; }

    [Display(Name = "产品图片", Description = "产品备注")]
    public string SamplePic { get; set; }
    [Display(Name = "询价单号", Description = "询价单号")]
    [MaxLength(20)]
    //[Required]
    public string InquiryNo { get; set; }
    [Display(Name = "任务单号", Description = "任务单号")]
    [MaxLength(20)]
    //[Required]
    public string TaskNo { get; set; }

  

    [Display(Name = "系统版本号", Description = "系统版本号")]
    public int Ver { get; set; }
    [Display(Name = "询价单", Description = "询价单")]
    public int InquiryId { get; set; }
    [ForeignKey("InquiryId")]
    [Display(Name = "询价单", Description = "询价单")]
    public Inquiry Inquiry { get; set; }
  }
}