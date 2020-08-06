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
  //客户银行账户
  public partial class CustomerBank:Entity
  {
    
    [Display(Name = "开户名称", Description = "开户名称")]
    [MaxLength(100)]
    [Required]
    public string AccountName { get; set; }
    [Display(Name = "银行名称", Description = "银行名称")]
    [MaxLength(100)]

    public string Bank { get; set; }
    [Display(Name = "银行账号", Description = "银行账号")]
    [MaxLength(100)]
    [Required]
    public string AccountNo { get; set; }
    
    [Display(Name = "账户用途", Description = "账户用途")]
    [MaxLength(100)]
    public string BankUse { get; set; }
    [Display(Name = "SWIFT号", Description = "SWIFT号")]
    [MaxLength(50)]
    public string SWIFT { get; set; }
    [Display(Name = "币值", Description = "币值")]
    [MaxLength(10)]
    public string CUR { get; set; }
    [Display(Name = "备注说明", Description = "备注说明")]
    [MaxLength(512)]
    public string Remark { get; set; }

  




    [Display(Name = "所属客户", Description = "所属客户")]
    [DefaultValue("customer.Id")]
    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    [Display(Name = "所属客户", Description = "所属客户")]
    public Customer Customer { get; set; }
    [Display(Name = "客户编号", Description = "客户编号(保存时系统自动分配也可以手工选择)")]
    [MaxLength(20)]
    [DefaultValue("customer.CustomerCode")]
    public string CustomerCode { get; set; }
    [Display(Name = "单位名称", Description = "单位名称")]
    [MaxLength(80)]
    //[Required]
    [DefaultValue("customer.CustomerName")]
    public string CustomerName { get; set; }
  }
}