using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CustomerWarehouseMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/3 14:14:51 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CustomerWarehouseMetadata))]
    public partial class CustomerWarehouse
    {
    }

    public partial class CustomerWarehouseMetadata
    {
        [Display(Name = "Customer",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerWarehouse))]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.CustomerWarehouse))]
        public int Id { get; set; }

        [Display(Name = "WarehouseCode",Description ="仓库代码",Prompt = "仓库代码",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(20)]
        public string WarehouseCode { get; set; }

        [Display(Name = "WarehouseName",Description ="仓库名称",Prompt = "仓库名称",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(128)]
        public string WarehouseName { get; set; }

        [Display(Name = "WarehouseType",Description ="仓库类型",Prompt = "仓库类型",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(128)]
        public string WarehouseType { get; set; }

        [Required(ErrorMessage = "Please enter : 厂区门禁管理")]
        [Display(Name = "FactoryGuard",Description ="厂区门禁管理",Prompt = "厂区门禁管理",ResourceType = typeof(resource.CustomerWarehouse))]
        public bool FactoryGuard { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(256)]
        public string Remark { get; set; }

        [Display(Name = "Provinces",Description ="省",Prompt = "省",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(50)]
        public string Provinces { get; set; }

        [Display(Name = "City",Description ="市",Prompt = "市",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(50)]
        public string City { get; set; }

        [Display(Name = "County",Description ="县",Prompt = "县",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(50)]
        public string County { get; set; }

        [Display(Name = "WAddress",Description ="仓库地址",Prompt = "仓库地址",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(256)]
        public string WAddress { get; set; }

        [Display(Name = "EAddress1",Description ="英文地址",Prompt = "英文地址",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(256)]
        public string EAddress1 { get; set; }

        [Display(Name = "Remark1",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(256)]
        public string Remark1 { get; set; }

        [Display(Name = "WUser",Description ="仓库负责人",Prompt = "仓库负责人",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(20)]
        public string WUser { get; set; }

        [Display(Name = "WDept",Description ="部门",Prompt = "部门",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(50)]
        public string WDept { get; set; }

        [Display(Name = "WTitle",Description ="职位",Prompt = "职位",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(50)]
        public string WTitle { get; set; }

        [Display(Name = "WSex",Description ="性别",Prompt = "性别",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(10)]
        public string WSex { get; set; }

        [Display(Name = "WPhone",Description ="固话",Prompt = "固话",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(256)]
        public string WPhone { get; set; }

        [Display(Name = "WFax",Description ="传真",Prompt = "传真",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(256)]
        public string WFax { get; set; }

        [Display(Name = "WMPhone1",Description ="手机1",Prompt = "手机1",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(256)]
        public string WMPhone1 { get; set; }

        [Display(Name = "WMPhone2",Description ="手机2",Prompt = "手机2",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(256)]
        public string WMPhone2 { get; set; }

        [Display(Name = "WEmail1",Description ="电子邮件",Prompt = "电子邮件",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(256)]
        public string WEmail1 { get; set; }

        [Required(ErrorMessage = "Please enter : 客户编号")]
        [Display(Name = "CustomerCode",Description ="客户编号",Prompt = "客户编号",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter : 所属客户")]
        [Display(Name = "CustomerId",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerWarehouse))]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CustomerWarehouse))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CustomerWarehouse))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CustomerWarehouse))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.CustomerWarehouse))]
        public int TenantId { get; set; }

    }

}
