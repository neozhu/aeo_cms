using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CategoryMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/30 12:00:01 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
    }

    public partial class CategoryMetadata
    {
        [Display(Name = "Parent",Description ="上级类别",Prompt = "上级类别",ResourceType = typeof(resource.Category))]
        public Category Parent { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.Category))]
        public int Id { get; set; }

        [Display(Name = "Name",Description ="类别名称",Prompt = "类别名称",ResourceType = typeof(resource.Category))]
        [MaxLength(128)]
        public string Name { get; set; }

        [Display(Name = "EName",Description ="英文名称",Prompt = "英文名称",ResourceType = typeof(resource.Category))]
        [MaxLength(128)]
        public string EName { get; set; }

        [Display(Name = "Icon",Description ="图标",Prompt = "图标",ResourceType = typeof(resource.Category))]
        [MaxLength(30)]
        public string Icon { get; set; }

        [Display(Name = "ParentId",Description ="上级类别",Prompt = "上级类别",ResourceType = typeof(resource.Category))]
        public int ParentId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.Category))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.Category))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.Category))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.Category))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.Category))]
        public int TenantId { get; set; }

    }

}
