using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="InquiryTaskProductMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/14 14:39:54 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(InquiryTaskProductMetadata))]
    public partial class InquiryTaskProduct
    {
    }

    public partial class InquiryTaskProductMetadata
    {
        [Display(Name = "InquiryTask",Description ="询价任务",Prompt = "询价任务",ResourceType = typeof(resource.InquiryTaskProduct))]
        public InquiryTask InquiryTask { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.InquiryTaskProduct))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 产品编号(自动生成,可手工修改)")]
        [Display(Name = "ProductNo",Description ="产品编号(自动生成,可手工修改)",Prompt = "产品编号(自动生成,可手工修改)",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(128)]
        public string ProductNo { get; set; }

        [Display(Name = "ProductName",Description ="中文品名",Prompt = "中文品名",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Display(Name = "CategoryName",Description ="产品类别",Prompt = "产品类别",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(128)]
        public string CategoryName { get; set; }

        [Display(Name = "ProductEnName",Description ="英文品名",Prompt = "英文品名",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(200)]
        public string ProductEnName { get; set; }

        [Display(Name = "CnDescription",Description ="中文描述",Prompt = "中文描述",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(50)]
        public string CnDescription { get; set; }

        [Display(Name = "EnDescription",Description ="英文描述",Prompt = "英文描述",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(50)]
        public string EnDescription { get; set; }

        [Display(Name = "ThirdProductNo",Description ="客户货号",Prompt = "客户货号",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(128)]
        public string ThirdProductNo { get; set; }

        [Required(ErrorMessage = "Please enter : 询价数量")]
        [Display(Name = "Qty",Description ="询价数量",Prompt = "询价数量",ResourceType = typeof(resource.InquiryTaskProduct))]
        public decimal Qty { get; set; }

        [Display(Name = "Unit",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(10)]
        public string Unit { get; set; }

        [Display(Name = "PriceType",Description ="价格类型",Prompt = "价格类型",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(30)]
        public string PriceType { get; set; }

        [Display(Name = "Price",Description ="报价金额",Prompt = "报价金额",ResourceType = typeof(resource.InquiryTaskProduct))]
        public decimal Price { get; set; }

        [Display(Name = "Executor",Description ="执行人",Prompt = "执行人",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(20)]
        public string Executor { get; set; }

        [Display(Name = "SupplierCode",Description ="供应商",Prompt = "供应商",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(20)]
        public string SupplierCode { get; set; }

        [Display(Name = "SupplierName",Description ="供应商名称",Prompt = "供应商名称",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(128)]
        public string SupplierName { get; set; }

        [Display(Name = "SamplePic",Description ="图片",Prompt = "图片",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(256)]
        public string SamplePic { get; set; }

        [Display(Name = "TaskNo",Description ="任务单号",Prompt = "任务单号",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(20)]
        public string TaskNo { get; set; }

        [Required(ErrorMessage = "Please enter : 询价任务")]
        [Display(Name = "InquiryTaskId",Description ="询价任务",Prompt = "询价任务",ResourceType = typeof(resource.InquiryTaskProduct))]
        public int InquiryTaskId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.InquiryTaskProduct))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.InquiryTaskProduct))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.InquiryTaskProduct))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.InquiryTaskProduct))]
        public int TenantId { get; set; }

    }

}
