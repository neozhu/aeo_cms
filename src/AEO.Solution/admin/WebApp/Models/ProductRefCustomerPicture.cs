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
  //本企业与上游客户的料号关系物料图片
  public partial class ProductRefCustomerPicture : Entity
  {

    [Display(Name = "客户编号", Description = "客户编号")]
    [MaxLength(20)]

    public string CustomerCode { get; set; }
    [Display(Name = "客户名称", Description = "客户名称")]
    [MaxLength(80)]
    [Required]
    public string CustomerName { get; set; }

     
    [Display(Name = "货号", Description = "货号")]
    [MaxLength(50)]
    public string ProductNo { get; set; }

    [Display(Name = "客户货号", Description = "客户货号")]
    [MaxLength(50)]
    public string ThirdProductNo { get; set; }

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
    public string FilePath { get; set; }
    [Display(Name = "相对路径", Description = "相对路径")]
    public string RelativePath { get; set; }
    [Display(Name = "关联单号", Description = "关联单号")]
    [MaxLength(100)]
    public string RefKey { get; set; }
    [Display(Name = "上传用户", Description = "上传用户")]
    [MaxLength(20)]
    public string Owner { get; set; }
    [Display(Name = "上传时间", Description = "上传时间")]
    public DateTime Upload { get; set; }
    [Display(Name = "产品备注", Description = "产品备注")]
    public string Remark { get; set; }


    [Display(Name = "所属产品", Description = "所属产品")]
    public int ProductId { get; set; }
    [Display(Name = "所属产品", Description = "所属产品")]
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    [Display(Name = "产品客户关系", Description = "产品客户关系")]
    public int ProductRefCustomerId { get; set; }
    [ForeignKey("ProductRefCustomerId")]
    [Display(Name = "产品客户关系", Description = "产品客户关系")]
    public ProductRefCustomer ProductRefCustomer { get; set; }
 
  }
}