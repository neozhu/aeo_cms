using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="ProductPurchaseHistoricalPriceMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/30 16:30:26 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(ProductPurchaseHistoricalPriceMetadata))]
    public partial class ProductPurchaseHistoricalPrice
    {
    }

    public partial class ProductPurchaseHistoricalPriceMetadata
    {
        [Display(Name = "Product",Description ="所属产品",Prompt = "所属产品",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        public Product Product { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 供应商代码")]
        [Display(Name = "SupplierCode",Description ="供应商代码",Prompt = "供应商代码",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        [MaxLength(20)]
        public string SupplierCode { get; set; }

        [Display(Name = "SupplierName",Description ="供应商名称",Prompt = "供应商名称",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        [MaxLength(80)]
        public string SupplierName { get; set; }

        [Display(Name = "ThirdProductNo",Description ="工厂货号",Prompt = "工厂货号",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        [MaxLength(80)]
        public string ThirdProductNo { get; set; }

        [Required(ErrorMessage = "Please enter : 询价时间")]
        [Display(Name = "QuoteDate",Description ="询价时间",Prompt = "询价时间",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        public DateTime QuoteDate { get; set; }

        [Display(Name = "CUR",Description ="币种",Prompt = "币种",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        [MaxLength(10)]
        public string CUR { get; set; }

        [Required(ErrorMessage = "Please enter : 单价")]
        [Display(Name = "UnitPrice",Description ="单价",Prompt = "单价",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Please enter : 数量")]
        [Display(Name = "Qty",Description ="数量",Prompt = "数量",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        public decimal Qty { get; set; }

        [Display(Name = "Source",Description ="来源",Prompt = "来源",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        [MaxLength(10)]
        public string Source { get; set; }

        [Display(Name = "DocNo",Description ="单据号",Prompt = "单据号",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        [MaxLength(100)]
        public string DocNo { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        [MaxLength(50)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 产品编号(自动生成,可手工修改)")]
        [Display(Name = "ProductNo",Description ="产品编号(自动生成,可手工修改)",Prompt = "产品编号(自动生成,可手工修改)",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        [MaxLength(128)]
        public string ProductNo { get; set; }

        [Display(Name = "ProductName",Description ="产品名称",Prompt = "产品名称",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please enter : 所属产品")]
        [Display(Name = "ProductId",Description ="所属产品",Prompt = "所属产品",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        public int ProductId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.ProductPurchaseHistoricalPrice))]
        public int TenantId { get; set; }

    }

}
