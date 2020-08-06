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
 
    #endregion
    #region 仓库地址

    [Display(Name = "仓库地址", Description = "仓库地址")]
    [MaxLength(256)]
    public string WAddress { get; set; }
    


    #endregion
    #region 联系人信息

    [Display(Name = "仓库负责人", Description = "仓库负责人")]
    [MaxLength(20)]
    public string WUser { get; set; }
  

  
    [Display(Name = "联系人电话", Description = "联系人电话")]
    [MaxLength(256)]
    public string WMPhone1 { get; set; }
    [Display(Name = "仓库电话", Description = "仓库电话")]
    [MaxLength(256)]
    public string WMPhone2 { get; set; }
    
    [Display(Name = "电子邮件", Description = "电子邮件")]
    [MaxLength(256)]
    public string WEmail1 { get; set; }
    [Display(Name = "传真", Description = "传真")]
    [MaxLength(256)]
    public string WFax { get; set; }


    #endregion
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(256)]
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