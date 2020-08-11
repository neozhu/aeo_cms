using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="AeoAuthTestMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/11 9:27:09 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(AeoAuthTestMetadata))]
    public partial class AeoAuthTest
    {
    }

    public partial class AeoAuthTestMetadata
    {
        [Display(Name = "Aeoquestions",Description ="Aeoquestions",Prompt = "Aeoquestions",ResourceType = typeof(resource.AeoAuthTest))]
        public AeoQuestion Aeoquestions { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.AeoAuthTest))]
        public int Id { get; set; }

        [Display(Name = "Name",Description ="企业名称",Prompt = "企业名称",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(128)]
        public string Name { get; set; }

        [Display(Name = "TradeCode",Description ="企业十位编码",Prompt = "企业十位编码",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(10)]
        public string TradeCode { get; set; }

        [Display(Name = "CreditCode",Description ="统一社会信用代码",Prompt = "统一社会信用代码",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(18)]
        public string CreditCode { get; set; }

        [Display(Name = "Ctype",Description ="企业类型",Prompt = "企业类型",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(128)]
        public string Ctype { get; set; }

        [Required(ErrorMessage = "Please enter : 测试编号")]
        [Display(Name = "TestNo",Description ="测试编号",Prompt = "测试编号",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(20)]
        public string TestNo { get; set; }

        [Display(Name = "AuthType",Description ="AEO认证类别",Prompt = "AEO认证类别",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(128)]
        public string AuthType { get; set; }

        [Display(Name = "MasterCustom",Description ="主管海关代码",Prompt = "主管海关代码",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(10)]
        public string MasterCustom { get; set; }

        [Display(Name = "RegistDate",Description ="海关注册日期",Prompt = "海关注册日期",ResourceType = typeof(resource.AeoAuthTest))]
        public DateTime RegistDate { get; set; }

        [Display(Name = "IsForeign",Description ="是否境外",Prompt = "是否境外",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(50)]
        public string IsForeign { get; set; }

        [Display(Name = "Zone",Description ="特殊区域",Prompt = "特殊区域",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(128)]
        public string Zone { get; set; }

        [Display(Name = "RegistedTime",Description ="海关注册时长",Prompt = "海关注册时长",ResourceType = typeof(resource.AeoAuthTest))]
        public decimal RegistedTime { get; set; }

        [Display(Name = "Unit",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(10)]
        public string Unit { get; set; }

        [Display(Name = "AuthDate",Description ="自评日期",Prompt = "自评日期",ResourceType = typeof(resource.AeoAuthTest))]
        public DateTime AuthDate { get; set; }

        [Display(Name = "Tester",Description ="测试人",Prompt = "测试人",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(28)]
        public string Tester { get; set; }

        [Display(Name = "Year",Description ="年份",Prompt = "年份",ResourceType = typeof(resource.AeoAuthTest))]
        public int Year { get; set; }

        [Display(Name = "BeginDate",Description ="评定开始日期",Prompt = "评定开始日期",ResourceType = typeof(resource.AeoAuthTest))]
        public DateTime BeginDate { get; set; }

        [Display(Name = "EndDate",Description ="评定结束日期",Prompt = "评定结束日期",ResourceType = typeof(resource.AeoAuthTest))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(512)]
        public string Remark { get; set; }

        [Display(Name = "Status",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(12)]
        public string Status { get; set; }

        [Display(Name = "StdScore",Description ="合格分数",Prompt = "合格分数",ResourceType = typeof(resource.AeoAuthTest))]
        public decimal StdScore { get; set; }

        [Display(Name = "Score",Description ="分数",Prompt = "分数",ResourceType = typeof(resource.AeoAuthTest))]
        public decimal Score { get; set; }

        [Display(Name = "Result",Description ="结果",Prompt = "结果",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(128)]
        public string Result { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.AeoAuthTest))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.AeoAuthTest))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.AeoAuthTest))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.AeoAuthTest))]
        public int TenantId { get; set; }

    }

}
