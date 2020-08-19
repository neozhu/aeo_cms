using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="InquiryFileMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/19 10:59:11 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(InquiryFileMetadata))]
    public partial class InquiryFile
    {
    }

    public partial class InquiryFileMetadata
    {
        [Display(Name = "Inquiry",Description ="询价单",Prompt = "询价单",ResourceType = typeof(resource.InquiryFile))]
        public Inquiry Inquiry { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.InquiryFile))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 文件名")]
        [Display(Name = "FileName",Description ="文件名",Prompt = "文件名",ResourceType = typeof(resource.InquiryFile))]
        [MaxLength(100)]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Please enter : 大小")]
        [Display(Name = "Size",Description ="大小",Prompt = "大小",ResourceType = typeof(resource.InquiryFile))]
        public decimal Size { get; set; }

        [Display(Name = "Folder",Description ="目录",Prompt = "目录",ResourceType = typeof(resource.InquiryFile))]
        [MaxLength(20)]
        public string Folder { get; set; }

        [Display(Name = "FilePath",Description ="保存路径",Prompt = "保存路径",ResourceType = typeof(resource.InquiryFile))]
        [MaxLength(50)]
        public string FilePath { get; set; }

        [Display(Name = "RelativePath",Description ="相对路径",Prompt = "相对路径",ResourceType = typeof(resource.InquiryFile))]
        [MaxLength(50)]
        public string RelativePath { get; set; }

        [Display(Name = "Owner",Description ="上传用户",Prompt = "上传用户",ResourceType = typeof(resource.InquiryFile))]
        [MaxLength(20)]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Please enter : 上传时间")]
        [Display(Name = "Upload",Description ="上传时间",Prompt = "上传时间",ResourceType = typeof(resource.InquiryFile))]
        public DateTime Upload { get; set; }

        [Display(Name = "Ext",Description ="附件类型",Prompt = "附件类型",ResourceType = typeof(resource.InquiryFile))]
        [MaxLength(100)]
        public string Ext { get; set; }

        [Display(Name = "FileId",Description ="文件ID",Prompt = "文件ID",ResourceType = typeof(resource.InquiryFile))]
        [MaxLength(100)]
        public string FileId { get; set; }

        [Required(ErrorMessage = "Please enter : 系统版本号")]
        [Display(Name = "Ver",Description ="系统版本号",Prompt = "系统版本号",ResourceType = typeof(resource.InquiryFile))]
        public int Ver { get; set; }

        [Display(Name = "InquiryId",Description ="询价单",Prompt = "询价单",ResourceType = typeof(resource.InquiryFile))]
        public int InquiryId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.InquiryFile))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.InquiryFile))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.InquiryFile))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.InquiryFile))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.InquiryFile))]
        public int TenantId { get; set; }

    }

}
