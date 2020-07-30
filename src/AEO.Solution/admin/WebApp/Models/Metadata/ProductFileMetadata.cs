using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="ProductFileMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/30 15:29:47 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(ProductFileMetadata))]
    public partial class ProductFile
    {
    }

    public partial class ProductFileMetadata
    {
        [Display(Name = "Product",Description ="所属产品",Prompt = "所属产品",ResourceType = typeof(resource.ProductFile))]
        public Product Product { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.ProductFile))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 文件名")]
        [Display(Name = "FileName",Description ="文件名",Prompt = "文件名",ResourceType = typeof(resource.ProductFile))]
        [MaxLength(128)]
        public string FileName { get; set; }

        [Display(Name = "Size",Description ="大小",Prompt = "大小",ResourceType = typeof(resource.ProductFile))]
        public decimal Size { get; set; }

        [Display(Name = "Folder",Description ="目录",Prompt = "目录",ResourceType = typeof(resource.ProductFile))]
        [MaxLength(128)]
        public string Folder { get; set; }

        [Display(Name = "FileId",Description ="文件ID",Prompt = "文件ID",ResourceType = typeof(resource.ProductFile))]
        [MaxLength(38)]
        public string FileId { get; set; }

        [Display(Name = "Ext",Description ="附件类型",Prompt = "附件类型",ResourceType = typeof(resource.ProductFile))]
        [MaxLength(10)]
        public string Ext { get; set; }

        [Display(Name = "FilePath",Description ="保存路径",Prompt = "保存路径",ResourceType = typeof(resource.ProductFile))]
        [MaxLength(50)]
        public string FilePath { get; set; }

        [Display(Name = "RelativePath",Description ="相对路径",Prompt = "相对路径",ResourceType = typeof(resource.ProductFile))]
        [MaxLength(50)]
        public string RelativePath { get; set; }

        [Display(Name = "Owner",Description ="上传用户",Prompt = "上传用户",ResourceType = typeof(resource.ProductFile))]
        [MaxLength(20)]
        public string Owner { get; set; }

        [Required(ErrorMessage = "Please enter : 上传时间")]
        [Display(Name = "Upload",Description ="上传时间",Prompt = "上传时间",ResourceType = typeof(resource.ProductFile))]
        public DateTime Upload { get; set; }

        [Required(ErrorMessage = "Please enter : 所属产品")]
        [Display(Name = "ProductId",Description ="所属产品",Prompt = "所属产品",ResourceType = typeof(resource.ProductFile))]
        public int ProductId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.ProductFile))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.ProductFile))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.ProductFile))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.ProductFile))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.ProductFile))]
        public int TenantId { get; set; }

    }

}
