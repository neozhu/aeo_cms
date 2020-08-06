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
  //客户共享记录
  public partial class CustomerShare : Entity
  {
    [Display(Name = "共享人", Description = "共享人")]
    [MaxLength(20)]
    [Required]
    public string  Owner{get;set;}
    [Display(Name = "部门", Description = "部门")]
    [MaxLength(20)]
    public string Dept { get; set; }
    [Display(Name = "人员", Description = "人员")]
    [MaxLength(20)]
    [Required]
    public string ShareTo { get; set; }
    [Display(Name = "模块", Description = "模块")]
    [MaxLength(50)]
    public string Module { get; set; }
    [Display(Name = "查询", Description = "查询")]
    [DefaultValue(true)]
    public bool Searchable { get; set; }
    [Display(Name = "编辑", Description = "编辑")]
    [DefaultValue(false)]
    public bool Editable { get; set; }


  

    [Display(Name = "所属客户", Description = "所属客户")]
    [DefaultValue("customer.Id")]
    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    [Display(Name = "所属客户", Description = "所属客户")]
    public Customer Customer { get; set; }
    [Display(Name = "客户编号", Description = "客户编号")]
    [MaxLength(20)]
    //[Required]
    [DefaultValue("customer.CustomerCode")]
    public string CustomerCode { get; set; }
    [Display(Name = "客户名称", Description = "客户名称")]
    [MaxLength(80)]
    //[Required]
    [DefaultValue("customer.CustomerName")]
    public string CustomerName { get; set; }
  }
}