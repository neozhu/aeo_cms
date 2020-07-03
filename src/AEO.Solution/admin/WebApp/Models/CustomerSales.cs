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
  //客户负责业务员关系历史表
  public partial class CustomerSales:Entity
  {
    [Key]
    public int Id { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(20)]
    public string Status { get; set; }
 
    [Display(Name = "业务员", Description = "业务员")]
    [MaxLength(20)]
    public string Salesman { get; set; }
 
    [Display(Name = "所属部门", Description = "所属部门")]
    [MaxLength(20)]
    public string Dept { get; set; }
    [Display(Name = "分配人", Description = "分配人")]
    [MaxLength(20)]
    public string Assigner { get; set; }
    [Display(Name = "分配时间", Description = "分配时间")]
    public DateTime? AssignDate { get; set; }
    [Display(Name = "终止理由", Description = "终止理由")]
    [MaxLength(256)]
    public string StopCase { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(256)]
    public string Remark { get; set; }


    [Display(Name = "客户编号", Description = "客户编号")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("customer.CustomerCode")]
    public string CustomerCode { get; set; }
    [Display(Name = "客户名称", Description = "客户名称")]
    [MaxLength(80)]
    [Required]
    [DefaultValue("customer.CustomerName")]
    public string CustomerName { get; set; }

    [Display(Name = "所属客户", Description = "所属客户")]
    [DefaultValue("customer.Id")]
    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    [Display(Name = "所属客户", Description = "所属客户")]
    public Customer Customer { get; set; }
  }
}