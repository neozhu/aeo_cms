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
  //客户文件
  public partial class CustomerFile:Entity
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
    [Display(Name = "客户编号", Description = "客户编号")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("customer.CustomerCode")]
    public string CustomerCode { get; set; }
    [Display(Name = "客户名称", Description = "客户名称")]
    [MaxLength(80)]
    [Required]
    [DefaultValue("customer.CustomerName")]
    public string CustomerName { get; set; }

    [Display(Name = "所属客户", Description = "所属客户")]
    [DefaultValue("customer.Id")]
    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    [Display(Name = "所属客户", Description = "所属客户")]
    public Customer Customer { get; set; }
  }
}