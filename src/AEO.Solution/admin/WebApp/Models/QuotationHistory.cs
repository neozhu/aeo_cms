using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //报价历史记录
  public partial class QuotationHistory:Entity
  {
    [Display(Name = "登记状态", Description = "登记状态")]
    [MaxLength(12)]
    public string Status { get; set; }
    
    [Display(Name = "产品编号", Description = "产品编号")]
    [MaxLength(128)]
    public string ProductNo { get; set; }
    [Display(Name = "中文品名", Description = "中文品名")]
    [MaxLength(200)]
    public string ProductName { get; set; }
    [Display(Name = "客户目标价格", Description = "客户目标价格")]
    public decimal TargetPrice { get; set; }
    [Display(Name = "竞争对手价格", Description = "竞争对手价格")]
    public decimal CompetitorPrice { get; set; }
    [Display(Name = "报价总结", Description = "报价总结")]
    [MaxLength(256)]
    public string Summary { get; set; }
    [Display(Name = "登记时间", Description = "登记时间")]
    public DateTime RecordDate { get; set; }
    [Display(Name = "登记人", Description = "登记人")]
    [MaxLength(20)]
    public string Owner { get; set; }
    [Display(Name = "报价商品项", Description = "报价商品项")]
    public int QuotationProductId { get; set; }
    [Display(Name = "报价单", Description = "报价单")]
    public int QuotationId { get; set; }
    [ForeignKey("QuotationId")]
    [Display(Name = "报价单", Description = "报价单")]
    public Quotation Quotation { get; set; }
  }
}