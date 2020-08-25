using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="ApproveHistoryMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/25 10:18:35 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(ApproveHistoryMetadata))]
    public partial class ApproveHistory
    {
    }

    public partial class ApproveHistoryMetadata
    {
        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.ApproveHistory))]
        public int Id { get; set; }

        [Display(Name = "RefId",Description ="关联ID",Prompt = "关联ID",ResourceType = typeof(resource.ApproveHistory))]
        public int RefId { get; set; }

        [Display(Name = "RefKey",Description ="关联单号",Prompt = "关联单号",ResourceType = typeof(resource.ApproveHistory))]
        [MaxLength(128)]
        public string RekKey { get; set; }

        [Display(Name = "Status",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.ApproveHistory))]
        [MaxLength(32)]
        public string Status { get; set; }

        [Display(Name = "Initiator",Description ="发起人",Prompt = "发起人",ResourceType = typeof(resource.ApproveHistory))]
        [MaxLength(32)]
        public string Initiator { get; set; }

        [Display(Name = "SubmitDate",Description ="提交时间",Prompt = "提交时间",ResourceType = typeof(resource.ApproveHistory))]
        public DateTime SubmitDate { get; set; }

        [Display(Name = "ToAuditor",Description ="待审人",Prompt = "待审人",ResourceType = typeof(resource.ApproveHistory))]
        [MaxLength(32)]
        public string ToAuditor { get; set; }

        [Display(Name = "Approver",Description ="审批人",Prompt = "审批人",ResourceType = typeof(resource.ApproveHistory))]
        [MaxLength(32)]
        public string Approver { get; set; }

        [Display(Name = "ApprovedDate",Description ="审批时间",Prompt = "审批时间",ResourceType = typeof(resource.ApproveHistory))]
        public DateTime ApprovedDate { get; set; }

        [Display(Name = "Result",Description ="审批意见",Prompt = "审批意见",ResourceType = typeof(resource.ApproveHistory))]
        [MaxLength(512)]
        public string Result { get; set; }

        [Display(Name = "Comment",Description ="审批说明",Prompt = "审批说明",ResourceType = typeof(resource.ApproveHistory))]
        [MaxLength(512)]
        public string Comment { get; set; }

        [Display(Name = "Remark",Description ="审批备注",Prompt = "审批备注",ResourceType = typeof(resource.ApproveHistory))]
        [MaxLength(512)]
        public string Remark { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.ApproveHistory))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.ApproveHistory))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.ApproveHistory))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.ApproveHistory))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.ApproveHistory))]
        public int TenantId { get; set; }

    }

}
