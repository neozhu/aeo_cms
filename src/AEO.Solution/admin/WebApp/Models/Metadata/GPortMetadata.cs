using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="GPortMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/27 10:40:04 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(GPortMetadata))]
    public partial class GPort
    {
    }

    public partial class GPortMetadata
    {
        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.GPort))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 代码")]
        [Display(Name = "code",Description ="代码",Prompt = "代码",ResourceType = typeof(resource.GPort))]
        [MaxLength(8)]
        public string code { get; set; }

        [Display(Name = "cn_name",Description ="中文名称",Prompt = "中文名称",ResourceType = typeof(resource.GPort))]
        [MaxLength(128)]
        public string cn_name { get; set; }

        [Display(Name = "en_name",Description ="英文名称",Prompt = "英文名称",ResourceType = typeof(resource.GPort))]
        [MaxLength(128)]
        public string en_name { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.GPort))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.GPort))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.GPort))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.GPort))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.GPort))]
        public int TenantId { get; set; }

    }

}
