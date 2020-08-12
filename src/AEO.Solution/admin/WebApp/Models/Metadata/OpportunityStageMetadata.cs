using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="OpportunityStageMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/12 15:03:26 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(OpportunityStageMetadata))]
    public partial class OpportunityStage
    {
    }

    public partial class OpportunityStageMetadata
    {
        [Display(Name = "BusinessOpportunity",Description ="商机",Prompt = "商机",ResourceType = typeof(resource.OpportunityStage))]
        public BusinessOpportunity BusinessOpportunity { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.OpportunityStage))]
        public int Id { get; set; }

        [Display(Name = "Stage",Description ="阶段",Prompt = "阶段",ResourceType = typeof(resource.OpportunityStage))]
        [MaxLength(128)]
        public string Stage { get; set; }

        [Required(ErrorMessage = "Please enter : 成功率")]
        [Display(Name = "SuccessRate",Description ="成功率",Prompt = "成功率",ResourceType = typeof(resource.OpportunityStage))]
        public decimal SuccessRate { get; set; }

        [Required(ErrorMessage = "Please enter : 确认时间")]
        [Display(Name = "ConfirmDate",Description ="确认时间",Prompt = "确认时间",ResourceType = typeof(resource.OpportunityStage))]
        public DateTime ConfirmDate { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.OpportunityStage))]
        [MaxLength(128)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 商机")]
        [Display(Name = "BusinessOpportunityId",Description ="商机",Prompt = "商机",ResourceType = typeof(resource.OpportunityStage))]
        public int BusinessOpportunityId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.OpportunityStage))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.OpportunityStage))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.OpportunityStage))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.OpportunityStage))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.OpportunityStage))]
        public int TenantId { get; set; }

    }

}
