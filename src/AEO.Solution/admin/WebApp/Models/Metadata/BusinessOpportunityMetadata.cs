using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="BusinessOpportunityMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/12 15:15:17 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(BusinessOpportunityMetadata))]
    public partial class BusinessOpportunity
    {
    }

    public partial class BusinessOpportunityMetadata
    {
        [Display(Name = "Customer",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.BusinessOpportunity))]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.BusinessOpportunity))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 商机名称")]
        [Display(Name = "Name",Description ="商机名称",Prompt = "商机名称",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter : 负责人")]
        [Display(Name = "Owner",Description ="负责人",Prompt = "负责人",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(20)]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Please enter : 所属客户")]
        [Display(Name = "CustomerId",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.BusinessOpportunity))]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter : 联系人")]
        [Display(Name = "ContactName",Description ="联系人",Prompt = "联系人",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(80)]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Please enter : 发现日期")]
        [Display(Name = "OpDate",Description ="发现日期",Prompt = "发现日期",ResourceType = typeof(resource.BusinessOpportunity))]
        public DateTime OpDate { get; set; }

        [Display(Name = "ProvidePeople",Description ="提供人",Prompt = "提供人",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(80)]
        public string ProvidePeople { get; set; }

        [Required(ErrorMessage = "Please enter : 机会来源")]
        [Display(Name = "Source",Description ="机会来源",Prompt = "机会来源",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(50)]
        public string Source { get; set; }

        [Display(Name = "MarketAction",Description ="市场活动",Prompt = "市场活动",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(128)]
        public string MarketAction { get; set; }

        [Display(Name = "Status",Description ="跟进状态",Prompt = "跟进状态",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(50)]
        public string Status { get; set; }

        [Display(Name = "Curr",Description ="币种",Prompt = "币种",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(50)]
        public string Curr { get; set; }

        [Display(Name = "PrDate",Description ="预计签单日期",Prompt = "预计签单日期",ResourceType = typeof(resource.BusinessOpportunity))]
        public DateTime PrDate { get; set; }

        [Display(Name = "Amount",Description ="预计成交金额",Prompt = "预计成交金额",ResourceType = typeof(resource.BusinessOpportunity))]
        public decimal Amount { get; set; }

        [Display(Name = "Content",Description ="商机内容",Prompt = "商机内容",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(512)]
        public string Content { get; set; }

        [Display(Name = "Stage",Description ="当前阶段",Prompt = "当前阶段",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(128)]
        public string Stage { get; set; }

        [Display(Name = "StageDate",Description ="更新时间",Prompt = "更新时间",ResourceType = typeof(resource.BusinessOpportunity))]
        public DateTime StageDate { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(128)]
        public string Remark { get; set; }

        [Display(Name = "CustomerCode",Description ="客户编号",Prompt = "客户编号",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.BusinessOpportunity))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.BusinessOpportunity))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.BusinessOpportunity))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.BusinessOpportunity))]
        public int TenantId { get; set; }

    }

}
