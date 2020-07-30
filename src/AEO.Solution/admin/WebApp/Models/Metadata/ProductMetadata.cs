using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="ProductMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/30 16:45:01 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
    }

    public partial class ProductMetadata
    {
        [Display(Name = "ProductFiles",Description ="ProductFiles",Prompt = "ProductFiles",ResourceType = typeof(resource.Product))]
        public ProductFile ProductFiles { get; set; }

        [Display(Name = "ProductPurchaseHistoricalPrices",Description ="ProductPurchaseHistoricalPrices",Prompt = "ProductPurchaseHistoricalPrices",ResourceType = typeof(resource.Product))]
        public ProductPurchaseHistoricalPrice ProductPurchaseHistoricalPrices { get; set; }

        [Display(Name = "ProductSalesHistoricalPrices",Description ="ProductSalesHistoricalPrices",Prompt = "ProductSalesHistoricalPrices",ResourceType = typeof(resource.Product))]
        public ProductSalesHistoricalPrice ProductSalesHistoricalPrices { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.Product))]
        public int Id { get; set; }

        [Display(Name = "ProductNo",Description ="产品编号(自动生成,可手工修改)",Prompt = "产品编号(自动生成,可手工修改)",ResourceType = typeof(resource.Product))]
        [MaxLength(128)]
        public string ProductNo { get; set; }

        [Display(Name = "Category",Description ="产品类别",Prompt = "产品类别",ResourceType = typeof(resource.Product))]
        [MaxLength(128)]
        public string Category { get; set; }

        [Display(Name = "ProductName",Description ="中文品名",Prompt = "中文品名",ResourceType = typeof(resource.Product))]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Display(Name = "ProductEnName",Description ="英文品名",Prompt = "英文品名",ResourceType = typeof(resource.Product))]
        [MaxLength(200)]
        public string ProductEnName { get; set; }

        [Display(Name = "Spec",Description ="规格型号",Prompt = "规格型号",ResourceType = typeof(resource.Product))]
        [MaxLength(100)]
        public string Spec { get; set; }

        [Display(Name = "CnDescription",Description ="中文描述",Prompt = "中文描述",ResourceType = typeof(resource.Product))]
        [MaxLength(50)]
        public string CnDescription { get; set; }

        [Display(Name = "EnDescription",Description ="英文描述",Prompt = "英文描述",ResourceType = typeof(resource.Product))]
        [MaxLength(50)]
        public string EnDescription { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.Product))]
        [MaxLength(50)]
        public string Remark { get; set; }

        [Display(Name = "Status",Description ="产品状态",Prompt = "产品状态",ResourceType = typeof(resource.Product))]
        [MaxLength(10)]
        public string Status { get; set; }

        [Display(Name = "Logo",Description ="产品图片",Prompt = "产品图片",ResourceType = typeof(resource.Product))]
        [MaxLength(50)]
        public string Logo { get; set; }

        [Display(Name = "HSCODE",Description ="海关编码",Prompt = "海关编码",ResourceType = typeof(resource.Product))]
        [MaxLength(10)]
        public string HSCODE { get; set; }

        [Display(Name = "HSADDTAXRATE",Description ="增值税率",Prompt = "增值税率",ResourceType = typeof(resource.Product))]
        public decimal HSADDTAXRATE { get; set; }

        [Display(Name = "HSBACKTAXRATE",Description ="退税率",Prompt = "退税率",ResourceType = typeof(resource.Product))]
        public decimal HSBACKTAXRATE { get; set; }

        [Display(Name = "GUIDEPRICE",Description ="销售指导价",Prompt = "销售指导价",ResourceType = typeof(resource.Product))]
        public decimal GUIDEPRICE { get; set; }

        [Display(Name = "CUSTBASIC",Description ="报关要素",Prompt = "报关要素",ResourceType = typeof(resource.Product))]
        [MaxLength(50)]
        public string CUSTBASIC { get; set; }

        [Display(Name = "COUNTRY",Description ="所属国家",Prompt = "所属国家",ResourceType = typeof(resource.Product))]
        [MaxLength(128)]
        public string COUNTRY { get; set; }

        [Display(Name = "TAXTYPE",Description ="所属国家",Prompt = "所属国家",ResourceType = typeof(resource.Product))]
        [MaxLength(50)]
        public string TAXTYPE { get; set; }

        [Display(Name = "TAXCLASS",Description ="税分类",Prompt = "税分类",ResourceType = typeof(resource.Product))]
        [MaxLength(50)]
        public string TAXCLASS { get; set; }

        [Display(Name = "Package",Description ="包装方式",Prompt = "包装方式",ResourceType = typeof(resource.Product))]
        [MaxLength(10)]
        public string Package { get; set; }

        [Display(Name = "InnerBoxQty",Description ="内装数量",Prompt = "内装数量",ResourceType = typeof(resource.Product))]
        public decimal InnerBoxQty { get; set; }

        [Display(Name = "Unit",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.Product))]
        [MaxLength(10)]
        public string Unit { get; set; }

        [Display(Name = "GWeight",Description ="毛重",Prompt = "毛重",ResourceType = typeof(resource.Product))]
        public decimal GWeight { get; set; }

        [Display(Name = "GWUnit",Description ="毛重单位",Prompt = "毛重单位",ResourceType = typeof(resource.Product))]
        [MaxLength(10)]
        public string GWUnit { get; set; }

        [Display(Name = "NWeight",Description ="净重",Prompt = "净重",ResourceType = typeof(resource.Product))]
        public decimal NWeight { get; set; }

        [Display(Name = "NWUnit",Description ="净重单位",Prompt = "净重单位",ResourceType = typeof(resource.Product))]
        [MaxLength(10)]
        public string NWUnit { get; set; }

        [Display(Name = "Volume",Description ="体积",Prompt = "体积",ResourceType = typeof(resource.Product))]
        public decimal Volume { get; set; }

        [Display(Name = "VUnit",Description ="体积单位",Prompt = "体积单位",ResourceType = typeof(resource.Product))]
        [MaxLength(10)]
        public string VUnit { get; set; }

        [Display(Name = "Length",Description ="长",Prompt = "长",ResourceType = typeof(resource.Product))]
        public decimal Length { get; set; }

        [Display(Name = "Width",Description ="宽",Prompt = "宽",ResourceType = typeof(resource.Product))]
        public decimal Width { get; set; }

        [Display(Name = "High",Description ="高",Prompt = "高",ResourceType = typeof(resource.Product))]
        public decimal High { get; set; }

        [Display(Name = "LUnit",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.Product))]
        [MaxLength(10)]
        public string LUnit { get; set; }

        [Required(ErrorMessage = "Please enter : 标志位")]
        [Display(Name = "Flag1",Description ="标志位",Prompt = "标志位",ResourceType = typeof(resource.Product))]
        public bool Flag1 { get; set; }

        [Required(ErrorMessage = "Please enter : 标志位")]
        [Display(Name = "Flag2",Description ="标志位",Prompt = "标志位",ResourceType = typeof(resource.Product))]
        public bool Flag2 { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Product))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Product))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Product))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Product))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.Product))]
        public int TenantId { get; set; }

    }

}
