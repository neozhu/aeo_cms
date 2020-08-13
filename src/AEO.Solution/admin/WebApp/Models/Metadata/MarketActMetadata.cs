using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="MarketActMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/13 11:31:39 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(MarketActMetadata))]
    public partial class MarketAct
    {
    }

    public partial class MarketActMetadata
    {
        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.MarketAct))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 市场活动名称")]
        [Display(Name = "Name",Description ="市场活动名称",Prompt = "市场活动名称",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter : 负责人")]
        [Display(Name = "Owner",Description ="负责人",Prompt = "负责人",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(20)]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Please enter : 活动状态")]
        [Display(Name = "Status",Description ="活动状态",Prompt = "活动状态",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(20)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter : 活动类型")]
        [Display(Name = "ActType",Description ="活动类型",Prompt = "活动类型",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(56)]
        public string ActType { get; set; }

        [Display(Name = "PlanStartDate",Description ="计划开始日期",Prompt = "计划开始日期",ResourceType = typeof(resource.MarketAct))]
        public DateTime PlanStartDate { get; set; }

        [Display(Name = "PlanFinishDate",Description ="计划完成日期",Prompt = "计划完成日期",ResourceType = typeof(resource.MarketAct))]
        public DateTime PlanFinishDate { get; set; }

        [Display(Name = "BudgetExpense",Description ="费用预算",Prompt = "费用预算",ResourceType = typeof(resource.MarketAct))]
        public decimal BudgetExpense { get; set; }

        [Display(Name = "Cur",Description ="币种",Prompt = "币种",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(56)]
        public string Cur { get; set; }

        [Display(Name = "Address",Description ="活动地点",Prompt = "活动地点",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(128)]
        public string Address { get; set; }

        [Display(Name = "PlanDesc",Description ="活动计划",Prompt = "活动计划",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(512)]
        public string PlanDesc { get; set; }

        [Display(Name = "ActualStartDate",Description ="实际开始日期",Prompt = "实际开始日期",ResourceType = typeof(resource.MarketAct))]
        public DateTime ActualStartDate { get; set; }

        [Display(Name = "ActualFinishDate",Description ="实际完成日期",Prompt = "实际完成日期",ResourceType = typeof(resource.MarketAct))]
        public DateTime ActualFinishDate { get; set; }

        [Display(Name = "ActExpense",Description ="实际投入",Prompt = "实际投入",ResourceType = typeof(resource.MarketAct))]
        public decimal ActExpense { get; set; }

        [Display(Name = "Income",Description ="预计收入",Prompt = "预计收入",ResourceType = typeof(resource.MarketAct))]
        public decimal Income { get; set; }

        [Display(Name = "ExecDesc",Description ="执行情况",Prompt = "执行情况",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(512)]
        public string ExecDesc { get; set; }

        [Display(Name = "SumaryDesc",Description ="活动总结",Prompt = "活动总结",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(512)]
        public string SumaryDesc { get; set; }

        [Display(Name = "EffectDesc",Description ="评估效果",Prompt = "评估效果",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(512)]
        public string EffectDesc { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.MarketAct))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.MarketAct))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.MarketAct))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.MarketAct))]
        public int TenantId { get; set; }

    }

}
