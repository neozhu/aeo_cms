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
  //本企业与上游客户的料号对应关系
  public partial class ProductRefCustomer:Entity
  {

    [Display(Name = "客户编号", Description = "客户编号")]
    [MaxLength(20)]
    [Required]
    public string CustomerCode { get; set; }
    [Display(Name = "客户名称", Description = "客户名称")]
    [MaxLength(80)]
    [Required]
    public string CustomerName { get; set; }

    [Display(Name = "客户类型", Description = "客户类型")]
    [MaxLength(20)]
    public string CustomerType { get; set; }
    [Display(Name = "是否境外企业", Description = "是否境外企业")]
    public bool Overseas { get; set; }
    [Display(Name = "所在区域", Description = "特殊区域")]
    [MaxLength(150)]
    public string Zone { get; set; }

    [Display(Name = "国家地区", Description = "国家地区")]
    [MaxLength(50)]
    [DefaultValue("中国")]
    public string Country { get; set; }

    
    [Display(Name = "货号", Description = "货号")]
    [MaxLength(50)]
    public string ProductNo { get; set; }
    #region 关联物料基本信息
    [Display(Name = "客户货号", Description = "客户货号")]
    [MaxLength(50)]
    public string ThirdProductNo { get; set; }

 
    [Display(Name = "客户物料性质", Description = "客户物料性质")]
    [MaxLength(50)]
    public string ThirdProductNature { get; set; }

    [Display(Name = "单位", Description = "单位")]
    [MaxLength(50)]
    public string ThirdUnit { get; set; }
    [Display(Name = "类型", Description = "类型")]
    [MaxLength(50)]
    public string ThirdProductType { get; set; }
    [Display(Name = "产品名称", Description = "产品名称")]
    [MaxLength(200)]
    public string ThirdProductName { get; set; }
    [Display(Name = "英文品名", Description = "英文品名")]
    [MaxLength(200)]
    public string ThirdProductEnName { get; set; }

    [Display(Name = "物料别名", Description = "物料别名")]
    [MaxLength(200)]
    public string ThirdAliasProductName { get; set; }
    
    #endregion

    #region 关联商品属性
    [Display(Name = "产品类别", Description = "产品类别")]
    [MaxLength(50)]
    public string CategoryName { get; set; }
     
    [Display(Name = "保税/非报税", Description = "保税/非报税")]
    [MaxLength(50)]
    public string Flag1 { get; set; }
    [Display(Name = "主辅料", Description = "主辅料")]
    [MaxLength(50)]
    public string Flag2 { get; set; }
    [Display(Name = "ABC类", Description = "ABC类")]
    [MaxLength(50)]
    public string Flag3 { get; set; }
    [Display(Name = "物料组别", Description = "物料组别")]
    [MaxLength(50)]
    public string Group { get; set; }

     
    [Display(Name = "毛重", Description = "毛重")]
    public decimal? GWeight { get; set; }
    [Display(Name = "毛重单位", Description = "毛重单位")]
    [MaxLength(10)]
    public string GWUnit { get; set; }
    [Display(Name = "净重", Description = "净重")]
    public decimal? NWeight { get; set; }
    [Display(Name = "净重单位", Description = "净重单位")]
    [MaxLength(10)]
    public string NWUnit { get; set; }
    [Display(Name = "体积", Description = "体积")]
    public decimal? Volume { get; set; }
    [Display(Name = "体积单位", Description = "体积单位")]
    [MaxLength(10)]
    public string VUnit { get; set; }
    [Display(Name = "长", Description = "长")]
    public decimal? Length { get; set; }
    [Display(Name = "宽", Description = "宽")]
    public decimal? Width { get; set; }
    [Display(Name = "高", Description = "高")]
    public decimal? High { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    public string LUnit { get; set; }
    [Display(Name = "物料属性1", Description = "物料属性1")]
    [MaxLength(128)]
    public string Attribute1 { get; set; }
    [Display(Name = "物料属性2", Description = "物料属性2")]
    [MaxLength(128)]
    public string Attribute2 { get; set; }
    [Display(Name = "物料属性3", Description = "物料属性3")]
    [MaxLength(128)]
    public string Attribute3 { get; set; }
    [Display(Name = "物料属性4", Description = "物料属性4")]
    [MaxLength(128)]
    public string Attribute4 { get; set; }
    [Display(Name = "物料属性5", Description = "物料属性5")]
    [MaxLength(128)]
    public string Attribute5 { get; set; }
    [Display(Name = "物料属性6", Description = "物料属性6")]
    [MaxLength(128)]
    public string Attribute6 { get; set; }
    [Display(Name = "物料属性7", Description = "物料属性7")]
    [MaxLength(128)]
    public string Attribute7 { get; set; }
    [Display(Name = "物料属性8", Description = "物料属性8")]
    [MaxLength(128)]
    public string Attribute8 { get; set; }



    #endregion

    #region 关联商品税务
   
    [Display(Name = "税务类型", Description = "所属国家")]
    [MaxLength(50)]
    public string TAXTYPE { get; set; }
    [Display(Name = "税分类", Description = "税分类")]
    [MaxLength(50)]
    public string TAXCLASS { get; set; }
    [Display(Name = "增值税率", Description = "增值税率")]
    public decimal? HSADDTAXRATE { get; set; }
    [Display(Name = "退税率", Description = "退税率")]
    public decimal? HSBACKTAXRATE { get; set; }
    #endregion

  
    [Display(Name = "产品备注", Description = "产品备注")]
    public string Remark { get; set; }

    [Display(Name = "所属产品", Description = "所属产品")]
    public int ProductId { get; set; }
    [Display(Name = "所属产品", Description = "所属产品")]
    [ForeignKey("ProductId")]
    public Product Product { get; set; }


  }
}