using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="ActionLogMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/30 15:48:48 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(ActionLogMetadata))]
    public partial class ActionLog
    {
    }

    public partial class ActionLogMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.ActionLog))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 关联ID")]
        [Display(Name = "RefId",Description ="关联ID",Prompt = "关联ID",ResourceType = typeof(resource.ActionLog))]
        public int RefId { get; set; }

        [Display(Name = "RefKey",Description ="关联单号",Prompt = "关联单号",ResourceType = typeof(resource.ActionLog))]
        [MaxLength(128)]
        public string RekKey { get; set; }

        [Required(ErrorMessage = "Please enter : 操作事件")]
        [Display(Name = "ActionDateTime",Description ="操作事件",Prompt = "操作事件",ResourceType = typeof(resource.ActionLog))]
        public DateTime ActionDateTime { get; set; }

        [Display(Name = "User",Description ="操作人员",Prompt = "操作人员",ResourceType = typeof(resource.ActionLog))]
        [MaxLength(20)]
        public string User { get; set; }

        [Display(Name = "Action",Description ="操作类型",Prompt = "操作类型",ResourceType = typeof(resource.ActionLog))]
        [MaxLength(20)]
        public string Action { get; set; }

        [Display(Name = "Content",Description ="操作内容",Prompt = "操作内容",ResourceType = typeof(resource.ActionLog))]
        [MaxLength(128)]
        public string Content { get; set; }

        [Required(ErrorMessage = "Please enter : 同步标志")]
        [Display(Name = "Flag",Description ="同步标志",Prompt = "同步标志",ResourceType = typeof(resource.ActionLog))]
        public bool Flag { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.ActionLog))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.ActionLog))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.ActionLog))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.ActionLog))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.ActionLog))]
        public int TenantId { get; set; }

    }

}
