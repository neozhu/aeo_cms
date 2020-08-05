using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Repository.Pattern.Ef6;

namespace WebApp.Models
{
  //询价单附件
  public partial class InquiryFile : Entity
  {

    [Display(Name = "文件名", Description = "文件名")]
    [MaxLength(100)]
    [Required]
    public string FileName { get; set; }
    [Display(Name = "大小", Description = "大小")]
    public decimal Size { get; set; }
    [Display(Name = "目录", Description = "目录")]
    [MaxLength(20)]
    public string Folder { get; set; }
    
  
    [Display(Name = "保存路径", Description = "保存路径")]
    public string FilePath { get; set; }
    [Display(Name = "相对路径", Description = "相对路径")]
    public string RelativePath { get; set; }
    [Display(Name = "上传用户", Description = "上传用户")]
    [MaxLength(20)]
    public string Owner { get; set; }
    [Display(Name = "上传时间", Description = "上传时间")]
    public DateTime Upload { get; set; }
    [Display(Name = "附件类型", Description = "附件类型")]
    [MaxLength(100)]

    public string Ext { get; set; }
    [Display(Name = "文件ID", Description = "文件ID")]
    [MaxLength(100)]
    public string FileId { get; set; }

    [Display(Name = "关联单号", Description = "关联单号")]
    [MaxLength(100)]
    public string RefKey { get; set; }

   
    [Display(Name = "系统版本号", Description = "系统版本号")]
    public int Ver { get; set; }
    [Display(Name = "询价单号", Description = "询价单号")]
    [MaxLength(20)]
    [Required]
    public string InquiryNo { get; set; }
    [Display(Name = "任务单号", Description = "任务单号")]
    [MaxLength(20)]
    //[Required]
    public string TaskNo { get; set; }
    [Display(Name = "询价单", Description = "询价单")]
    public int InquiryId { get; set; }
    [Display(Name = "询价单", Description = "询价单")]
    [ForeignKey("InquiryId")]
    public Inquiry Inquiry { get; set; }
  }
}