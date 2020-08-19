using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="InquiryMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/19 11:03:55 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(InquiryMetadata))]
    public partial class Inquiry
    {
    }

    public partial class InquiryMetadata
    {
        [Display(Name = "Company",Description ="公司",Prompt = "公司",ResourceType = typeof(resource.Inquiry))]
        public Company Company { get; set; }

        [Display(Name = "Customer",Description ="客户",Prompt = "客户",ResourceType = typeof(resource.Inquiry))]
        public Customer Customer { get; set; }

        [Display(Name = "Inquiryfiles",Description ="Inquiryfiles",Prompt = "Inquiryfiles",ResourceType = typeof(resource.Inquiry))]
        public InquiryFile Inquiryfiles { get; set; }

        [Display(Name = "Inquiryproducts",Description ="Inquiryproducts",Prompt = "Inquiryproducts",ResourceType = typeof(resource.Inquiry))]
        public InquiryProduct Inquiryproducts { get; set; }

        [Display(Name = "Inquiryrefs",Description ="Inquiryrefs",Prompt = "Inquiryrefs",ResourceType = typeof(resource.Inquiry))]
        public InquiryRef Inquiryrefs { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.Inquiry))]
        public int Id { get; set; }

        [Display(Name = "InquiryNo",Description ="询价单号",Prompt = "询价单号",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(20)]
        public string InquiryNo { get; set; }

        [Display(Name = "TaskNo",Description ="任务单号",Prompt = "任务单号",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(256)]
        public string TaskNo { get; set; }

        [Required(ErrorMessage = "Please enter : 状态")]
        [Display(Name = "Status",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(20)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter : 业务员")]
        [Display(Name = "Salesman",Description ="业务员",Prompt = "业务员",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(20)]
        public string Salesman { get; set; }

        [Required(ErrorMessage = "Please enter : 询价日期")]
        [Display(Name = "BeginDate",Description ="询价日期",Prompt = "询价日期",ResourceType = typeof(resource.Inquiry))]
        public DateTime BeginDate { get; set; }

        [Display(Name = "FeedbackDate",Description ="反馈日期",Prompt = "反馈日期",ResourceType = typeof(resource.Inquiry))]
        public DateTime FeedbackDate { get; set; }

        [Display(Name = "Demande",Description ="询价要求",Prompt = "询价要求",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(256)]
        public string Demande { get; set; }

        [Required(ErrorMessage = "Please enter : 客户")]
        [Display(Name = "CustomerId",Description ="客户",Prompt = "客户",ResourceType = typeof(resource.Inquiry))]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter : 客户编号")]
        [Display(Name = "CustomerCode",Description ="客户编号",Prompt = "客户编号",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Display(Name = "Country",Description ="国家地区",Prompt = "国家地区",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(50)]
        public string Country { get; set; }

        [Display(Name = "Cur",Description ="币种",Prompt = "币种",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(20)]
        public string Cur { get; set; }

        [Display(Name = "ExchangeRate",Description ="汇率",Prompt = "汇率",ResourceType = typeof(resource.Inquiry))]
        public decimal ExchangeRate { get; set; }

        [Required(ErrorMessage = "Please enter : 联系人")]
        [Display(Name = "ContactName",Description ="联系人",Prompt = "联系人",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(80)]
        public string ContactName { get; set; }

        [Display(Name = "ContactInfo",Description ="联系方式",Prompt = "联系方式",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(128)]
        public string ContactInfo { get; set; }

        [Required(ErrorMessage = "Please enter : 结束日期")]
        [Display(Name = "EndDate",Description ="结束日期",Prompt = "结束日期",ResourceType = typeof(resource.Inquiry))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Urgency",Description ="紧急程度",Prompt = "紧急程度",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(20)]
        public string Urgency { get; set; }

        [Required(ErrorMessage = "Please enter : 到期提醒")]
        [Display(Name = "PreRemind",Description ="到期提醒",Prompt = "到期提醒",ResourceType = typeof(resource.Inquiry))]
        public int PreRemind { get; set; }

        [Required(ErrorMessage = "Please enter : 创建人")]
        [Display(Name = "Check1",Description ="创建人",Prompt = "创建人",ResourceType = typeof(resource.Inquiry))]
        public bool Check1 { get; set; }

        [Display(Name = "Creator",Description ="创建人",Prompt = "创建人",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(20)]
        public string Creator { get; set; }

        [Display(Name = "Executor",Description ="执行人",Prompt = "执行人",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(20)]
        public string Executor { get; set; }

        [Required(ErrorMessage = "Please enter : 执行人")]
        [Display(Name = "Check2",Description ="执行人",Prompt = "执行人",ResourceType = typeof(resource.Inquiry))]
        public bool Check2 { get; set; }

        [Required(ErrorMessage = "Please enter : 责任人")]
        [Display(Name = "Check3",Description ="责任人",Prompt = "责任人",ResourceType = typeof(resource.Inquiry))]
        public bool Check3 { get; set; }

        [Display(Name = "Owner",Description ="责任人",Prompt = "责任人",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(20)]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Please enter : 公司")]
        [Display(Name = "CompanyId",Description ="公司",Prompt = "公司",ResourceType = typeof(resource.Inquiry))]
        public int CompanyId { get; set; }

        [Display(Name = "CompanyName",Description ="公司名称",Prompt = "公司名称",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(128)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please enter : 系统版本号")]
        [Display(Name = "Ver",Description ="系统版本号",Prompt = "系统版本号",ResourceType = typeof(resource.Inquiry))]
        public int Ver { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Inquiry))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Inquiry))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Inquiry))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.Inquiry))]
        public int TenantId { get; set; }

    }

}
