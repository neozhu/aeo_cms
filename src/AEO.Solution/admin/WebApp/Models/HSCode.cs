using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  public partial class HSCode:Entity
  {
    [Display(Name = "10位HS编码", Description = "10位HS编码")]
    [MaxLength(10)]
    [Required]
    public string hscode { get; set; }
    [Display(Name = "商品名称", Description = "商品名称")]
    [MaxLength(512)]
    public string cn_name { get; set; }
    [Display(Name = "商品英文名称", Description = "商品英文名称")]
    [MaxLength(256)]
    public string en_name { get; set; }
    [Display(Name = "申报要素", Description = "申报要素")]
    [MaxLength(256)]
    public string g_model { get; set; }
    [Display(Name = "第一法定单位代码", Description = "第一法定单位代码")]
    [MaxLength(3)]
    public string unit_code { get; set; }
    [Display(Name = "第一法定单位名称", Description = "第一法定单位名称")]
    [MaxLength(12)]
    public string unit_name { get; set; }
    [Display(Name = "第二法定单位代码", Description = "第二法定单位代码")]
    [MaxLength(3)]
    public string unit2_code { get; set; }
    [Display(Name = "第二法定单位名称", Description = "第二法定单位名称")]
    [MaxLength(12)]
    public string unit2_name { get; set; }
    [Display(Name = "监管条件代码", Description = "监管条件代码")]
    [MaxLength(256)]
    public string control_ma { get; set; }
    [Display(Name = "检验检疫类别代码", Description = "检验检疫类别代码")]
    [MaxLength(256)]
    public string ciq_ma { get; set; }
    [Display(Name = "进口最惠国税率", Description = "进口最惠国税率")]
    [MaxLength(56)]
    public string im_low_rate { get; set; }
    [Display(Name = "进口普通税率", Description = "进口普通税率")]
    [MaxLength(56)]
    public string im_normal_rate { get; set; }
    [Display(Name = "进口暂定税率", Description = "进口暂定税率")]
    [MaxLength(56)]
    public string im_temp_rate { get; set; }
    [Display(Name = "增值税税率", Description = "增值税税率")]
    [MaxLength(56)]
    public string im_tax_rate { get; set; }
    [Display(Name = "进口消费税税率", Description = "进口消费税税率")]
    [MaxLength(56)]
    public string im_consume_rate { get; set; }
    [Display(Name = "进口消费税税率", Description = "进口消费税税率")]
    [MaxLength(56)]
    public string ex_return_rate { get; set; }
    [Display(Name = "出口普通税率", Description = "出口普通税率")]
    [MaxLength(56)]
    public string ex_normal_rate { get; set; }
    
    [Display(Name = "出口暂定税率", Description = "出口暂定税率")]
    [MaxLength(56)]
    public string ex_temp_rate { get; set; }
    [Display(Name = "出口特殊税税率", Description = "出口特殊税税率")]
    [MaxLength(56)]
    public string ex_special_rate { get; set; }
    [Display(Name = "出口增值税税率", Description = "出口增值税税率")]
    [MaxLength(56)]
    public string ex_tax_rate { get; set; }
    [Display(Name = "商品备注", Description = "商品备注")]
    [MaxLength(512)]
    public string remark { get; set; }

  }
}