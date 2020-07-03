using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CustomerInvoiceMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/3 14:13:16 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CustomerInvoiceMetadata))]
    public partial class CustomerInvoice
    {
    }

    public partial class CustomerInvoiceMetadata
    {
        [Display(Name = "Customer",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerInvoice))]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.CustomerInvoice))]
        public int Id { get; set; }

        [Display(Name = "InvName",Description ="发票名称",Prompt = "发票名称",ResourceType = typeof(resource.CustomerInvoice))]
        [MaxLength(80)]
        public string InvName { get; set; }

        [Required(ErrorMessage = "Please enter : 发票类型")]
        [Display(Name = "InvType",Description ="发票类型",Prompt = "发票类型",ResourceType = typeof(resource.CustomerInvoice))]
        [MaxLength(100)]
        public string InvType { get; set; }

        [Display(Name = "InvCountry",Description ="发票国家",Prompt = "发票国家",ResourceType = typeof(resource.CustomerInvoice))]
        [MaxLength(100)]
        public string InvCountry { get; set; }

        [Required(ErrorMessage = "Please enter : 发票税点")]
        [Display(Name = "InvTax",Description ="发票税点",Prompt = "发票税点",ResourceType = typeof(resource.CustomerInvoice))]
        public decimal InvTax { get; set; }

        [Display(Name = "TaxNo",Description ="税务等级号",Prompt = "税务等级号",ResourceType = typeof(resource.CustomerInvoice))]
        [MaxLength(100)]
        public string TaxNo { get; set; }

        [Display(Name = "InvUse",Description ="发票用途",Prompt = "发票用途",ResourceType = typeof(resource.CustomerInvoice))]
        [MaxLength(256)]
        public string InvUse { get; set; }

        [Display(Name = "Remark",Description ="备注说明",Prompt = "备注说明",ResourceType = typeof(resource.CustomerInvoice))]
        [MaxLength(256)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 客户编号")]
        [Display(Name = "CustomerCode",Description ="客户编号",Prompt = "客户编号",ResourceType = typeof(resource.CustomerInvoice))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.CustomerInvoice))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter : 所属客户")]
        [Display(Name = "CustomerId",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerInvoice))]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CustomerInvoice))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CustomerInvoice))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CustomerInvoice))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CustomerInvoice))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.CustomerInvoice))]
        public int TenantId { get; set; }

    }

}
