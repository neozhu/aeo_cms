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
  //客户沟通记录
  public partial class CustomerCommunication : Entity
  {
    [Key]
    public int Id { get; set; }
    [Display(Name = "会议主题", Description = "会议主题")]
    [MaxLength(128)]
    [Required]
    public string Title { get; set; }
    [Display(Name = "沟通类型", Description = "沟通类型")]
    [MaxLength(20)]
    [DefaultValue("")]
    [Required]
    public string CommType { get; set; }
    
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(20)]
    [DefaultValue("")]
    public string Status { get; set; }
    [Display(Name = "业务员", Description = "业务员")]
    [MaxLength(20)]
    [DefaultValue("user")]
    public string Salesman { get; set; }

    [Display(Name = "参与人", Description = "参与人")]
    [MaxLength(128)]
    public string RefUsers { get; set; }
    [Display(Name = "开始日期", Description = "开始日期")]
    [DefaultValue(null)]
    public DateTime? BeginDate { get; set; }
    [Display(Name = "结束日期", Description = "结束日期")]
    [DefaultValue(null)]
    public DateTime? EndDate { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(20)]
    public string Remark { get; set; }
   
     

    [Display(Name = "客户编号", Description = "客户编号")]
    [MaxLength(20)]
    [Required]
    public string CustomerCode { get; set; }
    [Display(Name = "客户名称", Description = "客户名称")]
    [MaxLength(80)]
    [Required]
    public string CustomerName { get; set; }

    [Display(Name = "所属客户", Description = "所属客户")]
    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    [Display(Name = "所属客户", Description = "所属客户")]
    public Customer Customer { get; set; }
  }
}