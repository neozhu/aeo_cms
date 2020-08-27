using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //海关参数 口岸代码
  public partial class GPort:Entity
  {
    [Display(Name = "代码", Description = "代码")]
    [MaxLength(8)]
    [Required]
    public string code { get; set; }
    [Display(Name = "中文名称", Description = "中文名称")]
    [MaxLength(128)]
    public string cn_name { get; set; }
    [Display(Name = "英文名称", Description = "英文名称")]
    [MaxLength(128)]
    public string en_name { get; set; }
  }
}