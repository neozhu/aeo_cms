using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="QuotationChargeMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/27 13:52:29 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(QuotationChargeMetadata))]
    public partial class QuotationCharge
    {
    }

    public partial class QuotationChargeMetadata
    {
        [Display(Name = "Quotation",Description ="报价单",Prompt = "报价单",ResourceType = typeof(resource.QuotationCharge))]
        public Quotation Quotation { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.QuotationCharge))]
        public int Id { get; set; }

        [Display(Name = "Name",Description ="费用名称(中文)",Prompt = "费用名称(中文)",ResourceType = typeof(resource.QuotationCharge))]
        [MaxLength(128)]
        public string Name { get; set; }

        [Display(Name = "EName",Description ="费用名称(英文)",Prompt = "费用名称(英文)",ResourceType = typeof(resource.QuotationCharge))]
        [MaxLength(128)]
        public string EName { get; set; }

        [Required(ErrorMessage = "Please enter : 金额")]
        [Display(Name = "Amount",Description ="金额",Prompt = "金额",ResourceType = typeof(resource.QuotationCharge))]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please enter : 报价单")]
        [Display(Name = "QuotationId",Description ="报价单",Prompt = "报价单",ResourceType = typeof(resource.QuotationCharge))]
        public int QuotationId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.QuotationCharge))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.QuotationCharge))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.QuotationCharge))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.QuotationCharge))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.QuotationCharge))]
        public int TenantId { get; set; }

    }

}
