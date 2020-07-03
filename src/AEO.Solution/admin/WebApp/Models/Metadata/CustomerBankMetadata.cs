using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CustomerBankMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/3 13:28:36 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CustomerBankMetadata))]
    public partial class CustomerBank
    {
    }

    public partial class CustomerBankMetadata
    {
        [Display(Name = "Customer",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerBank))]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.CustomerBank))]
        public int Id { get; set; }

        [Display(Name = "CustomerCode",Description ="客户编号(保存时系统自动分配也可以手工选择)",Prompt = "客户编号(保存时系统自动分配也可以手工选择)",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 单位名称")]
        [Display(Name = "CustomerName",Description ="单位名称",Prompt = "单位名称",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter : 开户名称")]
        [Display(Name = "AccountName",Description ="开户名称",Prompt = "开户名称",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(100)]
        public string AccountName { get; set; }

        [Display(Name = "Bank",Description ="银行名称",Prompt = "银行名称",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(100)]
        public string Bank { get; set; }

        [Required(ErrorMessage = "Please enter : 银行账号")]
        [Display(Name = "AccountNo",Description ="银行账号",Prompt = "银行账号",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(100)]
        public string AccountNo { get; set; }

        [Required(ErrorMessage = "Please enter : 账户类型")]
        [Display(Name = "AccountType",Description ="账户类型",Prompt = "账户类型",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(100)]
        public string AccountType { get; set; }

        [Display(Name = "BankCountry",Description ="银行国家",Prompt = "银行国家",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(100)]
        public string BankCountry { get; set; }

        [Display(Name = "BankUse",Description ="账户用途",Prompt = "账户用途",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(100)]
        public string BankUse { get; set; }

        [Display(Name = "BankAddress1",Description ="开户行地址",Prompt = "开户行地址",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(100)]
        public string BankAddress1 { get; set; }

        [Display(Name = "BankAddress2",Description ="开户行英文地址",Prompt = "开户行英文地址",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(100)]
        public string BankAddress2 { get; set; }

        [Display(Name = "SWIFT",Description ="SWIFT号",Prompt = "SWIFT号",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(50)]
        public string SWIFT { get; set; }

        [Display(Name = "CUR",Description ="币值",Prompt = "币值",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(10)]
        public string CUR { get; set; }

        [Display(Name = "Remark",Description ="备注说明",Prompt = "备注说明",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(50)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 所属客户")]
        [Display(Name = "CustomerId",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerBank))]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CustomerBank))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CustomerBank))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CustomerBank))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.CustomerBank))]
        public int TenantId { get; set; }

    }

}
