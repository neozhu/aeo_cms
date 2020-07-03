using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CustomerAttentionProductMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/3 13:52:55 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CustomerAttentionProductMetadata))]
    public partial class CustomerAttentionProduct
    {
    }

    public partial class CustomerAttentionProductMetadata
    {
        [Display(Name = "Customer",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerAttentionProduct))]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.CustomerAttentionProduct))]
        public int Id { get; set; }

        [Display(Name = "CustomerCode",Description ="客户编号(保存时系统自动分配也可以手工选择)",Prompt = "客户编号(保存时系统自动分配也可以手工选择)",ResourceType = typeof(resource.CustomerAttentionProduct))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.CustomerAttentionProduct))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Display(Name = "ProductNo",Description ="产品编号(自动生成,可手工修改)",Prompt = "产品编号(自动生成,可手工修改)",ResourceType = typeof(resource.CustomerAttentionProduct))]
        [MaxLength(50)]
        public string ProductNo { get; set; }

        [Display(Name = "ProductName",Description ="中文品名",Prompt = "中文品名",ResourceType = typeof(resource.CustomerAttentionProduct))]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Display(Name = "CUR",Description ="币种",Prompt = "币种",ResourceType = typeof(resource.CustomerAttentionProduct))]
        [MaxLength(10)]
        public string CUR { get; set; }

        [Required(ErrorMessage = "Please enter : 单价")]
        [Display(Name = "Pric",Description ="单价",Prompt = "单价",ResourceType = typeof(resource.CustomerAttentionProduct))]
        public decimal Pric { get; set; }

        [Required(ErrorMessage = "Please enter : 报价次数")]
        [Display(Name = "SummaryQuote",Description ="报价次数",Prompt = "报价次数",ResourceType = typeof(resource.CustomerAttentionProduct))]
        public int SummaryQuote { get; set; }

        [Required(ErrorMessage = "Please enter : 订单次数")]
        [Display(Name = "SummaryOrders",Description ="订单次数",Prompt = "订单次数",ResourceType = typeof(resource.CustomerAttentionProduct))]
        public int SummaryOrders { get; set; }

        [Required(ErrorMessage = "Please enter : 所属客户")]
        [Display(Name = "CustomerId",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerAttentionProduct))]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CustomerAttentionProduct))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CustomerAttentionProduct))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CustomerAttentionProduct))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CustomerAttentionProduct))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.CustomerAttentionProduct))]
        public int TenantId { get; set; }

    }

}
