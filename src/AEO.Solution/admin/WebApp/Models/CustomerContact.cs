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
  //客户联系人
  public partial class CustomerContact:Entity
  {
    [Key]
    public int Id { get; set; }
    [Display(Name = "姓名", Description = "姓名")]
    [MaxLength(80)]
    [Required]
    public string Name { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(20)]
    [DefaultValue("启用")]
    public string Status { get; set; }
    [Display(Name = "英文名", Description = "英文名")]
    [MaxLength(80)]
    [Required]
    public string EName { get; set; }
    [Display(Name = "性别", Description = "性别")]
    [MaxLength(10)]
    [DefaultValue("男")]
    public string Sex { get; set; }
    [Display(Name = "部门", Description = "部门")]
    [MaxLength(80)]
    [Required]
    public string Dept { get; set; }
    [Display(Name = "职务", Description = "职务")]
    [MaxLength(80)]
    [Required]
    public string Duty { get; set; }

    [Display(Name = "手机号", Description = "手机号")]
    [MaxLength(50)]
    public string MobilePhone { get; set; }
    [Display(Name = "电话1", Description = "电话1")]
    [MaxLength(50)]
    public string PhoneNumber1 { get; set; }
    [Display(Name = "电话2", Description = "电话2")]
    [MaxLength(50)]
    public string PhoneNumber2 { get; set; }
    [Display(Name = "固话", Description = "固话")]
    [MaxLength(50)]
    public string PhoneNumber3 { get; set; }
    [Display(Name = "传真", Description = "传真")]
    [MaxLength(50)]
    public string Fax { get; set; }
    [Display(Name = "邮箱", Description = "邮箱")]
    [MaxLength(80)]
    [Required]
    public string Email { get; set; }


 
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(20)]
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