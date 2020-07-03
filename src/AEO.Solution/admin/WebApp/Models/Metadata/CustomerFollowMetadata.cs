using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CustomerFollowMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/3 14:02:40 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CustomerFollowMetadata))]
    public partial class CustomerFollow
    {
    }

    public partial class CustomerFollowMetadata
    {
        [Display(Name = "Customer",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerFollow))]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.CustomerFollow))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 客户联系人")]
        [Display(Name = "ContactName",Description ="客户联系人",Prompt = "客户联系人",ResourceType = typeof(resource.CustomerFollow))]
        [MaxLength(80)]
        public string ContactName { get; set; }

        [Display(Name = "FollowType",Description ="跟进方式",Prompt = "跟进方式",ResourceType = typeof(resource.CustomerFollow))]
        [MaxLength(20)]
        public string FollowType { get; set; }

        [Display(Name = "Status",Description ="跟进状态",Prompt = "跟进状态",ResourceType = typeof(resource.CustomerFollow))]
        [MaxLength(20)]
        public string Status { get; set; }

        [Display(Name = "Owner",Description ="跟进人",Prompt = "跟进人",ResourceType = typeof(resource.CustomerFollow))]
        [MaxLength(20)]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Please enter : 跟进时间")]
        [Display(Name = "FollowDate",Description ="跟进时间",Prompt = "跟进时间",ResourceType = typeof(resource.CustomerFollow))]
        public DateTime FollowDate { get; set; }

        [Display(Name = "Content",Description ="跟进内容",Prompt = "跟进内容",ResourceType = typeof(resource.CustomerFollow))]
        [MaxLength(50)]
        public string Content { get; set; }

        [Display(Name = "ReminderTime",Description ="设置提醒时间",Prompt = "设置提醒时间",ResourceType = typeof(resource.CustomerFollow))]
        public DateTime ReminderTime { get; set; }

        [Display(Name = "ReminderContent",Description ="提醒内容",Prompt = "提醒内容",ResourceType = typeof(resource.CustomerFollow))]
        [MaxLength(50)]
        public string ReminderContent { get; set; }

        [Display(Name = "ReminderTo",Description ="提醒人员",Prompt = "提醒人员",ResourceType = typeof(resource.CustomerFollow))]
        [MaxLength(200)]
        public string ReminderTo { get; set; }

        [Required(ErrorMessage = "Please enter : 客户编号")]
        [Display(Name = "CustomerCode",Description ="客户编号",Prompt = "客户编号",ResourceType = typeof(resource.CustomerFollow))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.CustomerFollow))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter : 所属客户")]
        [Display(Name = "CustomerId",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerFollow))]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CustomerFollow))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CustomerFollow))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CustomerFollow))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CustomerFollow))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.CustomerFollow))]
        public int TenantId { get; set; }

    }

}
