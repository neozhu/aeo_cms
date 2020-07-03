using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CustomerCommunicationMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/3 13:57:35 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CustomerCommunicationMetadata))]
    public partial class CustomerCommunication
    {
    }

    public partial class CustomerCommunicationMetadata
    {
        [Display(Name = "Customer",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerCommunication))]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.CustomerCommunication))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 会议主题")]
        [Display(Name = "Title",Description ="会议主题",Prompt = "会议主题",ResourceType = typeof(resource.CustomerCommunication))]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter : 沟通类型")]
        [Display(Name = "CommType",Description ="沟通类型",Prompt = "沟通类型",ResourceType = typeof(resource.CustomerCommunication))]
        [MaxLength(20)]
        public string CommType { get; set; }

        [Display(Name = "Status",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.CustomerCommunication))]
        [MaxLength(20)]
        public string Status { get; set; }

        [Display(Name = "Salesman",Description ="业务员",Prompt = "业务员",ResourceType = typeof(resource.CustomerCommunication))]
        [MaxLength(20)]
        public string Salesman { get; set; }

        [Display(Name = "RefUsers",Description ="参与人",Prompt = "参与人",ResourceType = typeof(resource.CustomerCommunication))]
        [MaxLength(128)]
        public string RefUsers { get; set; }

        [Display(Name = "BeginDate",Description ="开始日期",Prompt = "开始日期",ResourceType = typeof(resource.CustomerCommunication))]
        public DateTime BeginDate { get; set; }

        [Display(Name = "EndDate",Description ="结束日期",Prompt = "结束日期",ResourceType = typeof(resource.CustomerCommunication))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.CustomerCommunication))]
        [MaxLength(20)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 客户编号")]
        [Display(Name = "CustomerCode",Description ="客户编号",Prompt = "客户编号",ResourceType = typeof(resource.CustomerCommunication))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.CustomerCommunication))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter : 所属客户")]
        [Display(Name = "CustomerId",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerCommunication))]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CustomerCommunication))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CustomerCommunication))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CustomerCommunication))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CustomerCommunication))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.CustomerCommunication))]
        public int TenantId { get; set; }

    }

}
