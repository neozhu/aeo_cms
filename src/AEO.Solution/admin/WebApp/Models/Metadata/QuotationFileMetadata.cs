using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="QuotationFileMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/26 17:35:25 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(QuotationFileMetadata))]
    public partial class QuotationFile
    {
    }

    public partial class QuotationFileMetadata
    {
        [Display(Name = "Quotation",Description ="报价单",Prompt = "报价单",ResourceType = typeof(resource.QuotationFile))]
        public Quotation Quotation { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.QuotationFile))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 文件名")]
        [Display(Name = "FileName",Description ="文件名",Prompt = "文件名",ResourceType = typeof(resource.QuotationFile))]
        [MaxLength(100)]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Please enter : 大小")]
        [Display(Name = "Size",Description ="大小",Prompt = "大小",ResourceType = typeof(resource.QuotationFile))]
        public decimal Size { get; set; }

        [Display(Name = "Folder",Description ="目录",Prompt = "目录",ResourceType = typeof(resource.QuotationFile))]
        [MaxLength(20)]
        public string Folder { get; set; }

        [Display(Name = "FileId",Description ="文件ID",Prompt = "文件ID",ResourceType = typeof(resource.QuotationFile))]
        [MaxLength(100)]
        public string FileId { get; set; }

        [Display(Name = "Ext",Description ="附件类型",Prompt = "附件类型",ResourceType = typeof(resource.QuotationFile))]
        [MaxLength(100)]
        public string Ext { get; set; }

        [Display(Name = "FilePath",Description ="保存路径",Prompt = "保存路径",ResourceType = typeof(resource.QuotationFile))]
        [MaxLength(256)]
        public string FilePath { get; set; }

        [Display(Name = "RelativePath",Description ="相对路径",Prompt = "相对路径",ResourceType = typeof(resource.QuotationFile))]
        [MaxLength(256)]
        public string RelativePath { get; set; }

        [Display(Name = "RefKey",Description ="关联单号",Prompt = "关联单号",ResourceType = typeof(resource.QuotationFile))]
        [MaxLength(100)]
        public string RefKey { get; set; }

        [Display(Name = "Owner",Description ="上传用户",Prompt = "上传用户",ResourceType = typeof(resource.QuotationFile))]
        [MaxLength(20)]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Please enter : 上传时间")]
        [Display(Name = "Upload",Description ="上传时间",Prompt = "上传时间",ResourceType = typeof(resource.QuotationFile))]
        public DateTime Upload { get; set; }

        [Display(Name = "QpNo",Description ="报价单号",Prompt = "报价单号",ResourceType = typeof(resource.QuotationFile))]
        [MaxLength(20)]
        public string QpNo { get; set; }

        [Required(ErrorMessage = "Please enter : 报价单")]
        [Display(Name = "QuotationId",Description ="报价单",Prompt = "报价单",ResourceType = typeof(resource.QuotationFile))]
        public int QuotationId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.QuotationFile))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.QuotationFile))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.QuotationFile))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.QuotationFile))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.QuotationFile))]
        public int TenantId { get; set; }

    }

}
