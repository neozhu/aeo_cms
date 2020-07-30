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
  //产品图片
  public partial class ProductPricture : Entity
  {
    [Display(Name = "图片名称", Description = "图片名称")]
    [MaxLength(128)]
    [Required]
    public string FileName { get; set; }
    [Display(Name = "图片描述", Description = "图片描述")]
    [MaxLength(128)]
    public string Description { get; set; }
    [Display(Name = "顺序", Description = "顺序")]
    public int? LineNo { get; set; }

    [Display(Name = "大小", Description = "大小")]
    public decimal Size { get; set; }
    [Display(Name = "目录", Description = "目录")]
    [MaxLength(20)]
    [DefaultValue("图片")]
    public string Folder { get; set; }
    [Display(Name = "文件ID", Description = "文件ID")]
    [MaxLength(38)]
    public string FileId { get; set; }
    [Display(Name = "保存路径", Description = "保存路径")]
    public string FilePath { get; set; }
    [Display(Name = "相对路径", Description = "相对路径")]
    public string RelativePath { get; set; }
 
    [Display(Name = "所属产品", Description = "所属产品")]
    public int ProductId { get; set; }
    [Display(Name = "所属产品", Description = "所属产品")]
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
  }
}