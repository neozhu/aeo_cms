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
  //询价单关联任务
  public partial class InquiryRef:Entity
  {

    [Display(Name = "询价单号", Description = "询价单号")]
    [MaxLength(20)]
    [Required]
    public string InquiryNo { get; set; }
    [Display(Name = "任务单号", Description = "任务单号")]
    [MaxLength(20)]
    //[Required]
    public string TaskNo { get; set; }
    [Display(Name = "状态", Description = "状态")]
    [MaxLength(20)]
    [Required]
    [DefaultValue("草拟")]
    public string Status { get; set; }
    [Display(Name = "询价日期", Description = "询价日期")]
    [DefaultValue("now")]
    public DateTime BeginDate { get; set; }

    [Display(Name = "业务员", Description = "业务员")]
    [MaxLength(20)]
    //[Required]
    public string Salesman { get; set; }
    [Display(Name = "部门", Description = "部门")]
    [MaxLength(80)]
    public string Dept { get; set; }
    [Display(Name = "系统版本号", Description = "系统版本号")]
    public int Ver { get; set; }
    [Display(Name = "询价单", Description = "询价单")]
    public int InquiryId { get; set; }
    [ForeignKey("InquiryId")]
    [Display(Name = "询价单", Description = "询价单")]
    public Inquiry Inquiry { get; set; }
  }
}