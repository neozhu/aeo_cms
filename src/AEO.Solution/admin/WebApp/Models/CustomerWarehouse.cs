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
  //客户仓库
  public partial class CustomerWarehouse:Entity
  {
    [Key]
    public int Id { get; set; }
    #region 基本信息
    [Display(Name = "仓库代码", Description = "仓库代码")]
    [MaxLength(20)]
    public string WarehouseCode { get; set; }
    [Display(Name = "仓库名称", Description = "仓库名称")]
    [MaxLength(128)]
    public string WarehouseName { get; set; }
    [Display(Name = "仓库类型", Description = "仓库类型")]
    [MaxLength(128)]
    public string WarehouseType { get; set; }
    [Display(Name = "厂区门禁管理", Description = "厂区门禁管理")]
    public bool FactoryGuard { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(256)]
    public string Remark { get; set; }
    #endregion
    #region 仓库地址
    [Display(Name = "省", Description = "省")]
    [MaxLength(50)]
    public string Provinces { get; set; }
    [Display(Name = "市", Description = "市")]
    [MaxLength(50)]
    public string City { get; set; }
    [Display(Name = "县", Description = "县")]
    [MaxLength(50)]
    public string County { get; set; }
    [Display(Name = "仓库地址", Description = "仓库地址")]
    [MaxLength(256)]
    public string WAddress { get; set; }
    [Display(Name = "英文地址", Description = "英文地址")]
    [MaxLength(256)]
    public string EAddress1 { get; set; }
   
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(256)]
    public string Remark1 { get; set; }

    #endregion
    #region 联系人信息

    [Display(Name = "仓库负责人", Description = "仓库负责人")]
    [MaxLength(20)]
    public string WUser { get; set; }
    [Display(Name = "部门", Description = "部门")]
    [MaxLength(50)]
    public string WDept { get; set; }
    [Display(Name = "职位", Description = "职位")]
    [MaxLength(50)]
    public string WTitle { get; set; }
    [Display(Name = "性别", Description = "性别")]
    [MaxLength(10)]
    public string WSex { get; set; }
  
    [Display(Name = "固话", Description = "固话")]
    [MaxLength(256)]
    public string WPhone { get; set; }
    [Display(Name = "传真", Description = "传真")]
    [MaxLength(256)]
    public string WFax { get; set; }
    [Display(Name = "手机1", Description = "手机1")]
    [MaxLength(256)]
    public string WMPhone1 { get; set; }
    [Display(Name = "手机2", Description = "手机2")]
    [MaxLength(256)]
    public string WMPhone2 { get; set; }
    
    [Display(Name = "电子邮件", Description = "电子邮件")]
    [MaxLength(256)]
    public string WEmail1 { get; set; }



    #endregion
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