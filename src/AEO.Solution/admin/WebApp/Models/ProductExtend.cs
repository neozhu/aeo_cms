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
  //产品参数设定
  public partial class ProductExtend : Entity
  {

    #region 采购相关
    [Display(Name = "采购机构", Description = "采购机构")]
    [MaxLength(50)]
    public string PurchasingDepartment { get; set; }
    [Display(Name = "采购人员", Description = "采购人员")]
    [MaxLength(20)]
    public string Buyer { get; set; }
    [Display(Name = "最小采购量", Description = "最小采购量")]
    public decimal? MinPQty { get; set; }
    [Display(Name = "最小交货量", Description = "最小交货量")]
    public decimal? MinDQty { get; set; }

    [Display(Name = "单位", Description = "单位")]
    [MaxLength(20)]
    public string PUnit { get; set; }
    [Display(Name = "物料定价组1", Description = "物料定价组1")]
    [MaxLength(20)]
    public string PGroupPrice1 { get; set; }
    [Display(Name = "物料定价组2", Description = "物料定价组2")]
    [MaxLength(20)]
    public string PGroupPrice2 { get; set; }
    [Display(Name = "物料定价组3", Description = "物料定价组3")]
    [MaxLength(20)]
    public string PGroupPrice3 { get; set; }
    #endregion
    #region 销售相关参数
    [Display(Name = "销售机构", Description = "销售机构")]
    [MaxLength(50)]
    public string SalesDepartment { get; set; }
    [Display(Name = "销售人员", Description = "销售人员")]
    [MaxLength(20)]
    public string Sales { get; set; }
    [Display(Name = "最小订单量", Description = "最小订单量")]
    public decimal? MinOQty { get; set; }
    [Display(Name = "最小交货量", Description = "最小交货量")]
    public decimal? MinSQty { get; set; }

    [Display(Name = "单位", Description = "单位")]
    [MaxLength(20)]
    public string SUnit { get; set; }
    [Display(Name = "物料定价组1", Description = "物料定价组1")]
    [MaxLength(20)]
    public string SGroupPrice1 { get; set; }
    [Display(Name = "物料定价组2", Description = "物料定价组2")]
    [MaxLength(20)]
    public string SGroupPrice2 { get; set; }
    [Display(Name = "物料定价组3", Description = "物料定价组3")]
    [MaxLength(20)]
    public string SGroupPrice3 { get; set; }
    #endregion
    [Display(Name = "货号", Description = "货号(自动生成,可手工修改)")]
    [MaxLength(50)]
    public string ProductNo { get; set; }
    [Display(Name = "产品名称", Description = "产品名称")]
    [MaxLength(200)]
    public string ProductName { get; set; }
    [Display(Name = "所属产品", Description = "所属产品")]
    public int ProductId { get; set; }
    [Display(Name = "所属产品", Description = "所属产品")]
    [ForeignKey("ProductId")]
    public Product Product { get; set; }

  }
}