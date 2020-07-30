using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CompanyMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/30 11:08:32 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CompanyMetadata))]
    public partial class Company
    {
    }

    public partial class CompanyMetadata
    {
        [Display(Name = "Parent",Description ="母公司",Prompt = "母公司",ResourceType = typeof(resource.Company))]
        public Company Parent { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.Company))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 企业名称")]
        [Display(Name = "Name",Description ="企业名称",Prompt = "企业名称",ResourceType = typeof(resource.Company))]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "TradeCode",Description ="企业十位编码",Prompt = "企业十位编码",ResourceType = typeof(resource.Company))]
        [MaxLength(10)]
        public string TradeCode { get; set; }

        [Display(Name = "MasterCustom",Description ="主管海关代码",Prompt = "主管海关代码",ResourceType = typeof(resource.Company))]
        [MaxLength(10)]
        public string MasterCustom { get; set; }

        [Required(ErrorMessage = "Please enter : 统一社会信用代码")]
        [Display(Name = "CreditCode",Description ="统一社会信用代码",Prompt = "统一社会信用代码",ResourceType = typeof(resource.Company))]
        [MaxLength(18)]
        public string CreditCode { get; set; }

        [Display(Name = "Code",Description ="备案号",Prompt = "备案号",ResourceType = typeof(resource.Company))]
        [MaxLength(10)]
        public string Code { get; set; }

        [Display(Name = "Ctype",Description ="企业类型",Prompt = "企业类型",ResourceType = typeof(resource.Company))]
        [MaxLength(56)]
        public string Ctype { get; set; }

        [Display(Name = "Scope",Description ="经营范围",Prompt = "经营范围",ResourceType = typeof(resource.Company))]
        [MaxLength(512)]
        public string Scope { get; set; }

        [Display(Name = "Address",Description ="地址",Prompt = "地址",ResourceType = typeof(resource.Company))]
        [MaxLength(50)]
        public string Address { get; set; }

        [Display(Name = "LegalPerson",Description ="法人",Prompt = "法人",ResourceType = typeof(resource.Company))]
        [MaxLength(12)]
        public string LegalPerson { get; set; }

        [Display(Name = "Contect",Description ="联系人",Prompt = "联系人",ResourceType = typeof(resource.Company))]
        [MaxLength(12)]
        public string Contect { get; set; }

        [Display(Name = "PhoneNumber",Description ="联系电话",Prompt = "联系电话",ResourceType = typeof(resource.Company))]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter : 注册日期")]
        [Display(Name = "RegisterDate",Description ="注册日期",Prompt = "注册日期",ResourceType = typeof(resource.Company))]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "ExpirationDate",Description ="有效期",Prompt = "有效期",ResourceType = typeof(resource.Company))]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "ParentId",Description ="母公司",Prompt = "母公司",ResourceType = typeof(resource.Company))]
        public int ParentId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Company))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Company))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Company))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Company))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.Company))]
        public int TenantId { get; set; }

    }

}
