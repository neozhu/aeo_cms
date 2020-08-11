using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="AeoQuestionMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/11 9:21:13 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(AeoQuestionMetadata))]
    public partial class AeoQuestion
    {
    }

    public partial class AeoQuestionMetadata
    {
        [Display(Name = "AeoAuthTest",Description ="AEO自认证测评",Prompt = "AEO自认证测评",ResourceType = typeof(resource.AeoQuestion))]
        public AeoAuthTest AeoAuthTest { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.AeoQuestion))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 模板名")]
        [Display(Name = "Tpl",Description ="模板名",Prompt = "模板名",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(128)]
        public string Tpl { get; set; }

        [Display(Name = "AuthType",Description ="AEO认证类别",Prompt = "AEO认证类别",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(128)]
        public string AuthType { get; set; }

        [Required(ErrorMessage = "Please enter : 类别")]
        [Display(Name = "Category",Description ="类别",Prompt = "类别",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(128)]
        public string Category { get; set; }

        [Display(Name = "Description",Description ="说明",Prompt = "说明",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(128)]
        public string Description { get; set; }

        [Display(Name = "Code",Description ="代码",Prompt = "代码",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(12)]
        public string Code { get; set; }

        [Display(Name = "Title",Description ="项目",Prompt = "项目",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(128)]
        public string Title { get; set; }

        [Display(Name = "Short",Description ="简称",Prompt = "简称",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(128)]
        public string Short { get; set; }

        [Display(Name = "StdDescription",Description ="标准说明",Prompt = "标准说明",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(256)]
        public string StdDescription { get; set; }

        [Display(Name = "Notes",Description ="注意",Prompt = "注意",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(128)]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Please enter : 分数")]
        [Display(Name = "StdScore",Description ="分数",Prompt = "分数",ResourceType = typeof(resource.AeoQuestion))]
        public int StdScore { get; set; }

        [Required(ErrorMessage = "Please enter : 测试分数")]
        [Display(Name = "Score",Description ="测试分数",Prompt = "测试分数",ResourceType = typeof(resource.AeoQuestion))]
        public int Score { get; set; }

        [Display(Name = "ScoreDescription",Description ="评分说明",Prompt = "评分说明",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(256)]
        public string ScoreDescription { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(128)]
        public string Remark { get; set; }

        [Display(Name = "Tester",Description ="测试人",Prompt = "测试人",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(28)]
        public string Tester { get; set; }

        [Display(Name = "TestDateTime",Description ="测试时间",Prompt = "测试时间",ResourceType = typeof(resource.AeoQuestion))]
        public DateTime TestDateTime { get; set; }

        [Display(Name = "TestNo",Description ="测试编号",Prompt = "测试编号",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(20)]
        public string TestNo { get; set; }

        [Required(ErrorMessage = "Please enter : AEO自认证测评")]
        [Display(Name = "AeoAuthTestId",Description ="AEO自认证测评",Prompt = "AEO自认证测评",ResourceType = typeof(resource.AeoQuestion))]
        public int AeoAuthTestId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.AeoQuestion))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.AeoQuestion))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.AeoQuestion))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.AeoQuestion))]
        public int TenantId { get; set; }

    }

}
