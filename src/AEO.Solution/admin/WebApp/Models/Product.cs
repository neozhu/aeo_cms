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
  //产品主档
  public partial class Product:Entity
  {
    
    [Display(Name = "产品编号", Description = "产品编号(自动生成,可手工修改)")]
    [MaxLength(128)]
    [Index(IsUnique =true)]
    public string ProductNo { get; set; }
    [Display(Name = "产品类别", Description = "产品类别")]
    [MaxLength(128)]
    public string Category { get; set; }
    [Display(Name = "产品类别", Description = "产品类别")]
    public int? CategoryId { get; set; }
    [Display(Name = "中文品名", Description = "中文品名")]
    [MaxLength(200)]
    public string ProductName { get; set; }
    [Display(Name = "英文品名", Description = "英文品名")]
    [MaxLength(200)]
    public string ProductEnName { get; set; }
    [Display(Name = "规格型号", Description = "规格型号")]
    [MaxLength(100)]
    public string Spec { get; set; }
    [Display(Name = "中文描述", Description = "中文描述")]
    public string CnDescription { get; set; }
    [Display(Name = "英文描述", Description = "英文描述")]
    public string EnDescription { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    public string Unit { get; set; }
    [Display(Name = "备注", Description = "备注")]
    public string Remark { get; set; }
    [Display(Name = "产品状态", Description = "产品状态")]
    [MaxLength(10)]
    public string Status { get; set; }
    

    [Display(Name = "产品图片", Description = "产品图片")]
    public string Logo { get; set; }


    #region 贸易信息
    [Display(Name = "海关编码", Description = "海关编码")]
    [MaxLength(10)]
    public string HSCODE { get; set; }
    [Display(Name = "增值税率", Description = "增值税率")]
    public decimal? HSADDTAXRATE { get; set; }
    [Display(Name = "退税率", Description = "退税率")]
    public decimal? HSBACKTAXRATE { get; set; }
    [Display(Name = "销售指导价", Description = "销售指导价")]
    public decimal? GUIDEPRICE { get; set; }
    [Display(Name = "报关要素", Description = "报关要素")]
    public string CUSTBASIC { get; set; }
    [Display(Name = "所属国家", Description = "所属国家")]
    [MaxLength(128)]
    public string COUNTRY { get; set; }
    [Display(Name = "税务类型", Description = "所属国家")]
    [MaxLength(50)]
    public string TAXTYPE { get; set; }
    [Display(Name = "税分类", Description = "税分类")]
    [MaxLength(50)]
    public string TAXCLASS { get; set; }
    #endregion
    #region 包装箱信息
    [Display(Name = "包装方式", Description = "包装方式")]
    [MaxLength(10)]
    public string Package { get; set; }
    [Display(Name = "内装数量", Description = "内装数量")]
    [DefaultValue(null)]
    public decimal? InnerBoxQty { get; set; }
    [Display(Name = "数量单位", Description = "数量单位")]
    [MaxLength(10)]
    public string InnerUnit { get; set; }
    [Display(Name = "毛重", Description = "毛重")]
    [DefaultValue(null)]
    public decimal? GWeight { get; set; }
    [Display(Name = "毛重单位", Description = "毛重单位")]
    [MaxLength(10)]
    [DefaultValue(null)]
    public string GWUnit { get; set; }
    [Display(Name = "净重", Description = "净重")]
    [DefaultValue(null)]
    public decimal? NWeight { get; set; }
    [Display(Name = "净重单位", Description = "净重单位")]
    [MaxLength(10)]
    [DefaultValue(null)]
    public string NWUnit { get; set; }
    [Display(Name = "体积", Description = "体积")]
    [DefaultValue(null)]
    public decimal? Volume { get; set; }
    [Display(Name = "体积单位", Description = "体积单位")]
    [MaxLength(10)]
    [DefaultValue(null)]
    public string VUnit { get; set; }
    [Display(Name = "长", Description = "长")]
    [DefaultValue(null)]
    public decimal? Length { get; set; }
    [Display(Name = "宽", Description = "宽")]
    [DefaultValue(null)]
    public decimal? Width { get; set; }
    [Display(Name = "高", Description = "高")]
    [DefaultValue(null)]
    public decimal? High { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    public string LUnit { get; set; }

    #endregion

    #region 标志
    [Display(Name = "标志位", Description = "标志位")]
    public bool Flag1 { get; set; }
    [Display(Name = "标志位", Description = "标志位")]
    public bool Flag2 { get; set; }
    #endregion

    public Product()
    {
      this.ProductSalesHistoricalPrices = new HashSet<ProductSalesHistoricalPrice>();
      this.ProductPurchaseHistoricalPrices = new HashSet<ProductPurchaseHistoricalPrice>();
      this.ProductFiles = new HashSet<ProductFile>();
      this.ProductPrictures = new HashSet<ProductPricture>();
    }
    public virtual ICollection<ProductSalesHistoricalPrice> ProductSalesHistoricalPrices { get; set; }
    public virtual ICollection<ProductPurchaseHistoricalPrice> ProductPurchaseHistoricalPrices { get; set; }
    public virtual ICollection<ProductFile> ProductFiles { get; set; }

    public virtual ICollection<ProductPricture> ProductPrictures { get; set; }
  }
}