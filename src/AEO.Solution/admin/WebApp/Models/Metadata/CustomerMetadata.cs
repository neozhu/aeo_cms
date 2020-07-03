using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CustomerMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/3 14:54:31 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
    }

    public partial class CustomerMetadata
    {
        [Display(Name = "CustomerAttentionProducts",Description ="CustomerAttentionProducts",Prompt = "CustomerAttentionProducts",ResourceType = typeof(resource.Customer))]
        public CustomerAttentionProduct CustomerAttentionProducts { get; set; }

        [Display(Name = "CustomerBanks",Description ="CustomerBanks",Prompt = "CustomerBanks",ResourceType = typeof(resource.Customer))]
        public CustomerBank CustomerBanks { get; set; }

        [Display(Name = "CustomerCommunications",Description ="CustomerCommunications",Prompt = "CustomerCommunications",ResourceType = typeof(resource.Customer))]
        public CustomerCommunication CustomerCommunications { get; set; }

        [Display(Name = "CustomerContacts",Description ="CustomerContacts",Prompt = "CustomerContacts",ResourceType = typeof(resource.Customer))]
        public CustomerContact CustomerContacts { get; set; }

        [Display(Name = "CustomerFiles",Description ="CustomerFiles",Prompt = "CustomerFiles",ResourceType = typeof(resource.Customer))]
        public CustomerFile CustomerFiles { get; set; }

        [Display(Name = "CustomerFollows",Description ="CustomerFollows",Prompt = "CustomerFollows",ResourceType = typeof(resource.Customer))]
        public CustomerFollow CustomerFollows { get; set; }

        [Display(Name = "CustomerInvoices",Description ="CustomerInvoices",Prompt = "CustomerInvoices",ResourceType = typeof(resource.Customer))]
        public CustomerInvoice CustomerInvoices { get; set; }

        [Display(Name = "CustomerSales",Description ="CustomerSales",Prompt = "CustomerSales",ResourceType = typeof(resource.Customer))]
        public CustomerSales CustomerSales { get; set; }

        [Display(Name = "CustomerShares",Description ="CustomerShares",Prompt = "CustomerShares",ResourceType = typeof(resource.Customer))]
        public CustomerShare CustomerShares { get; set; }

        [Display(Name = "CustomerWarehouses",Description ="CustomerWarehouses",Prompt = "CustomerWarehouses",ResourceType = typeof(resource.Customer))]
        public CustomerWarehouse CustomerWarehouses { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.Customer))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 客户编号(保存时系统自动分配也可以手工选择)")]
        [Display(Name = "CustomerCode",Description ="客户编号(保存时系统自动分配也可以手工选择)",Prompt = "客户编号(保存时系统自动分配也可以手工选择)",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.Customer))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Display(Name = "CustomerEName",Description ="客户英文名称",Prompt = "客户英文名称",ResourceType = typeof(resource.Customer))]
        [MaxLength(80)]
        public string CustomerEName { get; set; }

        [Display(Name = "CustomerType",Description ="客户类型",Prompt = "客户类型",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string CustomerType { get; set; }

        [Required(ErrorMessage = "Please enter : 是否境外企业")]
        [Display(Name = "Overseas",Description ="是否境外企业",Prompt = "是否境外企业",ResourceType = typeof(resource.Customer))]
        public bool Overseas { get; set; }

        [Display(Name = "CustomerType3",Description ="客户类别",Prompt = "客户类别",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string CustomerType3 { get; set; }

        [Display(Name = "Capital",Description ="注册资本(万)",Prompt = "注册资本(万)",ResourceType = typeof(resource.Customer))]
        public decimal Capital { get; set; }

        [Display(Name = "CURR",Description ="币制",Prompt = "币制",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string CURR { get; set; }

        [Display(Name = "TaxProperty",Description ="税务资质",Prompt = "税务资质",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string TaxProperty { get; set; }

        [Display(Name = "ParentOrg",Description ="上级组织",Prompt = "上级组织",ResourceType = typeof(resource.Customer))]
        [MaxLength(80)]
        public string ParentOrg { get; set; }

        [Display(Name = "CustomMaster",Description ="注册海关",Prompt = "注册海关",ResourceType = typeof(resource.Customer))]
        [MaxLength(128)]
        public string CustomMaster { get; set; }

        [Display(Name = "TradeCode",Description ="海关十位编码",Prompt = "海关十位编码",ResourceType = typeof(resource.Customer))]
        [MaxLength(128)]
        public string TradeCode { get; set; }

        [Display(Name = "Country",Description ="国家地区",Prompt = "国家地区",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string Country { get; set; }

        [Display(Name = "Zone",Description ="特殊区域",Prompt = "特殊区域",ResourceType = typeof(resource.Customer))]
        [MaxLength(150)]
        public string Zone { get; set; }

        [Display(Name = "Scale",Description ="客户规模",Prompt = "客户规模",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string Scale { get; set; }

        [Display(Name = "Level",Description ="客户等级",Prompt = "客户等级",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string Level { get; set; }

        [Display(Name = "Value",Description ="客户价值",Prompt = "客户价值",ResourceType = typeof(resource.Customer))]
        [MaxLength(256)]
        public string Value { get; set; }

        [Display(Name = "CreditRating",Description ="客户资信等级",Prompt = "客户资信等级",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string CreditRating { get; set; }

        [Display(Name = "Source",Description ="客户来源",Prompt = "客户来源",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string Source { get; set; }

        [Display(Name = "Industry",Description ="所属行业",Prompt = "所属行业",ResourceType = typeof(resource.Customer))]
        [MaxLength(128)]
        public string Industry { get; set; }

        [Display(Name = "Cash",Description ="资金额度(万)",Prompt = "资金额度(万)",ResourceType = typeof(resource.Customer))]
        public decimal Cash { get; set; }

        [Display(Name = "CashCURR",Description ="币制",Prompt = "币制",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string CashCURR { get; set; }

        [Display(Name = "SDesc",Description ="优惠说明",Prompt = "优惠说明",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string SDesc { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string Remark { get; set; }

        [Display(Name = "CProvinces1",Description ="省",Prompt = "省",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string CProvinces1 { get; set; }

        [Display(Name = "CCity1",Description ="市",Prompt = "市",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string CCity1 { get; set; }

        [Display(Name = "CCounty1",Description ="县",Prompt = "县",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string CCounty1 { get; set; }

        [Display(Name = "CAddress1",Description ="详细地址",Prompt = "详细地址",ResourceType = typeof(resource.Customer))]
        [MaxLength(256)]
        public string CAddress1 { get; set; }

        [Display(Name = "CProvinces2",Description ="省",Prompt = "省",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string CProvinces2 { get; set; }

        [Display(Name = "CCity2",Description ="市",Prompt = "市",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string CCity2 { get; set; }

        [Display(Name = "CCounty2",Description ="县",Prompt = "县",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string CCounty2 { get; set; }

        [Display(Name = "CAddress2",Description ="详细地址",Prompt = "详细地址",ResourceType = typeof(resource.Customer))]
        [MaxLength(256)]
        public string CAddress2 { get; set; }

        [Display(Name = "EAddress1",Description ="英文地址",Prompt = "英文地址",ResourceType = typeof(resource.Customer))]
        [MaxLength(256)]
        public string EAddress1 { get; set; }

        [Display(Name = "EAddress2",Description ="国际地址",Prompt = "国际地址",ResourceType = typeof(resource.Customer))]
        [MaxLength(256)]
        public string EAddress2 { get; set; }

        [Display(Name = "PostCode",Description ="邮编",Prompt = "邮编",ResourceType = typeof(resource.Customer))]
        [MaxLength(10)]
        public string PostCode { get; set; }

        [Display(Name = "WebSite",Description ="公司网站",Prompt = "公司网站",ResourceType = typeof(resource.Customer))]
        [MaxLength(256)]
        public string WebSite { get; set; }

        [Display(Name = "BusinessScope",Description ="公司经营范围",Prompt = "公司经营范围",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string BusinessScope { get; set; }

        [Display(Name = "Remark1",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.Customer))]
        [MaxLength(256)]
        public string Remark1 { get; set; }

        [Display(Name = "Status",Description ="客户状态",Prompt = "客户状态",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string Status { get; set; }

        [Display(Name = "Status1",Description ="客户状态1",Prompt = "客户状态1",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string Status1 { get; set; }

        [Display(Name = "Status2",Description ="客户状态1",Prompt = "客户状态1",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string Status2 { get; set; }

        [Display(Name = "Status3",Description ="客户状态3",Prompt = "客户状态3",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string Status3 { get; set; }

        [Display(Name = "Status4",Description ="客户状态4",Prompt = "客户状态4",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string Status4 { get; set; }

        [Display(Name = "Status5",Description ="客户状态5",Prompt = "客户状态5",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string Status5 { get; set; }

        [Display(Name = "Logo",Description ="客户图片",Prompt = "客户图片",ResourceType = typeof(resource.Customer))]
        [MaxLength(50)]
        public string Logo { get; set; }

        [Display(Name = "CompanyCode",Description ="归属企业代码",Prompt = "归属企业代码",ResourceType = typeof(resource.Customer))]
        [MaxLength(10)]
        public string CompanyCode { get; set; }

        [Display(Name = "CompanyName",Description ="归属企业名称",Prompt = "归属企业名称",ResourceType = typeof(resource.Customer))]
        [MaxLength(128)]
        public string CompanyName { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Customer))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Customer))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Customer))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.Customer))]
        public int TenantId { get; set; }

    }

}
