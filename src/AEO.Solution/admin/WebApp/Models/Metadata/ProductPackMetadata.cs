using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="ProductPackMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/7/30 16:19:22 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(ProductPackMetadata))]
    public partial class ProductPack
    {
    }

    public partial class ProductPackMetadata
    {
        [Display(Name = "Product",Description ="所属产品",Prompt = "所属产品",ResourceType = typeof(resource.ProductPack))]
        public Product Product { get; set; }

        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.ProductPack))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 包装单位")]
        [Display(Name = "Package",Description ="包装单位",Prompt = "包装单位",ResourceType = typeof(resource.ProductPack))]
        [MaxLength(10)]
        public string Package { get; set; }

        [Display(Name = "InnerBoxQty",Description ="内装数量",Prompt = "内装数量",ResourceType = typeof(resource.ProductPack))]
        public decimal InnerBoxQty { get; set; }

        [Display(Name = "Length",Description ="长",Prompt = "长",ResourceType = typeof(resource.ProductPack))]
        public decimal Length { get; set; }

        [Display(Name = "Width",Description ="宽",Prompt = "宽",ResourceType = typeof(resource.ProductPack))]
        public decimal Width { get; set; }

        [Display(Name = "Height",Description ="高",Prompt = "高",ResourceType = typeof(resource.ProductPack))]
        public decimal Height { get; set; }

        [Display(Name = "Unit",Description ="长度单位",Prompt = "长度单位",ResourceType = typeof(resource.ProductPack))]
        [MaxLength(10)]
        public string Unit { get; set; }

        [Display(Name = "GWeight",Description ="毛重(kg)",Prompt = "毛重(kg)",ResourceType = typeof(resource.ProductPack))]
        public decimal GWeight { get; set; }

        [Display(Name = "NWeight",Description ="净重(kg)",Prompt = "净重(kg)",ResourceType = typeof(resource.ProductPack))]
        public decimal NWeight { get; set; }

        [Display(Name = "Volume",Description ="体积(m3)",Prompt = "体积(m3)",ResourceType = typeof(resource.ProductPack))]
        public decimal Volume { get; set; }

        [Display(Name = "TwentyQtc",Description ="20尺装量",Prompt = "20尺装量",ResourceType = typeof(resource.ProductPack))]
        public decimal TwentyQtc { get; set; }

        [Display(Name = "FortyQtc",Description ="40尺装量",Prompt = "40尺装量",ResourceType = typeof(resource.ProductPack))]
        public decimal FortyQtc { get; set; }

        [Display(Name = "FortyHQQtc",Description ="40HQ装量",Prompt = "40HQ装量",ResourceType = typeof(resource.ProductPack))]
        public decimal FortyHQQtc { get; set; }

        [Required(ErrorMessage = "Please enter : 默认包装")]
        [Display(Name = "Default",Description ="默认包装",Prompt = "默认包装",ResourceType = typeof(resource.ProductPack))]
        public bool Default { get; set; }

        [Required(ErrorMessage = "Please enter : 所属产品")]
        [Display(Name = "ProductId",Description ="所属产品",Prompt = "所属产品",ResourceType = typeof(resource.ProductPack))]
        public int ProductId { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.ProductPack))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.ProductPack))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.ProductPack))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.ProductPack))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.ProductPack))]
        public int TenantId { get; set; }

    }

}
