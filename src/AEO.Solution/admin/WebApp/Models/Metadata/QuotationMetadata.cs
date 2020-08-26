using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="QuotationMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/26 17:51:59 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(QuotationMetadata))]
    public partial class Quotation
    {
    }

    public partial class QuotationMetadata
    {
        [Display(Name = "Company",Description ="公司",Prompt = "公司",ResourceType = typeof(resource.Quotation))]
        public Company Company { get; set; }

        [Display(Name = "Customer",Description ="客户",Prompt = "客户",ResourceType = typeof(resource.Quotation))]
        public Customer Customer { get; set; }

        [Display(Name = "QuotationFiles",Description ="QuotationFiles",Prompt = "QuotationFiles",ResourceType = typeof(resource.Quotation))]
        public QuotationFile QuotationFiles { get; set; }

        [Display(Name = "QuotationProducts",Description ="QuotationProducts",Prompt = "QuotationProducts",ResourceType = typeof(resource.Quotation))]
        public QuotationProduct QuotationProducts { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.Quotation))]
        public int Id { get; set; }

        [Display(Name = "QpNo",Description ="报价单号",Prompt = "报价单号",ResourceType = typeof(resource.Quotation))]
        [MaxLength(20)]
        public string QpNo { get; set; }

        [Required(ErrorMessage = "Please enter : 业务员")]
        [Display(Name = "Salesman",Description ="业务员",Prompt = "业务员",ResourceType = typeof(resource.Quotation))]
        [MaxLength(20)]
        public string Salesman { get; set; }

        [Required(ErrorMessage = "Please enter : 公司")]
        [Display(Name = "CompanyId",Description ="公司",Prompt = "公司",ResourceType = typeof(resource.Quotation))]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Please enter : 公司代码")]
        [Display(Name = "CompanyCode",Description ="公司代码",Prompt = "公司代码",ResourceType = typeof(resource.Quotation))]
        [MaxLength(20)]
        public string CompanyCode { get; set; }

        [Display(Name = "CompanyName",Description ="公司名称",Prompt = "公司名称",ResourceType = typeof(resource.Quotation))]
        [MaxLength(128)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please enter : 客户")]
        [Display(Name = "CustomerId",Description ="客户",Prompt = "客户",ResourceType = typeof(resource.Quotation))]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter : 客户编号")]
        [Display(Name = "CustomerCode",Description ="客户编号",Prompt = "客户编号",ResourceType = typeof(resource.Quotation))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.Quotation))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Display(Name = "Country",Description ="国家地区",Prompt = "国家地区",ResourceType = typeof(resource.Quotation))]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter : 联系人")]
        [Display(Name = "ContactName",Description ="联系人",Prompt = "联系人",ResourceType = typeof(resource.Quotation))]
        [MaxLength(80)]
        public string ContactName { get; set; }

        [Display(Name = "ContactInfo",Description ="联系方式",Prompt = "联系方式",ResourceType = typeof(resource.Quotation))]
        [MaxLength(128)]
        public string ContactInfo { get; set; }

        [Display(Name = "QuoteDate",Description ="报价日期",Prompt = "报价日期",ResourceType = typeof(resource.Quotation))]
        public DateTime QuoteDate { get; set; }

        [Display(Name = "ExpiryDate",Description ="有效日期",Prompt = "有效日期",ResourceType = typeof(resource.Quotation))]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "LoadingPort",Description ="装货港",Prompt = "装货港",ResourceType = typeof(resource.Quotation))]
        [MaxLength(128)]
        public string LoadingPort { get; set; }

        [Display(Name = "DischargePort",Description ="卸货港",Prompt = "卸货港",ResourceType = typeof(resource.Quotation))]
        [MaxLength(128)]
        public string DischargePort { get; set; }

        [Display(Name = "Cur",Description ="币种",Prompt = "币种",ResourceType = typeof(resource.Quotation))]
        [MaxLength(20)]
        public string Cur { get; set; }

        [Required(ErrorMessage = "Please enter : 汇率")]
        [Display(Name = "ExchangeRate",Description ="汇率",Prompt = "汇率",ResourceType = typeof(resource.Quotation))]
        public decimal ExchangeRate { get; set; }

        [Display(Name = "PriceTerm",Description ="价格条款",Prompt = "价格条款",ResourceType = typeof(resource.Quotation))]
        [MaxLength(20)]
        public string PriceTerm { get; set; }

        [Display(Name = "PayMode",Description ="付款条件",Prompt = "付款条件",ResourceType = typeof(resource.Quotation))]
        [MaxLength(128)]
        public string PayMode { get; set; }

        [Required(ErrorMessage = "Please enter : 货值金额")]
        [Display(Name = "GoodsAmount",Description ="货值金额",Prompt = "货值金额",ResourceType = typeof(resource.Quotation))]
        public decimal GoodsAmount { get; set; }

        [Required(ErrorMessage = "Please enter : 附加费")]
        [Display(Name = "ChargeAmount",Description ="附加费",Prompt = "附加费",ResourceType = typeof(resource.Quotation))]
        public decimal ChargeAmount { get; set; }

        [Required(ErrorMessage = "Please enter : 总费用")]
        [Display(Name = "TotalAmount",Description ="总费用",Prompt = "总费用",ResourceType = typeof(resource.Quotation))]
        public decimal TotalAmount { get; set; }

        [Display(Name = "FormName",Description ="报价单格式",Prompt = "报价单格式",ResourceType = typeof(resource.Quotation))]
        [MaxLength(20)]
        public string FormName { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.Quotation))]
        [MaxLength(256)]
        public string Remark { get; set; }

        [Display(Name = "InquiryNo",Description ="询价单号",Prompt = "询价单号",ResourceType = typeof(resource.Quotation))]
        [MaxLength(20)]
        public string InquiryNo { get; set; }

        [Display(Name = "TaskNo",Description ="任务单号",Prompt = "任务单号",ResourceType = typeof(resource.Quotation))]
        [MaxLength(20)]
        public string TaskNo { get; set; }

        [Required(ErrorMessage = "Please enter : 系统版本号")]
        [Display(Name = "Ver",Description ="系统版本号",Prompt = "系统版本号",ResourceType = typeof(resource.Quotation))]
        public int Ver { get; set; }

        [Display(Name = "Initiator",Description ="发起人",Prompt = "发起人",ResourceType = typeof(resource.Quotation))]
        [MaxLength(32)]
        public string Initiator { get; set; }

        [Display(Name = "SubmitDate",Description ="提交时间",Prompt = "提交时间",ResourceType = typeof(resource.Quotation))]
        public DateTime SubmitDate { get; set; }

        [Display(Name = "ToAuditor",Description ="待审人",Prompt = "待审人",ResourceType = typeof(resource.Quotation))]
        [MaxLength(32)]
        public string ToAuditor { get; set; }

        [Display(Name = "Approver",Description ="审批人",Prompt = "审批人",ResourceType = typeof(resource.Quotation))]
        [MaxLength(32)]
        public string Approver { get; set; }

        [Display(Name = "ApprovedDate",Description ="审批时间",Prompt = "审批时间",ResourceType = typeof(resource.Quotation))]
        public DateTime ApprovedDate { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Quotation))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Quotation))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Quotation))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Quotation))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.Quotation))]
        public int TenantId { get; set; }

    }

}
