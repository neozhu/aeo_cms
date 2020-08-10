using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="QuestionTplMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/10 15:18:01 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(QuestionTplMetadata))]
    public partial class QuestionTpl
    {
    }

    public partial class QuestionTplMetadata
    {
        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.QuestionTpl))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 模板名")]
        [Display(Name = "Tpl",Description ="模板名",Prompt = "模板名",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(128)]
        public string Tpl { get; set; }

        [Required(ErrorMessage = "Please enter : AEO认证类别")]
        [Display(Name = "AuthType",Description ="AEO认证类别",Prompt = "AEO认证类别",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(128)]
        public string AuthType { get; set; }

        [Required(ErrorMessage = "Please enter : 类别")]
        [Display(Name = "Category",Description ="类别",Prompt = "类别",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(128)]
        public string Category { get; set; }

        [Display(Name = "Description",Description ="说明",Prompt = "说明",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(128)]
        public string Description { get; set; }

        [Display(Name = "Code",Description ="代码",Prompt = "代码",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(12)]
        public string Code { get; set; }

        [Display(Name = "Title",Description ="简称",Prompt = "简称",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(128)]
        public string Title { get; set; }

        [Display(Name = "StdDescription",Description ="标准说明",Prompt = "标准说明",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(256)]
        public string StdDescription { get; set; }

        [Display(Name = "Notes",Description ="注意",Prompt = "注意",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(128)]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Please enter : 分数")]
        [Display(Name = "StdScore",Description ="分数",Prompt = "分数",ResourceType = typeof(resource.QuestionTpl))]
        public int StdScore { get; set; }

        [Display(Name = "ScoreDescription",Description ="评分说明",Prompt = "评分说明",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(256)]
        public string ScoreDescription { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(128)]
        public string Remark { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.QuestionTpl))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.QuestionTpl))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.QuestionTpl))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.QuestionTpl))]
        public int TenantId { get; set; }

    }

}
