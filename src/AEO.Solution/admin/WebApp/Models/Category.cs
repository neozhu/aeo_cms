using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //产品类别
  public partial class Category:Entity
  {

    [Display(Name = "类别名称", Description = "类别名称")]
    [MaxLength(128)]
    public virtual string Name { get; set; }
    [Display(Name = "英文名称", Description = "英文名称")]
    [MaxLength(128)]
    public virtual string EName { get; set; }
    [Display(Name = "图标", Description = "图标")]
    [MaxLength(30)]
    public virtual string Icon { get; set; }

    [Display(Name = "上级类别", Description = "上级类别")]
    public virtual int? ParentId { get; set; }
    [ForeignKey("ParentId")]
    [Display(Name = "上级类别", Description = "上级类别")]
    public virtual Category Parent { get; set; }
  }
}