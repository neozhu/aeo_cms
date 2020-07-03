using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="CustomerFileMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/3 14:09:50 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(CustomerFileMetadata))]
    public partial class CustomerFile
    {
    }

    public partial class CustomerFileMetadata
    {
        [Display(Name = "Customer",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerFile))]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.CustomerFile))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 文件名")]
        [Display(Name = "FileName",Description ="文件名",Prompt = "文件名",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(100)]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Please enter : 大小")]
        [Display(Name = "Size",Description ="大小",Prompt = "大小",ResourceType = typeof(resource.CustomerFile))]
        public decimal Size { get; set; }

        [Display(Name = "Folder",Description ="目录",Prompt = "目录",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(20)]
        public string Folder { get; set; }

        [Display(Name = "FilePath",Description ="保存路径",Prompt = "保存路径",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(50)]
        public string FilePath { get; set; }

        [Display(Name = "RelativePath",Description ="相对路径",Prompt = "相对路径",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(50)]
        public string RelativePath { get; set; }

        [Display(Name = "Owner",Description ="上传用户",Prompt = "上传用户",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(20)]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Please enter : 上传时间")]
        [Display(Name = "Upload",Description ="上传时间",Prompt = "上传时间",ResourceType = typeof(resource.CustomerFile))]
        public DateTime Upload { get; set; }

        [Display(Name = "Ext",Description ="附件类型",Prompt = "附件类型",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(100)]
        public string Ext { get; set; }

        [Display(Name = "FileId",Description ="文件ID",Prompt = "文件ID",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(100)]
        public string FileId { get; set; }

        [Display(Name = "RefKey",Description ="关联单号",Prompt = "关联单号",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(100)]
        public string RefKey { get; set; }

        [Required(ErrorMessage = "Please enter : 客户编号")]
        [Display(Name = "CustomerCode",Description ="客户编号",Prompt = "客户编号",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(20)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Please enter : 客户名称")]
        [Display(Name = "CustomerName",Description ="客户名称",Prompt = "客户名称",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(80)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter : 所属客户")]
        [Display(Name = "CustomerId",Description ="所属客户",Prompt = "所属客户",ResourceType = typeof(resource.CustomerFile))]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.CustomerFile))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.CustomerFile))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.CustomerFile))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.CustomerFile))]
        public int TenantId { get; set; }

    }

}
