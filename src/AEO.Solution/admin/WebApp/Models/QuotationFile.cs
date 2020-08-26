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
  //报价单附件
  public partial class QuotationFile : Entity
  {
   
    [Display(Name = "文件名", Description = "文件名")]
    [MaxLength(100)]
    [Required]
    public string FileName { get; set; }
    [Display(Name = "大小", Description = "大小")]
    public decimal Size { get; set; }
    [Display(Name = "目录", Description = "目录")]
    [MaxLength(20)]
    [DefaultValue("图片")]
    public string Folder { get; set; }
    [Display(Name = "文件ID", Description = "文件ID")]
    [MaxLength(100)]
    public string FileId { get; set; }
    [Display(Name = "附件类型", Description = "附件类型")]
    [MaxLength(100)]
    
    public string Ext { get; set; }
    [Display(Name = "保存路径", Description = "保存路径")]
    [MaxLength(256)]
    public string FilePath { get; set; }
    [Display(Name = "相对路径", Description = "相对路径")]
    [MaxLength(256)]
    public string RelativePath { get; set; }
    [Display(Name = "关联单号", Description = "关联单号")]
    [MaxLength(100)]
    public string RefKey { get; set; }
    [Display(Name = "上传用户", Description = "上传用户")]
    [MaxLength(20)]
    public string Owner { get; set; }
    [Display(Name = "上传时间", Description = "上传时间")]
    public DateTime Upload { get; set; }

    [Display(Name = "报价单号", Description = "报价单号")]
    [MaxLength(20)]
    public string QpNo { get; set; }
    [Display(Name = "报价单", Description = "报价单")]
    public int QuotationId { get; set; }
    [ForeignKey("QuotationId")]
    [Display(Name = "报价单", Description = "报价单")]
    public Quotation Quotation { get; set; }
  }
}