using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CustomerContactMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/3 13:55:11 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CustomerContactMetadata))]
    public partial class CustomerContact
    {
    }

    public partial class CustomerContactMetadata
    {
        [Display(Name = "Customer",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerContact))]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.CustomerContact))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 姓名")]
        [Display(Name = "Name",Description ="姓名",Prompt = "姓名",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(80)]
        public string Name { get; set; }

        [Display(Name = "Status",Description ="状态",Prompt = "状态",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(20)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter : 英文名")]
        [Display(Name = "EName",Description ="英文名",Prompt = "英文名",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(80)]
        public string EName { get; set; }

        [Display(Name = "Sex",Description ="性别",Prompt = "性别",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(10)]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Please enter : 部门")]
        [Display(Name = "Dept",Description ="部门",Prompt = "部门",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(80)]
        public string Dept { get; set; }

        [Required(ErrorMessage = "Please enter : 职务")]
        [Display(Name = "Duty",Description ="职务",Prompt = "职务",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(80)]
        public string Duty { get; set; }

        [Display(Name = "MobilePhone",Description ="手机号",Prompt = "手机号",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(50)]
        public string MobilePhone { get; set; }

        [Display(Name = "PhoneNumber1",Description ="电话1",Prompt = "电话1",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(50)]
        public string PhoneNumber1 { get; set; }

        [Display(Name = "PhoneNumber2",Description ="电话2",Prompt = "电话2",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(50)]
        public string PhoneNumber2 { get; set; }

        [Display(Name = "PhoneNumber3",Description ="固话",Prompt = "固话",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(50)]
        public string PhoneNumber3 { get; set; }

        [Display(Name = "Fax",Description ="传真",Prompt = "传真",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(50)]
        public string Fax { get; set; }

        [Required(ErrorMessage = "Please enter : 邮箱")]
        [Display(Name = "Email",Description ="邮箱",Prompt = "邮箱",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(80)]
        public string Email { get; set; }

        [Display(Name = "Remark",Description ="备注",Prompt = "备注",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(20)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 客户编号")]
        [Display(Name = "CustomerCode",Description ="客户编号",Prompt = "客户编号",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter : 所属客户")]
        [Display(Name = "CustomerId",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerContact))]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CustomerContact))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CustomerContact))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CustomerContact))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.CustomerContact))]
        public int TenantId { get; set; }

    }

}
