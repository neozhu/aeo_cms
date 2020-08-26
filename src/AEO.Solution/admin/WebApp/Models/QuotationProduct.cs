using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //报价单产品明细
  public partial class QuotationProduct : Entity
  {
   
    #region 商品信息
    [Display(Name = "产品编号", Description = "产品编号")]
    [MaxLength(128)]
    public string ProductNo { get; set; }
    [Display(Name = "中文品名", Description = "中文品名")]
    [MaxLength(200)]
    public string ProductName { get; set; }
    [Display(Name = "产品类别", Description = "产品类别")]
    [MaxLength(50)]
    public string CategoryName { get; set; }
    [Display(Name = "英文品名", Description = "英文品名")]
    [MaxLength(200)]
    public string ProductEnName { get; set; }
    [Display(Name = "中文描述", Description = "中文描述")]
    [MaxLength(256)]
    public string CnDescription { get; set; }
    [Display(Name = "英文描述", Description = "英文描述")]
    [MaxLength(256)]
    public string EnDescription { get; set; }

    #region 贸易信息
    [Display(Name = "海关编码", Description = "海关编码")]
    [MaxLength(10)]
    public string HSCODE { get; set; }
    [Display(Name = "增值税率", Description = "增值税率")]
    public decimal? HSADDTAXRATE { get; set; }
    [Display(Name = "退税率", Description = "退税率")]
    public decimal? HSBACKTAXRATE { get; set; }
    [Display(Name = "报关要素", Description = "报关要素")]
    public string CUSTBASIC { get; set; }
    [Display(Name = "销售指导价", Description = "销售指导价")]
    public decimal? GUIDEPRICE { get; set; }
    #endregion
    [Display(Name = "备注", Description = "备注")]
    [MaxLength(256)]
    public string Remark { get; set; }
    #endregion

    #region 销售信息
   
    
    [Display(Name = "客户货号", Description = "客户货号")]
    [MaxLength(128)]
    public string ThirdProductNo { get; set; }
    [Display(Name = "数量", Description = "数量")]
    public decimal Qty { get; set; }
    [Display(Name = "单位", Description = "单位")]
    [MaxLength(10)]
    public string Unit { get; set; }
    [Display(Name = "报价", Description = "报价")]
    [DefaultValue(null)]
    public decimal? Price { get; set; }
    [Display(Name = "币种", Description = "币种")]
    [MaxLength(20)]
    [DefaultValue(null)]
    public string Cur { get; set; }
    [Display(Name = "金额", Description = "金额")]
    public decimal Amount { get; set; }
    [Display(Name = "美元金额", Description = "美元金额")]
    public decimal USDAmount { get; set; }
    [Display(Name = "人民币金额", Description = "人民币金额")]
    public decimal RMBAmount { get; set; }
    #endregion

    #region 佣金信息
    [Display(Name = "扣佣率/值", Description = "扣佣率/值")]
    public decimal? BrightcmsRate { get; set; }
    [Display(Name = "扣佣金额", Description = "扣佣金额")]
    public decimal? BrightcmsFcy { get; set; }
    [Display(Name = "付佣率/值", Description = "付佣率/值")]
    public decimal? DarkcmsRate { get; set; }
    [Display(Name = "付佣金额", Description = "付佣金额")]
    public decimal? DarkcmsFcy { get; set; }
    #endregion

    [Display(Name = "执行人", Description = "执行人")]
    [MaxLength(20)]
    public string Executor { get; set; }
    

    [Display(Name = "产品图片", Description = "产品图片")]
    [MaxLength(256)]
    public string Logo { get; set; }

    [Display(Name = "报价单号", Description = "报价单号")]
    [MaxLength(20)]
    [Required]
    public string QpNo { get; set; }


    [Display(Name = "报价单", Description = "报价单")]
    public int QuotationId { get; set; }
    [ForeignKey("QuotationId")]
    [Display(Name = "报价单", Description = "报价单")]
    public Quotation Quotation { get; set; }

    [Display(Name = "系统版本号", Description = "系统版本号")]
    public int Ver { get; set; }
  }
}