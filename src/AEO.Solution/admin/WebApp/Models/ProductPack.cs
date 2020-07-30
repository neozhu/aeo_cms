using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //产品包装信息
  public partial class ProductPack:Entity
  {

    [Display(Name = "包装单位", Description = "包装单位")]
    [MaxLength(10)]
    [Required]
    public string Package { get; set; }
    [Display(Name = "内装数量", Description = "内装数量")]
    public decimal? InnerBoxQty { get; set; }
    [Display(Name = "长", Description = "长")]
    public decimal? Length { get; set; }
    [Display(Name = "宽", Description = "宽")]
    public decimal? Width { get; set; }
    [Display(Name = "高", Description = "高")]
    public decimal? Height { get; set; }
    [Display(Name = "长度单位", Description = "长度单位")]
    [MaxLength(10)]
    public string Unit { get; set; }
    [Display(Name = "毛重(kg)", Description = "毛重(kg)")]
    public decimal? GWeight { get; set; }
    [Display(Name = "净重(kg)", Description = "净重(kg)")]
    public decimal? NWeight { get; set; }
    [Display(Name = "体积(m3)", Description = "体积(m3)")]
    public decimal? Volume { get; set; }
    [Display(Name = "20尺装量", Description = "20尺装量")]
    public decimal? TwentyQtc { get; set; }
    [Display(Name = "40尺装量", Description = "40尺装量")]
    public decimal? FortyQtc { get; set; }
    [Display(Name = "40HQ装量", Description = "40HQ装量")]
    public decimal? FortyHQQtc { get; set; }
    [Display(Name = "默认包装", Description = "默认包装")]
    public bool Default { get; set; }
    [Display(Name = "所属产品", Description = "所属产品")]
    public int ProductId { get; set; }
    [Display(Name = "所属产品", Description = "所属产品")]
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
  }
}