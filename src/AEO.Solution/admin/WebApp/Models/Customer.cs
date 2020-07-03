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
  //客户信息主档
  public partial class Customer:Entity
  {

    public Customer()
    {
      this.CustomerAttentionProducts = new HashSet<CustomerAttentionProduct>();
      this.CustomerBanks = new HashSet<CustomerBank>();
      this.CustomerContacts = new HashSet<CustomerContact>();
      this.CustomerFiles = new HashSet<CustomerFile>();
      this.CustomerFollows = new HashSet<CustomerFollow>();
      this.CustomerInvoices = new HashSet<CustomerInvoice>();
      this.CustomerSales = new HashSet<CustomerSales>();
      this.CustomerShares = new HashSet<CustomerShare>();
      this.CustomerWarehouses = new HashSet<CustomerWarehouse>();
      this.CustomerCommunications = new HashSet<CustomerCommunication>();
    }
    [Key]
    public int Id { get; set; }
    #region 基本信息
    [Display(Name = "客户编号", Description = "客户编号(保存时系统自动分配也可以手工选择)")]
    [MaxLength(20)]
    [Required]
    [Index(IsUnique = true)]
    public string CustomerCode { get; set; }
    [Display(Name = "客户名称", Description = "客户名称")]
    [MaxLength(80)]
    [Required]
    [Index(IsUnique =true)]
    public string CustomerName { get; set; }
    [Display(Name = "客户英文名称", Description = "客户英文名称")]
    [MaxLength(80)]
    public string CustomerEName { get; set; }

    [Display(Name = "客户类型", Description = "客户类型")]
    [MaxLength(20)]
    public string CustomerType { get; set; }

    [Display(Name = "是否境外企业", Description = "是否境外企业")]
    public bool Overseas { get; set; }
 

    [Display(Name = "客户类别", Description = "客户类别")]
    [MaxLength(20)]
    public string CustomerType3 { get; set; }
   
    [Display(Name = "注册资本(万)", Description = "注册资本(万)")]
    public decimal? Capital { get; set; }
    [Display(Name = "币制", Description = "币制")]
    [MaxLength(20)]
    public string CURR { get; set; }
    [Display(Name = "税务资质", Description = "税务资质")]
    [MaxLength(20)]
    public string TaxProperty { get; set; }
    [Display(Name = "上级组织", Description = "上级组织")]
    [MaxLength(80)]
    public string ParentOrg { get; set; }
    [Display(Name = "注册海关", Description = "注册海关")]
    [MaxLength(128)]
    public string CustomMaster { get; set; }
    [Display(Name = "海关十位编码", Description = "海关十位编码")]
    [MaxLength(128)]
    public string TradeCode { get; set; }
   
    [Display(Name = "国家地区", Description = "国家地区")]
    [MaxLength(50)]
    [DefaultValue("中国")]
    public string Country { get; set; }
    [Display(Name = "所在区域", Description = "特殊区域")]
    [MaxLength(150)]
    public string Zone { get; set; }
    #endregion
    #region CRM信息
    [Display(Name = "客户规模", Description = "客户规模")]
    [MaxLength(20)]
    public string Scale { get; set; }
    [Display(Name = "客户等级", Description = "客户等级")]
    [MaxLength(20)]
    public string Level { get; set; }
    [Display(Name = "客户价值", Description = "客户价值")]
    [MaxLength(256)]
    public string Value { get; set; }
  
    [Display(Name = "客户资信等级", Description = "客户资信等级")]
    [MaxLength(20)]
    public string CreditRating { get; set; }

    [Display(Name = "客户来源", Description = "客户来源")]
    [MaxLength(50)]
    public string Source { get; set; }
    [Display(Name = "所属行业", Description = "所属行业")]
    [MaxLength(128)]
    public string Industry { get; set; }
    [Display(Name = "资金额度(万)", Description = "资金额度(万)")]
    public decimal? Cash { get; set; }

    [Display(Name = "币制", Description = "币制")]
    [MaxLength(20)]
    public string CashCURR { get; set; }
    [Display(Name = "优惠说明", Description = "优惠说明")]
    public string SDesc { get; set; }
    [Display(Name = "备注", Description = "备注")]
    public string Remark { get; set; }
    #endregion
    #region 地址信息

   
 
    #region 国内工商地址
    [Display(Name = "省", Description = "省")]
    [MaxLength(50)]
    public string CProvinces1 { get; set; }
    [Display(Name = "市", Description = "市")]
    [MaxLength(50)]
    public string CCity1 { get; set; }
    [Display(Name = "县", Description = "县")]
    [MaxLength(50)]
    public string CCounty1 { get; set; }
    [Display(Name = "详细地址", Description = "详细地址")]
    [MaxLength(256)]
    public string CAddress1 { get; set; }

    #endregion
    #region 国内通讯地址
    [Display(Name = "省", Description = "省")]
    [MaxLength(50)]
    public string CProvinces2 { get; set; }
    [Display(Name = "市", Description = "市")]
    [MaxLength(50)]
    public string CCity2 { get; set; }
    [Display(Name = "县", Description = "县")]
    [MaxLength(50)]
    public string CCounty2 { get; set; }
    [Display(Name = "详细地址", Description = "详细地址")]
    [MaxLength(256)]
    public string CAddress2 { get; set; }

    #endregion
    [Display(Name = "英文地址", Description = "英文地址")]
    [MaxLength(256)]
    public string EAddress1 { get; set; }
    [Display(Name = "国际地址", Description = "国际地址")]
    [MaxLength(256)]
    public string EAddress2 { get; set; }
    [Display(Name = "邮编", Description = "邮编")]
    [MaxLength(10)]
    public string PostCode { get; set; }
    [Display(Name = "公司网站", Description = "公司网站")]
    [MaxLength(256)]
    public string WebSite { get; set; }

    #endregion
    #region 公司经营范围
    [Display(Name = "公司经营范围", Description = "公司经营范围")]
    public string BusinessScope { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(256)]
    public string Remark1 { get; set; }
    #endregion
    #region 其它
    [Display(Name = "客户状态", Description = "客户状态")]
    [MaxLength(20)]
    public string Status { get; set; }
    [Display(Name = "客户状态1", Description = "客户状态1")]
    [MaxLength(20)]
    public string Status1 { get; set; }
    [Display(Name = "客户状态1", Description = "客户状态1")]
    [MaxLength(20)]
    public string Status2 { get; set; }
    [Display(Name = "客户状态3", Description = "客户状态3")]
    [MaxLength(20)]
    public string Status3 { get; set; }
    [Display(Name = "客户状态4", Description = "客户状态4")]
    [MaxLength(20)]
    public string Status4 { get; set; }
    [Display(Name = "客户状态5", Description = "客户状态5")]
    [MaxLength(20)]
    public string Status5 { get; set; }
    [Display(Name = "客户图片", Description = "客户图片")]
    public string Logo { get; set; }
    #endregion
    #region 归属
    [Display(Name = "归属企业代码", Description = "归属企业代码")]
    [MaxLength(10)]
    public string CompanyCode { get; set; }
    [Display(Name = "归属企业名称", Description = "归属企业名称")]
    [MaxLength(128)]
    public string CompanyName { get; set; }

    #endregion

    //关联明细
    //客户关注产品
    public virtual ICollection<CustomerAttentionProduct> CustomerAttentionProducts { get; set; }
    //客户银行
    public virtual ICollection<CustomerBank> CustomerBanks { get; set; }
    //客户联系人
    public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
    //客户文件
    public virtual ICollection<CustomerFile> CustomerFiles { get; set; }
    //客户跟进情况
    public virtual ICollection<CustomerFollow> CustomerFollows { get; set; }
    //客户开票信息
    public virtual ICollection<CustomerInvoice> CustomerInvoices { get; set; }
    //客户负责业务员关系历史表
    public virtual ICollection<CustomerSales> CustomerSales { get; set; }
    //客户共享记录
    public virtual ICollection<CustomerShare> CustomerShares { get; set; }
    //客户仓库
    public virtual ICollection<CustomerWarehouse> CustomerWarehouses { get; set; }
    //客户沟通记录
    public virtual ICollection<CustomerCommunication> CustomerCommunications { get; set; }
  }
}