using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //附件文件
  public partial class Attachment:Entity
  {
    [Display(Name = "文件名", Description = "文件名")]
    [MaxLength(128)]
    [Required]
    public string FileName { get; set; }
    [Display(Name = "文件ID", Description = "文件ID")]
    [MaxLength(128)]
    public string FileId { get; set; }
    [Display(Name = "附件类型", Description = "附件类型")]
    [MaxLength(10)]
    public string Ext { get; set; }
    [Display(Name = "文件大小", Description = "文件大小")]
    public decimal? Size { get; set; }
    [Display(Name = "保存路径", Description = "保存路径")]
    public string FilePath { get; set; }
    [Display(Name = "相对路径", Description = "相对路径")]
    public string RelativePath { get; set; }
    [Display(Name = "关联单号", Description = "关联单号")]
    [MaxLength(128)]
    public string RefKey { get; set; }
    [Display(Name = "上传用户", Description = "上传用户")]
    [MaxLength(20)]
    public string Owner { get; set; }
    [Display(Name = "上传时间", Description = "上传时间")]
    public DateTime Upload { get; set; }
  }
}