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
    [Display(Name = "姓名", Description = "姓名")]
    [MaxLength(80)]
    [Required]
    public string Name { get; set; }
    [Display(Name = "称谓", Description = "称谓")]
    [MaxLength(10)]
    [DefaultValue("先生")]
    public string Appellation { get; set; }
    [Display(Name = "性别", Description = "性别")]
    [MaxLength(10)]
    [DefaultValue("男")]
    public string Sex { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(20)]
    [DefaultValue("启用")]
    public string Status { get; set; }
    [Display(Name = "负责人", Description = "负责人")]
    [MaxLength(20)]
    public string Owner { get; set; }
    [Display(Name = "职务", Description = "职务")]
    [MaxLength(80)]
    [Required]
    public string Job { get; set; }
    [Display(Name = "微信", Description = "微信")]
    [MaxLength(50)]
    public string Wx { get; set; }

    [Display(Name = "手机号", Description = "手机号")]
    [MaxLength(50)]
    public string MobilePhone { get; set; }
    [Display(Name = "固话", Description = "固话")]
    [MaxLength(50)]
    public string PhoneNumber { get; set; }
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