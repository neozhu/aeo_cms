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
  //产品附件
  public partial class ProductFile:Entity
  {
    [Display(Name = "文件名", Description = "文件名")]
    [MaxLength(128)]
    [Required]
    public string FileName { get; set; }
    [Display(Name = "大小", Description = "大小")]
    public decimal? Size { get; set; }
    [Display(Name = "目录", Description = "目录")]
    [MaxLength(128)]
    [DefaultValue("图片")]
    public string Folder { get; set; }
    [Display(Name = "文件ID", Description = "文件ID")]
    [MaxLength(38)]
    public string FileId { get; set; }
    [Display(Name = "附件类型", Description = "附件类型")]
    [MaxLength(10)]
    
    public string Ext { get; set; }
    [Display(Name = "保存路径", Description = "保存路径")]
    public string FilePath { get; set; }
    [Display(Name = "相对路径", Description = "相对路径")]
    public string RelativePath { get; set; }
    [Display(Name = "上传用户", Description = "上传用户")]
    [MaxLength(20)]
    public string Owner { get; set; }
    [Display(Name = "上传时间", Description = "上传时间")]
    public DateTime Upload { get; set; }
 
    [Display(Name = "所属产品", Description = "所属产品")]
    public int ProductId { get; set; }
    [Display(Name = "所属产品", Description = "所属产品")]
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
  }
}