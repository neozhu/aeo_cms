using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CustomerShareMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/3 14:05:38 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CustomerShareMetadata))]
    public partial class CustomerShare
    {
    }

    public partial class CustomerShareMetadata
    {
        [Display(Name = "Customer",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerShare))]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.CustomerShare))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 共享人")]
        [Display(Name = "Owner",Description ="共享人",Prompt = "共享人",ResourceType = typeof(resource.CustomerShare))]
        [MaxLength(20)]
        public string Owner { get; set; }

        [Display(Name = "Dept",Description ="部门",Prompt = "部门",ResourceType = typeof(resource.CustomerShare))]
        [MaxLength(20)]
        public string Dept { get; set; }

        [Required(ErrorMessage = "Please enter : 人员")]
        [Display(Name = "ShareTo",Description ="人员",Prompt = "人员",ResourceType = typeof(resource.CustomerShare))]
        [MaxLength(20)]
        public string ShareTo { get; set; }

        [Display(Name = "Module",Description ="模块",Prompt = "模块",ResourceType = typeof(resource.CustomerShare))]
        [MaxLength(50)]
        public string Module { get; set; }

        [Required(ErrorMessage = "Please enter : 查询")]
        [Display(Name = "Searchable",Description ="查询",Prompt = "查询",ResourceType = typeof(resource.CustomerShare))]
        public bool Searchable { get; set; }

        [Required(ErrorMessage = "Please enter : 编辑")]
        [Display(Name = "Editable",Description ="编辑",Prompt = "编辑",ResourceType = typeof(resource.CustomerShare))]
        public bool Editable { get; set; }

        [Required(ErrorMessage = "Please enter : 客户编号")]
        [Display(Name = "CustomerCode",Description ="客户编号",Prompt = "客户编号",ResourceType = typeof(resource.CustomerShare))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.CustomerShare))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter : 所属客户")]
        [Display(Name = "CustomerId",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerShare))]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CustomerShare))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CustomerShare))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CustomerShare))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CustomerShare))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.CustomerShare))]
        public int TenantId { get; set; }

    }

}
