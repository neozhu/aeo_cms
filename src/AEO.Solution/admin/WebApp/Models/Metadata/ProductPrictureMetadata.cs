using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="ProductPrictureMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/30 15:30:53 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(ProductPrictureMetadata))]
    public partial class ProductPricture
    {
    }

    public partial class ProductPrictureMetadata
    {
        [Display(Name = "Product",Description ="所属产品",Prompt = "所属产品",ResourceType = typeof(resource.ProductPricture))]
        public Product Product { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id",Description ="Id",Prompt = "Id",ResourceType = typeof(resource.ProductPricture))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 图片名称")]
        [Display(Name = "FileName",Description ="图片名称",Prompt = "图片名称",ResourceType = typeof(resource.ProductPricture))]
        [MaxLength(128)]
        public string FileName { get; set; }

        [Display(Name = "Description",Description ="图片描述",Prompt = "图片描述",ResourceType = typeof(resource.ProductPricture))]
        [MaxLength(128)]
        public string Description { get; set; }

        [Display(Name = "LineNo",Description ="顺序",Prompt = "顺序",ResourceType = typeof(resource.ProductPricture))]
        public int LineNo { get; set; }

        [Required(ErrorMessage = "Please enter : 大小")]
        [Display(Name = "Size",Description ="大小",Prompt = "大小",ResourceType = typeof(resource.ProductPricture))]
        public decimal Size { get; set; }

        [Display(Name = "Folder",Description ="目录",Prompt = "目录",ResourceType = typeof(resource.ProductPricture))]
        [MaxLength(20)]
        public string Folder { get; set; }

        [Display(Name = "FileId",Description ="文件ID",Prompt = "文件ID",ResourceType = typeof(resource.ProductPricture))]
        [MaxLength(38)]
        public string FileId { get; set; }

        [Display(Name = "FilePath",Description ="保存路径",Prompt = "保存路径",ResourceType = typeof(resource.ProductPricture))]
        [MaxLength(50)]
        public string FilePath { get; set; }

        [Display(Name = "RelativePath",Description ="相对路径",Prompt = "相对路径",ResourceType = typeof(resource.ProductPricture))]
        [MaxLength(50)]
        public string RelativePath { get; set; }

        [Required(ErrorMessage = "Please enter : 所属产品")]
        [Display(Name = "ProductId",Description ="所属产品",Prompt = "所属产品",ResourceType = typeof(resource.ProductPricture))]
        public int ProductId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.ProductPricture))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.ProductPricture))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.ProductPricture))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.ProductPricture))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : Tenant Id")]
        [Display(Name = "TenantId",Description ="Tenant Id",Prompt = "Tenant Id",ResourceType = typeof(resource.ProductPricture))]
        public int TenantId { get; set; }

    }

}
