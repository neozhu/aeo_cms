using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="QuotationProductMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/26 17:40:53 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(QuotationProductMetadata))]
    public partial class QuotationProduct
    {
    }

    public partial class QuotationProductMetadata
    {
        [Display(Name = "Quotation",Description ="报价单",Prompt = "报价单",ResourceType = typeof(resource.QuotationProduct))]
        public Quotation Quotation { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.QuotationProduct))]
        public int Id { get; set; }

        [Display(Name = "ProductNo",Description ="产品编号",Prompt = "产品编号",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(128)]
        public string ProductNo { get; set; }

        [Display(Name = "ProductName",Description ="中文品名",Prompt = "中文品名",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Display(Name = "CategoryName",Description ="产品类别",Prompt = "产品类别",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [Display(Name = "ProductEnName",Description ="英文品名",Prompt = "英文品名",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(200)]
        public string ProductEnName { get; set; }

        [Display(Name = "CnDescription",Description ="中文描述",Prompt = "中文描述",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(256)]
        public string CnDescription { get; set; }

        [Display(Name = "EnDescription",Description ="英文描述",Prompt = "英文描述",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(256)]
        public string EnDescription { get; set; }

        [Display(Name = "HSCODE",Description ="海关编码",Prompt = "海关编码",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(10)]
        public string HSCODE { get; set; }

        [Display(Name = "HSADDTAXRATE",Description ="增值税率",Prompt = "增值税率",ResourceType = typeof(resource.QuotationProduct))]
        public decimal HSADDTAXRATE { get; set; }

        [Display(Name = "HSBACKTAXRATE",Description ="退税率",Prompt = "退税率",ResourceType = typeof(resource.QuotationProduct))]
        public decimal HSBACKTAXRATE { get; set; }

        [Display(Name = "CUSTBASIC",Description ="报关要素",Prompt = "报关要素",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(50)]
        public string CUSTBASIC { get; set; }

        [Display(Name = "GUIDEPRICE",Description ="销售指导价",Prompt = "销售指导价",ResourceType = typeof(resource.QuotationProduct))]
        public decimal GUIDEPRICE { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(256)]
        public string Remark { get; set; }

        [Display(Name = "ThirdProductNo",Description ="客户货号",Prompt = "客户货号",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(128)]
        public string ThirdProductNo { get; set; }

        [Required(ErrorMessage = "Please enter : 数量")]
        [Display(Name = "Qty",Description ="数量",Prompt = "数量",ResourceType = typeof(resource.QuotationProduct))]
        public decimal Qty { get; set; }

        [Display(Name = "Unit",Description ="单位",Prompt = "单位",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(10)]
        public string Unit { get; set; }

        [Display(Name = "Price",Description ="报价",Prompt = "报价",ResourceType = typeof(resource.QuotationProduct))]
        public decimal Price { get; set; }

        [Display(Name = "Cur",Description ="币种",Prompt = "币种",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(20)]
        public string Cur { get; set; }

        [Required(ErrorMessage = "Please enter : 金额")]
        [Display(Name = "Amount",Description ="金额",Prompt = "金额",ResourceType = typeof(resource.QuotationProduct))]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please enter : 美元金额")]
        [Display(Name = "USDAmount",Description ="美元金额",Prompt = "美元金额",ResourceType = typeof(resource.QuotationProduct))]
        public decimal USDAmount { get; set; }

        [Required(ErrorMessage = "Please enter : 人民币金额")]
        [Display(Name = "RMBAmount",Description ="人民币金额",Prompt = "人民币金额",ResourceType = typeof(resource.QuotationProduct))]
        public decimal RMBAmount { get; set; }

        [Display(Name = "BrightcmsRate",Description ="扣佣率/值",Prompt = "扣佣率/值",ResourceType = typeof(resource.QuotationProduct))]
        public decimal BrightcmsRate { get; set; }

        [Display(Name = "BrightcmsFcy",Description ="扣佣金额",Prompt = "扣佣金额",ResourceType = typeof(resource.QuotationProduct))]
        public decimal BrightcmsFcy { get; set; }

        [Display(Name = "DarkcmsRate",Description ="付佣率/值",Prompt = "付佣率/值",ResourceType = typeof(resource.QuotationProduct))]
        public decimal DarkcmsRate { get; set; }

        [Display(Name = "DarkcmsFcy",Description ="付佣金额",Prompt = "付佣金额",ResourceType = typeof(resource.QuotationProduct))]
        public decimal DarkcmsFcy { get; set; }

        [Display(Name = "Executor",Description ="执行人",Prompt = "执行人",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(20)]
        public string Executor { get; set; }

        [Display(Name = "Logo",Description ="产品图片",Prompt = "产品图片",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(256)]
        public string Logo { get; set; }

        [Required(ErrorMessage = "Please enter : 报价单号")]
        [Display(Name = "QpNo",Description ="报价单号",Prompt = "报价单号",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(20)]
        public string QpNo { get; set; }

        [Required(ErrorMessage = "Please enter : 报价单")]
        [Display(Name = "QuotationId",Description ="报价单",Prompt = "报价单",ResourceType = typeof(resource.QuotationProduct))]
        public int QuotationId { get; set; }

        [Required(ErrorMessage = "Please enter : 系统版本号")]
        [Display(Name = "Ver",Description ="系统版本号",Prompt = "系统版本号",ResourceType = typeof(resource.QuotationProduct))]
        public int Ver { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.QuotationProduct))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.QuotationProduct))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.QuotationProduct))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.QuotationProduct))]
        public int TenantId { get; set; }

    }

}
