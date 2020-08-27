using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //报价单其它收费明细
  public partial class QuotationCharge:Entity
  {
    [Display(Name = "费用名称(中文)", Description = "费用名称(中文)")]
    [MaxLength(128)]
    public string Name { get; set; }
    [Display(Name = "费用名称(英文)", Description = "费用名称(英文)")]
    [MaxLength(128)]
    public string EName { get; set; }
    [Display(Name = "金额", Description = "金额")]
    public decimal Amount { get; set; }

    [Display(Name = "报价单", Description = "报价单")]
    public int QuotationId { get; set; }
    [ForeignKey("QuotationId")]
    [Display(Name = "报价单", Description = "报价单")]
    public Quotation Quotation { get; set; }
  }
}