using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="InquiryRefMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/19 10:57:50 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(InquiryRefMetadata))]
    public partial class InquiryRef
    {
    }

    public partial class InquiryRefMetadata
    {
        [Display(Name = "Inquiry",Description ="询价单",Prompt = "询价单",ResourceType = typeof(resource.InquiryRef))]
        public Inquiry Inquiry { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.InquiryRef))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 询价单号")]
        [Display(Name = "InquiryNo",Description ="询价单号",Prompt = "询价单号",ResourceType = typeof(resource.InquiryRef))]
        [MaxLength(20)]
        public string InquiryNo { get; set; }

        [Display(Name = "TaskNo",Description ="任务单号",Prompt = "任务单号",ResourceType = typeof(resource.InquiryRef))]
        [MaxLength(20)]
        public string TaskNo { get; set; }

        [Required(ErrorMessage = "Please enter : 状态")]
        [Display(Name = "Status",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.InquiryRef))]
        [MaxLength(20)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter : 询价日期")]
        [Display(Name = "BeginDate",Description ="询价日期",Prompt = "询价日期",ResourceType = typeof(resource.InquiryRef))]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Salesman",Description ="业务员",Prompt = "业务员",ResourceType = typeof(resource.InquiryRef))]
        [MaxLength(20)]
        public string Salesman { get; set; }

        [Display(Name = "Dept",Description ="部门",Prompt = "部门",ResourceType = typeof(resource.InquiryRef))]
        [MaxLength(80)]
        public string Dept { get; set; }

        [Required(ErrorMessage = "Please enter : 系统版本号")]
        [Display(Name = "Ver",Description ="系统版本号",Prompt = "系统版本号",ResourceType = typeof(resource.InquiryRef))]
        public int Ver { get; set; }

        [Required(ErrorMessage = "Please enter : 询价单")]
        [Display(Name = "InquiryId",Description ="询价单",Prompt = "询价单",ResourceType = typeof(resource.InquiryRef))]
        public int InquiryId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.InquiryRef))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.InquiryRef))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.InquiryRef))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.InquiryRef))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.InquiryRef))]
        public int TenantId { get; set; }

    }

}
