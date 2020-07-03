using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CustomerSalesMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/3 13:51:05 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CustomerSalesMetadata))]
    public partial class CustomerSales
    {
    }

    public partial class CustomerSalesMetadata
    {
        [Display(Name = "Customer",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerSales))]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.CustomerSales))]
        public int Id { get; set; }

        [Display(Name = "Status",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.CustomerSales))]
        [MaxLength(20)]
        public string Status { get; set; }

        [Display(Name = "Salesman",Description ="业务员",Prompt = "业务员",ResourceType = typeof(resource.CustomerSales))]
        [MaxLength(20)]
        public string Salesman { get; set; }

        [Display(Name = "Dept",Description ="所属部门",Prompt = "所属部门",ResourceType = typeof(resource.CustomerSales))]
        [MaxLength(20)]
        public string Dept { get; set; }

        [Display(Name = "Assigner",Description ="分配人",Prompt = "分配人",ResourceType = typeof(resource.CustomerSales))]
        [MaxLength(20)]
        public string Assigner { get; set; }

        [Display(Name = "AssignDate",Description ="分配时间",Prompt = "分配时间",ResourceType = typeof(resource.CustomerSales))]
        public DateTime AssignDate { get; set; }

        [Display(Name = "StopCase",Description ="终止理由",Prompt = "终止理由",ResourceType = typeof(resource.CustomerSales))]
        [MaxLength(256)]
        public string StopCase { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.CustomerSales))]
        [MaxLength(256)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 客户编号")]
        [Display(Name = "CustomerCode",Description ="客户编号",Prompt = "客户编号",ResourceType = typeof(resource.CustomerSales))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.CustomerSales))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter : 所属客户")]
        [Display(Name = "CustomerId",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerSales))]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CustomerSales))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CustomerSales))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CustomerSales))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CustomerSales))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.CustomerSales))]
        public int TenantId { get; set; }

    }

}
