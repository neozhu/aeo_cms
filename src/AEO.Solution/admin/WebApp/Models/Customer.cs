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
      //this.CustomerInvoices = new HashSet<CustomerInvoice>();
      this.CustomerSales = new HashSet<CustomerSales>();
      this.CustomerShares = new HashSet<CustomerShare>();
      this.CustomerWarehouses = new HashSet<CustomerWarehouse>();
    }

    #region 客户信息
    [Display(Name = "客户编号", Description = "客户编号(保存时系统自动分配也可以手工选择)")]
    [MaxLength(32)]
    //[Required]
    [Index(IsUnique = true)]
    public string CustomerCode { get; set; }
    [Display(Name = "客户简称", Description = "客户简称")]
    [MaxLength(128)]
    public string BaseName { get; set; }
    [Display(Name = "客户名称", Description = "客户名称")]
    [MaxLength(128)]
    [Required]
    [Index(IsUnique = true)]
    public string CustomerName { get; set; }
    [Display(Name = "客户类型", Description = "客户类型")]
    [MaxLength(20)]
    public string CustomerType { get; set; }
    [Display(Name = "国家地区", Description = "国家地区")]
    [MaxLength(128)]
    [DefaultValue("中国")]
    public string Country { get; set; }
    [Display(Name = "客户等级", Description = "客户等级")]
    [MaxLength(20)]
    public string Level { get; set; }

    [Display(Name = "客户来源", Description = "客户来源")]
    [MaxLength(50)]
    public string Source { get; set; }
    [Display(Name = "公司电话", Description = "公司电话")]
    [MaxLength(50)]
    public string Telephone { get; set; }

    [Display(Name = "公司传真", Description = "公司传真")]
    [MaxLength(50)]
    public string Fax { get; set; }
    [Display(Name = "负责人", Description = "负责人")]
    [MaxLength(20)]
    public string Owner { get; set; }

    [Display(Name = "公司网站", Description = "公司网站")]
    [MaxLength(256)]
    public string WebSite { get; set; }
    [Display(Name = "所属行业", Description = "所属行业")]
    [MaxLength(128)]
    public string Industry { get; set; }
    [Display(Name = "公司经营范围", Description = "公司经营范围")]
    [MaxLength(512)]
    public string BusinessScope { get; set; }
    [Display(Name = "详细地址", Description = "详细地址")]
    [MaxLength(256)]
    public string Address { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(512)]
    public string Remark { get; set; }

    [Display(Name = "收款方式", Description = "收款方式")]
    [MaxLength(128)]
    public string Payment { get; set; }


    #region 关务贸易相关
    [Display(Name = "企业十位编码", Description = "企业十位编码")]
    [MaxLength(10)]
    public string TradeCode { get; set; }
    [Display(Name = "主管海关代码", Description = "主管海关代码")]
    [MaxLength(4)]
    public string MasterCustom { get; set; }
    [Display(Name = "统一社会信用代码", Description = "统一社会信用代码")]
    [MaxLength(18)]
    //[Index(IsUnique = true)]
    //[Required]
    public string CreditCode { get; set; }
    #endregion


    #endregion
    #region 主联系人
    [Display(Name = "主联系人", Description = "主联系人")]
    [MaxLength(80)]
    [Required]
    public string ContactName { get; set; }
    [Display(Name = "称谓", Description = "称谓")]
    [MaxLength(10)]
    [DefaultValue("先生")]
    public string Appellation { get; set; }
    [Display(Name = "性别", Description = "性别")]
    [MaxLength(10)]
    [DefaultValue("男")]
    public string Sex { get; set; }

    [Display(Name = "职务", Description = "职务")]
    [MaxLength(80)]
    [Required]
    public string Job { get; set; }
    [Display(Name = "微信", Description = "微信")]
    [MaxLength(50)]
    public string Wx { get; set; }

    [Display(Name = "电话", Description = "电话")]
    [MaxLength(50)]
    public string PhoneNumber { get; set; }
    [Display(Name = "邮箱", Description = "邮箱")]
    [MaxLength(80)]
    [Required]
    public string Email { get; set; }
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(20)]
    public string ContactRemark { get; set; }
    #endregion






    #region 其它
    [Display(Name = "客户状态", Description = "客户状态")]
    [MaxLength(10)]
    public string Status { get; set; }
    [Display(Name = "标志", Description = "标志")]
    public bool Flag { get; set; }
    [Display(Name = "客户图片", Description = "客户图片")]
    public string Logo { get; set; }
    [Display(Name = "最近联系时间", Description = "最近联系时间")]
    [DefaultValue(null)]
    public DateTime? LastContactDate { get; set; }
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
   // public virtual ICollection<CustomerInvoice> CustomerInvoices { get; set; }
    //客户负责业务员关系历史表
    public virtual ICollection<CustomerSales> CustomerSales { get; set; }
    //客户共享记录
    public virtual ICollection<CustomerShare> CustomerShares { get; set; }
    //客户仓库
    public virtual ICollection<CustomerWarehouse> CustomerWarehouses { get; set; }
  }
}