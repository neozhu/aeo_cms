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
  //本企业与上游客户的料号关系海关贸易信息
  public partial class ProductRefCustomerTrade : Entity
  {

    [Display(Name = "客户编号", Description = "客户编号")]
    [MaxLength(20)]

    public string CustomerCode { get; set; }
    [Display(Name = "客户名称", Description = "客户名称")]
    [MaxLength(80)]
    [Required]
    public string CustomerName { get; set; }
    [Display(Name = "客户类型", Description = "客户类型")]
    [MaxLength(20)]
    public string CustomerType { get; set; }

    [Display(Name = "货号", Description = "货号")]
    [MaxLength(50)]
    public string ProductNo { get; set; }

    [Display(Name = "客户货号", Description = "客户货号")]
    [MaxLength(50)]
    public string ThirdProductNo { get; set; }
    [Display(Name = "关联物料性质", Description = "关联物料性质")]
    [MaxLength(50)]
    public string ERP_MATERIALPROP { get; set; }
    [Display(Name = "海关物料性质", Description = "海关物料性质")]
    [MaxLength(50)]
    public string MTPCKENDPROMARK { get; set; }
    
    [Display(Name = "主管海关", Description = "主管海关")]
    [MaxLength(50)]
    public string CUSTOMS_CODE { get; set; }
    [Display(Name = "账册号码", Description = "账册号码")]
    [MaxLength(50)]
    public string F_EMS_NO { get; set; }
    [Display(Name = "账册类型", Description = "账册类型")]
    [MaxLength(50)]
    public string EMS_TYPE { get; set; }
    [Display(Name = "海关商品序号", Description = "海关商品序号")]
    [MaxLength(50)]
    public string CUS_GDS_SEQNO { get; set; }
    [Display(Name = "关务归并序号", Description = "关务归并序号")]
    [MaxLength(50)]
    public string CPLX_EMS_SEQNO { get; set; }
    [Display(Name = "商品编码", Description = "商品编码")]
    [MaxLength(50)]
    public string GDECD { get; set; }
    [Display(Name = "检验检疫编码", Description = "检验检疫编码")]
    [MaxLength(50)]
    public string CIQ_CODE { get; set; }
    [Display(Name = "海关监管条件", Description = "海关监管条件")]
    [MaxLength(50)]
    public string CST_CTRL_MARK { get; set; }
    [Display(Name = "检验检疫类别", Description = "检验检疫类别")]
    [MaxLength(50)]
    public string CIQ_CTRL_MARK { get; set; }
    [Display(Name = "海关中文名称", Description = "海关中文名称")]
    [MaxLength(50)]
    public string NEMS_BOND_INVT_NO { get; set; }
    [Display(Name = "申报单位", Description = "申报单位")]
    [MaxLength(50)]
    public string F_UNIT { get; set; }

    [Display(Name = "法定单位", Description = "法定单位")]
    [MaxLength(50)]
    public string LAWF_UNITCD { get; set; }

    [Display(Name = "第二法定单位", Description = "第二法定单位")]
    [MaxLength(50)]
    public string SECD_LAWF_UNITCD { get; set; }
    [Display(Name = "申报要素", Description = "申报要素")]
    [MaxLength(50)]
    public string GDS_SPCF_MODEL_DESC { get; set; }
    [Display(Name = "海关申报要素项", Description = "海关申报要素项")]
    [MaxLength(50)]
    public string GDS_SPCF_MODEL_CUS { get; set; }
    [Display(Name = "海关规格", Description = "海关规格")]
    [MaxLength(50)]
    public string GDS_MODEL { get; set; }

    [Display(Name = "备注", Description = "备注")]
    public string Remark { get; set; }


    [Display(Name = "所属产品", Description = "所属产品")]
    public int ProductId { get; set; }
    [Display(Name = "所属产品", Description = "所属产品")]
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    [Display(Name = "产品客户关系", Description = "产品客户关系")]
    public int ProductRefCustomerId { get; set; }
    [Display(Name = "产品客户关系", Description = "产品客户关系")]
    [ForeignKey("ProductRefCustomerId")]
    public ProductRefCustomer ProductRefCustomer { get; set; }

  }
}